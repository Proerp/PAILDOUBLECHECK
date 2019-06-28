using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{
    public class OrganizationalUnit
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public OrganizationalUnit(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetOrganizationalUnitIndexes();

            this.OrganizationalUnitEditable();

            this.OrganizationalUnitAdd();
            this.OrganizationalUnitRemove();
        }

        private void GetOrganizationalUnitIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      OrganizationalUnits.OrganizationalUnitID, OrganizationalUnits.Name AS OrganizationalUnitName, OrganizationalUnits.LocationID, Locations.Name AS LocationName, Locations.Name + '\\' + OrganizationalUnits.Name AS LocationOrganizationalUnitName " + "\r\n";
            queryString = queryString + "       FROM        Locations INNER JOIN OrganizationalUnits ON Locations.LocationID = OrganizationalUnits.LocationID " + "\r\n";
            queryString = queryString + "       ORDER BY    Locations.Name, OrganizationalUnits.Name " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetOrganizationalUnitIndexes", queryString);
        }

        private void OrganizationalUnitEditable()
        {
            string[] queryArray = new string[13];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM Users WHERE OrganizationalUnitID = @EntityID ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM OrganizationalUnitUsers WHERE OrganizationalUnitID = @EntityID ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM BinLocations WHERE OrganizationalUnitID = @EntityID ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM DeliveryAdvices WHERE OrganizationalUnitID = @EntityID ";
            queryArray[4] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM GoodsIssues WHERE OrganizationalUnitID = @EntityID ";
            queryArray[5] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM GoodsIssueDetails WHERE OrganizationalUnitID = @EntityID ";
            queryArray[6] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM GoodsReceipts WHERE OrganizationalUnitID = @EntityID ";
            queryArray[7] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM GoodsReceiptDetails WHERE OrganizationalUnitID = @EntityID ";
            queryArray[8] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM Pickups WHERE OrganizationalUnitID = @EntityID ";
            queryArray[9] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM SalesOrders WHERE OrganizationalUnitID = @EntityID ";
            queryArray[10] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM TransferOrders WHERE OrganizationalUnitID = @EntityID ";
            queryArray[11] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM WarehouseAdjustments WHERE OrganizationalUnitID = @EntityID ";
            queryArray[12] = " SELECT TOP 1 @FoundEntity = OrganizationalUnitID FROM WarehouseAdjustmentDetails WHERE OrganizationalUnitID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("OrganizationalUnitEditable", queryArray);
        }


        private void OrganizationalUnitAdd()
        {
            string queryString = " @LocationID int, @Code nvarchar(60), @Name nvarchar(60) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(OrganizationalUnitID) FROM OrganizationalUnits WHERE LocationID = @LocationID AND Code = @Code) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE         @OrganizationalUnitID Int" + "\r\n";
            queryString = queryString + "                   INSERT INTO     OrganizationalUnits (LocationID, Code, Name) VALUES (@LocationID, @Code, @Name); " + "\r\n";
            queryString = queryString + "                   SELECT          @OrganizationalUnitID = SCOPE_IDENTITY(); " + "\r\n";

            queryString = queryString + "                   INSERT INTO     AccessControls (OrganizationalUnitID, NMVNTaskID, UserID, AccessLevel, ApprovalPermitted, UnApprovalPermitted, VoidablePermitted, UnVoidablePermitted, ShowDiscount, InActive) " + "\r\n";
            queryString = queryString + "                   SELECT          @OrganizationalUnitID, ModuleDetails.ModuleDetailID, Users.UserID, 0 AS AccessLevel, 0 AS ApprovalPermitted, 0 AS UnApprovalPermitted, 0 AS VoidablePermitted, 0 AS UnVoidablePermitted, 0 AS ShowDiscount, 0 AS InActive " + "\r\n";
            queryString = queryString + "                   FROM            ModuleDetails CROSS JOIN Users;" + "\r\n";
            //queryString = queryString + "                 WHERE           ModuleDetails.InActive = 0; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Thêm mới trùng organizational unit.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("OrganizationalUnitAdd", queryString);
        }


        private void OrganizationalUnitRemove()
        {
            string queryString = " @OrganizationalUnitID int, @Code nvarchar(256)" + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           DECLARE     @FoundEntitys TABLE (FoundEntity int NULL) " + "\r\n";
            queryString = queryString + "           INSERT INTO @FoundEntitys EXEC OrganizationalUnitEditable @OrganizationalUnitID " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(*) FROM @FoundEntitys WHERE NOT FoundEntity IS NULL) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DELETE FROM     AccessControls WHERE OrganizationalUnitID = @OrganizationalUnitID " + "\r\n";
            queryString = queryString + "                   DELETE FROM     OrganizationalUnits WHERE OrganizationalUnitID = @OrganizationalUnitID " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Không thể xóa ' + @Code + '.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("OrganizationalUnitRemove", queryString);
        }


    }
}
