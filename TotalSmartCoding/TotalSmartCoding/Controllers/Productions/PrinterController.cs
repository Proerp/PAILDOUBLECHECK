﻿using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using TotalBase;
using TotalBase.Enums;
using TotalCore.Services.Productions;
using TotalDTO.Productions;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalSmartCoding.Libraries.Communications;

namespace TotalSmartCoding.Controllers.Productions
{
    public class PrinterController : CodingController
    {
        #region Storage

        private IBatchService batchService;

        private FillingData privateFillingData;

        private readonly GlobalVariables.PrinterName printerName;
        private readonly bool isLaser;


        private IONetSocket ionetSocket;
        private IOSerialPort ioserialPort;


        private bool onPrinting;
        private bool resetMessage;


        private string lastNACKCode;
        private int lastProductCounting = 0;

        #endregion Storage


        #region Contructor

        public PrinterController(IBatchService batchService, FillingData fillingData, GlobalVariables.PrinterName printerName) : this(batchService, fillingData, printerName, false) { }
        public PrinterController(IBatchService batchService, FillingData fillingData, GlobalVariables.PrinterName printerName, bool isLaser)
        {

            try
            {
                this.batchService = batchService;

                this.FillingData = fillingData;
                this.privateFillingData = this.FillingData.ShallowClone();

                this.printerName = printerName;
                this.isLaser = isLaser;

                ProductionAPIs productionAPIs = new ProductionAPIs();

                this.ionetSocket = new IONetSocket(IPAddress.Parse(productionAPIs.IpAddress((int)this.printerName)), 7000, this.isLaser);
                this.ioserialPort = new IOSerialPort(GlobalVariables.ComportName, 9600, Parity.None, 8, StopBits.One, false, "Zebra");


                this.ioserialPort.PropertyChanged += new PropertyChangedEventHandler(ioserialPort_PropertyChanged);
            }
            catch (Exception exception)
            {
                this.MainStatus = exception.Message;
            }
        }


