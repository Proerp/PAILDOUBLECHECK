﻿using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;

using Ninject;
using AutoMapper;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalCore.Services.Productions;
using TotalDTO.Productions;
using TotalService.Productions;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Communications;
using TotalSmartCoding.ViewModels.Productions;
using TotalSmartCoding.Controllers.Productions;
using TotalSmartCoding.Controllers.APIs.Productions;


namespace TotalSmartCoding.Controllers.Productions
{
    public class ScannerController : CodingController //this is CommonThreadProperty
    {
        #region Storage

        private FillingData privateFillingData;
        private IBatchService batchService;

        private PackController packController;
        private CartonController cartonController;
        private PalletController palletController;

        private PackViewModel packViewModel;
        private CartonViewModel cartonViewModel;
        private PalletViewModel palletViewModel;

        private IONetSocket ionetSocketPack;
        private IONetSocket ionetSocketCarton;
        private IONetSocket ionetSocketLabel;
        private IONetSocket ionetSocketPallet;


        private BarcodeQueue<PackDTO> packQueue;
        private BarcodeQueue<PackDTO> packsetQueue;

        private BarcodeQueue<CartonDTO> cartonPendingQueue;
        private BarcodeQueue<CartonDTO> cartonQueue;
        private BarcodeQueue<CartonDTO> localcartonsetQueue;
        private BarcodeQueue<CartonDTO> cartonsetQueue
        {
            get { return this.localcartonsetQueue; }
            set
            {
                this.localcartonsetQueue = value;
                this.FillingData.CartonsetQueueCount = this.localcartonsetQueue.Count;

                if (this.FillingData.CartonsetQueueCount > 0 || (!GlobalEnums.DrumWithDigit && !this.FillingData.HasCarton))
                    this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Freshnew;
            }
        }

        private BarcodeQueue<PalletDTO> palletQueue;
        private BarcodeQueue<PalletDTO> palletPickupQueue;


        private BarcodeScannerStatus[] barcodeScannerStatus;

        #endregion Storage


        #region Contructor

        public ScannerController(IBatchService batchService, FillingData fillingData)
        {
            try
            {
                this.batchService = batchService;

                base.FillingData = fillingData;


                this.packViewModel = CommonNinject.Kernel.Get<PackViewModel>();
                this.cartonViewModel = CommonNinject.Kernel.Get<CartonViewModel>();
                this.palletViewModel = CommonNinject.Kernel.Get<PalletViewModel>();

                this.packController = new PackController(CommonNinject.Kernel.Get<IPackService>(), this.packViewModel);
                this.cartonController = new CartonController(CommonNinject.Kernel.Get<ICartonService>(), this.cartonViewModel);
                this.palletController = new PalletController(CommonNinject.Kernel.Get<IPalletService>(), this.palletViewModel);

                ProductionAPIs productionAPIs = new ProductionAPIs();

                this.ionetSocketPack = new IONetSocket(IPAddress.Parse(productionAPIs.IpAddress((int)GlobalVariables.ScannerName.Base + (int)GlobalVariables.ScannerName.PackScanner)), 23, 120); //PORT 2112: DATA LOGIC
                this.ionetSocketCarton = new IONetSocket(IPAddress.Parse(productionAPIs.IpAddress((int)GlobalVariables.ScannerName.Base + (int)GlobalVariables.ScannerName.CartonScanner)), 23, 120);
                this.ionetSocketLabel = new IONetSocket(IPAddress.Parse(productionAPIs.IpAddress((int)GlobalVariables.ScannerName.Base + (int)GlobalVariables.ScannerName.LabelScanner)), 23, 120);
                this.ionetSocketPallet = new IONetSocket(IPAddress.Parse(productionAPIs.IpAddress((int)GlobalVariables.ScannerName.Base + (int)GlobalVariables.ScannerName.PalletScanner)), 23, 120);


                this.packQueue = new BarcodeQueue<PackDTO>(this.FillingData.HasPackLabel, this.FillingData.NoSubQueue, this.FillingData.ItemPerSubQueue, this.FillingData.RepeatSubQueueIndex) { ItemPerSet = this.FillingData.PackPerCarton };
                this.packsetQueue = new BarcodeQueue<PackDTO>(this.FillingData.HasPackLabel, this.FillingData.NoSubQueue, this.FillingData.ItemPerSubQueue, false) { ItemPerSet = this.FillingData.PackPerCarton };

                this.cartonPendingQueue = new BarcodeQueue<CartonDTO>(this.FillingData.HasCartonLabel) { ItemPerSet = this.FillingData.CartonPerPallet };
                this.cartonQueue = new BarcodeQueue<CartonDTO>(this.FillingData.HasCartonLabel) { ItemPerSet = this.FillingData.CartonPerPallet };
                this.cartonsetQueue = new BarcodeQueue<CartonDTO>(this.FillingData.HasCartonLabel) { ItemPerSet = this.FillingData.CartonPerPallet };

                this.palletQueue = new BarcodeQueue<PalletDTO>();
                this.palletPickupQueue = new BarcodeQueue<PalletDTO>();


                base.FillingData.PropertyChanged += new PropertyChangedEventHandler(fillingData_PropertyChanged);

                this.barcodeScannerStatus = new BarcodeScannerStatus[3];
                this.barcodeScannerStatus[(int)GlobalVariables.ScannerName.QualityScanner - 1] = new BarcodeScannerStatus(GlobalVariables.ScannerName.QualityScanner);
                this.barcodeScannerStatus[(int)GlobalVariables.ScannerName.PackScanner - 1] = new BarcodeScannerStatus(GlobalVariables.ScannerName.PackScanner);
                this.barcodeScannerStatus[(int)GlobalVariables.ScannerName.CartonScanner - 1] = new BarcodeScannerStatus(GlobalVariables.ScannerName.CartonScanner);
            }
            catch (Exception exception)
            {
                this.MainStatus = exception.Message;
            }
        }

