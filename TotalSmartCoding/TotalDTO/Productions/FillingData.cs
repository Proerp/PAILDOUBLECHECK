using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalModel.Helpers;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDTO.Productions
{
    public class FillingData : NotifyPropertyChangeObject
    {

        public int NoSubQueue { get { return !this.HasPack || (this.PackPerCarton / 4) == 0 ? 1 : (this.PackPerCarton / 4); } } //GlobalVariables.NoSubQueue()
        public int ItemPerSubQueue { get { return GlobalVariables.NoItemDiverter(); } }
        public bool RepeatSubQueueIndex { get { return GlobalVariables.RepeatedSubQueueIndex(); } }



        public int CartonsetQueueCount { get; set; }
        public GlobalVariables.ZebraStatus CartonsetQueueZebraStatus { get; set; } //AT INITIALIZE, THIS = 0 (RIGHT AFTER CartonsetQueue IS SET). AFTER PRINT: THIS = 1. WHEN USER PRESS RE-PRINT => THIS = -1

        public BarcodeQueue<CartonDTO> CartontoZebraQueue;


        public string NextAutoPackCode { get; set; }
        public string NextAutoCartonCode { get; set; }
        public string NextAutoPalletCode { get; set; }


        private int commodityID;
        private string commodityCode;
        private string commodityAPICode;
        private string commodityOfficialCode;
        private string commodityName;
        private decimal volume;
        private decimal packageVolume;

        private int shelflife;
        private bool isPailLabel;

        private string batchCode;
        private DateTime settingDate;

        private string nextDigitNo;
        private string nextPackNo;
        private string nextCartonNo;
        private string nextPalletNo;

        private string batchDigitNo;
        private string batchPackNo;
        private string batchCartonNo;
        private string batchPalletNo;

        private string sentDigitNo;
        private string sentPackNo;
        private string sentCartonNo;
        private string sentPalletNo;

        private string finalCartonNo;

        private string remarks;


        #region Contructor

        public FillingData()
        {
            this.settingDate = DateTime.Now;

            this.CartontoZebraQueue = new BarcodeQueue<CartonDTO>(this.HasCartonLabel);
        }


        public FillingData ShallowClone()
        {
            return (FillingData)this.MemberwiseClone();
        }

        #endregion Contructor


        #region Public Properties


        public GlobalVariables.FillingLine FillingLineID { get { return GlobalVariables.FillingLineID; } }
        public string FillingLineCode { get { return GlobalVariables.FillingLineCode; } }
        public string FillingLineName { get { return GlobalVariables.FillingLineName; } }

        public bool HasPack { get { return this.FillingLineID == GlobalVariables.FillingLine.Smallpack; } }
        public bool HasCarton { get { return this.FillingLineID == GlobalVariables.FillingLine.Smallpack || this.FillingLineID == GlobalVariables.FillingLine.Pail || this.FillingLineID == GlobalVariables.FillingLine.Medium4L || (this.FillingLineID == GlobalVariables.FillingLine.Import && this.CartonPerPallet > 1); } } //TESTDOUBLECHECK { get { return false; } }
        public bool HasLabel { get { return this.HasPackLabel || this.HasCartonLabel; } } //TESTDOUBLECHECK { get { return false; } }
        public bool HasPallet { get { return true; } }
        public bool HasCheck { get { return true; } }

        public bool HasPackLabel { get { return true; } }
        public bool HasCartonLabel { get { return this.HasCarton && true; } }
        public bool IgnoreNoreadCarton { get { return !this.HasPack & false; } } //THIS PROPERTY WILL DEFINE WHEN A Noread CARTON IS IGNORED. NOW, FOR THE PAIL: NEVER IGONRE, BECAUSE: EACH PAIL HAS A TSA LABEL ==> MUST PUT Noread INTO cartonPendingQueue

        public bool CartonViaPalletZebra { get { return this.FillingLineID == GlobalVariables.FillingLine.Import; } }
        public bool PalletCameraOnly { get { return this.FillingLineID == GlobalVariables.FillingLine.Import; } }

        public int CommodityID
        {
            get { return this.commodityID; }
            set
            {
                if (this.commodityID != value) { ApplyPropertyChange<FillingData, int>(ref this.commodityID, o => o.CommodityID, value); }
            }
        }


        public string CommodityCode
        {
            get { return this.commodityCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityCode, o => o.CommodityCode, value); }
        }

        public string CommodityAPICode
        {
            get { return this.commodityAPICode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityAPICode, o => o.CommodityAPICode, value); }
        }

        public string CommodityOfficialCode
        {
            get { return this.commodityOfficialCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityOfficialCode, o => o.CommodityOfficialCode, value); }
        }

        public string CommodityName
        {
            get { return this.commodityName; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityName, o => o.CommodityName, value); }
        }

        public decimal Volume
        {
            get { return this.volume; }
            set { ApplyPropertyChange<FillingData, decimal>(ref this.volume, o => o.Volume, value); }
        }

        public decimal PackageVolume
        {
            get { return this.packageVolume; }
            set { ApplyPropertyChange<FillingData, decimal>(ref this.packageVolume, o => o.PackageVolume, value); }
        }

        public int Shelflife
        {
            get { return this.shelflife; }
            set { ApplyPropertyChange<FillingData, int>(ref this.shelflife, o => o.Shelflife, value); }
        }

        public bool IsPailLabel
        {
            get { return this.isPailLabel; }
            set { ApplyPropertyChange<FillingData, bool>(ref this.isPailLabel, o => o.IsPailLabel, value); }
        }



        private bool autoBarcode;
        public bool AutoBarcode
        {
            get { return this.autoBarcode; }
            set
            {
                ApplyPropertyChange<FillingData, bool>(ref this.autoBarcode, o => o.AutoBarcode, value);
                GlobalEnums.OnTestPrinter = this.AutoBarcode; GlobalEnums.OnTestScanner = this.AutoBarcode;
            }
        }

        private bool autoCarton;
        public bool AutoCarton
        {
            get { return this.autoCarton; }
            set { ApplyPropertyChange<FillingData, bool>(ref this.autoCarton, o => o.AutoCarton, value); }
        }

        //-------------------------
        private int batchID;
        public int BatchID
        {
            get { return this.batchID; }
            set { ApplyPropertyChange<FillingData, int>(ref this.batchID, o => o.BatchID, value); }
        }

        public string BatchCode
        {
            get { return this.batchCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.batchCode, o => o.BatchCode, value); }
        }


        private DateTime entryDate;
        public virtual DateTime EntryDate
        {
            get { return this.entryDate; }
            set { ApplyPropertyChange<FillingData, DateTime>(ref this.entryDate, o => o.EntryDate, value); }
        }


        public DateTime SettingDate
        {
            get { return this.EntryDate; } //LẤY BATCH DATE LÀM NGÀY SẢN XUẤT ==> WILL BE PRINT BY THIS //get { return this.settingDate; }
            set
            {
                if (this.settingDate != value)
                {
                    ApplyPropertyChange<FillingData, DateTime>(ref this.settingDate, o => o.SettingDate, value);
                    NotifyPropertyChanged("SettingDateShortDateFormat");
                }
            }
        }

        public string SettingDateShortDateFormat { get { return this.SettingDate.ToShortDateString(); } }


        private int entryMonthID;
        public int EntryMonthID
        {
            get { return this.entryMonthID; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, int>(ref this.entryMonthID, o => o.EntryMonthID, value); }
        }

        //-------------------------

        public string NextDigitNo
        {
            get { return this.nextDigitNo; }

            set
            {
                if (value != this.nextDigitNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextDigitNo, o => o.NextDigitNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string BatchDigitNo
        {
            get { return this.batchDigitNo; }

            set
            {
                if (value != this.batchDigitNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.batchDigitNo, o => o.BatchDigitNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string SentDigitNo
        {
            get { return this.sentDigitNo; }

            set
            {
                if (value != this.sentDigitNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.sentDigitNo, o => o.SentDigitNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string NextPackNo
        {
            get { return this.nextPackNo; }

            set
            {
                if (value != this.nextPackNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextPackNo, o => o.NextPackNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string BatchPackNo
        {
            get { return this.batchPackNo; }

            set
            {
                if (value != this.batchPackNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.batchPackNo, o => o.BatchPackNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }


        public string SentPackNo
        {
            get { return this.sentPackNo; }

            set
            {
                if (value != this.sentPackNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.sentPackNo, o => o.SentPackNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }




        public string NextCartonNo
        {
            get { return this.nextCartonNo; }

            set
            {
                if (value != this.nextCartonNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextCartonNo, o => o.NextCartonNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string BatchCartonNo
        {
            get { return this.batchCartonNo; }

            set
            {
                if (value != this.batchCartonNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.batchCartonNo, o => o.BatchCartonNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string SentCartonNo
        {
            get { return this.sentCartonNo; }

            set
            {
                if (value != this.sentCartonNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.sentCartonNo, o => o.SentCartonNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }


        public string NextPalletNo
        {
            get { return this.nextPalletNo; }

            set
            {
                if (value != this.nextPalletNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextPalletNo, o => o.NextPalletNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string BatchPalletNo
        {
            get { return this.batchPalletNo; }

            set
            {
                if (value != this.batchPalletNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.batchPalletNo, o => o.BatchPalletNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }


        public string SentPalletNo
        {
            get { return this.sentPalletNo; }

            set
            {
                if (value != this.sentPalletNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.sentPalletNo, o => o.SentPalletNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }


        public string FinalCartonNo
        {
            get { return this.finalCartonNo; }

            set
            {
                if (value != this.finalCartonNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.finalCartonNo, o => o.FinalCartonNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        //-------------------------



        public string Remarks
        {
            get { return this.remarks; }
            set { ApplyPropertyChange<FillingData, string>(ref this.remarks, o => o.Remarks, value); }
        }













        private int packPerCarton;
        public int PackPerCarton
        {
            get { return this.packPerCarton; }
            set { ApplyPropertyChange<FillingData, int>(ref this.packPerCarton, o => o.PackPerCarton, value); }
        }


        private int cartonPerPallet;
        public int CartonPerPallet
        {
            get { return this.cartonPerPallet; }
            set { ApplyPropertyChange<FillingData, int>(ref this.cartonPerPallet, o => o.CartonPerPallet, value); }
        }


        private int nthCartontoZebra;
        public int NthCartontoZebra
        {
            get { return this.nthCartontoZebra; }
            set { ApplyPropertyChange<FillingData, int>(ref this.nthCartontoZebra, o => o.NthCartontoZebra, value); }
        }









        #endregion Public Properties





        public bool IsChange(BatchIndex batchIndex)
        {
            return this.BatchID != batchIndex.BatchID || this.BatchCode != batchIndex.BatchCode || this.EntryMonthID != batchIndex.EntryMonthID || this.CommodityID != batchIndex.CommodityID || this.AutoCarton != batchIndex.AutoCarton 
                || this.NextPackNo != batchIndex.NextPackNo || this.NextCartonNo != batchIndex.NextCartonNo || this.NextPalletNo != batchIndex.NextPalletNo                
                || this.SentPackNo != batchIndex.SentPackNo || this.SentCartonNo != batchIndex.SentCartonNo || this.SentPalletNo != batchIndex.SentPalletNo;
            //|| this.BatchPackNo != batchIndex.BatchPackNo || this.BatchCartonNo != batchIndex.BatchCartonNo || this.BatchPalletNo != batchIndex.BatchPalletNo
        }








        public string FirstLineA1(bool isReadableText)
        {
            return "CXVHP";
        }

        public string FirstLineA2(bool isReadableText)
        {
            return this.SettingDate.ToString("yyMMdd");
        }


        public string SecondLineA1(bool isReadableText)
        {
            return this.CommodityCode;
        }

        public string SecondLineA2(bool isReadableText)
        {
            return this.CommodityAPICode;
        }

        public string ThirdLineA1(bool isReadableText)
        {
            return this.BatchCode.Substring(0, 7);
        }

    }
}
