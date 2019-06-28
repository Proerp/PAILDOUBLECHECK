using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Pallet
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Pallet(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.PalletSaveRelative();

            this.PalletEditable();


            this.GetPallets();
            this.GetPalletChanged();

            this.PalletUpdateEntryStatus();

            this.SearchPallets();
        }

        private void PalletSaveRelative()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @CartonIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @EntityID int, @SaveRelativeOption int, @CartonIDs varchar(3999) " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@CartonIDs <> '') " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "               UPDATE      Cartons" + "\r\n";
            queryString = queryString + "               SET         PalletID = @EntityID, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME CartonID PASS BY VARIBLE: CartonIDs
            queryString = queryString + "               WHERE       PalletID IS NULL AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + " AND CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs)) " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "               UPDATE      Cartons" + "\r\n";
            queryString = queryString + "               SET         PalletID = NULL, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME CartonID PASS BY VARIBLE: CartonIDs
            queryString = queryString + "               WHERE       PalletID = @EntityID AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + " AND CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs)) " + "\r\n";

            
            queryString = queryString + "           IF @@ROWCOUNT <> (SELECT CartonCounts FROM Pallets WHERE PalletID = @EntityID)  OR  @@ROWCOUNT <> ((SELECT (LEN(@CartonIDs) - LEN(REPLACE(@CartonIDs, ',', '')))) + 1) " + "\r\n"; //CHECK BOTH CONDITION FOR SURE. BUT: WE CAN OMIT THE SECOND CONDITION 
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'System Error: Some carton does not exist!' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            if (GlobalEnums.BPFillingSystem)
            {
                queryString = queryString + "       ELSE " + "\r\n";
                queryString = queryString + "           BEGIN " + "\r\n";
                queryString = queryString + "               IF (@SaveRelativeOption = 1) " + "\r\n";
                queryString = queryString + "                   INSERT INTO BPFillingSystem.dbo.DataDetailCarton (Carton2018ID, PalletID, CartonDate, CartonStatus, CartonBarcode, FillingLineID, Pack00Barcode, Pack01Barcode, Pack02Barcode, Pack03Barcode, Pack04Barcode, Pack05Barcode, Pack06Barcode, Pack07Barcode, Pack08Barcode, Pack09Barcode, Pack10Barcode, Pack11Barcode, Pack12Barcode, Pack13Barcode, Pack14Barcode, Pack15Barcode, Pack16Barcode, Pack17Barcode, Pack18Barcode, Pack19Barcode, Pack20Barcode, Pack21Barcode, Pack22Barcode, Pack23Barcode) " + "\r\n";
                queryString = queryString + "                   SELECT CartonID, @EntityID AS PalletID, EntryDate AS CartonDate, 0 AS CartonStatus, Code AS CartonBarcode, FillingLineID, '' AS Pack00Barcode, '' AS Pack01Barcode, '' AS Pack02Barcode, '' AS Pack03Barcode, '' AS Pack04Barcode, '' AS Pack05Barcode, '' AS Pack06Barcode, '' AS Pack07Barcode, '' AS Pack08Barcode, '' AS Pack09Barcode, '' AS Pack10Barcode, '' AS Pack11Barcode, '' AS Pack12Barcode, '' AS Pack13Barcode, '' AS Pack14Barcode, '' AS Pack15Barcode, '' AS Pack16Barcode, '' AS Pack17Barcode, '' AS Pack18Barcode, '' AS Pack19Barcode, '' AS Pack20Barcode, '' AS Pack21Barcode, '' AS Pack22Barcode, '' AS Pack23Barcode FROM Cartons WHERE PalletID = @EntityID " + "\r\n";
                queryString = queryString + "               ELSE " + "\r\n";
                queryString = queryString + "                   DELETE FROM BPFillingSystem.dbo.Cartons WHERE PalletID = @EntityID " + "\r\n";
                queryString = queryString + "           END " + "\r\n";
            }

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PalletSaveRelative", queryString);
        }


        private void PalletEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = PalletID FROM Pallets WHERE PalletID = @EntityID AND NOT  some ID: pickup already IS NULL"; SHOULD CHECK AGAIN

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PalletEditable", queryArray);
        }




        private void GetPalletChanged()
        {
            string queryString = " @FillingLineID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT PalletChanged FROM FillingLines WHERE FillingLineID = @FillingLineID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPalletChanged", queryString);
        }

        private void GetPallets()
        {
            string queryString = " @FillingLineID int, @BatchID int, @EntryStatusIDs varchar(3999) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE FillingLines SET PalletChanged = 0 WHERE FillingLineID = @FillingLineID " + "\r\n";
            queryString = queryString + "       SELECT * FROM Pallets WHERE FillingLineID = @FillingLineID AND (QuantityPickup = 0 OR BatchID = @BatchID) AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs))  " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPallets", queryString);
        }

        private void PalletUpdateEntryStatus()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @PalletIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @PalletIDs varchar(3999), @EntryStatusID int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Pallets" + "\r\n";
            queryString = queryString + "       SET         EntryStatusID = @EntryStatusID " + "\r\n";
            queryString = queryString + "       WHERE       PalletID IN (SELECT Id FROM dbo.SplitToIntList (@PalletIDs)) " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PalletUpdateEntryStatus", queryString);
        }



        private void SearchPallets()
        {
            string queryString;

            queryString = " @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       SELECT TOP (200) * FROM Pallets WHERE Code LIKE '%' + @Barcode+ '%' ORDER BY EntryDate DESC " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchPallets", queryString);
        }

    }
}
