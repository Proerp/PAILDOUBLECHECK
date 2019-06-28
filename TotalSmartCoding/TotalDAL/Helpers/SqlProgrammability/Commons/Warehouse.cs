using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class Warehouse
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Warehouse(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetWarehouseIndexes();

            this.WarehouseEditable();
            this.WarehouseDeletable();
            this.WarehouseSaveRelative();

            this.GetWarehouseBases();
            this.GetWarehouseTrees();

            this.GetWarehouseLocationID();
        }


        private void GetWarehouseIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Warehouses.WarehouseID, Warehouses.Code, Warehouses.Name, Locations.Name AS LocationName " + "\r\n";
            queryString = queryString + "       FROM        Warehouses INNER JOIN Locations ON Warehouses.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "       WHERE      (SELECT TOP 1 OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.Warehouses + " AND AccessControls.AccessLevel > 0) > 0 " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseIndexes", queryString);
        }


        private void WarehouseSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("WarehouseSaveRelative", queryString);
        }


        private void WarehouseEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("WarehouseEditable", queryArray);
        }

        private void WarehouseDeletable()
        {
            string[] queryArray = new string[12];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM BinLocations                 WHERE WarehouseID = @EntityID ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM GoodsReceiptDetails          WHERE WarehouseID = @EntityID OR WarehouseIssueID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM GoodsReceipts                WHERE WarehouseID = @EntityID ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM PickupDetails                WHERE WarehouseID = @EntityID ";
            queryArray[4] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM Pickups                      WHERE WarehouseID = @EntityID ";
            queryArray[5] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM TransferOrderDetails         WHERE WarehouseID = @EntityID OR WarehouseReceiptID = @EntityID ";
            queryArray[6] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM TransferOrders               WHERE WarehouseID = @EntityID OR WarehouseReceiptID = @EntityID ";
            queryArray[7] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM GoodsIssueDetails            WHERE WarehouseID = @EntityID OR WarehouseReceiptID = @EntityID ";
            queryArray[8] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM GoodsIssues                  WHERE WarehouseID = @EntityID OR WarehouseReceiptID = @EntityID ";
            queryArray[9] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM GoodsIssueTransferDetails    WHERE WarehouseID = @EntityID OR WarehouseReceiptID = @EntityID ";            
            queryArray[10] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM WarehouseAdjustmentDetails   WHERE WarehouseID = @EntityID OR WarehouseReceiptID = @EntityID ";
            queryArray[11] = " SELECT TOP 1 @FoundEntity = WarehouseID FROM WarehouseAdjustments         WHERE WarehouseID = @EntityID OR WarehouseReceiptID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("WarehouseDeletable", queryArray);
        }

        private void GetWarehouseBases()
        {
            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseBases", this.GetWarehouseBUILD(0));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseBase", this.GetWarehouseBUILD(1));
            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseBaseByCode", this.GetWarehouseBUILD(2));
        }

        private string GetWarehouseBUILD(int switchID)
        {
            string queryString;

            queryString = (switchID == 0 ? "" : (switchID == 1 ? "@WarehouseID int" : "@Code nvarchar(50)")) + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      WarehouseID, Code, Name, LocationID " + "\r\n";
            queryString = queryString + "       FROM        Warehouses " + "\r\n";
            queryString = queryString + (switchID == 0 ? "" : "WHERE " + (switchID == 1 ? "WarehouseID = @WarehouseID" : "Code = @Code")) + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            return queryString;
        }

        private void GetWarehouseTrees()
        {
            string queryString;

            queryString = " @LocationID int" + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " AS NodeID, 0 AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, '[All]' AS Code, NULL AS Name, NULL AS ParameterName, CAST(CASE WHEN @LocationID IS NULL THEN 1 ELSE 0 END AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " + LocationID AS NodeID, " + GlobalEnums.RootNode + " AS ParentNodeID, LocationID AS PrimaryID, NULL AS AncestorID, Name AS Code, NULL AS Name, 'LocationID' AS ParameterName, CAST(CASE WHEN NOT @LocationID IS NULL AND LocationID = @LocationID THEN 1 ELSE 0 END AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        Locations " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      WarehouseID AS NodeID, " + GlobalEnums.AncestorNode + " + LocationID AS ParentNodeID, WarehouseID AS PrimaryID, LocationID AS AncestorID, Code, Name, 'WarehouseID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        Warehouses " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseTrees", queryString);
        }

        private void GetWarehouseLocationID()
        {
            string queryString;

            queryString = " @WarehouseID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TOP 1 LocationID FROM Warehouses WHERE WarehouseID = @WarehouseID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetWarehouseLocationID", queryString);
        }

    }
}