        private void ioserialPort_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                PropertyInfo prop = this.GetType().GetProperty(e.PropertyName, BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(this, sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null), null);
                else
                    this.MainStatus = e.PropertyName + ": " + sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null).ToString();
            }
            catch (Exception exception)
            {
                this.MainStatus = exception.Message;
            }
        }

        #endregion Contructor


        #region Public Properties


        public void StartPrint()
        {
            if (GlobalEnums.OnTestPrinter) this.MainStatus = "Đang in ...";
            this.OnPrinting = true;
        }
        public void StopPrint() { this.OnPrinting = false; }


        public bool OnPrinting
        {
            get { return this.onPrinting; }
            private set { this.onPrinting = value; this.resetMessage = true; }
        }

        public string NextDigitNo
        {
            get { return this.privateFillingData.NextDigitNo; }
            private set
            {
                if (this.privateFillingData.NextDigitNo != value)
                {
                    this.privateFillingData.NextDigitNo = value;
                    this.NotifyPropertyChanged("NextDigitNo");

                    if (this.OnPrinting && GlobalEnums.DrumWithDigit && !this.FillingData.HasCarton) this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Freshnew;
                }
            }
        }

        public string NextPackNo
        {
            get { return this.privateFillingData.NextPackNo; }
            private set
            {
                if (this.privateFillingData.NextPackNo != value)
                {
                    this.privateFillingData.NextPackNo = value;
                    this.NotifyPropertyChanged("NextPackNo");
                }
            }
        }

        public string NextCartonNo
        {
            get { return this.privateFillingData.NextCartonNo; }
            private set
            {
                if (this.privateFillingData.NextCartonNo != value)
                {
                    this.privateFillingData.NextCartonNo = value;
                    this.NotifyPropertyChanged("NextCartonNo");
                }
            }
        }




        public string SentDigitNo
        {
            get { return this.privateFillingData.SentDigitNo; }
            private set
            {
                if (this.privateFillingData.SentDigitNo != value)
                {
                    this.privateFillingData.SentDigitNo = value;
                    this.NotifyPropertyChanged("SentDigitNo");

                    if (this.OnPrinting && GlobalEnums.DrumWithDigit && !this.FillingData.HasCarton) this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Freshnew;
                }
            }
        }

        public string SentPackNo
        {
            get { return this.privateFillingData.SentPackNo; }
            private set
            {
                if (this.privateFillingData.SentPackNo != value)
                {
                    this.privateFillingData.SentPackNo = value;
                    this.NotifyPropertyChanged("SentPackNo");
                }
            }
        }

        public string SentCartonNo
        {
            get { return this.privateFillingData.SentCartonNo; }
            private set
            {
                if (this.privateFillingData.SentCartonNo != value)
                {
                    this.privateFillingData.SentCartonNo = value;
                    this.NotifyPropertyChanged("SentCartonNo");
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

        public string NextPalletNo
        {
            get { return this.privateFillingData.NextPalletNo; }
            private set
            {
                if (this.privateFillingData.NextPalletNo != value)
                {
                    this.privateFillingData.NextPalletNo = value;
                    this.NotifyPropertyChanged("NextPalletNo");
                }
            }
        }

        public string SentPalletNo
        {
            get { return this.privateFillingData.SentPalletNo; }
            private set
            {
                if (this.privateFillingData.SentPalletNo != value)
                {
                    this.privateFillingData.SentPalletNo = value;
                    this.NotifyPropertyChanged("SentPalletNo");
                }
            }
        }

        public string getPreviousNo()
        {
            return this.getPreviousNo(this.getNextNo());
        }
        public string getPreviousNo(string nextNo)
        {
            return (int.Parse(nextNo) - 1).ToString("0000000").Substring(1);
        }

        private string getNextNo()
        {
            return this.getNextNo(false);
        }

        private string getNextNo(bool sendCartontoZebra)
        {
            if (this.printerName == GlobalVariables.PrinterName.DigitInkjet || this.printerName == GlobalVariables.PrinterName.PackInkjet)
            {
                if (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Pail)
                    return this.NextCartonNo;
                else
                    if (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Drum)
                        return this.printerName == GlobalVariables.PrinterName.DigitInkjet ? this.NextDigitNo : this.NextCartonNo;
                    else
                        return this.NextPackNo;
            }
            else
                if (this.printerName == GlobalVariables.PrinterName.CartonInkjet || sendCartontoZebra)
                    return this.NextCartonNo;
                else
                    if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                        return this.NextPalletNo;
                    else
                        return "XXXXXX"; //THIS return value WILL RAISE ERROR TO THE CALLER FUNCTION, BUCAUSE IT DON'T HAVE A CORRECT FORMAT

        }


        private string getSentNo()
        {
            if (this.printerName == GlobalVariables.PrinterName.DigitInkjet || this.printerName == GlobalVariables.PrinterName.PackInkjet)
            {
                if (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Pail)
                    return this.SentCartonNo;
                else
                    if (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Drum)
                        return this.printerName == GlobalVariables.PrinterName.DigitInkjet ? this.SentDigitNo : this.SentCartonNo;
                    else
                        return this.SentPackNo;
            }
            else
                if (this.printerName == GlobalVariables.PrinterName.CartonInkjet)
                    return this.SentCartonNo;
                else
                    if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                        return this.SentPalletNo;
                    else
                        return "XXXXXX"; //THIS return value WILL RAISE ERROR TO THE CALLER FUNCTION, BUCAUSE IT DON'T HAVE A CORRECT FORMAT

        }


        private void feedbackNextNo(string nextNo)
        {
            this.feedbackNextNo(nextNo, "");
        }

        private void feedbackNextNo(string nextNo, string receivedFeedback)
        {
            this.feedbackNextNo(nextNo, receivedFeedback, false);
        }

        private void feedbackNextNo(string nextNo, string receivedFeedback, bool sendCartontoZebra)
        {
            if (nextNo == "" && receivedFeedback.Length > 12)
            {
                int serialNumber = 0;
                if (int.TryParse(receivedFeedback.Substring(6, 6), out serialNumber))
                    nextNo = serialNumber.ToString("0000000").Substring(1);//SHOULD OR NOT: Increase serialNumber by 1 (BECAUSE: nextNo MUST GO AHEAD BY 1??): TEST AT DATMY: FOR AX350: NO NEED, BUCAUSE: AX350 RETURN THE NEXT VALUE. BUT FOR A200+: RETURN THE PRINTED VALUE
            }

            if (nextNo != "")
            {
                if (this.printerName == GlobalVariables.PrinterName.DigitInkjet)
                    this.NextDigitNo = nextNo;
                else
                    if (this.printerName == GlobalVariables.PrinterName.PackInkjet)
                        this.NextPackNo = nextNo;
                    else
                        if (this.printerName == GlobalVariables.PrinterName.CartonInkjet || sendCartontoZebra)
                            this.NextCartonNo = nextNo;
                        else
                            if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                                this.NextPalletNo = nextNo;

                lock (this.batchService) //ALL PrinterController MUST SHARE THE SAME IBatchService, BECAUSE WE NEED TO LOCK IBatchService IN ORDER TO CORRECTED UPDATE DATA BY IBatchService
                {
                    if (!this.batchService.CommonUpdate(this.FillingData.BatchID, this.printerName == GlobalVariables.PrinterName.PackInkjet || (GlobalEnums.DrumWithDigit && this.printerName == GlobalVariables.PrinterName.DigitInkjet) ? nextNo : "", this.printerName == GlobalVariables.PrinterName.CartonInkjet || sendCartontoZebra ? nextNo : "", this.printerName == GlobalVariables.PrinterName.PalletLabel && !sendCartontoZebra ? nextNo : ""))
                        this.MainStatus = this.batchService.ServiceTag;
                }
            }

            //////#region 27.NOV.2017 - BREAK WHEN NextDigitNo - NextPackNo > 3
            //////if (this.privateFillingData.FillingLineID == GlobalVariables.FillingLine.Smallpack || this.privateFillingData.FillingLineID == GlobalVariables.FillingLine.Pail)
            //////{
            //////    int nextDigitNo; int nextPackNo = 0;
            //////    if (int.TryParse(this.FillingData.NextDigitNo, out nextDigitNo) && int.TryParse(this.FillingData.NextPackNo, out nextPackNo) && Math.Abs(nextDigitNo - nextPackNo) > 3)
            //////    {
            //////        this.storeMessage("  "); Thread.Sleep(500); 
            //////        throw new System.InvalidOperationException("Lỗi số đếm: Số in trên cổ chai: " + this.FillingData.NextDigitNo + ". Số barcode: " + this.FillingData.NextPackNo);
            //////    }
            //////    //this.MainStatus = this.NextDigitNo + " : "  + this.FillingData.NextPackNo + " <> " + (Math.Abs(nextDigitNo - nextPackNo)).ToString();
            //////}
            //////#endregion

        }


        private void feedbackSentNo(string sentNo)
        {
            if (sentNo != "")
            {
                if (this.printerName == GlobalVariables.PrinterName.DigitInkjet)
                    this.SentDigitNo = sentNo;
                else
                    if (this.printerName == GlobalVariables.PrinterName.PackInkjet)
                        this.SentPackNo = sentNo;
                    else
                        if (this.printerName == GlobalVariables.PrinterName.CartonInkjet)
                            this.SentCartonNo = sentNo;
                        else
                            if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                                this.SentPalletNo = sentNo;

                lock (this.batchService) //ALL PrinterController MUST SHARE THE SAME IBatchService, BECAUSE WE NEED TO LOCK IBatchService IN ORDER TO CORRECTED UPDATE DATA BY IBatchService
                {
                    if (!this.batchService.ExtraUpdate(this.FillingData.BatchID, this.printerName == GlobalVariables.PrinterName.PackInkjet || (GlobalEnums.DrumWithDigit && this.printerName == GlobalVariables.PrinterName.DigitInkjet) ? sentNo : "", this.printerName == GlobalVariables.PrinterName.CartonInkjet ? sentNo : "", this.printerName == GlobalVariables.PrinterName.PalletLabel ? sentNo : ""))
                        this.MainStatus = this.batchService.ServiceTag;
                }
            }
        }


        public string NextAutoBarcodeCode
        {
            get
            {
                if (this.printerName == GlobalVariables.PrinterName.PackInkjet)
                    return this.FillingData.NextAutoPackCode;
                else
                    if (this.printerName == GlobalVariables.PrinterName.CartonInkjet)
                        return this.FillingData.NextAutoCartonCode;
                    else
                        if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                            return this.FillingData.NextAutoPalletCode;
                        else
                            return "";
            }
            private set
            {
                if ((this.FillingData.AutoBarcode || (GlobalEnums.CBPP && this.printerName == GlobalVariables.PrinterName.PalletLabel)) && this.NextAutoBarcodeCode != value)
                {
                    if (this.printerName == GlobalVariables.PrinterName.PackInkjet)
                        this.FillingData.NextAutoPackCode = value;
                    else
                        if (this.printerName == GlobalVariables.PrinterName.CartonInkjet)
                            this.FillingData.NextAutoCartonCode = value;
                        else
                            if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                                this.FillingData.NextAutoPalletCode = value;

                    this.NotifyPropertyChanged("NextAutoBarcodeCode");
                }
            }
        }

        #endregion Public Properties


        #region Message Configuration

        /// <summary>
        /// Numeric Serial Only, No Alpha Serial, Zero Leading, 6 Digit: 000001 -> 999999, Step 1, Start this.currentSerialNumber, Repeat: 0 
        /// Don use this Startup Serial Value, because some Dimino printer do no work!!! - DON't KNOW!!! serialNumberFormat = GlobalVariables.charESC + "/j/" + serialNumberIndentity.ToString() + "/N/06/000001/999999/000001/Y/N/0/" + this.currentSerialNumber + "/00000/N/"; //WITH START VALUE---No need to update serial number
        /// WITH START VALUE = 1 ---> NEED TO UPDATE serial number
        /// 
        /// serialNumberIndentity = 1 when print as text on first line, 2 when insert into 2D Barcode
        /// 
        /// IMPORTANT: [EAN BARCODE] DOES NOT ALLOW INSERT SERIAL NUMBER
        /// </summary>
        /// <param name="serialNumberIndentity"></param>
        /// <returns></returns>
        private string dominoSerialNumber(int serialNumberIndentity)
        {
            if (this.printerName != GlobalVariables.PrinterName.PalletLabel)
                if ((this.printerName == GlobalVariables.PrinterName.PackInkjet || this.printerName == GlobalVariables.PrinterName.CartonInkjet) && GlobalEnums.Buffered)
                    return this.getSentNo(); //CommonExpressions.IncrementSerialNo(this.getSentNo(), 1);
                else
                    return GlobalVariables.charESC + "/j/" + serialNumberIndentity.ToString() + "/N/06/000001/999999/000001/Y/N/0/000001/00000/N/";
            else
                return this.privateFillingData.NextPalletNo; //---Dont use counter (This will be updated MANUALLY for each pallet)
        }

        private string firstLine(bool isReadableText)
        {
            return this.firstLine(isReadableText, false);
        }

        private string firstLine(bool isReadableText, bool sendCartontoZebra)
        {
            return this.firstLineA2(isReadableText, sendCartontoZebra);
        }

        private string firstLineA1(bool isReadableText)
        {
            return this.privateFillingData.FirstLineA1(isReadableText);
        }

        private string firstLineA2(bool isReadableText)
        {
            return this.firstLineA2(isReadableText, false);
        }

        private string firstLineA2(bool isReadableText, bool sendCartontoZebra)
        {
            return this.privateFillingData.FirstLineA2(isReadableText) + (this.printerName == GlobalVariables.PrinterName.PackInkjet ? "B" : (this.printerName == GlobalVariables.PrinterName.CartonInkjet || sendCartontoZebra ? "C" : (this.printerName == GlobalVariables.PrinterName.PalletLabel ? "P" : "")));
        }

        public string secondLine(bool isReadableText)
        {
            return this.secondLineA1(isReadableText); // + this.secondLineA2(isReadableText)
        }

        public string secondLineA1(bool isReadableText)
        {
            return this.privateFillingData.SecondLineA1(isReadableText);
        }

        public string secondLineA2(bool isReadableText)
        {
            return this.privateFillingData.SecondLineA2(isReadableText);
        }

        private string thirdLine(bool isReadableText, int serialIndentity)
        {
            return this.thirdLine(isReadableText, serialIndentity, false);
        }

        private string thirdLine(bool isReadableText, int serialIndentity, bool sendCartontoZebra)
        {
            return this.thirdLineA1(isReadableText) + this.thirdLineA2(isReadableText, serialIndentity, sendCartontoZebra);
        }

        private string thirdLineA1(bool isReadableText)
        {
            return this.privateFillingData.ThirdLineA1(isReadableText);
        }

        private string thirdLineA2(bool isReadableText, int serialIndentity)
        {
            return this.thirdLineA2(isReadableText, serialIndentity, false);
        }

        private string thirdLineA2(bool isReadableText, int serialIndentity, bool sendCartontoZebra)
        {
            return this.privateFillingData.FillingLineCode + (serialIndentity < 0 ? "" : (serialIndentity == 1 || serialIndentity == 2 ? (GlobalEnums.OnTestPrinter ? this.getNextNo() : this.dominoSerialNumber(serialIndentity)) : ((GlobalEnums.DrumWithDigit && !this.FillingData.HasCarton) ? this.getPreviousNo(this.FillingData.NextDigitNo) : (sendCartontoZebra ? this.getNextNo(true) : (this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Freshnew ? this.getNextNo() : this.getPreviousNo())))));
        }

        private string wholeBarcode(int serialIndentity)
        {
            return this.wholeBarcode(serialIndentity, false);
        }

        private string wholeBarcode(int serialIndentity, bool sendCartontoZebra)
        {
            return this.FirstMessageLine(false) + " " + this.SecondMessageLine(false) + " " + this.ThirdMessageLine(serialIndentity, false);

            return this.firstLine(false, sendCartontoZebra) + this.secondLine(false) + this.thirdLine(false, serialIndentity, sendCartontoZebra);
        }

        #region CBPP
        private string FirstMessageLine(bool isReadableText) //Only DominoPrinterName.CartonInkjet: HAS SERIAL NUMBER (BUT WILL BE UPDATE MANUAL FOR EACH CARTON - BECAUSE: [EAN BARCODE] DOES NOT ALLOW INSERT SERIAL NUMBER) ===> FOR THIS: BatchSerialNumber FOR EVERY PACK: NEVER USE
        {
            return ((this.printerName == GlobalVariables.PrinterName.PackInkjet || this.printerName == GlobalVariables.PrinterName.CartonInkjet) && isReadableText ? this.privateFillingData.EntryMonthID.ToString("00") + " " : "") + this.privateFillingData.BatchCode;
        }

        private string SecondMessageLine(bool isReadableText)
        {
            return (isReadableText ? this.privateFillingData.Shelflife.ToString("00") : "") + (!isReadableText ? this.privateFillingData.CommodityCode + " " : "NSX") + this.FillingData.SettingDate.ToString("ddMMyy");
            //return "NSX " + GlobalVariables.charESC + "/n/1/A/" + GlobalVariables.charESC + "/n/1/F/" + GlobalVariables.charESC + "/n/1/D/";
        }

        private string ThirdMessageLine(int serialNumberIndentity, bool isReadableText) //serialNumberIndentity = 1 when print as text on first line, 2 when insert into 2D Barcode
        {
            //string serialNumberFormat = ""; //Numeric Serial Only, No Alpha Serial, Zero Leading, 6 Digit: 000001 -> 999999, Step 1, Start this.privateFillingData.MonthSerialNumber, Repeat: 0
            //serialNumberFormat = GlobalVariables.charESC + "/j/" + serialNumberIndentity.ToString() + "/N/06/000001/999999/000001/Y/N/0/000001/00000/N/"; //WITH START VALUE = 1 ---> NEED TO UPDATE serial number

            return ((this.printerName != GlobalVariables.PrinterName.PackInkjet && this.printerName != GlobalVariables.PrinterName.CartonInkjet) || isReadableText ? this.privateFillingData.CommodityCode : "") + ((this.printerName != GlobalVariables.PrinterName.PackInkjet && this.printerName != GlobalVariables.PrinterName.CartonInkjet) || !isReadableText ? this.privateFillingData.EntryMonthID.ToString("00") : "") + this.privateFillingData.FillingLineCode + (isReadableText ? " " : "") + ((GlobalEnums.OnTestPrinter || (GlobalEnums.CBPP && this.printerName == GlobalVariables.PrinterName.PalletLabel)) ? this.getNextNo() : this.dominoSerialNumber(serialNumberIndentity));
        }
        #endregion

        private string EANInitialize(string twelveDigitCode)
        {

            int iSum = 0; int iDigit = 0;

            twelveDigitCode = twelveDigitCode.Replace("/", "");

            // Calculate the checksum digit here.
            for (int i = twelveDigitCode.Length; i >= 1; i--)
            {
                iDigit = Convert.ToInt32(twelveDigitCode.Substring(i - 1, 1));
                if (i % 2 == 0)
                {	// odd
                    iSum += iDigit * 3;
                }
                else
                {	// even
                    iSum += iDigit * 1;
                }
            }

            int iCheckSum = (10 - (iSum % 10)) % 10;
            return twelveDigitCode + iCheckSum.ToString();

            #region Checksum rule
            ////Checksum rule
            ////Lấy tổng tất cả các số ở vị trí lẻ (1,3,5,7,9,11) được một số A.
            ////Lấy tổng tất cả các số ở vị trí chẵn (2,4,6,8,10,12). Tổng này nhân với 3 được một số (B).
            ////Lấy tổng của A và B được số A+B.
            ////Lấy phần dư trong phép chia của A+B cho 10, gọi là số x. Nếu số dư này bằng 0 thì số kiểm tra bằng 0, nếu nó khác 0 thì số kiểm tra là phần bù (10-x) của số dư đó.

            //Generate check sum number
            //            --**************************************
            //-- Name: Check Digit for EAN13 Barcode
            //-- Description:Calculates the Check Digit for a EAN13 Barcode
            //-- By: Conradude
            //--
            //-- Inputs:SELECT dbo.fu_EAN13CheckDigit('600100700010')
            //--
            //-- Returns:String with 13 Digits
            //--
            //-- Side Effects:Hair Growth on the palms of your hand
            //--
            //--This code is copyrighted and has-- limited warranties.Please see http://www.Planet-Source-Code.com/vb/scripts/ShowCode.asp?txtCodeId=891&lngWId=5--for details.--**************************************

            //CREATE FUNCTION fu_EAN13CheckDigit (@Barcode varchar(12))																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																								-->>>><<><<<<<>>>>>>>>>>>>>>>>< <><><>												
            //RETURNS varchar(13)
            //AS
            //BEGIN
            //DECLARE @SUM int ,
            //    @COUNTER int,
            //    @RETURN varchar(13),
            //    @Val1 int,
            //    @Val2 int	
            //SET @COUNTER = 1
            //SET @SUM = 0
            //WHILE @Counter < 13
            //BEGIN
            //    SET @VAL1 = SUBSTRING(@Barcode,@counter,1) * 1
            //    SET @VAL2 = SUBSTRING(@Barcode,@counter + 1,1) * 3
            //    SET @SUM = @VAL1 + @SUM	
            //    SET @SUM = @VAL2 + @SUM
            //    SET @Counter = @Counter + 2
            //END
            //SET @Counter = ROUND(@SUM + 5,-1)
            //SET @Return = @BARCODE + CONVERT(varchar,((@Counter - @SUM)))
            //RETURN @Return
            //END
            #endregion


        }

        private string wholeMessageLine()
        {
            return this.wholeMessageLine(false);
        }

        private string wholeMessageLine(bool sendCartontoZebra)
        {//THE FUNCTION laserDigitMessage totally base on this.wholeMessageLine. Later, if there is any thing change in this.wholeMessageLine, THE FUNCTION laserDigitMessage should be considered
            if (this.printerName == GlobalVariables.PrinterName.DigitInkjet)
            {
                return GlobalVariables.charESC + "u/1/ " + GlobalVariables.charESC + "/r/" + GlobalVariables.charESC + "u/1/" + this.ThirdMessageLine(1, true);

                return ".              . " + this.firstLineA2(true) + " " + this.thirdLine(true, 1) + " .              ."; //GlobalVariables.charESC + "u/1/" + 
            }
            else if (this.printerName == GlobalVariables.PrinterName.PackInkjet || this.printerName == GlobalVariables.PrinterName.CartonInkjet)
            {
                return GlobalVariables.charESC + "u/3/" + GlobalVariables.charESC + "/z/1/0/26/20/20/1/0/0/0/" + this.wholeBarcode(2) + "/" + GlobalVariables.charESC + "/z/0" + //2D Barcode
                       GlobalVariables.charESC + "u/1/" + this.FirstMessageLine(true) + "/" +  //First Line
                       GlobalVariables.charESC + "/r/" + GlobalVariables.charESC + "u/1/" + this.ThirdMessageLine(1, true) +   //Second Line
                       GlobalVariables.charESC + "/r/" + GlobalVariables.charESC + "u/1/" + this.SecondMessageLine(true);     //Third Line   


                return GlobalVariables.charESC + "u/3/" + GlobalVariables.charESC + "/z/1/0/26/20/20/1/0/0/0/" + this.wholeBarcode(2) + "/" + GlobalVariables.charESC + "/z/0" + //2D DATA MATRIX Barcode
                       GlobalVariables.charESC + "u/1/" + " " + this.firstLineA1(true) + this.firstLine(true) + "/" +
                       GlobalVariables.charESC + "/r/" + " " + GlobalVariables.charESC + "u/1/" + this.secondLine(true) + this.secondLineA2(true) +
                       GlobalVariables.charESC + "/r/" + " " + GlobalVariables.charESC + "u/1/" + this.thirdLine(true, 1);
            }
            else
                if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                {
                    string stringMessage = ""; string stringMessageBegin = ""; string stringMessageEnd = ""; string stringMessageText = "";

                    stringMessageBegin = stringMessageBegin + "^XA"; //[^XA - Indicates start of label format.]
                    stringMessageBegin = stringMessageBegin + "^LH60,20"; //[^LH - Sets label home position 80 dots to the right and 30 dots down from top edge of label.]


                    if (sendCartontoZebra)
                    {
                        stringMessageText = stringMessageText + "^FO500,70 ^AV ^FD" + this.firstLineA1(true) + this.firstLine(true, sendCartontoZebra) + "^FS";//[^FO0,330 - Set field origin 10 dots to the right and 330 dots down from the home position defined by the ^LH instruction.] [^AG - Select font “G.”] [^FD - Start of field data.] [ZEBRA - Actual field data.] [^FS - End of field data.]
                        stringMessageText = stringMessageText + "^FO500,171 ^AV ^FD" + this.secondLine(true) + this.secondLineA2(true) + "^FS";
                        stringMessageText = stringMessageText + "^FO500,270 ^AV ^FD" + this.thirdLine(true, 0, sendCartontoZebra) + "^FS";
                    }
                    else
                    {
                        stringMessageText = stringMessageText + "^FO785,10 ^AV ^FD" + this.firstLineA1(true) + "^FS";//[^FO0,330 - Set field origin 10 dots to the right and 330 dots down from the home position defined by the ^LH instruction.] [^AG - Select font “G.”] [^FD - Start of field data.] [ZEBRA - Actual field data.] [^FS - End of field data.]
                        stringMessageText = stringMessageText + "^FO785,68 ^AV ^FD" + this.firstLineA2(true) + "^FS";
                        stringMessageText = stringMessageText + "^FO785,131 ^AV ^FD" + this.secondLineA1(true) + "^FS";
                        stringMessageText = stringMessageText + "^FO785,194 ^AV ^FD" + this.secondLineA2(true) + "^FS";
                        stringMessageText = stringMessageText + "^FO785,257 ^AV ^FD" + this.thirdLineA1(true) + "^FS";
                        stringMessageText = stringMessageText + "^FO785,320 ^AV ^FD" + this.thirdLineA2(true, 0) + "^FS";
                    }

                    stringMessageEnd = stringMessageEnd + "^XZ"; //[^XZ - Indicates end of label format.]

                    if (this.OnPrinting)
                    {
                        stringMessage = stringMessage + stringMessageBegin;

                        if (sendCartontoZebra)
                            stringMessage = stringMessage + "^FO50,50  ^BXN,16,200  ^FD" + this.wholeBarcode(0, sendCartontoZebra) + "^FS";// [^FO0,10 - Set field origin 10 dots to the right and 10 dots down from the home position defined by the ^LH instruction.] [^BC - Select Code 128 bar code.] [^FD - Start of field data for the bar code.] [AAA001 - Actual field data.] [^FS - End of field data.]
                        else
                            stringMessage = stringMessage + "^FO0,20  ^BC,360,N  ^FD" + this.wholeBarcode(0) + "^FS";// [^FO0,10 - Set field origin 10 dots to the right and 10 dots down from the home position defined by the ^LH instruction.] [^BC - Select Code 128 bar code.] [^FD - Start of field data for the bar code.] [AAA001 - Actual field data.] [^FS - End of field data.]

                        stringMessage = stringMessage + stringMessageText;
                        stringMessage = stringMessage + stringMessageEnd;
                    }
                    else //TEST PAGE ONLY
                    {
                        stringMessage = stringMessage + stringMessageBegin;
                        stringMessage = stringMessage + "^FO0,30 ^AS ^FD" + "If you can read this, your printer is ready" + "^FS";
                        stringMessage = stringMessage + "^FO0,80 ^AS ^FD" + "**PLEASE PRESS THE START BUTTON TO BEGIN**" + "^FS";
                        stringMessage = stringMessage + stringMessageText;
                        stringMessage = stringMessage + stringMessageEnd;
                    }

                    return stringMessage;
                }
                else
                { //1D BARCODE: THIS IS JUST FOR BACKUP ONLY (CBPP OLD VERSION FOR CARTON)
                    return GlobalVariables.charESC + "u/2/" + GlobalVariables.charESC + "/q/6/" + this.ThirdMessageLine(1, false) + "/" + this.privateFillingData.BatchCartonNo.Substring(2) + GlobalVariables.charESC + "/q/0" +
                           GlobalVariables.charESC + "u/1/  " + this.FirstMessageLine(true) + "/ " + this.privateFillingData.Shelflife.ToString("00") + "/" + //First Line
                           GlobalVariables.charESC + "/r/  " + GlobalVariables.charESC + "u/1/" + this.ThirdMessageLine(1, true) + "/ " + this.FillingData.SettingDate.ToString("dd/MM/yy");
                }
        }

        private string laserDigitMessage(bool isSerialNumber)
        {//THE FUNCTION laserDigitMessage totally base on this.wholeMessageLine. Later, if there is any thing change in this.wholeMessageLine, THE FUNCTION laserDigitMessage should be considered
            if (isSerialNumber)
                return this.getNextNo();
            else
            {
                return this.privateFillingData.CommodityCode + this.privateFillingData.EntryMonthID.ToString("00") + this.privateFillingData.FillingLineCode;

                return this.firstLineA2(true) + " " + this.thirdLine(true, -1); //THIS IS AS THE SAME AS wholeMessageLine.GlobalVariables.PrinterName.DigitInkjet
            }
        }//NOTE: NEVER CHANGE THIS FUNCTION WITHOUT HAVE A LOOK AT this.wholeMessageLine

        #endregion Message Configuration


        #region Public Method

        private bool Connect()
        {
            try
            {
                if (!GlobalEnums.OnTestPrinter)
                {
                    this.MainStatus = "Bắt đầu kết nối ....";

                    if (this.printerName != GlobalVariables.PrinterName.PalletLabel)
                        this.ionetSocket.Connect();

                    if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                    {
                        this.ioserialPort.Connect();
                        //this.ioserialPort.WritetoSerial(this.wholeMessageLine());//FOR TEST AT DESIGN ONLY                        
                    }
                }
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
                if (!GlobalEnums.OnTestPrinter && !(GlobalEnums.CBPP && this.printerName == GlobalVariables.PrinterName.PalletLabel))
                {
                    this.ionetSocket.Disconnect();
                    this.ioserialPort.Disconnect();
                }

                this.setLED();
                this.MainStatus = "Đã ngắt kết nối ...";
                return true;
            }

            catch (Exception exception)
            {
                this.MainStatus = exception.Message; return false;
            }

        }


        private void storeMessage(string stringMessage)
        {
            string receivedFeedback = "";

            //S: Message Storage
            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/S/001/" + stringMessage + "/" + GlobalVariables.charEOT);
            if (this.waitforDomino(ref receivedFeedback, true)) Thread.Sleep(750); else throw new System.InvalidOperationException("Lỗi cài đặt bản tin 001: " + receivedFeedback);

            //P: Message To Head Assignment '//CHU Y QUAN TRONG: DUA MSG SO 1 LEN DAU IN (SAN SANG KHI IN, BOI VI KHI IN TA STORAGE MSG VAO VI TRI SO 1 MA KHONG QUAN TAM DEN VI TRI SO 2, 3,...)
            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/P/1/001/" + GlobalVariables.charEOT); //FOR AX SERIES: MUST CALL P: Message To Head Assignment FOR EVERY CALL S: Message Storage
            if (this.waitforDomino(ref receivedFeedback, true)) Thread.Sleep(1000); else throw new System.InvalidOperationException("Lỗi sẳn sàng in phun, bản tin 001: " + receivedFeedback);
        }


        /// <summary>
        /// NEVER waiforACK WHEN This.IsLaser
        /// </summary>
        /// <param name="receivedFeedback"></param>
        /// <param name="waitforACK"></param>
        /// <returns></returns>
        private bool waitforDomino(ref string receivedFeedback, bool waitforACK)
        {
            return waitforDomino(ref receivedFeedback, waitforACK, "", 0);
        }

        /// <summary>
        /// /// NEVER waitForACK WHEN This.IsLaser
        /// </summary>
        /// <param name="receivedFeedback"></param>
        /// <param name="waitforACK"></param>
        /// <param name="commandCode"></param>
        /// <param name="commandLength"></param>
        /// <returns></returns>
        private bool waitforDomino(ref string receivedFeedback, bool waitforACK, string commandCode, long commandLength)
        {
            try
            {
                receivedFeedback = this.ionetSocket.ReadoutStream();

                if (!this.isLaser && waitforACK)
                {
                    if (receivedFeedback.ElementAt(0) == GlobalVariables.charACK)
                        return true;
                    else
                    {
                        if (receivedFeedback.ElementAt(0) == GlobalVariables.charNACK && receivedFeedback.Length >= 4) lastNACKCode = receivedFeedback.Substring(1, 3); //[0]: NACK + [1][2][3]: 3 Digit --- Error Code
                        return false;
                    }
                }
                else if (commandLength == 0 || receivedFeedback.Length >= commandLength)
                {
                    if (this.isLaser)
                        return receivedFeedback.Contains(commandCode);
                    else//receivedFeedback(0): ESC;----receivedFeedback(1): COMMAND;----receivedFeedback(2->N): PARAMETER;----receivedFeedback(receivedFeedback.Length): EOT
                        if (receivedFeedback.ElementAt(0) == GlobalVariables.charESC && receivedFeedback.ElementAt(1) == commandCode.ElementAt(0) && receivedFeedback.ElementAt(receivedFeedback.Length - 1) == GlobalVariables.charEOT) return true; else return false;
                }
                else return false;
            }

            catch (Exception exception)
            {
                this.MainStatus = exception.Message; return false;
            }

        }



        private bool getRepackPrintedIndex(ref string receivedFeedback)
        {
            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/T/1/?/" + GlobalVariables.charEOT);//T: READ Product Counting
            this.waitforDomino(ref receivedFeedback, true);

            if (receivedFeedback.Length < 9)
                throw new System.InvalidOperationException("Lỗi đọc bộ đếm (len < 9): " + receivedFeedback);
            else
            {
                receivedFeedback = receivedFeedback.Substring(3, 10);//Response to ?: Esc/T/A/NNNNNNNNNN/Eot (Return counter value): Return 10 digit: NEW PROTOCOL: FILE WORD: [Connecting A-Series plus to the world.doc]

                int newProductCounting;
                if (int.TryParse(receivedFeedback, out  newProductCounting))
                {
                    if (newProductCounting > this.lastProductCounting) //this.lastProductCounting != newProductCounting
                    {
                        this.feedbackNextNo(CommonExpressions.IncrementSerialNo(this.getNextNo(), newProductCounting - this.lastProductCounting));
                        this.lastProductCounting = newProductCounting; return true;
                    }
                }
            }
            return false;
        }


        private bool sendtoBuffer()
        {
            string receivedFeedback = ""; //TOTAL LEN: 0065 (INCLUDE 3 Delimiter: ';') BARCODE: INDEX: 1, LEN: 29;         TEXT LINE 1: INDEX: 2, LEN: 11;         TEXT LINE 3: INDEX: 3, LEN: 11;         TEXT LINE 4: INDEX: 4, LEN: 11

            //int i = this.wholeBarcode(2).Length;
            //int j = this.firstLine(true, true).Length;
            //int k = this.secondLine(true, true).Length;
            //int t = this.thirdLine(true, 1, true).Length;

            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/O/E/" + "0065" + "/" + this.wholeBarcode(2) + ";" + this.FirstMessageLine(true) + ";" + this.ThirdMessageLine(1, true) + ";" + this.SecondMessageLine(true) + "/" + GlobalVariables.charEOT); Thread.Sleep(20);//OE (4Fh 45h): Streaming the data to the printer

            return this.waitforDomino(ref receivedFeedback, true);
        }



        private int nthCartontoZebra { get; set; }
        public void StartSendCartontoZebra(int nthCartontoZebra)
        {
            if (this.nthCartontoZebra == 0)
            {
                this.MainStatus = ""; this.MainStatus = "Đang in " + nthCartontoZebra.ToString("N0") + " carton";
                this.nthCartontoZebra = nthCartontoZebra;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void sendCartontoZebra()
        {
            if (this.FillingData.CartonViaPalletZebra && this.nthCartontoZebra > 0)
            {
                if (GlobalEnums.SendToZebra)
                {
                    CartonDTO cartonDTO = new CartonDTO() { Code = this.wholeBarcode(0, true) };
                    if (!this.FillingData.AutoCarton) this.ioserialPort.WritetoSerial(this.wholeMessageLine(true));
                    lock (this.FillingData.CartontoZebraQueue) { this.FillingData.CartontoZebraQueue.Enqueue(cartonDTO); }

                    this.feedbackNextNo(CommonExpressions.IncrementSerialNo(this.getNextNo(true)), "", true);
                    this.nthCartontoZebra--;
                }
            }
        }





        /// <summary>
        /// ONLY WHEN: Freshnew OR Reprint
        /// WHEN: Freshnew => Printing1: WAIT FOR PRINTING
        /// WHEN: Reprint => Reprinting1: WAIT FOR REPRINTING
        /// </summary>
        private void sendtoZebra()
        {
            if (this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Freshnew || this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Reprint)
            {//ONLY PRINT WHEN: PrintStatus.Freshnew: AUTO PRINT FOR EACH NEW CartonsetQueue, AND: WHEN = PrintStatus.Reprint: USER PRESS RE-PRINT BUTTON

                if (GlobalEnums.SendToZebra)
                {
                    this.ioserialPort.WritetoSerial(this.wholeMessageLine());
                    this.FillingData.CartonsetQueueZebraStatus = this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Freshnew ? GlobalVariables.ZebraStatus.Printing1 : GlobalVariables.ZebraStatus.Reprinting1; Thread.Sleep(88);
                }
            }
        }

        /// <summary>
        /// ONLY WHEN: Printing1 OR Reprinting1
        /// WHEN: Printing1 => Printing2 => Printing3 => IF STILL NOT ACK: Freshnew
        /// WHEN: Reprinting1 => Reprinting2 => Reprinting3 => IF STILL NOT ACK: Reprint
        /// 
        /// FINALLY: WHEN ACK: IF >= Printing1 => MANUAL INCREASE BY 1 THE NextNo FOR THE Freshnew: Format 7 digit, then cut 6 right digit: This will reset a 0 when reach the limit of 6 digit
        /// </summary>
        private void waitforZebra()
        {
            if (this.FillingData.CartonsetQueueZebraStatus >= GlobalVariables.ZebraStatus.Printing1 || this.FillingData.CartonsetQueueZebraStatus <= GlobalVariables.ZebraStatus.Reprinting1)
            {
                string stringReadFrom = "";
                if (true || this.ioserialPort.ReadoutSerial(true, ref stringReadFrom)) //NOW: THE ZEBRA USING IN THIS CHEVRON PROJECT DOES NOT SUPPORT: "Error Detection Protocol" => WE CAN NOT USING TRANSACTION TO GET RESPOND FROM ZEBRA PRINTER
                {
                    this.feedbackNextNo(this.FillingData.CartonsetQueueZebraStatus >= GlobalVariables.ZebraStatus.Printing1 ? CommonExpressions.IncrementSerialNo(this.getNextNo()) : this.getNextNo());
                    this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Printed;
                }
                else
                {
                    this.MainStatus = "Lỗi máy in zebra. Đang thử in lại ... ";
                    if (this.FillingData.CartonsetQueueZebraStatus >= GlobalVariables.ZebraStatus.Printing1) this.FillingData.CartonsetQueueZebraStatus = this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Printing1 ? GlobalVariables.ZebraStatus.Printing2 : (this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Printing2 ? GlobalVariables.ZebraStatus.Printing3 : GlobalVariables.ZebraStatus.Freshnew);
                    if (this.FillingData.CartonsetQueueZebraStatus <= GlobalVariables.ZebraStatus.Reprinting1) this.FillingData.CartonsetQueueZebraStatus = this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Reprinting1 ? GlobalVariables.ZebraStatus.Reprinting2 : (this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Reprinting2 ? GlobalVariables.ZebraStatus.Reprinting3 : GlobalVariables.ZebraStatus.Reprint);
                }
            }
        }



        #region STASTUS
        private bool lfStatusLED(ref string receivedFeedback)
        {//DISPLAY 3 LEDS STATUS
            try
            {
                if (this.isLaser)
                {//RESULT GETSTATUS <severity>: • 0=information • 1=warning • 2=temporary fault • 3=critical fault • 4=critical fault (needs to be reset by hardware) 
                    this.LedGreenOn = receivedFeedback.ElementAt(17).ToString() == "0" || receivedFeedback.ElementAt(17).ToString() == "1";
                    this.LedAmberOn = receivedFeedback.ElementAt(17).ToString() == "1" || receivedFeedback.ElementAt(17).ToString() == "2";
                    this.LedRedOn = receivedFeedback.ElementAt(17).ToString() == "3" || receivedFeedback.ElementAt(17).ToString() == "4";
                }
                else
                { //DATE 26-JUL-2017: LedGreenOn = LedGreenOn (ORIGINAL LedGreenOn) || LedAmberOn. THIS MEANS: LedGreenOn OR LedAmberOn: THE PRINTER IS READY FOR PRINTING!!!
                    this.LedGreenOn = ((int)receivedFeedback.ElementAt(7) & int.Parse("1")) == 1 || ((int)receivedFeedback.ElementAt(7) & int.Parse("2")) == 2;
                    this.LedAmberOn = ((int)receivedFeedback.ElementAt(7) & int.Parse("2")) == 2;
                    this.LedRedOn = ((int)receivedFeedback.ElementAt(7) & int.Parse("3")) == 3;
                }
                this.NotifyPropertyChanged("LedStatus");

                return true;
            }

            catch (Exception exception)
            {
                this.MainStatus = exception.Message; return false;
            }

        }


        private string lStatusHHMM;
        private bool lfStatusHistory(ref string receivedFeedback)
        {
            try
            {
                if (lStatusHHMM != "" + receivedFeedback.ElementAt(7) + receivedFeedback.ElementAt(8) + receivedFeedback.ElementAt(9) + receivedFeedback.ElementAt(10))
                {
                    lStatusHHMM = "" + receivedFeedback.ElementAt(7) + receivedFeedback.ElementAt(8) + receivedFeedback.ElementAt(9) + receivedFeedback.ElementAt(10);
                    this.MainStatus = "" + receivedFeedback.ElementAt(3) + receivedFeedback.ElementAt(4) + receivedFeedback.ElementAt(5);//Get the status TEXT & Maybe: Add to database
                }
                return true;
            }
            catch (Exception exception)
            {
                this.MainStatus = exception.Message; return false;
            }
        }


        private bool lfStatusAlert(ref string receivedFeedback)
        {
            return true;
        }

        //Private Function lfStatusAlert(ByRef lInReceive() As Byte) As Boolean
        //    Static lAlertArray() As Byte '//lAlertArray: DE LUU LAI Alert TRUOC DO, KHI Alert THAY DOI => UPDATE TO DATABASE
        //    Dim lAlertNew As Boolean, i As Long

        //On Error GoTo ARRAY_INIT '//KIEM TRA, TRONG T/H CHU KHOI TAO CHO lAlertArray THI REDIM lAlertArray (0)
        //    If LBound(lAlertArray()) >= 0 Then GoTo ARRAY_OK
        //ARRAY_INIT:
        //    ReDim lAlertArray(0)

        //ARRAY_OK:
        //On Error GoTo ERR_HANDLER

        //    lAlertNew = UBound(lAlertArray) <> UBound(lInReceive)
        //    If Not lAlertNew Then
        //        For i = LBound(lInReceive) To UBound(lInReceive)
        //            If lInReceive(i) <> lAlertArray(i) Then lAlertNew = True: Exit For
        //        Next i
        //    End If
        //    If lAlertNew Then
        //        'DISPLAY NEW ALERT
        //        'SAVE NEW ALERT
        //        'Debug.Print "ALERT - " + Chr(lInReceive(5)) + Chr(lInReceive(6)) + Chr(lInReceive(7))
        //    End If

        //    lfStatusAlert = True
        //ERR_RESUME:
        //    Exit Function
        //ERR_HANDLER:
        //    Call psShowError: lfStatusAlert = False: GoTo ERR_RESUME
        //End Function

        #endregion STASTUS





        #endregion Public Method


        #region Public Thread

        public void ThreadRoutine()
        {
            this.privateFillingData = this.FillingData.ShallowClone(); //WE NEED TO CLONE FillingData, BECAUSE: IN THIS CONTROLLER: WE HAVE TO UPDATE THE NEW PRINTED BARCODE NUMBER TO FillingData, WHICH IS CREATED IN ANOTHER THREAD (FillingData IS CREATED IN VIEW: SmartCoding). SO THAT: WE CAN NOT UPDATE FillingData DIRECTLY, INSTEAD: WE REAISE EVENT ProertyChanged => THEN: WE CATCH THE EVENT IN SmartCoding VIEW AND UPDATE BACK TO THE ORIGINAL FillingData, BECAUSE: THE ORIGINAL FillingData IS CREATED AND BINDED IN THE VIEW: SmartCoding

            string receivedFeedback = ""; bool printerReady = false; bool readytoPrint = false; bool headEnable = false;
            this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Freshnew; //JUST ADDED 06-08-2018 [FOR IMPORT LINE]

            this.LoopRoutine = true; this.StopPrint();


            //if (GlobalEnums.OnTestPrinter && this.printerName != GlobalVariables.PrinterName.DigitInkjet) this.feedbackNextNo(CommonExpressions.IncrementSerialNo(this.getNextNo()));

            //This command line is specific to: PalletLabel ON FillingLine.Drum || CartonInkjet ON FillingLine.Pail (Just here only for this specific)
            //(!GlobalEnums.OnTestPrinter && GlobalEnums.CBPP && this.printerName == GlobalVariables.PrinterName.PalletLabel) || 
            if ((this.FillingData.FillingLineID == GlobalVariables.FillingLine.Drum && !(this.printerName == GlobalVariables.PrinterName.PalletLabel || (GlobalEnums.DrumWithDigit && this.printerName == GlobalVariables.PrinterName.DigitInkjet)))
                || (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Pail && this.printerName == GlobalVariables.PrinterName.PackInkjet)
                || (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Medium4L && (this.printerName == GlobalVariables.PrinterName.DigitInkjet || this.printerName == GlobalVariables.PrinterName.PackInkjet))
                || (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Import && (this.printerName == GlobalVariables.PrinterName.DigitInkjet || this.printerName == GlobalVariables.PrinterName.PackInkjet || this.printerName == GlobalVariables.PrinterName.CartonInkjet))
                || (this.printerName == GlobalVariables.PrinterName.DigitInkjet && GlobalEnums.OnTestDigit)) { this.setLED(true, this.LedAmberOn, this.LedRedOn); return; } //DO NOTHING


            try
            {
                if (GlobalEnums.OnTestPrinter || (GlobalEnums.CBPP && this.printerName == GlobalVariables.PrinterName.PalletLabel))
                {
                    this.setLED(true, this.LedAmberOn, this.LedRedOn);
                    this.NextAutoBarcodeCode = "";
                }
                else
                {
                    if (!this.Connect()) throw new System.InvalidOperationException("Lỗi kết nối máy in");

                    if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                    { //SHOULD HAVE COMMAND TO CHECK ZEBRA PRINTER EXISTING AND WORKING WELL. !!!!BUT: NOW: THE ZEBRA USING IN THIS CHEVRON PROJECT DOES NOT SUPPORT: "Error Detection Protocol" => WE CAN NOT USING TRANSACTION TO GET RESPOND FROM ZEBRA PRINTER 
                        if (!GlobalEnums.OnTestZebra) this.ioserialPort.WritetoSerial(this.wholeMessageLine()); //TRY TO PRINT THE TEST PAGE -> TO VERIFY THAT: THE ZEBRA IS OK
                        this.setLED(true, this.LedAmberOn, this.LedRedOn);
                    }
                    else
                    {//USING DOMINO PRINTER
                        #region INITIALISATION PRINTER
                        do  //INITIALISATION COMMAND
                        {
                            if (this.isLaser)
                            {
                                this.ionetSocket.WritetoStream("GETVERSION"); //Obtains the alphanumeric identifier of the printer
                                if (this.waitforDomino(ref receivedFeedback, false, "RESULT GETVERSION", "RESULT GETVERSION".Length)) printerReady = true; //Printer Identity OK"
                            }
                            else
                            {
                                this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/A/?/" + GlobalVariables.charEOT);   //A: Printer Identity
                                if (this.waitforDomino(ref receivedFeedback, false, "A", 14)) printerReady = true; //A: Printer Identity OK"
                            }


                            if (printerReady)
                            {
                                do //CHECK PRINTER READY TO PRINT
                                {
                                    if (this.isLaser)
                                        this.ionetSocket.WritetoStream("GETSTATUS"); //Determines the current status of the controller
                                    else
                                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/O/1/?/" + GlobalVariables.charEOT);  //O/1: Current status


                                    if ((this.isLaser && this.waitforDomino(ref receivedFeedback, false, "RESULT GETSTATUS", "RESULT GETSTATUS".Length)) || (!this.isLaser && this.waitforDomino(ref receivedFeedback, false, "O", 9)))
                                    {
                                        lfStatusLED(ref receivedFeedback);
                                        readytoPrint = this.LedGreenOn || this.LedAmberOn; this.LedGreenOn = false; //After Set LED, If LedGreenOn => ReadyToPrint
                                    }


                                    if (!readytoPrint)
                                    {
                                        if (this.isLaser)
                                        {
                                            this.MainStatus = "Máy in laser chưa sẳn sàng in, vui lòng kiểm tra lại.";
                                            Thread.Sleep(20000);
                                        }
                                        else
                                        {
                                            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/O/S/1/" + GlobalVariables.charEOT); //O/S/1: Turn on ink-jet
                                            if (this.waitforDomino(ref receivedFeedback, true))
                                            {
                                                this.MainStatus = "Đang khởi động máy in, vui lòng chờ trong ít phút.";
                                                Thread.Sleep(50000);
                                            }
                                            else throw new System.InvalidOperationException("Lỗi không thể khởi động máy in: " + receivedFeedback);
                                        }
                                    }
                                    else //readytoPrint: OK
                                    {
                                        if (this.isLaser)
                                            this.ionetSocket.WritetoStream("GETMARKMODE"); //Determines the current state of the marking engine on the laser controller
                                        else
                                            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/Q/1/?/" + GlobalVariables.charEOT);    //Q: HEAD ENABLE: ENABLE


                                        if ((this.isLaser && this.waitforDomino(ref receivedFeedback, false, "RESULT GETMARKMODE", "RESULT GETMARKMODE".Length)) || (!this.isLaser && this.waitforDomino(ref receivedFeedback, false, "Q", 5)))
                                        {
                                            if ((this.isLaser && receivedFeedback.ElementAt(19).ToString() == "1") || (!this.isLaser && receivedFeedback.ElementAt(3).ToString() == "Y"))
                                                headEnable = true;
                                            else
                                            {
                                                if (this.isLaser)
                                                    this.ionetSocket.WritetoStream("MARK START");
                                                else
                                                    this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/Q/1/Y/" + GlobalVariables.charEOT);


                                                if ((this.isLaser && this.waitforDomino(ref receivedFeedback, false, "OK", "OK".Length)) || (!this.isLaser && this.waitforDomino(ref receivedFeedback, true)))
                                                {
                                                    this.MainStatus = this.isLaser ? "Đang bật chế độ in" : "Đang mở in phun" + ", vui lòng chờ trong ít phút.";
                                                    Thread.Sleep(10000);
                                                }
                                                else throw new System.InvalidOperationException("Lỗi mở in phun: " + receivedFeedback);
                                            }
                                        }
                                    }

                                } while (this.LoopRoutine && (!readytoPrint || !headEnable));
                            }
                            else
                            {
                                this.MainStatus = "Không thể kết nối máy in. Đang tự động thử kết nối lại ... Nhấn Disconnect để thoát.";
                            }
                        } while (this.LoopRoutine && !printerReady && !readytoPrint && !headEnable);

                        #endregion INITIALISATION COMMAND


                        #region GENERAL SETUP (NOT LASER ONLY)
                        if (!this.isLaser)
                        {
                            //C: Set Clock
                            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/C/" + DateTime.Now.ToString("yyyy/MM/dd/00/hh/mm/ss") + "/" + GlobalVariables.charEOT);     //C: Set Clock
                            if (!this.waitforDomino(ref receivedFeedback, true)) throw new System.InvalidOperationException("Lỗi cài đặt ngày giờ máy in phun: " + receivedFeedback);

                            //T: Reset Product Counting
                            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/T/1/0/" + GlobalVariables.charEOT);
                            if (!this.waitforDomino(ref receivedFeedback, true)) throw new System.InvalidOperationException("Lỗi cài đặt bộ đếm số lần in phun: " + receivedFeedback);
                        }
                        #endregion GENERAL SETUP


                        #region Status (NOT LASER ONLY)
                        //SET STATUS
                        if (!this.isLaser)
                        {
                            this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/0/N/0/" + GlobalVariables.charEOT);     //0: Status Report Mode: OFF: NO ERROR REPORTING
                            if (!this.waitforDomino(ref receivedFeedback, true)) throw new System.InvalidOperationException("NMVN: Can not set status report mode: " + receivedFeedback);

                            //co gang viet cho nay cho hay hay
                            //this.WriteToStream( GlobalVariables.charESC + "/1/C/?/" + GlobalVariables.charEOT) ;   //1: REQUEST CURRENT STATUS
                            //if (!this.ReadFromStream(ref receivedFeedback, false, "1", 12) )
                            //    throw new System.InvalidOperationException("NMVN: Can not request current status: " + receivedFeedback);
                            //else
                            ////        Debug.Print "STATUS " + Chr(lInReceive(3)) + Chr(lInReceive(4)) + Chr(lInReceive(5))
                            ////'        If Not ((lInReceive(3) = Asc("0") Or lInReceive(3) = Asc("1")) And lInReceive(4) = Asc("0") And lInReceive(5) = Asc("0")) Then GoTo ERR_HANDLER   'NOT (READY OR WARNING)
                            ////    End If
                        }
                        #endregion Status
                    }

                }
                while (this.LoopRoutine)    //MAIN LOOP. STOP WHEN PRESS DISCONNECT
                {
                    if (GlobalEnums.OnTestPrinter || (GlobalEnums.CBPP && this.printerName == GlobalVariables.PrinterName.PalletLabel))
                    {
                        if (this.OnPrinting)
                        {
                            if ((this.NextAutoBarcodeCode == "" || this.NextAutoBarcodeCode == null) && (this.printerName != GlobalVariables.PrinterName.CartonInkjet || int.Parse(this.getNextNo()) <= int.Parse(this.FillingData.FinalCartonNo)))
                            {
                                this.NextAutoBarcodeCode = this.wholeBarcode(this.printerName != GlobalVariables.PrinterName.PalletLabel ? 2 : 0);
                                this.feedbackNextNo(CommonExpressions.IncrementSerialNo(this.getNextNo()));
                            }
                            this.MainStatus = "Đang chạy máy in ảo ...";
                        }
                        else
                            this.MainStatus = "Đang kết nối với máy in ảo.";
                    }
                    else
                    {
                        if (!this.OnPrinting)
                        {
                            if (GlobalEnums.DrumWithDigit && !this.FillingData.HasCarton) this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Printed; //INIT BY PRINTED!!! WILL BE SET FOR NEW PRINT WHEN DIGIT DOMINO PRINTER DOING PRINTING

                            #region Reset Message: Clear message: printerName != PalletLabel

                            if (this.printerName != GlobalVariables.PrinterName.PalletLabel && this.resetMessage)
                            {
                                this.MainStatus = "Vui lòng chờ ... ";

                                if (this.isLaser)
                                {
                                    this.ionetSocket.WritetoStream("MARK STOP");
                                    if (this.waitforDomino(ref receivedFeedback, false, "OK", "OK".Length)) Thread.Sleep(7000); else throw new System.InvalidOperationException("Can not disables printing ... : " + receivedFeedback);
                                }
                                else
                                {
                                    this.storeMessage("  ");
                                    this.feedbackSentNo(this.getNextNo());
                                }

                                if (this.isLaser)
                                    this.ionetSocket.WritetoStream("LOADPROJECT store: SLASHSYMBOL Empty2018");
                                else
                                    this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/I/1/ /" + GlobalVariables.charEOT); //SET OF: Print Acknowledgement Flags I

                                if ((this.isLaser && this.waitforDomino(ref receivedFeedback, false, "OK", "OK".Length)) || (!this.isLaser && this.waitforDomino(ref receivedFeedback, true))) Thread.Sleep(250); else throw new System.InvalidOperationException("Can not set off printing acknowledge/ Load Empty2018 project: " + receivedFeedback);

                                this.resetMessage = false; //Setup first message: Only one times      
                                this.MainStatus = "Đang kết nối với máy in.";
                            }
                            #endregion Reset Message: Clear message: this.printerName != GlobalVariables.PrinterName.PalletLabel
                        }

                        else //this.OnPrinting
                        {
                            #region Reset Message: printerName != PalletLabel

                            if (this.printerName != GlobalVariables.PrinterName.PalletLabel && this.resetMessage)
                            {
                                #region SETUP MESSAGE
                                this.MainStatus = "Vui lòng chờ ...";

                                if (this.isLaser && this.printerName == GlobalVariables.PrinterName.DigitInkjet) //stringWriteTo = " SETVARIABLES \"Text01\" \"10081\"\r\n"
                                {//BEGINTRANS [ENTER] OK   SETTEXT "Text 1" "Domino AG" [ENTER]   OK   SETTEXT "Barcode 1" "Sator Laser GmbH" [ENTER]   OK EXECTRANS [ENTER] OK MSG 1 
                                    //this.WriteToStream("BEGINTRANS");
                                    //if (this.ReadFromStream(ref receivedFeedback, false, "OK", "OK".Length)) Thread.Sleep(20); else throw new System.InvalidOperationException("NMVN: Can not set message: " + receivedFeedback);

                                    this.ionetSocket.WritetoStream("LOADPROJECT store: SLASHSYMBOL " + (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Smallpack ? "Digit1L" : (this.FillingData.FillingLineID == GlobalVariables.FillingLine.Pail ? "Pail2018" : "Pail2018")));
                                    if (this.waitforDomino(ref receivedFeedback, false, "OK", "OK".Length)) Thread.Sleep(500); else throw new System.InvalidOperationException("NMVN: Can not load message: " + receivedFeedback);

                                    this.ionetSocket.WritetoStream("SETVARIABLES \"MonthCodeAndLine\" \"" + this.laserDigitMessage(false) + "\""); //AT CHEVRON: USING SETTEXT, INSTAED OF SETVARIABLES
                                    if (this.waitforDomino(ref receivedFeedback, false, "OK", "OK".Length)) Thread.Sleep(20); else throw new System.InvalidOperationException("NMVN: Can not set message code: " + receivedFeedback);

                                    this.ionetSocket.WritetoStream("SETCOUNTERVALUE Serialnumber01 " + this.laserDigitMessage(true));
                                    if (this.waitforDomino(ref receivedFeedback, false, "OK", "OK".Length)) Thread.Sleep(20); else throw new System.InvalidOperationException("NMVN: Can not set message counter: " + receivedFeedback);

                                    this.ionetSocket.WritetoStream("MARK START");
                                    if (this.waitforDomino(ref receivedFeedback, false, "OK", "OK".Length)) Thread.Sleep(7000); else throw new System.InvalidOperationException("NMVN: Can not enables marking ... : " + receivedFeedback);

                                    //this.WriteToStream("EXECTRANS");
                                    //if (this.ReadFromStream(ref receivedFeedback, false, "OK", "OK".Length)) Thread.Sleep(20); else throw new System.InvalidOperationException("NMVN: Can not set message: " + receivedFeedback);
                                }
                                else
                                {
                                    if (GlobalEnums.Buffered)
                                    {
                                        this.lastProductCounting = 0;//VARIBLE PLAY A RULE OF POINTER TO THE BUFFERS
                                        this.feedbackSentNo(this.getNextNo());

                                        //O/E:----- //Esc/O/E/0000/0/Eot: EMPTY DATA QUEUE
                                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/O/E/0000/0/" + GlobalVariables.charEOT);
                                        if (this.waitforDomino(ref receivedFeedback, true)) Thread.Sleep(30); else throw new System.InvalidOperationException("Lỗi xóa buffer: " + receivedFeedback);

                                        //P: Message To Head Assignment 
                                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/P/1/002/" + GlobalVariables.charEOT); //USING UI TO SETUP MSG 002: USING UPDATABLE FIELDS FOR BOTH TEXT + BARCODE
                                        if (this.waitforDomino(ref receivedFeedback, true)) Thread.Sleep(1000); else throw new System.InvalidOperationException("Lỗi bản tin 002: " + receivedFeedback);

                                        //T: Reset Product Counting
                                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/T/1/0/" + GlobalVariables.charEOT); //THIS VERY IMPORTANT TO GET TO KNOW HOW MANY DATA ENTRY IS PRINTED
                                        if (!this.waitforDomino(ref receivedFeedback, true)) throw new System.InvalidOperationException("Lỗi cài đặt bộ đếm số lần in phun: " + receivedFeedback);
                                    }
                                    else
                                    {
                                        this.storeMessage(this.wholeMessageLine()); //SHOULD Update serial number: - Note: Some DOMINO firmware version does not need to update serial number. Just set startup serial number only when insert serial number. BUT: FOR SURE, It will be updated FOR ALL

                                        //    U: UPDATE SERIAL NUMBER - Counter 1
                                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/U/001/1/" + this.getNextNo() + "/" + GlobalVariables.charEOT);
                                        if (this.waitforDomino(ref receivedFeedback, true)) Thread.Sleep(1000); else throw new System.InvalidOperationException("Lỗi không thể cài đặt số thứ tự sản phẩm: " + receivedFeedback);

                                        //    U: UPDATE SERIAL NUMBER - Counter 2
                                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/U/001/2/" + this.getNextNo() + "/" + GlobalVariables.charEOT);
                                        if (this.waitforDomino(ref receivedFeedback, true)) Thread.Sleep(1000); else throw new System.InvalidOperationException("Lỗi không thể cài đặt số thứ tự sản phẩm: " + receivedFeedback);
                                    }
                                }
                                #endregion SETUP MESSAGE

                                this.MainStatus = "Đang in ...";
                                if (this.OnPrinting) this.resetMessage = false; //Setup first message: Only one times   //THE CONDITIONAL CHECK: if (this.OnPrinting) ADDED ON 26/MAY/2018: ONLY SET this.resetMessage TO false WHEN STILL this.OnPrinting (THE USE CASE: WHEN CAMERA FAIL TO START => IT WILL CALL this.StopPrint();  RIGHT AFTER CLICK START BUTTON ===> BUT THE THREAD OF PRINTER WILL SET this.resetMessage = false; WITHIN CURRENT LOOP (AT THE END OF THE FIRST LOOP WHEN CLICK START BUTTON) ====> SO: CHECK STILL this.OnPrinting BEFORE SET this.resetMessage TO false)
                            }

                            #endregion Reset Message: this.printerName != GlobalVariables.PrinterName.PalletLabel


                            #region Read counter: printerName == DigitInkjet || printerName == PackInkjet || printerName == CartonInkjet
                            if (this.printerName == GlobalVariables.PrinterName.DigitInkjet || this.printerName == GlobalVariables.PrinterName.PackInkjet || this.printerName == GlobalVariables.PrinterName.CartonInkjet)
                            {
                                if (this.isLaser)
                                {
                                    this.ionetSocket.WritetoStream("GETSTATUS"); //Determines the current status of the controller
                                    if (this.waitforDomino(ref receivedFeedback, false, "RESULT GETSTATUS", "RESULT GETSTATUS".Length))
                                    {
                                        lfStatusLED(ref receivedFeedback);
                                        if (!this.LedGreenOn && !this.LedAmberOn) throw new System.InvalidOperationException("Connection fail! Please check your printer.");
                                    }
                                    else
                                        throw new System.InvalidOperationException("Connection fail! Please check your printer.");
                                }
                                else
                                    if (!GlobalEnums.Buffered)
                                    {
                                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/U/001/1/?/" + GlobalVariables.charEOT);//    U: Read Counter 1 (ONLY COUNTER 1---COUNTER 2: THE SAME COUNTER 1: Principlely)
                                        if (this.waitforDomino(ref receivedFeedback, false, "U", 13))
                                            this.feedbackNextNo("", receivedFeedback);
                                    }

                                if (!GlobalEnums.Buffered) Thread.Sleep(800);
                            }
                            #endregion Read counter


                            #region Setup for every message: printerName == PalletLabel
                            if (this.printerName == GlobalVariables.PrinterName.PalletLabel)
                            {
                                //if (!this.FillingData.HasCarton && this.FillingData.CartonsetQueueZebraStatus == GlobalVariables.ZebraStatus.Printed) 
                                //    this.FillingData.CartonsetQueueZebraStatus = GlobalVariables.ZebraStatus.Freshnew;

                                if (this.FillingData.CartonViaPalletZebra && this.FillingData.HasCarton)
                                {
                                    this.sendCartontoZebra();
                                    Thread.Sleep(800);
                                }

                                if (this.FillingData.CartonsetQueueCount > 0 || !this.FillingData.HasCarton)
                                {
                                    this.sendtoZebra();
                                    this.waitforZebra();
                                }

                            }

                            if (!this.isLaser && GlobalEnums.Buffered && (this.printerName == GlobalVariables.PrinterName.DigitInkjet || this.printerName == GlobalVariables.PrinterName.PackInkjet || this.printerName == GlobalVariables.PrinterName.CartonInkjet))
                            {//SentNo: MEANS: HAS BEEN SENT ALREADY
                                if (int.Parse(this.getSentNo()) - int.Parse(this.getNextNo()) < 25) //SEND DOUBLE TIME FOR THE LAST
                                { if (this.sendtoBuffer()) this.feedbackSentNo(CommonExpressions.IncrementSerialNo(this.getSentNo(), 1)); } //25: MEAN: WE USE MAXIMUN 25 BUFFER ENTRY FOR CACHED DATA (THE MAX CAPACITY OF BUFFER ARE 32 ENTRY)
                                else
                                    this.getRepackPrintedIndex(ref receivedFeedback);
                            }
                            #endregion setup for every message: printerName == PalletLabel
                        }


                        if (!this.OnPrinting)
                        {
                            #region Get current status: ONLY printerName != PalletLabel
                            if (this.printerName != GlobalVariables.PrinterName.PalletLabel)
                            {
                                if (this.isLaser)
                                    this.ionetSocket.WritetoStream("GETSTATUS"); //Determines the current status of the controller
                                else
                                    this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/O/1/?/" + GlobalVariables.charEOT);  //O/1: Current status

                                if ((this.isLaser && this.waitforDomino(ref receivedFeedback, false, "RESULT GETSTATUS", "RESULT GETSTATUS".Length)) || (!this.isLaser && this.waitforDomino(ref receivedFeedback, false, "O", 9)))
                                {
                                    lfStatusLED(ref receivedFeedback);
                                    if (!this.LedGreenOn && !this.LedAmberOn) throw new System.InvalidOperationException("Connection fail! Please check your printer.");
                                }


                                ////'//STATUS ONLY.BEGIN
                                //this.WriteToStream(GlobalVariables.charESC + "/1/H/?/" + GlobalVariables.charEOT);      //H: Request History Status
                                //if (this.ReadFromStream(ref receivedFeedback, false, "1", 12)) this.lfStatusHistory(ref receivedFeedback);

                                //this.WriteToStream(GlobalVariables.charESC + "/O/1/?/" + GlobalVariables.charEOT);      //O/1: Get Current LED Status
                                //if (this.ReadFromStream(ref receivedFeedback, false, "O", 9)) this.lfStatusLED(ref receivedFeedback);

                                //this.WriteToStream(GlobalVariables.charESC + "/O/2/?/" + GlobalVariables.charEOT);      //O/2: Get Current Alert
                                //if (this.ReadFromStream(ref receivedFeedback, false, "O", 0)) this.lfStatusAlert(ref receivedFeedback);
                                ////'//STATUS ONLY.END
                            }
                            #endregion Get current status
                            Thread.Sleep(110);
                        }

                    }
                } //End while this.LoopRoutine

                #region ON EXIT LOOP
                if (!GlobalEnums.OnTestPrinter && this.printerName != GlobalVariables.PrinterName.PalletLabel)
                {
                    if (this.isLaser)
                        this.ionetSocket.WritetoStream("GETSTATUS"); //Determines the current status of the controller
                    else
                        this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/O/1/?/" + GlobalVariables.charEOT);  //O/1: Current status

                    if ((this.isLaser && this.waitforDomino(ref receivedFeedback, false, "RESULT GETSTATUS", "RESULT GETSTATUS".Length)) || (!this.isLaser && this.waitforDomino(ref receivedFeedback, false, "O", 9)))
                        lfStatusLED(ref receivedFeedback);
                }
                #endregion ON EXIT LOOP
            }
            catch (Exception exception)
            {
                this.LoopRoutine = false;
                this.MainStatus = exception.Message;

                this.setLED(true, this.LedAmberOn, true);
            }
            finally
            {
                this.Disconnect();
            }

        }

        #endregion Public Thread

    }
}
