using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Carton
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Carton(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.CartonSaveRelative();

            this.CartonEditable();

            this.GetCartons();
            this.GetCartonAttributes();

            this.CartonUpdateEntryStatus();
            this.CartonUpdateSubmitStatus();

            this.CartonChecked();

            this.SearchCartons();
        }

        private void CartonSaveRelative()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @PackIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @EntityID int, @SaveRelativeOption int, @PackIDs varchar(3999), @DeletePack bit " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@PackIDs <> '') " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (@SaveRelativeOption = 1) " + "\r\n";

            queryString = queryString + "               UPDATE      Packs" + "\r\n";
            queryString = queryString + "               SET         CartonID = @EntityID, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME PackID PASS BY VARIBLE: PackIDs
            queryString = queryString + "               WHERE       CartonID IS NULL AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + " AND PackID IN (SELECT Id FROM dbo.SplitToIntList (@PackIDs)) " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 

            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   IF (@DeletePack = 1) " + "\r\n";
            queryString = queryString + "                       DELETE      " + "\r\n";
            queryString = queryString + "                       FROM        Packs " + "\r\n";
            queryString = queryString + "                       WHERE       CartonID = @EntityID AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n";
            queryString = queryString + "                   ELSE " + "\r\n";
            queryString = queryString + "                       UPDATE      Packs" + "\r\n";
            queryString = queryString + "                       SET         CartonID = NULL, EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Readytoset + "\r\n"; //WHERE: NOT BELONG TO ANY CARTON, AND NUMBER OF PACK EFFECTED: IS THE SAME PackID PASS BY VARIBLE: PackIDs
            queryString = queryString + "                       WHERE       CartonID = @EntityID AND EntryStatusID = " + (int)GlobalVariables.BarcodeStatus.Wrapped + "\r\n";
            queryString = queryString + "               END " + "\r\n";


            queryString = queryString + "           IF @@ROWCOUNT <> (SELECT PackCounts FROM Cartons WHERE CartonID = @EntityID)  OR  @@ROWCOUNT <> ((SELECT (LEN(@PackIDs) - LEN(REPLACE(@PackIDs, ',', '')))) + 1) " + "\r\n"; //CHECK BOTH CONDITION FOR SURE. BUT: WE CAN OMIT THE SECOND CONDITION
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'System Error: Some pack does not exist!' + cast(@@ROWCOUNT as nvarchar) ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CartonSaveRelative", queryString);
        }


        private void CartonEditable()
        {
            string[] queryArray = new string[1];
            
            queryArray[0] = " SELECT TOP 1 @FoundEntity = CartonID FROM Cartons WHERE CartonID = @EntityID AND NOT PalletID IS NULL";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CartonEditable", queryArray);
        }





        private void GetCartons()
        {
            string sqlSelect = "       SELECT * FROM Cartons WHERE FillingLineID = @FillingLineID AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs)) " + "\r\n";

            string queryString = " @FillingLineID int, @EntryStatusIDs varchar(3999), @PalletID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF ((@FillingLineID IS NULL OR @FillingLineID = 0) AND @EntryStatusIDs IS NULL AND @PalletID IS NOT NULL) " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           SELECT * FROM Cartons WHERE PalletID = @PalletID " + "\r\n";
            queryString = queryString + "       END " + "\r\n";
            queryString = queryString + "   ELSE " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           IF (@PalletID IS NULL) " + "\r\n";
            queryString = queryString + "               " + sqlSelect + "\r\n";
            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               " + sqlSelect + " AND PalletID = @PalletID " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCartons", queryString);
        }

        private void GetCartonAttributes()
        {
            string sqlSelect = "                    Cartons.CartonID, Cartons.EntryDate, Cartons.FillingLineID, FillingLines.Code AS FillingLineCode, FillingLines.Name AS FillingLineName, Cartons.BatchID, Batches.EntryDate AS BatchEntryDate, Batches.Code AS BatchCode, " + "\r\n";
            sqlSelect = sqlSelect + "               Cartons.LocationID, Cartons.CommodityID, Commodities.Code AS CommodityCode, Commodities.OfficialCode, Commodities.Name AS CommodityName, Cartons.PalletID, Cartons.Code, Cartons.Label, Cartons.Quantity, Cartons.LineVolume, Cartons.PackCounts, Cartons.EntryStatusID, Cartons.SubmitStatusID, Cartons.Remarks " + "\r\n";
            sqlSelect = sqlSelect + "       FROM    Cartons " + "\r\n";
            sqlSelect = sqlSelect + "               INNER JOIN Batches ON Cartons.BatchID = Batches.BatchID " + "\r\n";
            sqlSelect = sqlSelect + "               INNER JOIN FillingLines ON Cartons.FillingLineID = FillingLines.FillingLineID " + "\r\n";
            sqlSelect = sqlSelect + "               INNER JOIN Commodities ON Cartons.CommodityID = Commodities.CommodityID " + "\r\n";

            string sqlWhere = "             WHERE   Cartons.FillingLineID = @FillingLineID AND NOT Cartons.PalletID IS NULL AND Cartons.SubmitStatusID IN (SELECT Id FROM dbo.SplitToIntList (@SubmitStatusIDs)) " + "\r\n";

            string queryString = " @FillingLineID int, @SubmitStatusIDs varchar(3999), @PalletID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF ((@FillingLineID IS NULL OR @FillingLineID = 0) AND @SubmitStatusIDs IS NULL AND @PalletID IS NOT NULL) " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           SELECT  " + sqlSelect + " WHERE PalletID = @PalletID " + "\r\n";
            queryString = queryString + "       END " + "\r\n";
            queryString = queryString + "   ELSE " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           IF (@PalletID IS NULL) " + "\r\n";
            queryString = queryString + "               SELECT  TOP 500 " + sqlSelect + sqlWhere + "\r\n";
            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               SELECT  TOP 500 " + sqlSelect + sqlWhere + " AND PalletID = @PalletID " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCartonAttributes", queryString);
        }

        private void CartonUpdateEntryStatus()
        {
            //BE CAREFULL WHEN SAVE: NEED TO SET @CartonIDs (FOR BOTH WHEN SAVE - Update AND DELETE - Undo
            string queryString = " @CartonIDs varchar(3999), @EntryStatusID int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Cartons" + "\r\n";
            queryString = queryString + "       SET         EntryStatusID = @EntryStatusID " + "\r\n";
            queryString = queryString + "       WHERE       CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs)) " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> ((SELECT (LEN(@CartonIDs) - LEN(REPLACE(@CartonIDs, ',', '')))) + 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'System Error: Some carton does not exist!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("CartonUpdateEntryStatus", queryString);
        }


        private void CartonUpdateSubmitStatus()
        {
            string queryUpdate = "        UPDATE    Cartons SET SubmitStatusID = @SubmitStatusID, Remarks = @Remarks " + "\r\n";

            string queryString = " @CartonIDs varchar(3999), @SubmitStatusID int, @Remarks nvarchar(100) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       IF (CHARINDEX(',', @CartonIDs) = 0) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n"; //ONE CartonID
            queryString = queryString + "               " + queryUpdate + "\r\n";
            queryString = queryString + "               WHERE       CartonID = CAST(@CartonIDs AS int) " + "\r\n";
            queryString = queryString + "           END " + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n"; //MULTIPLE CartonIDs
            queryString = queryString + "               " + queryUpdate + "\r\n";
            queryString = queryString + "               WHERE       CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs)) " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            queryString = queryString + "       IF @@ROWCOUNT = ((SELECT (LEN(@CartonIDs) - LEN(REPLACE(@CartonIDs, ',', '')))) + 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               IF (CHARINDEX(',', @CartonIDs) = 0) " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n"; //ONE CartonID => ONE PalletID AND ONE FillingLineID
            queryString = queryString + "                       DECLARE @PalletID int, @FillingLineID int " + "\r\n";
            queryString = queryString + "                       SELECT  @PalletID = PalletID, @FillingLineID = FillingLineID FROM Cartons WHERE CartonID = CAST(@CartonIDs AS int) " + "\r\n";

            queryString = queryString + "                       IF (NOT EXISTS(SELECT TOP (1) CartonID FROM Cartons WHERE SubmitStatusID >= 0 AND SubmitStatusID <> " + (int)GlobalVariables.SubmitStatus.Created + " AND PalletID = @PalletID)) " + "\r\n";
            queryString = queryString + "                           BEGIN " + "\r\n";//CHECK AND UPDATE: ONE PalletID AND ONE FillingLineID
            queryString = queryString + "                               UPDATE      Pallets SET QuantityPickup = Quantity, LineVolumePickup = LineVolume WHERE PalletID = @PalletID " + "\r\n";
            queryString = queryString + "                               UPDATE      FillingLines SET PalletChanged = 1 WHERE FillingLineID = @FillingLineID " + "\r\n"; 
            queryString = queryString + "                           END " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";
            queryString = queryString + "               ELSE " + "\r\n";
            queryString = queryString + "                   BEGIN " + "\r\n"; //MULTIPLE CartonIDs => MULTIPLE PalletIDs AND MULTIPLE FillingLineIDs
            queryString = queryString + "                       DECLARE @PalletIDs nvarchar(3000), @FillingLineIDs nvarchar(3000) " + "\r\n";
            queryString = queryString + "                       SELECT  @PalletIDs = STUFF((SELECT ',' + CAST(PalletID AS varchar) FROM (SELECT DISTINCT PalletID FROM Cartons WHERE CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs))) DistinctPalletIDs FOR XML PATH('')) ,1,1,'') " + "\r\n";
            queryString = queryString + "                       SELECT  @FillingLineIDs = STUFF((SELECT ',' + CAST(FillingLineID AS varchar) FROM (SELECT DISTINCT FillingLineID FROM Cartons WHERE CartonID IN (SELECT Id FROM dbo.SplitToIntList (@CartonIDs))) DistinctFillingLineIDs FOR XML PATH('')) ,1,1,'') " + "\r\n";

            queryString = queryString + "                       IF (NOT EXISTS(SELECT TOP (1) CartonID FROM Cartons WHERE SubmitStatusID >= 0 AND SubmitStatusID <> " + (int)GlobalVariables.SubmitStatus.Created + " AND PalletID IN (SELECT Id FROM dbo.SplitToIntList (@PalletIDs)))) " + "\r\n";
            queryString = queryString + "                           BEGIN " + "\r\n"; //CHECK AND UPDATE: MULTIPLE PalletIDs AND MULTIPLE FillingLineIDs
            queryString = queryString + "                               UPDATE      Pallets SET QuantityPickup = Quantity, LineVolumePickup = LineVolume WHERE PalletID IN (SELECT Id FROM dbo.SplitToIntList (@PalletIDs)) " + "\r\n";
            queryString = queryString + "                               UPDATE      FillingLines SET PalletChanged = 1 WHERE FillingLineID IN (SELECT Id FROM dbo.SplitToIntList (@FillingLineIDs)) " + "\r\n";
            queryString = queryString + "                           END " + "\r\n";
            queryString = queryString + "                   END " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'System Error: Some carton does not exist!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("CartonUpdateSubmitStatus", queryString);
        }



        private void CartonChecked()
        {
            string queryString = " @BatchID int, @Label varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Cartons " + "\r\n";
            queryString = queryString + "       SET         CheckedOut = CheckedOut + 1, CheckedDate = GETDATE() " + "\r\n";
            queryString = queryString + "       WHERE       BatchID = @BatchID AND Label = @Label AND NOT PalletID IS NULL " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT = 0 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N' không tìm thấy trong hệ thống!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("CartonChecked", queryString);
        }

        private void SearchCartons()
        {
            string queryString;

            queryString = " @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       SELECT TOP (200) * FROM Cartons WHERE Code LIKE '%' + @Barcode + '%' OR Label LIKE '%' + @Barcode + '%' ORDER BY EntryDate DESC " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchCartons", queryString);
        }

    }
}
