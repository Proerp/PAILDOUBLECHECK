using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Batch
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Batch(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetBatchIndexes();
            this.GetBatchSumups();

            this.BatchEditable();
            this.BatchPostSaveValidate();

            this.BatchToggleApproved();
            this.BatchToggleVoid();

            this.BatchInitReference();

            this.GetBatchMaxNo();
            this.BatchCommonUpdate();
            this.BatchExtraUpdate();
            this.BatchExtendedUpdate();
        }


        private void GetBatchIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime, @FillingLineID int, @ActiveOption int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Batches.BatchID, CAST(Batches.EntryDate AS DATE) AS EntryDate, Batches.EntryMonthID, Batches.Reference, Batches.Code AS BatchCode, Batches.FillingLineID, Batches.CommodityID, Commodities.Code AS CommodityCode, Commodities.OfficialCode AS CommodityOfficialCode, Commodities.Name AS CommodityName, Commodities.APICode AS CommodityAPICode, Commodities.Volume, Commodities.PackageVolume, Commodities.PackPerCarton, Commodities.CartonPerPallet, Commodities.Shelflife, " + "\r\n";
            queryString = queryString + "                   IIF(Batches.NextPackNo > Batches.SentPackNo, Batches.NextPackNo, Batches.SentPackNo) AS NextPackNo, IIF(Batches.NextCartonNo > Batches.SentCartonNo, Batches.NextCartonNo, Batches.SentCartonNo) AS NextCartonNo, IIF(Batches.NextPalletNo > Batches.SentPalletNo, Batches.NextPalletNo, Batches.SentPalletNo) AS NextPalletNo, Batches.SentPackNo, Batches.SentCartonNo, Batches.SentPalletNo, Batches.BatchPackNo, Batches.BatchCartonNo, Batches.BatchPalletNo, Batches.FinalCartonNo, Batches.AutoBarcode, Batches.AutoCarton, Batches.Description, Batches.Remarks, Batches.CreatedDate, Batches.EditedDate, Batches.IsDefault, Batches.InActive " + "\r\n";
            queryString = queryString + "       FROM        Batches INNER JOIN " + "\r\n";
            queryString = queryString + "                   Commodities ON Batches.FillingLineID = @FillingLineID AND (@ActiveOption = " + (int)GlobalEnums.ActiveOption.Both + " OR Batches.InActive = @ActiveOption) AND ((Batches.EntryDate >= @FromDate AND Batches.EntryDate <= @ToDate) OR Batches.IsDefault = 1) AND Batches.CommodityID = Commodities.CommodityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchIndexes", queryString);
        }

        private void GetBatchSumups()
        {
            string queryString = " @FromDate DateTime, @ToDate DateTime, @EntryStatusIDs varchar(3999) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       SET         @ToDate = DATEADD (hour, 23, DATEADD (minute, 59, DATEADD (second, 59, @ToDate))) " + "\r\n";
            queryString = queryString + "       SELECT      CartonSumups.FillingLineID, CartonSumups.BatchID, Batches.Code AS BatchCode, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.OfficialCode AS CommodityOfficialCode, Commodities.Name AS CommodityName, CartonSumups.EntryDate, CartonSumups.CartonCounts, ISNULL(UniqueCartonSumups.UniqueCartonCounts, 0) AS UniqueCartonCounts, ISNULL(UniqueCartonSumups.UniqueCheckedOut, 0) AS UniqueCheckedOut " + "\r\n";
            queryString = queryString + "       FROM        " + "\r\n";
            queryString = queryString + "                  (SELECT          FillingLineID, BatchID, CAST(EntryDate AS DATE) AS EntryDate, COUNT(*) AS CartonCounts " + "\r\n";
            queryString = queryString + "                   FROM            Cartons " + "\r\n";
            queryString = queryString + "                   WHERE           EntryDate >= @FromDate AND EntryDate <= @ToDate AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs)) " + "\r\n";
            queryString = queryString + "                   GROUP BY        FillingLineID, BatchID, CAST(EntryDate AS DATE)) CartonSumups " + "\r\n";
            
            queryString = queryString + "                   INNER JOIN Batches ON CartonSumups.BatchID = Batches.BatchID " + "\r\n";
            queryString = queryString + "                   INNER JOIN Commodities ON Batches.CommodityID = Commodities.CommodityID " + "\r\n"; 

            queryString = queryString + "                   LEFT JOIN " + "\r\n";
            queryString = queryString + "                  (SELECT          FillingLineID, BatchID, EntryDate, COUNT(*) AS UniqueCartonCounts, SUM(CheckedOut) AS UniqueCheckedOut " + "\r\n";
            queryString = queryString + "                   FROM           (SELECT        FillingLineID, BatchID, CAST(EntryDate AS DATE) AS EntryDate, Label, MAX(IIF(CheckedOut > 0, 1, 0)) AS CheckedOut FROM Cartons WHERE EntryDate >= @FromDate AND EntryDate <= @ToDate AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs)) GROUP BY FillingLineID, BatchID, CAST(EntryDate AS DATE), Label) UniqueCartons" + "\r\n";
            queryString = queryString + "                   GROUP BY        FillingLineID, BatchID, EntryDate) UniqueCartonSumups ON CartonSumups.FillingLineID = UniqueCartonSumups.FillingLineID AND CartonSumups.BatchID = UniqueCartonSumups.BatchID AND CartonSumups.EntryDate = UniqueCartonSumups.EntryDate " + "\r\n";

            queryString = queryString + "       ORDER BY    CartonSumups.EntryDate DESC " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchSumups", queryString);
        }

        private void BatchEditable()
        {
            string[] queryArray = new string[4];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = BatchID FROM Batches WHERE BatchID = @EntityID AND InActive = 1 "; //Don't allow edit after void
            queryArray[1] = " SELECT TOP 1 @FoundEntity = BatchID FROM Pallets WHERE BatchID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = BatchID FROM Cartons WHERE BatchID = @EntityID ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = BatchID FROM Packs WHERE BatchID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BatchEditable", queryArray);
        }

        private void BatchPostSaveValidate()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Ngày xuất kho: ' + CAST(GoodsIssueDetails.EntryDate AS nvarchar) FROM BatchDetails INNER JOIN GoodsIssueDetails ON BatchDetails.BatchID = @EntityID AND BatchDetails.GoodsIssueDetailID = GoodsIssueDetails.GoodsIssueDetailID AND BatchDetails.EntryDate < GoodsIssueDetails.EntryDate ";
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Ngày xuất kho: ' + CAST(CAST(GoodsIssueDetails.EntryDate AS Date) AS nvarchar) + N' (Ngày HĐ phải sau ngày xuất kho)' FROM BatchDetails INNER JOIN GoodsIssueDetails ON BatchDetails.BatchID = @EntityID AND BatchDetails.GoodsIssueDetailID = GoodsIssueDetails.GoodsIssueDetailID AND BatchDetails.VATInvoiceDate < CAST(GoodsIssueDetails.EntryDate AS Date) ";
            //queryArray[2] = " SELECT TOP 1 @FoundEntity = N'Số lượng xuất hóa đơn vượt quá số lượng xuất kho: ' + CAST(ROUND(GoodsIssueDetails.Quantity - GoodsIssueDetails.QuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) + ' OR free quantity: ' + CAST(ROUND(GoodsIssueDetails.FreeQuantity - GoodsIssueDetails.FreeQuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") AS nvarchar) FROM BatchDetails INNER JOIN GoodsIssueDetails ON BatchDetails.BatchID = @EntityID AND BatchDetails.GoodsIssueDetailID = GoodsIssueDetails.GoodsIssueDetailID AND (ROUND(GoodsIssueDetails.Quantity - GoodsIssueDetails.QuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") < 0 OR ROUND(GoodsIssueDetails.FreeQuantity - GoodsIssueDetails.FreeQuantityInvoice, " + (int)GlobalEnums.rndQuantity + ") < 0) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BatchPostSaveValidate", queryArray);
        }

        private void BatchToggleApproved()
        {
            string queryString = " @EntityID int, @Approved bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       DECLARE @FillingLineID int ";
            queryString = queryString + "       SELECT @FillingLineID = FillingLineID FROM Batches WHERE BatchID = @EntityID ";

            queryString = queryString + "       UPDATE      Batches  SET IsDefault = ~@Approved WHERE FillingLineID = @FillingLineID AND BatchID <> @EntityID AND IsDefault =  @Approved" + "\r\n";
            queryString = queryString + "       UPDATE      Batches  SET IsDefault =  @Approved WHERE FillingLineID = @FillingLineID AND BatchID =  @EntityID AND IsDefault = ~@Approved" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Không thể cài đặt batch này cho sản xuất' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchToggleApproved", queryString);
        }

        private void BatchToggleVoid()
        {
            string queryString = " @EntityID int, @InActive bit, @VoidTypeID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      FillingLines SET PalletChanged = 1 WHERE FillingLineID = (SELECT FillingLineID FROM Batches WHERE BatchID = @EntityID) " + "\r\n";
            queryString = queryString + "       UPDATE      Batches  SET InActive = @InActive, InActiveDate = GetDate() WHERE BatchID = @EntityID AND InActive = ~@InActive" + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'Batch không tồn tại hoặc ' + iif(@InActive = 0, 'đang', 'dừng')  + ' sản xuất' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";


            this.totalSmartCodingEntities.CreateStoredProcedure("BatchToggleVoid", queryString);
        }

        private void BatchInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("Batches", "BatchID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.Batch));
            this.totalSmartCodingEntities.CreateTrigger("BatchInitReference", simpleInitReference.CreateQuery());
        }


        private void GetBatchMaxNo()
        {
            string querySELECT = "              SELECT      RIGHT(MAX(NextPackNo + 1000001), 6) AS NextPackNo, RIGHT(MAX(BatchPackNo + 1000001), 6) AS BatchPackNo, RIGHT(MAX(NextCartonNo + 1000001), 6) AS NextCartonNo, RIGHT(MAX(BatchCartonNo + 1000001), 6) AS BatchCartonNo, RIGHT(MAX(NextPalletNo + 1000001), 6) AS NextPalletNo, RIGHT(MAX(BatchPalletNo + 1000001), 6) AS BatchPalletNo " + "\r\n";
            querySELECT = querySELECT + "       FROM        Batches " + "\r\n";
            querySELECT = querySELECT + "       WHERE       FillingLineID = @FillingLineID AND CommodityID = @CommodityID ";

            string queryString = "  @FillingLineID int, @CommodityID int, @EntryMonthID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       " + querySELECT + " AND EntryMonthID = @EntryMonthID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchMaxNoByEntryMonthID", queryString);

            queryString = "         @FillingLineID int, @CommodityID int, @Code nvarchar(20) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       " + querySELECT + " AND Code = @Code " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBatchMaxNoByCode", queryString);
        }

        private void BatchCommonUpdate()
        {
            string queryString = " @BatchID int, @NextPackNo nvarchar(10), @NextCartonNo nvarchar(10), @NextPalletNo nvarchar(10) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Batches" + "\r\n";
            queryString = queryString + "       SET         NextPackNo = CASE WHEN @NextPackNo != '' THEN @NextPackNo ELSE NextPackNo END, NextCartonNo = CASE WHEN @NextCartonNo != '' THEN @NextCartonNo ELSE NextCartonNo END, NextPalletNo = CASE WHEN @NextPalletNo != '' THEN @NextPalletNo ELSE NextPalletNo END " + "\r\n";
            queryString = queryString + "       WHERE       BatchID = @BatchID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchCommonUpdate", queryString);
        }

        private void BatchExtendedUpdate()
        {
            string queryString = " @BatchID int, @BatchPackNo nvarchar(10), @BatchCartonNo nvarchar(10), @BatchPalletNo nvarchar(10) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Batches" + "\r\n";
            queryString = queryString + "       SET         BatchPackNo = CASE WHEN @BatchPackNo != '' THEN @BatchPackNo ELSE BatchPackNo END, BatchCartonNo = CASE WHEN @BatchCartonNo != '' THEN @BatchCartonNo ELSE BatchCartonNo END, BatchPalletNo = CASE WHEN @BatchPalletNo != '' THEN @BatchPalletNo ELSE BatchPalletNo END " + "\r\n";
            queryString = queryString + "       WHERE       BatchID = @BatchID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchExtendedUpdate", queryString);
        }


        private void BatchExtraUpdate()
        {
            string queryString = " @BatchID int, @SentPackNo nvarchar(10), @SentCartonNo nvarchar(10), @SentPalletNo nvarchar(10) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Batches" + "\r\n";
            queryString = queryString + "       SET         SentPackNo = CASE WHEN @SentPackNo != '' THEN @SentPackNo ELSE SentPackNo END, SentCartonNo = CASE WHEN @SentCartonNo != '' THEN @SentCartonNo ELSE SentCartonNo END, SentPalletNo = CASE WHEN @SentPalletNo != '' THEN @SentPalletNo ELSE SentPalletNo END " + "\r\n";
            queryString = queryString + "       WHERE       BatchID = @BatchID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BatchExtraUpdate", queryString);
        }

    }
}
