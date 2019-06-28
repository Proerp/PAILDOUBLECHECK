using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class BinLocation
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public BinLocation(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetBinLocationIndexes();

            this.BinLocationEditable();
            this.BinLocationDeletable();
            this.BinLocationSaveRelative();
            this.BinLocationPostSaveValidate();

            this.GetBinLocationBases();
        }


        private void GetBinLocationIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BinLocations.BinLocationID, BinLocations.Code AS BinLocationCode, BinLocations.Name AS BinLocationName, Locations.Name AS LocationName, BinLocations.InActive, BinLocations.Remarks " + "\r\n";
            queryString = queryString + "       FROM        BinLocations " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON BinLocations.OrganizationalUnitID IN (SELECT OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.BinLocations + " AND AccessControls.AccessLevel > 0) AND BinLocations.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "       WHERE      (SELECT TOP 1 OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.BinLocations + " AND AccessControls.AccessLevel > 0) > 0 " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBinLocationIndexes", queryString);
        }



        private void BinLocationSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("BinLocationSaveRelative", queryString);
        }


        private void BinLocationPostSaveValidate()
        {
            string[] queryArray = new string[2];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = N'Vui lòng kiểm tra kho' FROM BinLocations WHERE BinLocationID = @EntityID AND LocationID <> WarehouseID ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = N'Trùng bin: ' + Code FROM BinLocations GROUP BY LocationID, Code HAVING COUNT(*) > 1 ";
            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BinLocationPostSaveValidate", queryArray);
        }


        private void BinLocationEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BinLocationEditable", queryArray);
        }

        private void BinLocationDeletable()
        {
            string[] queryArray = new string[5];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = BinLocationID FROM PickupDetails WHERE BinLocationID = @EntityID ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = BinLocationID FROM GoodsReceiptDetails WHERE BinLocationID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = BinLocationID FROM GoodsIssueDetails WHERE BinLocationID = @EntityID ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = BinLocationID FROM GoodsIssueTransferDetails WHERE BinLocationID = @EntityID ";
            queryArray[4] = " SELECT TOP 1 @FoundEntity = BinLocationID FROM WarehouseAdjustmentDetails WHERE BinLocationID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("BinLocationDeletable", queryArray);
        }

        private void GetBinLocationBases()
        {
            string queryString;

            queryString = " @WarehouseID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      BinLocationID, Code, Name " + "\r\n";
            queryString = queryString + "       FROM        BinLocations " + "\r\n";
            queryString = queryString + "       WHERE       LocationID = (SELECT TOP 1 LocationID FROM Warehouses WHERE WarehouseID = @WarehouseID) " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetBinLocationBases", queryString);
        }

    }
}
