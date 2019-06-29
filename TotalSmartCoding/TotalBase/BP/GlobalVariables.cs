using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalBase
{
    public class GlobalVariables
    {

        public static bool MyTest = false;

        public static bool shouldRestoreProcedure = false;


        public enum ENMVNTaskID
        {
            EEmployeeCategory = 1,
            EEmployeeType = 2,
            EEmployeeName = 3
        }


        public const char charESC = (char)27;//char.ConvertFromUtf32(27);//1BH
        public const char charEOT = (char)4;//char.ConvertFromUtf32(4);//04H
        public const char charACK = (char)6;//char.ConvertFromUtf32(6);//06H
        public const char charNACK = (char)21;//char.ConvertFromUtf32(21);//15H

        public const char charPrintingACK = (char)28;//char.ConvertFromUtf32(21);//1CH

        public const char charSTX = (char)02;//char.ConvertFromUtf32(02);//02H
        public const char charETX = (char)03;//char.ConvertFromUtf32(03);//03H
        //public const char charTAB = (char)09;
        public const string charTAB = "<HT>";

        public const char doubleTabChar = (char)09;




        public const char charLF = (char)10;//char.ConvertFromUtf32( );//
        public const char charCR = (char)13;//char.ConvertFromUtf32( );//


        public static int[] DominoProductCounter = new int[4] { 0, 0, 0, 0 };   //This array use in associated with [enum DominoPrinterName] => Important Note: element 0: not use!!!





        public enum FillingLine
        {
            None = 0,
            Smallpack = 1,
            Pail = 5,
            Drum = 3,
            Medium4L = 4,
            Import = 8,


            Pickup = 68,
            GoodsIssue = 88
        }


        public enum PrinterName
        {
            DigitInkjet = 1,
            PackInkjet = 2,
            CartonInkjet = 3,
            PalletLabel = 6
        }


        public enum ScannerName
        {
            Base = 100000, //BASE VALUE TO ADD TO DeviceID VALUE: START DeviceID VALUE OF SCANNER IS: 10000 + 1, 10000 + 2, 10000 + 3, ...
            QualityScanner = 1,
            PackScanner = 2,
            CartonScanner = 3,
            PalletScanner = 6,
            LabelScanner = 8,
            CheckScanner = 10
        }

        public enum BarcodeName
        {
            Digit = 1,
            Pack = 2,
            Carton = 3,
            Pallet = 6
        }

        public enum BarcodeStatus
        {
            Freshnew = 0,
            Readytoset = 2,
            Wrapped = 6,

            Pending = 8,

            Noread = 9,

            EmptyCarton = 10,

            Tsa = 88,

            HasSent = 99,
            Deleted = 199
        }

        public enum SubmitStatus
        {
            Freshnew = 0,
            Created = 201,
            Exported = 1,
            Failed = 100,
            Deleted = -1
        }

        public enum ZebraStatus
        {
            Freshnew = 0,
            Reprint = -1,


            Printed = 1,


            //WAIT FOR 3 TIMES TO ENSURE RECEIVE ACK/ NACK FROM ZEBRA PRINTER
            Printing1 = 90,
            Printing2 = 91,
            Printing3 = 92,

            Reprinting1 = -90,
            Reprinting2 = -91,
            Reprinting3 = -92
        }

        public static bool IgnoreEmptyCarton = true;
        public static bool IgnoreEmptyPallet = true;

        public static bool DuplicateCartonBarcodeFound = false;

        public static int LocationID = -1;


        public static int ConfigID = -1;
        public static int ConfigVersionID(int configID)
        {
            if (configID == (int)GlobalVariables.FillingLine.None)
                return 11;
            else if (configID == (int)GlobalVariables.FillingLine.Pickup)
                return 11;
            else if (configID == (int)GlobalVariables.FillingLine.GoodsIssue)
                return 11;



            else if (configID == (int)GlobalVariables.FillingLine.Smallpack || configID == (int)GlobalVariables.FillingLine.Pail || configID == (int)GlobalVariables.FillingLine.Medium4L || configID == (int)GlobalVariables.FillingLine.Import || configID == (int)GlobalVariables.FillingLine.Drum)
                return 11; //PAY ATTENTION WHEN CHANGE THIS VALUE BECAUSE: THIS IS USING ON THE FILLING LINES
            else
                return 11;
        }

        public static int MaxConfigVersionID()
        {
            int maxConfigVersionID = 0;
            foreach (GlobalVariables.FillingLine fillingLine in Enum.GetValues(typeof(GlobalVariables.FillingLine)))
            {
                if (maxConfigVersionID < GlobalVariables.ConfigVersionID((int)fillingLine)) maxConfigVersionID = GlobalVariables.ConfigVersionID((int)fillingLine);
            }

            return maxConfigVersionID;
        }

        public static FillingLine FillingLineID = FillingLine.Pail;
        public static string FillingLineCode = "P1";
        public static string FillingLineName = FillingLine.Pail.ToString();

        public static int noItemPerCartonSetByProductID = 0;

        public static int PortNumber = 7000;
        public static string ComportName = "COM6";

        //public static string ReportServerUrl = "http://dell-e7240t/ReportServer_SQL2016";
        public static string ReportServerUrl = "http://vnhpbcvmsql01/ReportServer_SQL01";

        public const int ServerLineID = 99;


        #region  IP ADDRESS.BACKUP OPLY
        public static string IpAddress(GlobalVariables.FillingLine fillingLineID, PrinterName dominoPrinterNameID)
        {
            switch (fillingLineID)
            {
                //case FillingLine.Smallpack:
                //    switch (dominoPrinterNameID)
                //    {
                //        case PrinterName.DigitInkjet:
                //            return "172.21.67.157";
                //        case PrinterName.PackInkjet:
                //            return "172.21.67.158";
                //        case PrinterName.CartonInkjet:
                //            return "172.21.67.159";
                //        default:
                //            return "127.0.0.1";
                //    }

                //case FillingLine.Pail:
                //    switch (dominoPrinterNameID)
                //    {
                //        case PrinterName.DigitInkjet:
                //            return "172.21.67.165";
                //        case PrinterName.CartonInkjet:
                //            return "172.21.67.163";
                //        default:
                //            return "127.0.0.1";
                //    }
                //case FillingLine.Drum:
                //    switch (dominoPrinterNameID)
                //    {
                //        case PrinterName.DigitInkjet:
                //            return "172.21.67.167";
                //        default:
                //            return "127.0.0.1";
                //    }


                case FillingLine.Medium4L:
                    switch (dominoPrinterNameID)
                    {
                        case PrinterName.CartonInkjet:
                            return "172.21.67.152";
                        default:
                            return "127.0.0.1";
                    }

                case FillingLine.Import:
                    switch (dominoPrinterNameID)
                    {
                        case PrinterName.DigitInkjet:
                            return "127.0.0.1";
                        default:
                            return "127.0.0.1";
                    }

                default:
                    return "127.0.0.1";
            }

        }
        public static string IpAddress(GlobalVariables.FillingLine fillingLineID, ScannerName barcodeScannerName)
        {
            switch (fillingLineID)
            {
                //case FillingLine.Smallpack:
                //    switch (barcodeScannerName)
                //    {
                //        case ScannerName.QualityScanner:
                //            return "127.0.0.1";
                //        case ScannerName.PackScanner:
                //            return "172.21.67.168";
                //        case ScannerName.CartonScanner:
                //            return "172.21.67.169";
                //        case ScannerName.PalletScanner:
                //            return "172.21.67.170";
                //        default:
                //            return "127.0.0.1";
                //    }
                //case FillingLine.Pail:
                //    switch (barcodeScannerName)
                //    {
                //        case ScannerName.QualityScanner:
                //            return "127.0.0.1";
                //        case ScannerName.PackScanner:
                //            return "127.0.0.1"; //172.21.67.52
                //        case ScannerName.CartonScanner:
                //            return "172.21.67.172";
                //        case ScannerName.PalletScanner:
                //            return "172.21.67.173";
                //        default:
                //            return "127.0.0.1";
                //    }

                //case FillingLine.Drum:
                //    switch (barcodeScannerName)
                //    {
                //        case ScannerName.QualityScanner:
                //            return "127.0.0.1";
                //        case ScannerName.PackScanner:
                //            return "127.0.0.1";
                //        case ScannerName.CartonScanner:
                //            return "127.0.0.1";
                //        case ScannerName.PalletScanner:
                //            return "172.21.67.175";
                //        default:
                //            return "127.0.0.1";
                //    }


                case FillingLine.Medium4L:
                    switch (barcodeScannerName)
                    {
                        case ScannerName.CartonScanner:
                            return "172.21.67.153";
                        case ScannerName.PalletScanner:
                            return "172.21.67.154";
                        default:
                            return "127.0.0.1";
                    }

                case FillingLine.Import:
                    switch (barcodeScannerName)
                    {
                        case ScannerName.PalletScanner:
                            return "172.21.67.181";
                        default:
                            return "127.0.0.1";
                    }

                default:
                    return "127.0.0.1";
            }

        }
        #endregion IP ADDRESS.BACKUP OPLY




        public static int NoItemDiverter()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case FillingLine.Smallpack:
                    return 6;
                case FillingLine.Pail:
                    return 1;
                case FillingLine.Medium4L:
                    return 1;
                case FillingLine.Import:
                    return 1;
                case FillingLine.Drum:
                    return 1;
                default:
                    return 1;
            }
        }


        public static int NoSubQueue()
        {
            //IMPORTANT:
            //NoItemPerCarton must be a multiples of NoSubQueue
            //This means: (GlobalVariables.NoItemPerCarton % GlobalVariables.NoSubQueue == 0)

            switch (GlobalVariables.FillingLineID)
            {
                case FillingLine.Smallpack:
                    return 6;
                case FillingLine.Pail:
                    return 1;
                case FillingLine.Medium4L:
                    return 1;
                case FillingLine.Import:
                    return 1;
                case FillingLine.Drum:
                    return 1;
                default:
                    return 1;
            }
        }


        public static int NoItemPerCarton()
        {
            //IMPORTANT:
            //------NoItemPerCarton must be a multiples of NoSubQueue
            //------This means: (GlobalVariables.NoItemPerCarton % GlobalVariables.NoSubQueue == 0)


            //It is free to change NoItemPerCarton
            //with one thing should be considered:
            //----If NoItemPerCarton <= 24: There is nothing must modify
            //----BUT: If NoItemPerCarton > 24: MUST MODIFY THE CODES IN: BarcodeScannerBLL.cs: When copy Data FROM packInOneCarton TO cartonDataTable; AND viseversa

            switch (GlobalVariables.FillingLineID)
            {
                case FillingLine.Smallpack:
                    return 24;
                case FillingLine.Pail:
                    return 1;
                case FillingLine.Medium4L:
                    return 1;
                case FillingLine.Import:
                    return 1;
                case FillingLine.Drum:
                    return GlobalVariables.noItemPerCartonSetByProductID;
                default:
                    return 1;
            }
        }


        public static bool RepeatedSubQueueIndex()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case FillingLine.Smallpack:
                    return false; //AT BP CASTROL: CO LINE => THIS WILL BE true
                case FillingLine.Pail:
                    return false;
                case FillingLine.Medium4L:
                    return false;
                case FillingLine.Import:
                    return false;
                case FillingLine.Drum:
                    return false;
                default:
                    return false;
            }
        }

    }






}