        public void Initialize()
        {
            try
            {
                this.packQueue.Clear();
                this.packsetQueue.Clear();
                this.cartonQueue.Clear();
                this.cartonsetQueue.Clear();
                this.cartonPendingQueue.Clear();

                IList<Pack> packs = this.packController.packService.GetPacks(this.FillingData.FillingLineID, (int)GlobalVariables.BarcodeStatus.Freshnew + "," + (int)GlobalVariables.BarcodeStatus.Readytoset, null);
                if (packs.Count > 0)
                {
                    packs.Each(pack =>
                    {
                        PackDTO packDTO = Mapper.Map<Pack, PackDTO>(pack);
                        if (packDTO.EntryStatusID == (int)GlobalVariables.BarcodeStatus.Freshnew)
                            this.packQueue.Enqueue(packDTO, false);
                        else
                            this.packsetQueue.Enqueue(packDTO, false);
                    });
                    this.NotifyPropertyChanged("PackQueue");
                    this.NotifyPropertyChanged("PacksetQueue");
                }

                IList<Carton> cartons = this.cartonController.cartonService.GetCartons(this.FillingData.FillingLineID, (int)GlobalVariables.BarcodeStatus.Freshnew + "," + (int)GlobalVariables.BarcodeStatus.Readytoset + "," + (int)GlobalVariables.BarcodeStatus.Pending + "," + (int)GlobalVariables.BarcodeStatus.Noread, null);
                if (cartons.Count > 0)
                {
                    cartons.Each(carton =>
                    {
                        CartonDTO cartonDTO = Mapper.Map<Carton, CartonDTO>(carton);
                        if (cartonDTO.EntryStatusID == (int)GlobalVariables.BarcodeStatus.Freshnew)
                            this.cartonQueue.Enqueue(cartonDTO, false);
                        else
                            if (cartonDTO.EntryStatusID == (int)GlobalVariables.BarcodeStatus.Readytoset)
                                this.cartonsetQueue.Enqueue(cartonDTO, false);
                            else //BarcodeStatus.Pending, BarcodeStatus.Noread
                            {
                                if (cartonDTO.BatchID == this.FillingData.BatchID) //11MAR2019: KHÔNG CHO XÓA PENDING ==> CHỈ LOAD PENDING CỦA BATCH HIỆN HÀNH
                                    this.cartonPendingQueue.Enqueue(cartonDTO, false);
                            }
                    });

                    this.cartonsetQueue = this.cartonsetQueue;

                    this.NotifyPropertyChanged("CartonQueue");
                    this.NotifyPropertyChanged("CartonsetQueue");
                    this.NotifyPropertyChanged("CartonPendingQueue");
                }

                this.InitializePallet(); //if (!GlobalEnums.OnTestPalletReceivedNow) this.InitializePallet();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void InitializePallet()
        {
            try
            {
                lock (this.palletQueue)
                {
                    lock (this.palletPickupQueue)
                    {
                        this.palletQueue.Clear();
                        this.palletPickupQueue.Clear();

                        IPalletService localPalletService = CommonNinject.Kernel.Get<IPalletService>(); //SHOULD TO CREATE NEW IPalletService => INSTEAF OF USING CURRENT this.cartonController.cartonService: TO AVOID CASH OF ENTITYFRAMEWORK
                        IList<Pallet> pallets = localPalletService.GetPallets(this.FillingData.FillingLineID, this.FillingData.BatchID, (int)GlobalVariables.BarcodeStatus.Freshnew + "," + (int)GlobalVariables.BarcodeStatus.Readytoset);
                        if (pallets.Count > 0)
                        {
                            pallets.Each(pallet =>
                            {
                                PalletDTO palletDTO = Mapper.Map<Pallet, PalletDTO>(pallet);
                                if (palletDTO.QuantityPickup == 0) this.palletQueue.Enqueue(palletDTO, false); else this.palletPickupQueue.Enqueue(palletDTO, false);
                            });
                        }
                    }
                }
                this.NotifyPropertyChanged("PalletQueue");
                this.NotifyPropertyChanged("PalletPickupQueue");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void fillingData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "PackPerCarton") { this.packQueue.NoSubQueue = this.FillingData.NoSubQueue; this.packQueue.ItemPerSet = this.FillingData.PackPerCarton; this.packsetQueue.NoSubQueue = this.FillingData.NoSubQueue; this.packsetQueue.ItemPerSet = this.FillingData.PackPerCarton; }
                if (e.PropertyName == "CartonPerPallet") { this.cartonPendingQueue.ItemPerSet = this.FillingData.CartonPerPallet; this.cartonQueue.ItemPerSet = this.FillingData.CartonPerPallet; this.cartonsetQueue.ItemPerSet = this.FillingData.CartonPerPallet; }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Contructor


        #region Public Properties

        public bool OnScanning { get; private set; }

        public void StartScanner()
        {
            if (!this.OnScanning) this.ClearSocket(); //CHECK !this.OnScanning JUST FOR SURE, BECAUSE WE DO NOT TEST BEFORE. I WORRY THAT this.ClearSocket WILL CONFLICT WITH ThreadRoutine ReadoutStream. I THINK THIS IS NOT NECCESSARY!!!
            this.OnScanning = true;
        }
        public void StopScanner() { this.OnScanning = false; }

        public int PackQueueCount { get { return this.packQueue.Count; } }
        public int PacksetQueueCount { get { return this.packsetQueue.Count; } }
        public int CartonPendingQueueCount { get { return this.cartonPendingQueue.Count; } }
        public int CartonQueueCount { get { return this.cartonQueue.Count; } }
        public int CartonsetQueueCount { get { return this.cartonsetQueue.Count; } }
        public int PalletQueueCount { get { return this.palletQueue.Count; } }
        public int PalletPickupQueueCount { get { return this.palletPickupQueue.Count; } }


        private int packIgnoreCount;
        public int PackIgnoreCount
        {
            get { return this.packIgnoreCount; }
            set
            {
                if (this.packIgnoreCount != value)
                {
                    this.packIgnoreCount = value;
                    this.NotifyPropertyChanged("PackIgnoreCount");
                }
            }
        }

        private int cartonIgnoreCount;
        public int CartonIgnoreCount
        {
            get { return this.cartonIgnoreCount; }
            set
            {
                if (this.cartonIgnoreCount != value)
                {
                    this.cartonIgnoreCount = value;
                    this.NotifyPropertyChanged("CartonIgnoreCount");
                }
            }
        }

        public bool AllQueueEmpty { get { return (this.packQueue == null && this.packsetQueue == null && this.cartonPendingQueue == null && this.cartonQueue == null && this.cartonsetQueue == null) || (this.PackQueueCount == 0 && this.packsetQueue.Count == 0 && this.CartonQueueCount == 0 && this.cartonsetQueue.Count == 0); } }// 11/MAR/2019: && this.CartonPendingQueueCount == 0: KHÔNG CHO XÓA CARTON NOREAD                // && this.PalletQueueCount == 0 : HIEN TAI: KHONG CO CACH NAO UNWRAP PALLET TO CARTON => SO NO NEED TO CHECK ALL PalletQueueCount



        public string BatchDigitNo
        {
            get { return this.privateFillingData.BatchDigitNo; }
            private set
            {
                if (this.privateFillingData.BatchDigitNo != value)
                {
                    this.privateFillingData.BatchDigitNo = value;
                    this.NotifyPropertyChanged("BatchDigitNo");
                }
            }
        }

        public string BatchPackNo
        {
            get { return this.privateFillingData.BatchPackNo; }
            private set
            {
                if (this.privateFillingData.BatchPackNo != value)
                {
                    this.privateFillingData.BatchPackNo = value;
                    this.NotifyPropertyChanged("BatchPackNo");
                }
            }
        }

        public string BatchCartonNo
        {
            get { return this.privateFillingData.BatchCartonNo; }
            private set
            {
                if (this.privateFillingData.BatchCartonNo != value)
                {
                    this.privateFillingData.BatchCartonNo = value;
                    this.NotifyPropertyChanged("BatchCartonNo");
                }
            }
        }

        public string BatchPalletNo
        {
            get { return this.privateFillingData.BatchPalletNo; }
            private set
            {
                if (this.privateFillingData.BatchPalletNo != value)
                {
                    this.privateFillingData.BatchPalletNo = value;
                    this.NotifyPropertyChanged("BatchPalletNo");
                }
            }
        }

        private string getBatchNo(GlobalVariables.BarcodeName barcodeName)
        {
            return this.getBatchNo(barcodeName, false);
        }

        private string getBatchNo(GlobalVariables.BarcodeName barcodeName, bool sendCartontoZebra)
        {
            if (barcodeName == GlobalVariables.BarcodeName.Digit || barcodeName == GlobalVariables.BarcodeName.Pack)
            {
                if (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Pail)
                    return this.BatchCartonNo;
                else
                    if (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Drum)
                        return barcodeName == GlobalVariables.BarcodeName.Digit ? this.BatchDigitNo : this.BatchCartonNo;
                    else
                        return this.BatchPackNo;
            }
            else
                if (barcodeName == GlobalVariables.BarcodeName.Carton || sendCartontoZebra)
                    return this.BatchCartonNo;
                else
                    if (barcodeName == GlobalVariables.BarcodeName.Pallet)
                        return this.BatchPalletNo;
                    else
                        return "XXXXXX"; //THIS return value WILL RAISE ERROR TO THE CALLER FUNCTION, BUCAUSE IT DON'T HAVE A CORRECT FORMAT

        }



        private void feedbackBatchNo(GlobalVariables.BarcodeName barcodeName, string batchNo)
        {
            this.feedbackBatchNo(barcodeName, batchNo, "");
        }

        private void feedbackBatchNo(GlobalVariables.BarcodeName barcodeName, string batchNo, string receivedFeedback)
        {
            this.feedbackBatchNo(barcodeName, batchNo, receivedFeedback, false);
        }

        private void feedbackBatchNo(GlobalVariables.BarcodeName barcodeName, string batchNo, string receivedFeedback, bool sendCartontoZebra)
        {
            if (batchNo == "" && receivedFeedback.Length > 12)
            {
                int serialNumber = 0;
                if (int.TryParse(receivedFeedback.Substring(6, 6), out serialNumber))
                    batchNo = serialNumber.ToString("0000000").Substring(1);//SHOULD OR NOT: Increase serialNumber by 1 (BECAUSE: batchNo MUST GO AHEAD BY 1??): TEST AT DATMY: FOR AX350: NO NEED, BUCAUSE: AX350 RETURN THE NEXT VALUE. BUT FOR A200+: RETURN THE PRINTED VALUE
            }

            if (batchNo != "")
            {
                if (barcodeName == GlobalVariables.BarcodeName.Digit)
                    this.BatchDigitNo = batchNo;
                else
                    if (barcodeName == GlobalVariables.BarcodeName.Pack)
                        this.BatchPackNo = batchNo;
                    else
                        if (barcodeName == GlobalVariables.BarcodeName.Carton || sendCartontoZebra)
                            this.BatchCartonNo = batchNo;
                        else
                            if (barcodeName == GlobalVariables.BarcodeName.Pallet)
                                this.BatchPalletNo = batchNo;

                lock (this.batchService) //ALL PrinterController MUST SHARE THE SAME IBatchService, BECAUSE WE NEED TO LOCK IBatchService IN ORDER TO CORRECTED UPDATE DATA BY IBatchService
                {
                    if (!this.batchService.ExtendedUpdate(this.FillingData.BatchID, barcodeName == GlobalVariables.BarcodeName.Pack || (GlobalEnums.DrumWithDigit && barcodeName == GlobalVariables.BarcodeName.Digit) ? batchNo : "", barcodeName == GlobalVariables.BarcodeName.Carton || sendCartontoZebra ? batchNo : "", barcodeName == GlobalVariables.BarcodeName.Pallet && !sendCartontoZebra ? batchNo : ""))
                        this.MainStatus = this.batchService.ServiceTag;
                }
            }
        }

        #endregion Public Properties


        #region LedMCU

        public bool LedMCUQualityOn
        {
            get { return !this.barcodeScannerStatus[(int)GlobalVariables.ScannerName.QualityScanner - 1].MCUReady; }
        }

        public bool LedMCUMatchingOn
        {
            get { return !this.barcodeScannerStatus[(int)GlobalVariables.ScannerName.PackScanner - 1].MCUReady; }
        }

        public bool LedMCUCartonOn
        {
            get { return !this.barcodeScannerStatus[(int)GlobalVariables.ScannerName.CartonScanner - 1].MCUReady; }
        }

        public void SetBarcodeScannerStatus(GlobalVariables.ScannerName barcodeScannerNameID, bool mcuReady)
        {
            if (this.barcodeScannerStatus[(int)barcodeScannerNameID - 1].MCUReady != mcuReady)
            {
                this.barcodeScannerStatus[(int)barcodeScannerNameID - 1].MCUReady = mcuReady;
                this.NotifyPropertyChanged("LedMCU");
            }
        }

        public void SetBarcodeScannerStatus(GlobalVariables.ScannerName barcodeScannerNameID, string mcuStatus)
        {
            if (this.barcodeScannerStatus[(int)barcodeScannerNameID - 1].MCUStatus != mcuStatus)
            {
                this.barcodeScannerStatus[(int)barcodeScannerNameID - 1].MCUStatus = mcuStatus;
                this.MainStatus = barcodeScannerNameID.ToString() + ": " + mcuStatus;
            }

            this.SetBarcodeScannerStatus(barcodeScannerNameID, false);
        }
        #endregion

        #region Public Method

        public void ResetNextPackQueueID() { this.packQueue.ResetNextQueueID(); this.NotifyPropertyChanged("PackQueue"); }

        public string NextPackQueueDescription { get { return this.packQueue.NextQueueDescription; } }

        public DataTable GetPackQueue()
        {
            if (this.packQueue != null)
            {
                lock (this.packQueue)
                {
                    return this.packQueue.ConverttoTable();
                }
            }
            else return null;
        }

        public DataTable GetPacksetQueue()
        {
            if (this.packsetQueue != null)
            {
                lock (this.packsetQueue)
                {
                    return this.packsetQueue.ConverttoTable();
                }
            }
            else return null;
        }

        public DataTable GetCartonPendingQueue()
        {
            if (this.cartonPendingQueue != null)
            {
                lock (this.cartonPendingQueue)
                {
                    return this.cartonPendingQueue.ConverttoTable();
                }
            }
            else return null;
        }

        public DataTable GetCartonQueue()
        {
            if (this.cartonQueue != null)
            {
                lock (this.cartonQueue)
                {
                    return this.cartonQueue.ConverttoTable();
                }
            }
            else return null;
        }

        public DataTable GetCartonsetQueue()
        {
            if (this.cartonsetQueue != null)
            {
                lock (this.cartonsetQueue)
                {
                    return this.cartonsetQueue.ConverttoTable();
                }
            }
            else return null;
        }

        public DataTable GetPalletQueue()
        {
            if (this.palletQueue != null)
            {
                lock (this.palletQueue)
                {
                    return this.palletQueue.ConverttoTable();
                }
            }
            else return null;
        }

        public DataTable GetPalletPickupQueue()
        {
            if (this.palletPickupQueue != null)
            {
                lock (this.palletPickupQueue)
                {
                    return this.palletPickupQueue.ConverttoTable();
                }
            }
            else return null;
        }



        private bool Connect()
        {
            try
            {
                this.PackIgnoreCount = 0;
                this.CartonIgnoreCount = 0;
                if (GlobalEnums.OnTestScanner)
                {
                    //this.StartScanner();
                }
                else
                {
                    this.MainStatus = "Đang kết nối máy đọc mã vạch ...";

                    if (this.FillingData.HasPack && !this.FillingData.PalletCameraOnly)
                        this.ionetSocketPack.Connect();

                    if (this.FillingData.HasCarton && !this.FillingData.PalletCameraOnly)
                    {
                        this.ionetSocketCarton.Connect();
                        this.ionetSocketCarton.WritetoStream("||>SET OUTPUT.ACTION 1 1\r\n");  //SET IO TO CLEAR ALARM
                    }

                    if (this.FillingData.HasLabel && !this.FillingData.PalletCameraOnly)
                    {
                        this.ionetSocketLabel.Connect();
                        this.ionetSocketLabel.WritetoStream("||>SET OUTPUT.ACTION 1 1\r\n");  //SET IO TO CLEAR ALARM
                    }

                    if (this.FillingData.HasPallet && !GlobalEnums.CBPP && !GlobalEnums.OnTestPalletScanner)
                        this.ionetSocketPallet.Connect();
                }

                this.setLED(true, false, false);

                return true;
            }

            catch (Exception exception)
            {
                this.MainStatus = exception.Message; return false;
            }
        }

        private bool Disconnect()
        {
            try
            {
                this.MainStatus = "Đã ngắt kết nối ...";

                this.ionetSocketPack.Disconnect();

                this.ionetSocketPallet.Disconnect();


                if (!GlobalEnums.OnTestScanner && this.FillingData.HasCarton && !this.FillingData.PalletCameraOnly)
                    this.ionetSocketCarton.WritetoStream("||>SET OUTPUT.ACTION 1 1\r\n");  //SET IO TO CLEAR ALARM

                this.ionetSocketCarton.Disconnect();


                if (!GlobalEnums.OnTestScanner && this.FillingData.HasCartonLabel && !this.FillingData.PalletCameraOnly)
                    this.ionetSocketLabel.WritetoStream("||>SET OUTPUT.ACTION 1 1\r\n");  //SET IO TO CLEAR ALARM

                this.ionetSocketLabel.Disconnect();

                this.setLED();

                return true;
            }

            catch (Exception exception)
            {
                this.MainStatus = exception.Message; return false;
            }

        }

        private bool ClearSocket()
        {
            try
            {
                if (!GlobalEnums.OnTestScanner)
                {
                    if (this.FillingData.HasPack && !this.FillingData.PalletCameraOnly)
                    {
                        lock (this.ionetSocketPack)
                        {
                            this.ionetSocketPack.ReadoutStream();
                        }
                    }

                    if (this.FillingData.HasCarton && !this.FillingData.PalletCameraOnly)
                    {
                        lock (this.ionetSocketCarton)
                        {
                            this.ionetSocketCarton.ReadoutStream();
                        }
                    }

                    if (this.FillingData.HasLabel && !this.FillingData.PalletCameraOnly)
                    {
                        lock (this.ionetSocketLabel)
                        {
                            this.ionetSocketLabel.ReadoutStream();
                        }
                    }

                    if (this.FillingData.HasPallet && !GlobalEnums.CBPP && !GlobalEnums.OnTestPalletScanner)
                    {
                        lock (this.ionetSocketPallet)
                        {
                            this.ionetSocketPallet.ReadoutStream();
                        }
                    }
                }

                return true;
            }

            catch (Exception exception)
            {
                this.MainStatus = exception.Message; return false;
            }
        }


        #endregion Public Method


        #region Public Thread

        /// <summary>
        /// TEST: 
        /// 1) CARTON: NO READ
        /// 2) PALLET: MORE THAN 1 PALLET IS IN THE QUEUE. 
        /// HOW THE ZEBRA WORK? 
        /// WHAT'S HAPPEND WHEN 1 BARCODE PRINTED BY ZEBRA IS READ MORE THAN 1 TIMES (DUPLICATE READ)?
        /// HOW TO RE-PRINNT?
        /// HOW TO PROCESS DUPLICATE CODE IN THE WHOLE TABLE (BY READ 1 CODE MORE THAN 1 TIMES: PACK/ CARTON/ PALLET)
        /// </summary>
        public void ThreadRoutine()
        {
            this.privateFillingData = this.FillingData.ShallowClone();

            string stringReceived = ""; bool packQueueChanged = false; bool packsetQueueChanged = false; bool cartonPendingQueueChanged = false; bool cartonQueueChanged = false; bool cartonsetQueueChanged = false; bool palletQueueChanged = false;

            this.LoopRoutine = true; this.StopScanner();

            try
            {
                if (!this.Connect()) throw new System.InvalidOperationException("Lỗi kết nối đầu đọc mã vạch");

                while (this.LoopRoutine)    //MAIN LOOP. STOP WHEN PRESS DISCONNECT
                {
                    this.MainStatus = this.OnScanning ? "Đang đọc mã vạch ..." : "Sẳn sàng đọc mã vạch";

                    if (this.OnScanning && this.FillingData.HasPack)
                    {
                        if (this.waitforPack(ref stringReceived))
                            packQueueChanged = this.ReceivePack(stringReceived) || packQueueChanged;

                        packsetQueueChanged = this.MakePackset(); packQueueChanged = packQueueChanged || packsetQueueChanged;
                    }

                    if (this.OnScanning && this.FillingData.HasCarton)
                    {
                        if (this.waitforCarton(ref stringReceived))
                        {
                            cartonQueueChanged = this.ReceiveCarton(stringReceived) || cartonQueueChanged;
                            packsetQueueChanged = packsetQueueChanged || cartonQueueChanged; cartonPendingQueueChanged = cartonQueueChanged;
                        }
                    }

                    if (this.OnScanning && this.FillingData.HasCartonLabel)
                    {
                        if (this.waitforLabel(ref stringReceived))
                        {
                            cartonQueueChanged = this.ReceiveCarton(stringReceived, false) || cartonQueueChanged;
                            packsetQueueChanged = packsetQueueChanged || cartonQueueChanged; cartonPendingQueueChanged = cartonPendingQueueChanged || cartonQueueChanged;
                        }
                    }

                    if (this.OnScanning && (this.FillingData.HasCarton || this.FillingData.HasCartonLabel))
                    {
                        cartonsetQueueChanged = this.MakeCartonset(); cartonQueueChanged = cartonQueueChanged || cartonsetQueueChanged;
                    }

                    if (this.FillingData.HasPallet)
                    {
                        if (this.OnScanning && this.waitforPallet(ref stringReceived))
                        {
                            palletQueueChanged = this.ReceivePallet(stringReceived) || palletQueueChanged;
                            cartonsetQueueChanged = cartonsetQueueChanged || palletQueueChanged;
                        }
                        else
                            if (this.palletController.palletService.GetPalletChanged(this.FillingData.FillingLineID))
                                this.InitializePallet();
                    }


                    if (packQueueChanged) { this.NotifyPropertyChanged("PackQueue"); packQueueChanged = false; }
                    if (packsetQueueChanged) { this.NotifyPropertyChanged("PacksetQueue"); packsetQueueChanged = false; }

                    if (cartonPendingQueueChanged) { this.NotifyPropertyChanged("CartonPendingQueue"); cartonPendingQueueChanged = false; }
                    if (cartonQueueChanged) { this.NotifyPropertyChanged("CartonQueue"); cartonQueueChanged = false; }
                    if (cartonsetQueueChanged) { this.NotifyPropertyChanged("CartonsetQueue"); cartonsetQueueChanged = false; }

                    if (palletQueueChanged) { this.NotifyPropertyChanged("PalletQueue"); palletQueueChanged = false; }

                    if (this.OnScanning && this.FillingData.HasCarton && this.FillingData.HasCartonLabel && !this.FillingData.PalletCameraOnly)
                        this.cartonQueue.FindNullInValid();

                    if (!GlobalEnums.OnTestScanner && this.FillingData.HasCarton && !this.FillingData.PalletCameraOnly && GlobalEnums.IOAlarm)
                        this.ionetSocketCarton.WritetoStream("||>SET OUTPUT.ACTION 1 0\r\n"); //SET IO TO ACTIVE ALARM
                    if (!GlobalEnums.OnTestScanner && this.FillingData.HasCartonLabel && !this.FillingData.PalletCameraOnly && GlobalEnums.IOAlarm)
                        this.ionetSocketLabel.WritetoStream("||>SET OUTPUT.ACTION 1 0\r\n"); //SET IO TO ACTIVE ALARM

                    Thread.Sleep(100);

                } //End while this.LoopRoutine
            }
            catch (Exception exception)
            {
                this.LoopRoutine = false;
                this.MainStatus = exception.Message;

                this.setLED(this.LedGreenOn, this.LedAmberOn, true);
            }
            finally
            {
                this.Disconnect();
            }

        }








        private bool waitforPack(ref string stringReceived)
        {
            if (GlobalEnums.OnTestScanner)
                if ((DateTime.Now.Millisecond / (int)GlobalEnums.OnRecivedMillisecond) > 0 && this.FillingData.NextAutoPackCode != "" && this.PackQueueCount < 6 * this.FillingData.PackPerCarton) { stringReceived = this.FillingData.NextAutoPackCode; this.FillingData.NextAutoPackCode = ""; } else stringReceived = "";
            else
                stringReceived = this.ionetSocketPack.ReadoutStream().Trim();

            return stringReceived != null && stringReceived != "";
        }

        private bool ReceivePack(string stringReceived)
        {
            bool barcodeReceived = false;
            string[] arrayBarcode = stringReceived.Split(new string[] { GlobalVariables.charTAB }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string stringBarcode in arrayBarcode)
            {
                string receivedBarcode = stringBarcode.Trim();
                if (receivedBarcode != "NoRead")
                {
                    lock (this.packQueue)
                    {
                        PackDTO messageData = this.addPack(receivedBarcode + (GlobalEnums.CBPP ? " " + this.getBatchNo(GlobalVariables.BarcodeName.Pack) : ""), this.packQueue.NextQueueID);
                        if (messageData != null)
                        {
                            this.packQueue.Enqueue(messageData);
                            barcodeReceived = true;
                        }

                        if (GlobalEnums.CBPP) this.feedbackBatchNo(GlobalVariables.BarcodeName.Pack, CommonExpressions.IncrementSerialNo(this.getBatchNo(GlobalVariables.BarcodeName.Pack)));
                    }
                }
                else
                    this.PackIgnoreCount++;
            }

            return barcodeReceived;
        }

        private PackDTO addPack(string packCode, int queueID)
        {
            try
            {
                PackDTO packDTO = new PackDTO(this.FillingData) { Code = packCode, QueueID = queueID };

                lock (this.packController)
                {
                    PackController freshPackController = new PackController(CommonNinject.Kernel.Get<IPackService>(), this.packViewModel);
                    if (freshPackController.packService.Save(packDTO))
                        return packDTO;
                    else
                        throw new Exception();
                }
            }
            catch (System.Exception exception)
            {
                string msg = exception.Message;
                if (exception.InnerException != null)
                {
                    msg = msg + exception.InnerException.Message;


                    if (exception.InnerException.InnerException != null)
                    {
                        msg = msg + exception.InnerException.InnerException.Message;
                    }
                }

                throw new Exception("Lỗi lưu mã vạch chai [" + packCode + "] " + msg);
            }
        }

        private bool MakePackset()
        {
            bool isSuccessfully = false;
            if (this.OnScanning)
            {
                lock (this.packsetQueue)
                {
                    if (this.packsetQueue.Count <= 0)
                    {
                        lock (this.packQueue)
                        {
                            this.packsetQueue = this.packQueue.Dequeueset();
                        }

                        if (this.packsetQueue.Count > 0)
                        {
                            isSuccessfully = true;
                            lock (this.packController)
                            {
                                if (!this.packController.packService.UpdateEntryStatus(this.packsetQueue.EntityIDs, GlobalVariables.BarcodeStatus.Readytoset)) throw new Exception("Lỗi lưu trạng thái chai: " + this.packController.packService.ServiceTag);
                            }
                        }
                    }
                }
            }
            return isSuccessfully;
        }








        private bool waitforLabel(ref string stringReceived)
        {
            if (GlobalEnums.OnTestScanner) //THE SAME AS waitforCarton
            {
                Random random = new Random();
                if ((DateTime.Now.Millisecond / (int)GlobalEnums.OnRecivedMillisecond) > 0 && this.FillingData.NextAutoCartonCode != "" && this.CartonQueueCount < this.FillingData.CartonPerPallet + 2 && (this.packsetQueue.Count > 0 || !this.FillingData.HasPack)) { stringReceived = GlobalEnums.OnTestCartonNoreadNow ? "NoRead" : ("HTTP://CONNECT.CASTROL.COM/ID-C4STR0L" + random.Next(1, 10).ToString("000")); this.FillingData.NextAutoCartonCode = ""; GlobalEnums.OnTestCartonNoreadNow = false; } else stringReceived = "";
            }
            else
                if (this.FillingData.PalletCameraOnly)
                    stringReceived = "AutoLabelOnly";
                else
                    stringReceived = this.ionetSocketLabel.ReadoutStream().Trim();

            return stringReceived != null && stringReceived != "";
        }





        private bool waitforCarton(ref string stringReceived)
        {
            if (GlobalEnums.OnTestScanner)
                if ((DateTime.Now.Millisecond / (int)GlobalEnums.OnRecivedMillisecond) > 0 && this.FillingData.NextAutoCartonCode != "" && this.CartonQueueCount < this.FillingData.CartonPerPallet + 2 && (this.packsetQueue.Count > 0 || !this.FillingData.HasPack)) { stringReceived = GlobalEnums.OnTestCartonNoreadNow ? "NoRead" : this.FillingData.NextAutoCartonCode; this.FillingData.NextAutoCartonCode = ""; GlobalEnums.OnTestCartonNoreadNow = false; } else stringReceived = "";
            else
                if (this.FillingData.PalletCameraOnly)
                {
                    stringReceived = "";
                    lock (this.FillingData.CartontoZebraQueue)
                    {
                        if (this.FillingData.CartontoZebraQueue.Count > 0)
                        {
                            CartonDTO cartonDTO = this.FillingData.CartontoZebraQueue.Dequeue();
                            if (cartonDTO != null) stringReceived = cartonDTO.Code;
                        }
                    }
                }
                else
                    stringReceived = this.ionetSocketCarton.ReadoutStream().Trim();

            return stringReceived != null && stringReceived != "";
        }

        /// <summary>
        /// NOT FillingData.IgnoreNoreadCarton:         CARTON: WHEN RECEIVE NoRead => ACCEPT: IT WILL ADD NEW CARTON, WHICH CODE WILL BE 'NoRead' 
        ///                                             AND THEN: => USER MUST UPDATE ACTUAL BARCODE BY MANUALL LATER/ OR: REMOVE CARTON WRAPPER => TO WRAP CARTON AGAIN (PRINT + SCAN AGAIN) FROM THE PRODUCTION LINE
        /// 
        /// FillingData.IgnoreNoreadCarton:             CARTON: NO READ => IGNORE. MEANS: USER MUST TRY TO SCAN UNTIL READ SUCCESSFULLY/ OR USER MUST REMOVE THIS PAIL OUT OF PRODUCTION LINE (DELETE) (JUST ONLY SUCCESSFULLY READ => TO ADD TO CartonQueue)
        ///                                             IF REPEATED READ => THE SOFTWARE JUST ACCEPT THE FIRST ONE ONLY (ONLY FOR: NOT FillingData.HasPack)
        /// 
        /// WHEN this.packsetQueue.Count = 0: EMPTY CARTON WILL BE IGNORE. SEE MatchingAndAddCarton FOR MORE INFORMATION
        /// </summary>
        /// <param name="stringReceived"></param>
        /// <returns></returns>
        private string cartonCodePrevious;
        private string cartonLabelPrevious;

        private bool ReceiveCarton(string stringReceived) { return this.ReceiveCarton(stringReceived, true); }
        private bool ReceiveCarton(string stringReceived, bool codeVslabel)
        {
            bool barcodeReceived = false;
            string[] arrayBarcode = stringReceived.Split(new string[] { GlobalVariables.charTAB }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string stringBarcode in arrayBarcode)
            {
                string receivedBarcode = stringBarcode.Trim();
                if (this.FillingData.IgnoreNoreadCarton) receivedBarcode = receivedBarcode.Replace("NoRead", "").Trim(); //IGNORE WHEN NoRead (IF MATCHING BEFORE FILLING: USER MUST RE-PRINT BEFORE READ AGAIN. IF MATCHING AFTER FILL: USER MUST TRY TO READ UNTILL SUCCESS, OTHERWISE: THEY EMPTY THE BOX => THE RE-PRINT BEFORE READ AGAIN)

                if (!codeVslabel)
                    if (receivedBarcode.IndexOf("HTTP://CONNECT.CASTROL.COM/ID-") == -1)
                        receivedBarcode = "NoRead";
                    else
                        receivedBarcode = receivedBarcode.Replace("HTTP://CONNECT.CASTROL.COM/ID-", "").Trim();

                //NOTES: this.FillingData.HasPack && lastCartonCode == receivedBarcode: KHI HasPack: TRÙNG CARTON  || HOẶC LÀ "NoRead": THI CẦN PHẢI ĐƯA SANG 1 QUEUE KHÁC. XỬ LÝ CUỐI CA
                if (receivedBarcode != "")
                {

                    #region CHECK DUPLICATE BARCODE (JUST ONLY HERE) //07-JAN-2019
                    if (receivedBarcode != "NoRead")
                    {
                        if ((codeVslabel && cartonCodePrevious == receivedBarcode) || (!codeVslabel && cartonLabelPrevious == receivedBarcode))
                        {
                            this.ionetSocketCarton.WritetoStream("||>SET OUTPUT.ACTION 1 0\r\n"); //SET IO TO ACTIVE ALARM
                            this.MainStatus = ""; this.MainStatus = "Hai mã vạch liên tiếp trùng nhau: " + receivedBarcode;
                        }
                        else
                        {
                            if (codeVslabel) cartonCodePrevious = receivedBarcode;
                            if (!codeVslabel) cartonLabelPrevious = receivedBarcode;
                        }
                    }
                    #endregion CHECK DUPLICATE BARCODE


                    if (this.matchPacktoCarton((codeVslabel ? receivedBarcode + (GlobalEnums.CBPP ? " " + this.getBatchNo(GlobalVariables.BarcodeName.Carton) : "") : ""), (codeVslabel ? "" : receivedBarcode)))
                        barcodeReceived = true;
                    else
                        this.CartonIgnoreCount++;

                    if (GlobalEnums.CBPP && codeVslabel) this.feedbackBatchNo(GlobalVariables.BarcodeName.Carton, CommonExpressions.IncrementSerialNo(this.getBatchNo(GlobalVariables.BarcodeName.Carton)));
                }
            }
            return barcodeReceived;
        }

        private bool matchPacktoCarton(string cartonCode, string cartonLabel)
        {
            if (cartonCode == "" && cartonLabel == "") return false;

            if (this.FillingData.HasPack && this.packsetQueue.Count <= 0) { this.MakePackset(); }

            CartonDTO cartonDTO = null; bool addNew = false; bool codeVslabel = cartonCode != null && cartonCode != "";

            lock (this.cartonQueue)
            {
                lock (this.packsetQueue)
                {//BY NOW: GlobalVariables.IgnoreEmptyCarton = TRUE. LATER, WE WILL ADD AN OPTION ON MENU FOR USER, IF NEEDED
                    if (!GlobalVariables.IgnoreEmptyCarton || this.packsetQueue.Count > 0 || !this.FillingData.HasPack) //BY NOT this.FillingData.HasPack: this.packsetQueue.Count WILL ALWAYS BE: 0 (NO PACK RECEIVED)
                    {//IF this.packsetQueue.Count <= 0 => THIS WILL SAVE AN EMPTY CARTON. this.packsetQueue.EntityIDs WILL BE BLANK => NO PACK BE UPDATED FOR THIS CARTON
                        cartonDTO = this.cartonQueue.FindNullItem(codeVslabel);
                        if (cartonDTO == null) //CHECK FOR ADD NEW CARTON OR: JUST UPDATE ONLY
                        {
                            addNew = true;
                            cartonDTO = new CartonDTO(this.FillingData) { Code = this.interpretBarcode(cartonCode), Label = this.interpretBarcode(cartonLabel), PackCounts = this.packsetQueue.Count, Quantity = 1, LineVolume = (this.FillingData.HasPack ? this.packsetQueue.TotalLineVolume : this.FillingData.PackageVolume) };
                        }
                        else
                        {
                            if (codeVslabel) cartonDTO.Code = cartonCode;
                            if (!codeVslabel) cartonDTO.Label = cartonLabel;
                        }

                        if (cartonCode == "NoRead" || cartonLabel == "NoRead") cartonDTO.EntryStatusID = (int)GlobalVariables.BarcodeStatus.Noread;

                        lock (this.cartonController)
                        {
                            CartonController freshCartonController = new CartonController(CommonNinject.Kernel.Get<ICartonService>(), this.cartonViewModel);
                            freshCartonController.cartonService.ServiceBag["EntryStatusIDs"] = cartonDTO.EntryStatusID;
                            freshCartonController.cartonService.ServiceBag["PackIDs"] = this.FillingData.HasPack && addNew ? this.packsetQueue.EntityIDs : ""; //VERY IMPORTANT: NEED TO ADD PackIDs TO NEW CartonDTO. WHEN NOT addNew: NNO NEED TO UPDATE PackIDs (BECAUSE: THERE IS NO CHANGE TO PackIDs COLLECTION)
                            if (freshCartonController.cartonService.Save(cartonDTO))
                            {
                                if (addNew)
                                {
                                    if (this.FillingData.HasCartonLabel) this.cartonQueue.Enqueue(cartonDTO); //(*)ADD TO cartonQueue FIRST FOR NEW cartonDTO
                                    this.packsetQueue = new BarcodeQueue<PackDTO>(this.FillingData.HasPackLabel, this.FillingData.NoSubQueue, this.FillingData.ItemPerSubQueue, false) { ItemPerSet = this.FillingData.PackPerCarton }; //CLEAR AFTER ADD TO CartonDTO                                
                                }
                            }
                            else
                                throw new Exception("Lỗi lưu mã vạch carton: " + cartonDTO.Code);
                        }

                        if (!this.FillingData.HasCartonLabel)
                        {
                            if (cartonCode == "NoRead" || cartonLabel == "NoRead")
                                this.cartonPendingQueue.Enqueue(cartonDTO);
                            else
                                this.cartonQueue.Enqueue(cartonDTO);
                        }
                    }
                    else
                        return false;
                }
            }

            if (this.FillingData.HasCartonLabel && cartonDTO != null && !addNew && cartonDTO.Code != "" && cartonDTO.Label != "" & (cartonDTO.Code.IndexOf("NoRead") != -1 || cartonDTO.Label.IndexOf("NoRead") != -1))
                this.MoveCartonToPendingQueue(cartonDTO.CartonID, false, false); //(*)SECOND: MOVE TO cartonPendingQueue 

            return true;
        }

        private bool MakeCartonset()
        {
            bool isSuccessfully = false;
            if (this.OnScanning)
            {
                lock (this.cartonsetQueue)
                {
                    if (this.cartonsetQueue.Count <= 0)
                    {
                        lock (this.cartonQueue)
                        {
                            this.cartonsetQueue = this.cartonQueue.Dequeueset();
                        }

                        if (this.cartonsetQueue.Count > 0)
                        {
                            isSuccessfully = true;
                            lock (this.cartonController)
                            {
                                if (!this.cartonController.cartonService.UpdateEntryStatus(this.cartonsetQueue.EntityIDs, GlobalVariables.BarcodeStatus.Readytoset)) throw new Exception("Lỗi lưu trạng thái carton: " + this.cartonController.cartonService.ServiceTag);
                            }
                        }
                    }
                }
            }
            return isSuccessfully;
        }








        private bool waitforPallet(ref string stringReceived)
        {
            if (GlobalEnums.CBPP || GlobalEnums.OnTestScanner || GlobalEnums.OnTestPalletScanner) //NOTE: 05/MAR/2018: 1 == 1 || : RECEIVE PALLET IMMEDIATELY
                if ((GlobalEnums.OnTestPalletReceivedNow) && ((DateTime.Now.Second % 10) == 0 || GlobalEnums.OnTestPalletReceivedNow) && this.FillingData.NextAutoPalletCode != "" && ((this.cartonsetQueue.Count > 0 && (1 == 1 || this.cartonQueue.Count + 2 > this.FillingData.CartonPerPallet || GlobalEnums.OnTestPalletReceivedImmediately)) || !this.FillingData.HasCarton)) { stringReceived = this.FillingData.NextAutoPalletCode; this.FillingData.NextAutoPalletCode = ""; GlobalEnums.OnTestPalletReceivedNow = (GlobalEnums.NoPallet ? GlobalEnums.OnTestPalletReceivedNow : false); GlobalEnums.OnTestPalletReceivedImmediately = false; } else stringReceived = "";
            else
                stringReceived = this.ionetSocketPallet.ReadoutStream().Trim();

            if (GlobalEnums.ShowStringReceived && stringReceived != null && stringReceived != "") { this.MainStatus = ""; this.MainStatus = stringReceived; }

            return stringReceived != null && stringReceived != "";
        }

        /// <summary>
        /// ONLY WHEN cartonsetQueue IS FULL (MEANS: ONE PALLET IS AVAILABLE): THE SOFTWARE WILL SEND DATA TO ZEBRA FOR PRINTING ONE LABEL
        /// AND: ONLY ONE PRINT PER PALLET (MEANS: PER EACH FULL cartonsetQueue). IF NEEDED: USER MUST PRESS PRINT AGAIN => TO PRINT MANUALLY
        /// THE SOFTWARE ONLY CHECK DUPLICATE CODE FOR CURRENT PALLET WITH THE PREVIOUS RECEIVED CODE ONLY. THE UNIQUE CONSTRAINT ON TABLE WILL CHECK THE WHOLE TABLE FOR DUPLICATE CODE
        /// </summary>
        /// <param name="stringReceived"></param>
        /// <returns></returns>
        private string lastPalletCode = "";
        private bool ReceivePallet(string stringReceived) //ONLY WHEN cartonsetQueue IS FULL + ONLY ONE PRINT PER PALLET: thêm số đếm printed cho cartonsetQueue?????
        {
            bool barcodeReceived = false;
            string[] arrayBarcode = stringReceived.Split(new string[] { GlobalVariables.charTAB }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string stringBarcode in arrayBarcode)
            {
                string receivedBarcode = stringBarcode.Replace("NoRead", "").Trim();//FOR PALLET: IGNORE FOR NoRead => USER MUST TRY TO READ AGAIN,     OR: EVENT PRESS BUTTON TO RE-PRINT MANUALLY IN ORDER TO READ AGAIN
                if (receivedBarcode != "" && lastPalletCode != receivedBarcode)
                    if (this.matchCartontoPallet(receivedBarcode))
                    {
                        lastPalletCode = receivedBarcode;
                        barcodeReceived = true;
                    }
            }
            return barcodeReceived;
        }

        private bool matchCartontoPallet(string palletCode)
        {
            lock (this.palletQueue)
            {
                lock (this.cartonsetQueue)
                {
                    if (!GlobalVariables.IgnoreEmptyPallet || ((this.cartonsetQueue.Count > 0 || !this.FillingData.HasCarton) && (this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Printed || GlobalEnums.OnTestScanner || GlobalEnums.OnTestPalletReceivedNow))) //BY NOW: GlobalVariables.IgnoreEmptyPallet = TRUE. LATER, WE WILL ADD AN OPTION ON MENU FOR USER, IF NEEDED.               NOTES: HERE WE CHECK this.FillingData.CartonsetQueueLabelPrintedCount != 0: TO ENSURE THAT A NEW LABEL HAS BEEN PRINTED BY PrinterController IN ORDER TO MatchingAndAddPallet
                    {//IF this.cartonsetQueue.Count <= 0 => THIS WILL SAVE AN EMPTY PALLET: this.cartonsetQueue.EntityIDs WILL BE BLANK => NO CARTON BE UPDATED FOR THIS PALLET

                        PalletDTO palletDTO = new PalletDTO(this.FillingData) { Code = palletCode, Label = "", PackCounts = this.cartonsetQueue.PackCounts, CartonCounts = this.cartonsetQueue.Count, Quantity = (this.FillingData.HasCarton ? this.cartonsetQueue.TotalQuantity : 1), LineVolume = (this.FillingData.HasCarton ? this.cartonsetQueue.TotalLineVolume : this.FillingData.PackageVolume) };

                        lock (this.palletController)
                        {
                            PalletController freshPalletController = new PalletController(CommonNinject.Kernel.Get<IPalletService>(), this.palletViewModel);
                            freshPalletController.palletService.ServiceBag["CartonIDs"] = this.cartonsetQueue.EntityIDs; //VERY IMPORTANT: NEED TO ADD CartonIDs TO NEW PalletDTO
                            if (freshPalletController.palletService.Save(palletDTO))
                                this.cartonsetQueue = new BarcodeQueue<CartonDTO>(this.FillingData.HasCartonLabel); //CLEAR AFTER ADD TO PalletDTO
                            else
                                throw new Exception("Lỗi lưu pallet: " + palletDTO.Code);
                        }

                        this.palletQueue.Enqueue(palletDTO);
                        return true;
                    }
                    else
                        return false;
                }
            }
        }

        private string interpretBarcode(string barcode)
        {
            if (barcode == "NoRead")
                barcode = this.FillingData.FirstLineA1(false) + this.FillingData.FirstLineA2(false) + this.FillingData.SecondLineA1(false) + this.FillingData.SecondLineA2(false) + "NoRead" + DateTime.Now.ToString("hhmm");
            return barcode;
        }

        public void Reprint()
        {
            try
            {
                if (this.OnScanning)
                {
                    lock (this.cartonsetQueue)
                    {
                        if ((this.cartonsetQueue.Count > 0 || !this.FillingData.HasCarton) && this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Printed)
                            this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Reprint;
                    }
                }
            }
            catch (Exception exception)
            {
                this.MainStatus = exception.Message;
            }
        }

        #endregion Public Thread


        ///WE CAN NOT IMPLEMENT TRANSACTION FOR THESE Handle Exception METHOD (BECAUSE WE CAN NOT UNDO THE BarcodeQueue)
        ///LATER: WE CAN ADD CODE TO ENFORE THE SOFTWARE EXIT IMMEDIATELY, RIGT TIME THE ERROR OCCUR
        ///AFTER EXXIT AND RESTART THE SOFTWARE WILL LOAD PENDING DATA AGAIN
        ///IMPORTANT: SHOULD IMPLEMENT LATER: TO PREVENT UN-PREDICTABLE FOR UN PROPER EXITING SOFTWARE WHEN HANDLE EXCEPTION (EXIT IMMEDIATELY), WE ONLY ALLOW HANDLE EXCEPYION WHEN THE SYSTEM PAUSE (NO ITEM WAS ADDED AT THE TIME WE EXIT THE SYSTEM)
        #region Handle Exception

        public void ToggleLastCartonset(bool lastsetProcessing)
        { //HERE WE ONLY HAVE ToggleLastCartonset. LATER: WE CAN HAVE ToggleLastPackset EASYLY, BY ADD THE SAME METHOD HERE WITHOUT ANY CODE SOMEWHERE ELSE
            this.cartonQueue.LastsetProcessing = lastsetProcessing;
        }


        /// <summary>
        /// If this.Count = ItemPerSet then Reallocation an equal amount of messageData to every subQueue. Each subQueue will have the same NoSubQueue.
        /// </summary>
        /// <returns></returns>
        public bool ReAllocationPack()
        {
            if (this.packQueue.Count >= this.packQueue.ItemPerSet)
            {
                lock (this.packQueue)
                {
                    for (int subQueueID = 0; subQueueID < this.packQueue.NoSubQueue; subQueueID++)
                    {
                        while (this.packQueue.GetSubQueueCount(subQueueID) < (this.packQueue.ItemPerSet / this.packQueue.NoSubQueue) && this.packQueue.Count >= this.packQueue.ItemPerSet)
                        {
                            #region Get a message and swap its' QueueID

                            for (int i = 0; i < this.packQueue.NoSubQueue; i++)
                            {
                                if (this.packQueue.GetSubQueueCount(i) > (this.packQueue.ItemPerSet / this.packQueue.NoSubQueue))
                                {
                                    PackDTO packDTO = this.packQueue.ElementAt(i, this.packQueue.GetSubQueueCount(i) - 1).ShallowClone(); //Get the last pack of SubQueue(i)
                                    packDTO.QueueID = subQueueID; //Set new QueueID

                                    lock (this.packController)
                                    {
                                        if (this.packController.packService.UpdateQueueID(packDTO.PackID.ToString(), packDTO.QueueID))
                                        {
                                            this.packQueue.Dequeue(packDTO.PackID); //First: Remove from old subQueue
                                            this.packQueue.Enqueue(packDTO, false); //Next: Add to new subQueue
                                        }
                                        else throw new System.ArgumentException("Unknown error: Fail to handle this pack", "Can not change new queue ID: " + this.packController.packService.ServiceTag);
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                }

                this.NotifyPropertyChanged("PackQueue");
                return true;
            }
            else
            {
                this.MainStatus = "Số lượng chai tồn không đủ đóng carton";
                return false;
            }
        }

        public bool RemovePackInPackQueue(int packID)
        {
            return this.RemovePackInPackQueue(packID, false);
        }

        public bool RemovePackInPackQueue(int packID, bool removeAll)
        {
            if (packID <= 0 && !removeAll) return false;

            lock (this.packQueue)
            {
                lock (this.packController)
                {
                    do
                    {
                        if (this.packQueue.Count > 0)
                        {
                            PackDTO messageData = this.packQueue.Dequeue(removeAll ? 0 : packID);
                            if (messageData != null)
                            {
                                this.NotifyPropertyChanged("PackQueue");

                                if (!this.packController.packService.Delete(messageData.PackID)) throw new System.ArgumentException("Lỗi", "Không thể xóa chai từ CSDL: " + messageData.Code);
                            }
                            else throw new System.ArgumentException("Lỗi", "Không tìm thấy chai trên hàng");
                        }
                        else throw new System.ArgumentException("Lỗi", "Không còn chai trên hàng");
                    } while (removeAll && this.packQueue.Count > 0);

                    return true; //Delete successfully
                }
            }
        }

        public bool MovePacksetToCartonPendingQueue(int packID)
        {
            if (packID <= 0) return false;

            this.ReceiveCarton("NoRead");
            this.NotifyPropertyChanged("PacksetQueue");
            this.NotifyPropertyChanged("CartonPendingQueue");

            return true;



            //THE FOLLOWING CODE IS ReplacePackInPacksetQueue BY THE FIRST Pack IN PackQueue: THIS CODE IS VERY OK
            //TUY NHIEN: KHONG THUC TE, BOI VI: NEU DELETE 1 PACK => REPLACE BY 1 PACK FROM PackQueue => THUC TE KHÔNG LÀM DUOC, VI NÓ RAT MƠ HỒ: REPLACED BY WHAT?
            lock (this.packQueue)
            {
                lock (this.packsetQueue)
                {
                    if (this.packQueue.Count > 0 && this.packsetQueue.Count == this.packsetQueue.ItemPerSet)
                    {
                        PackDTO messageData = this.packQueue.ElementAt(0).ShallowClone(); //Get the first pack

                        if (this.packsetQueue.Replace(packID, messageData)) //messageData.QueueID: Will change to new value (new position) after replace
                        {
                            this.packQueue.Dequeue(messageData.PackID); //Dequeue the first pack

                            this.NotifyPropertyChanged("PacksetQueue");
                            this.NotifyPropertyChanged("PackQueue");

                            lock (this.packController)
                            {
                                if (!this.packController.packService.Delete(packID)) throw new System.ArgumentException("Fail to handle this pack", "Can not delete pack from the line");

                                if (!this.packController.packService.UpdateQueueID(messageData.PackID.ToString(), messageData.QueueID)) throw new System.ArgumentException("Fail to handle this pack", "Can not update new pack subqueue");

                                if (!this.packController.packService.UpdateEntryStatus(messageData.PackID.ToString(), GlobalVariables.BarcodeStatus.Readytoset)) throw new System.ArgumentException("Fail to handle this pack", "Can not update new pack status");
                                else return true;
                            }
                        }
                        else throw new System.ArgumentException("Fail to handle this pack", "Can not replace pack");
                    }
                    else throw new System.ArgumentException("Fail to handle this pack", "No pack found on the matching line");
                }
            }
        }

        public bool MoveCartonToPendingQueue(int cartonID, bool fromCartonsetQueue, bool externalCall)
        {
            if (cartonID <= 0) return false;

            BarcodeQueue<CartonDTO> movedCartonQueue = fromCartonsetQueue ? this.cartonsetQueue : this.cartonQueue;

            lock (this.cartonQueue)
            {
                lock (this.cartonsetQueue)
                {
                    if (movedCartonQueue.Count > 0 && (true || !fromCartonsetQueue || this.cartonQueue.Count > 0))
                    {
                        CartonDTO cartonDTO = movedCartonQueue.Dequeue(cartonID);
                        if (cartonDTO != null)
                        {
                            lock (this.cartonPendingQueue)
                            {
                                cartonDTO.EntryStatusID = (int)GlobalVariables.BarcodeStatus.Pending;
                                this.cartonPendingQueue.Enqueue(cartonDTO);

                                if (externalCall)
                                {
                                    this.NotifyPropertyChanged("CartonPendingQueue");
                                    if (fromCartonsetQueue) this.NotifyPropertyChanged("CartonsetQueue"); else this.NotifyPropertyChanged("CartonQueue");
                                }

                                lock (this.cartonController)
                                {

                                    if (this.cartonController.cartonService.UpdateEntryStatus(cartonDTO.CartonID.ToString(), GlobalVariables.BarcodeStatus.Pending))
                                    {
                                        if (fromCartonsetQueue & this.cartonQueue.Count > 0)
                                        {
                                            cartonDTO = this.cartonQueue.Dequeue();
                                            if (cartonDTO != null)
                                            {
                                                cartonDTO.EntryStatusID = (int)GlobalVariables.BarcodeStatus.Readytoset;
                                                this.cartonsetQueue.Enqueue(cartonDTO);

                                                if (externalCall)
                                                {
                                                    this.NotifyPropertyChanged("CartonQueue");
                                                    this.NotifyPropertyChanged("CartonsetQueue");
                                                }

                                                if (this.cartonController.cartonService.UpdateEntryStatus(cartonDTO.CartonID.ToString(), GlobalVariables.BarcodeStatus.Readytoset)) return true;
                                                else throw new System.ArgumentException("Fail to handle this carton", "Can not delete carton from the line");
                                            }
                                            else throw new System.ArgumentException("Fail to handle this carton", "Can not remove carton from the line");
                                        }
                                        else
                                            return true;
                                    }
                                    else throw new System.ArgumentException("Fail to handle this carton", "Can not delete carton from the line");
                                }
                            }
                        }
                        else throw new System.ArgumentException("Fail to handle this carton", "Can not remove carton from the line");
                    }
                    else throw new System.ArgumentException("Fail to handle this carton", "No carton found on the line");
                }
            }
        }


        public bool TakebackCartonFromPendingQueue(int cartonID)
        {
            if (cartonID <= 0) return false;

            lock (this.cartonPendingQueue)
            {
                lock (this.cartonQueue)
                {
                    if (this.cartonPendingQueue.Count > 0)
                    {
                        CartonDTO cartonDTO = this.cartonPendingQueue.Dequeue(cartonID);
                        if (cartonDTO != null)
                        {

                            cartonDTO.EntryStatusID = (int)GlobalVariables.BarcodeStatus.Freshnew;
                            this.cartonQueue.Enqueue(cartonDTO);

                            this.NotifyPropertyChanged("CartonPendingQueue");
                            this.NotifyPropertyChanged("CartonQueue");

                            lock (this.cartonController)
                            {
                                if (this.cartonController.cartonService.UpdateEntryStatus(cartonDTO.CartonID.ToString(), GlobalVariables.BarcodeStatus.Freshnew))
                                    return true;
                                else throw new System.ArgumentException("Fail to handle this carton", "Can not delete carton from the line");
                            }

                        }
                        else throw new System.ArgumentException("Fail to handle this carton", "Can not remove carton from the line");
                    }
                    else throw new System.ArgumentException("Fail to handle this carton", "No carton found on the line");
                }
            }
        }



        public Boolean UnwrapCartontoPack(int cartonID)
        {
            if (cartonID <= 0) return false;

            lock (this.packsetQueue)
            {
                if (this.packsetQueue.Count <= 0)
                {
                    lock (this.cartonPendingQueue)
                    {
                        lock (this.packController)
                        {
                            IList<Pack> packs = this.packController.packService.GetPacks(this.FillingData.FillingLineID, (int)GlobalVariables.BarcodeStatus.Wrapped + "", cartonID);
                            if (packs.Count == this.FillingData.PackPerCarton)
                            {
                                packs.Each(pack =>
                                { //(***)
                                    if (pack.BatchID == this.FillingData.BatchID && pack.CommodityID == this.FillingData.CommodityID)
                                    {
                                        PackDTO packDTO = Mapper.Map<Pack, PackDTO>(pack);
                                        packDTO.EntryStatusID = (int)GlobalVariables.BarcodeStatus.Readytoset;
                                        this.packsetQueue.Enqueue(packDTO, false);
                                    }
                                    else
                                        throw new Exception("Lỗi: Mã sản phẩm, số batch của carton không đúng mã sản phẩm, số batch của chuyền");
                                });
                                this.cartonPendingQueue.Dequeue(cartonID);

                                this.NotifyPropertyChanged("PacksetQueue");
                                this.NotifyPropertyChanged("CartonPendingQueue");

                                lock (this.cartonController)
                                {
                                    this.cartonController.cartonService.ServiceBag["EntryStatusIDs"] = (int)GlobalVariables.BarcodeStatus.Noread + "," + (int)GlobalVariables.BarcodeStatus.Pending; //THIS CARTON MUST BE Noread || Pending IN ORDER TO UNWRAP TO PACK
                                    this.cartonController.cartonService.ServiceBag["PackIDs"] = this.packsetQueue.EntityIDs; //SEE (***): WE HAVE ADDED ALL PACK OF THIS CARTON TO packsetQueue ALREADY. SO, NOW WE CAN USE this.packsetQueue.EntityIDs FOR ServiceBag["PackIDs"]
                                    if (!this.cartonController.cartonService.Delete(cartonID)) throw new System.ArgumentException("Lỗi", "Không thể xóa carton trên CSDL");
                                }
                                return true;
                            }
                            else
                            {
                                this.MainStatus = "Không thể đóng lại carton, do số lượng chai của carton và trên chuyền không phù hợp.";
                                return false;
                            }
                        }
                    }
                }
                else throw new System.ArgumentException("Fail to handle this carton", "Another carton is on the line");
            }
        }

        public Boolean DeleteCarton(int cartonID, bool removeAll)
        {
            if (cartonID <= 0) return false;

            lock (this.cartonPendingQueue)
            {
                lock (this.packController)
                {
                    lock (this.cartonController)
                    {
                        do
                        {
                            if (this.cartonPendingQueue.Count > 0)
                            {
                                if (removeAll) cartonID = this.cartonPendingQueue.ElementAt(0).CartonID;

                                IList<Pack> packs = this.packController.packService.GetPacks(this.FillingData.FillingLineID, (int)GlobalVariables.BarcodeStatus.Wrapped + "", cartonID);

                                this.cartonController.cartonService.ServiceBag["EntryStatusIDs"] = (int)GlobalVariables.BarcodeStatus.Noread + "," + (int)GlobalVariables.BarcodeStatus.Pending; //THIS CARTON MUST BE Noread || Pending IN ORDER TO UNWRAP TO PACK
                                this.cartonController.cartonService.ServiceBag["PackIDs"] = string.Join(",", packs.Select(d => d.PackID));
                                this.cartonController.cartonService.ServiceBag["DeletePack"] = true;
                                if (!this.cartonController.cartonService.Delete(cartonID)) throw new System.ArgumentException("Lỗi", "Không thể xóa carton trên CSDL");

                                this.cartonPendingQueue.Dequeue(cartonID);

                                this.NotifyPropertyChanged("CartonPendingQueue");
                            }
                            else throw new System.ArgumentException("Lỗi", "Không còn carton");
                        } while (removeAll && this.cartonPendingQueue.Count > 0);
                    }
                }
                return true;
            }
        }


        public Boolean UpdateCartonBarcode(int cartonID, string cartonBarcode)
        {
            if (cartonID <= 0 || cartonBarcode.Length < 12) throw new Exception("Invalid barcode: It is required 12 letters barcode [" + cartonBarcode + "]");

            lock (this.cartonQueue)
            {
                return true;
                // can phai xem lai doan code sau;
                //DataDetail.DataDetailCartonRow dataDetailCartonRow = this.cartonDataTable.FindByCartonID(cartonID);
                //if (dataDetailCartonRow != null && dataDetailCartonRow.CartonStatus == (byte)GlobalVariables.BarcodeStatus.Noread)
                //{
                //    lock (this.CartonTableAdapter)
                //    {
                //        int rowsAffected = this.CartonTableAdapter.UpdateCartonBarcode((byte)GlobalVariables.BarcodeStatus.Freshnew, cartonBarcode, dataDetailCartonRow.CartonID);
                //        if (rowsAffected == 1)
                //        {
                //            dataDetailCartonRow.CartonStatus = (byte)GlobalVariables.BarcodeStatus.Freshnew;
                //            dataDetailCartonRow.CartonBarcode = cartonBarcode;
                //        }
                //        else throw new System.ArgumentException("Fail to handle this carton", "Insufficient update carton");
                //    }

                //    this.NotifyPropertyChanged("CartonQueue");

                //    return true;
                //}
                //else return false;
            }

        }

        #endregion Handle Exception

        #region NmvnBackup



        //private BackupLogEventTableAdapter backupLogEventTableAdapter;
        //protected BackupLogEventTableAdapter BackupLogEventTableAdapter
        //{
        //    get
        //    {
        //        if (backupLogEventTableAdapter == null) backupLogEventTableAdapter = new BackupLogEventTableAdapter();
        //        return backupLogEventTableAdapter;
        //    }
        //}


        public void BackupData()
        {
            //    int backupID = 0;
            //    try
            //    {
            //        string backupLocation = "C:\\BPFillingSystem\\Database\\BACKUP";

            //        DataDetailCartonTableAdapter dataDetailCartonTableAdapter = new DataDetailCartonTableAdapter();
            //        DateTime? minOfCartonDate = (DateTime?)dataDetailCartonTableAdapter.GetMinOfCartonDate();
            //        DateTime? maxOfCartonDate = (DateTime?)dataDetailCartonTableAdapter.GetMaxOfCartonDate();

            //        int noBackupDate = 45 + 1;

            //        if (minOfCartonDate != null && maxOfCartonDate != null && ((DateTime)minOfCartonDate).AddDays(noBackupDate).Date.AddSeconds(-1) < maxOfCartonDate)
            //        { //NEED TO BACKUP
            //            //

            //            //==OKKK== ADODatabase.ExecuteNonQuery("EXEC BackupNmvnDatabases 'BPFillingSystem' , 'D:\\VC PROJECTS\\BP\DATA\\bak'")
            //            //==OKKK== EXEC RestoreNmvnDatabases 'BPFillingSystem_ok', 'D:\\VC PROJECTS\\BP\\DATA\\BakBPFillingSystem_FULL_04192017_202140.BAK' , 'D:\\VC PROJECTS\\BP\DATA\\bak1'

            //            DataDetail.BackupLogEventDataTable backupLogEventDataTable = new DataDetail.BackupLogEventDataTable();
            //            DataDetail.BackupLogEventRow backupLogEventRow = backupLogEventDataTable.NewBackupLogEventRow();

            //            backupLogEventDataTable.AddBackupLogEventRow(backupLogEventRow);

            //            if (this.BackupLogEventTableAdapter.Update(backupLogEventRow) == 1) { backupID = backupLogEventRow.BackupID; } else throw new Exception("Insufficient save backup log");


            //            SqlParameter[] sqlParameter = new SqlParameter[6];
            //            sqlParameter[0] = new SqlParameter("databaseName", GlobalMsADO.DatabaseName); sqlParameter[0].SqlDbType = SqlDbType.NVarChar; sqlParameter[0].Direction = ParameterDirection.Input;
            //            sqlParameter[1] = new SqlParameter("backupLocation", backupLocation); sqlParameter[1].SqlDbType = SqlDbType.NVarChar; sqlParameter[1].Direction = ParameterDirection.Input;
            //            sqlParameter[2] = new SqlParameter("restoreLocation", ""); sqlParameter[2].SqlDbType = SqlDbType.NVarChar; sqlParameter[2].Direction = ParameterDirection.Input;
            //            sqlParameter[3] = new SqlParameter("backupID", backupID); sqlParameter[3].SqlDbType = SqlDbType.Int; sqlParameter[3].Direction = ParameterDirection.Input;
            //            sqlParameter[4] = new SqlParameter("beginningCartonDate", minOfCartonDate); sqlParameter[4].SqlDbType = SqlDbType.DateTime; sqlParameter[4].Direction = ParameterDirection.Input;
            //            sqlParameter[5] = new SqlParameter("endingCartonDate", ((DateTime)minOfCartonDate).AddDays(noBackupDate).Date.AddSeconds(-1)); sqlParameter[5].SqlDbType = SqlDbType.DateTime; sqlParameter[5].Direction = ParameterDirection.Input;
            //            ADODatabase.ExecuteNonQuery("BackupNmvnDatabases", CommandType.StoredProcedure, "", sqlParameter, 60 * 15);

            //            //this.BackupLogEventTableAdapter.BackupNmvnDatabases(GlobalMsADO.DatabaseName, backupLocation, restoreLocation, backupID, minOfCartonDate, ((DateTime)minOfCartonDate).AddDays(10));

            //            //this.NmvnBackupLogEventChange = false;
            //            //this.NmvnBackupLogEventChange = true; //NO NEED TO RAISE EVENT
            //        }

            //    }
            //    catch (System.Exception exception)
            //    {
            //        ADODatabase.ExecuteNonQuery("UPDATE BackupLogEvent SET Description = CASE WHEN Description IS NULL THEN '' ELSE Description END + N'" + exception.Message.Substring(0, exception.Message.Length > 50 ? 50 : exception.Message.Length) + "' WHERE BackupID = " + backupID);
            //    }
        }



        #endregion NmvnBackup

    }

    public class BarcodeScannerStatus
    {
        public GlobalVariables.ScannerName BarcodeScannerNameID { get; set; }
        public bool MCUReady { get; set; }
        public string MCUStatus { get; set; }

        public BarcodeScannerStatus(GlobalVariables.ScannerName barcodeScannerNameID)
        {
            this.BarcodeScannerNameID = barcodeScannerNameID;
            this.MCUReady = true;
            this.MCUStatus = "";
        }
    }
}
