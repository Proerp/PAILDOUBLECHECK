using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{
    public class UserControl
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public UserControl(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetUserControlIndexes();

            this.UserControlEditable();

            this.UserControlRegister();
            this.UserControlUnregister();
            this.UserControlSetAdmin();
            this.UserControlToggleVoid();

            this.GetUserControlGroups();
            this.GetUserControlAvailableGroups();

            this.GetUserControlSalespersons();
            this.GetUserControlAvailableSalespersons();

            this.UserControlAddSalesperson();
            this.UserControlRemoveSalesperson();
        }

        private void GetUserControlIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime, @ActiveOption int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       UPDATE Users SET InActive = 1 WHERE SecurityIdentifier IN (SELECT SecurityIdentifier FROM Users WHERE InActive = 1) " + "\r\n";
            queryString = queryString + "       UPDATE Users SET IsDatabaseAdmin = 1 WHERE SecurityIdentifier IN (SELECT SecurityIdentifier FROM Users WHERE IsDatabaseAdmin = 1) " + "\r\n";

            queryString = queryString + "       SELECT      MIN(UserID) AS UserID, SecurityIdentifier, UserName, IsDatabaseAdmin, InActive, N'Chevron Vietnam' AS UserControlType " + "\r\n";
            queryString = queryString + "       FROM        Users " + "\r\n";
            queryString = queryString + "       WHERE       @ActiveOption = " + (int)GlobalEnums.ActiveOption.Both + " OR InActive = @ActiveOption " + "\r\n";
            queryString = queryString + "       GROUP BY    SecurityIdentifier, UserName, IsDatabaseAdmin, InActive " + "\r\n";
            queryString = queryString + "       ORDER BY    UserName " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserControlIndexes", queryString);
        }


        private void UserControlEditable()
        {
            string[] queryArray = new string[9];

            string queryString = "              DECLARE @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + "       SELECT TOP 1 @SecurityIdentifier = SecurityIdentifier FROM Users WHERE UserID = @EntityID " + "\r\n";

            queryArray[0] = " SELECT TOP 1 @FoundEntity = UserID FROM BinLocations WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = UserID FROM SalesOrders WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = UserID FROM DeliveryAdvices WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[3] = " SELECT TOP 1 @FoundEntity = UserID FROM TransferOrders WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[4] = " SELECT TOP 1 @FoundEntity = UserID FROM GoodsIssues WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[5] = " SELECT TOP 1 @FoundEntity = UserID FROM Pickups WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[6] = " SELECT TOP 1 @FoundEntity = UserID FROM GoodsReceipts WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[7] = " SELECT TOP 1 @FoundEntity = UserID FROM WarehouseAdjustments WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";
            queryArray[8] = " SELECT TOP 1 @FoundEntity = UserID FROM Forecasts WHERE UserID IN (SELECT UserID FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("UserControlEditable", queryArray, queryString);
        }


        private void UserControlRegister()
        {
            string queryString = " @FirstName nvarchar(60), @LastName nvarchar(60), @UserName nvarchar(256), @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";
           
            queryString = queryString + "           IF (SELECT COUNT(UserID) FROM Users WHERE SecurityIdentifier = @SecurityIdentifier) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";

            queryString = queryString + "                   DECLARE @LocationID int, @OrganizationalUnitID int " + "\r\n";

            queryString = queryString + "                   DECLARE Action_Cursor CURSOR FOR SELECT LocationID, MIN(OrganizationalUnitID) AS OrganizationalUnitID FROM OrganizationalUnits GROUP BY LocationID OPEN Action_Cursor; " + "\r\n";
            queryString = queryString + "                   FETCH NEXT FROM Action_Cursor INTO @LocationID, @OrganizationalUnitID; " + "\r\n";
            queryString = queryString + "                   WHILE @@FETCH_STATUS = 0 " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";

            queryString = queryString + "                           EXEC UserRegister @LocationID, @OrganizationalUnitID, @FirstName, @LastName, @UserName, @SecurityIdentifier, 0, 0, 0 " + "\r\n";
            
            queryString = queryString + "                           FETCH NEXT FROM Action_Cursor INTO @LocationID, @OrganizationalUnitID; " + "\r\n";

            queryString = queryString + "                       END" + "\r\n";
            queryString = queryString + "                   CLOSE Action_Cursor; " + "\r\n";
            queryString = queryString + "                   DEALLOCATE Action_Cursor " + "\r\n";

            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Đăng ký trùng user (UserControl)' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserControlRegister", queryString);
        }

        private void UserControlUnregister()
        {
            string queryString = " @UserID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           DECLARE @SecurityIdentifier nvarchar(256), @UserName nvarchar(256), @OrganizationalUnitName nvarchar(256) " + "\r\n";
            queryString = queryString + "           SELECT TOP 1 @SecurityIdentifier = SecurityIdentifier, @UserName = UserName FROM Users WHERE UserID = @UserID " + "\r\n";

            queryString = queryString + "           DECLARE     @FoundEntitys TABLE (FoundEntity int NULL) " + "\r\n";
            queryString = queryString + "           INSERT INTO @FoundEntitys EXEC UserControlEditable @UserID " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(*) FROM @FoundEntitys WHERE NOT FoundEntity IS NULL) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";

            //                                              REMOVE GROUP MEMBER. SEE STORED: UserGroupRemoveMember
            queryString = queryString + "                   DELETE FROM     UserGroupDetails WHERE SecurityIdentifier = @SecurityIdentifier; " + "\r\n";
            //                                              REMOVE SALESPERSON.  SEE STORED: UserControlRemoveSalesperson
            queryString = queryString + "                   DELETE FROM     UserSalespersons WHERE SecurityIdentifier = @SecurityIdentifier; " + "\r\n";

            queryString = queryString + "                   DECLARE Action_Cursor CURSOR FOR SELECT Users.UserID, Users.UserName, OrganizationalUnits.Name AS OrganizationalUnitName FROM Users INNER JOIN OrganizationalUnits ON Users.OrganizationalUnitID = OrganizationalUnits.OrganizationalUnitID WHERE Users.SecurityIdentifier = @SecurityIdentifier OPEN Action_Cursor; " + "\r\n";
            queryString = queryString + "                   FETCH NEXT FROM Action_Cursor INTO @UserID, @UserName, @OrganizationalUnitName; " + "\r\n";
            queryString = queryString + "                   WHILE @@FETCH_STATUS = 0 " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";

            queryString = queryString + "                           EXEC UserUnregister @UserID, @UserName, @OrganizationalUnitName " + "\r\n";

            queryString = queryString + "                           FETCH NEXT FROM Action_Cursor INTO @UserID, @UserName, @OrganizationalUnitName; " + "\r\n";

            queryString = queryString + "                       END" + "\r\n";
            queryString = queryString + "                   CLOSE Action_Cursor; " + "\r\n";
            queryString = queryString + "                   DEALLOCATE Action_Cursor " + "\r\n";


            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Không thể hủy đăng ký ' + @UserName + N', do ' + @UserName + N' hiện đang có dữ liệu.' + N'\r\n\r\n\r\nVui lòng Inactive để dừng đăng ký và sử dụng ' + @UserName + '.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserControlUnregister", queryString);
        }


        private void UserControlSetAdmin()
        {
            string queryString = " @EntityID int, @IsDatabaseAdmin bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE @UserID int " + "\r\n";

            queryString = queryString + "                   DECLARE Action_Cursor CURSOR FOR SELECT UserID FROM Users WHERE SecurityIdentifier IN (SELECT SecurityIdentifier FROM Users WHERE UserID = @EntityID) OPEN Action_Cursor; " + "\r\n";
            queryString = queryString + "                   FETCH NEXT FROM Action_Cursor INTO @UserID; " + "\r\n";
            queryString = queryString + "                   WHILE @@FETCH_STATUS = 0 " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";

            queryString = queryString + "                           EXEC UserSetAdmin @UserID, @IsDatabaseAdmin " + "\r\n";

            queryString = queryString + "                           FETCH NEXT FROM Action_Cursor INTO @UserID; " + "\r\n";

            queryString = queryString + "                       END" + "\r\n";
            queryString = queryString + "                   CLOSE Action_Cursor; " + "\r\n";
            queryString = queryString + "                   DEALLOCATE Action_Cursor " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserControlSetAdmin", queryString);
        }

        private void UserControlToggleVoid()
        {
            string queryString = " @EntityID int, @InActive bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE @UserID int " + "\r\n";

            queryString = queryString + "                   DECLARE Action_Cursor CURSOR FOR SELECT UserID FROM Users WHERE SecurityIdentifier IN (SELECT SecurityIdentifier FROM Users WHERE UserID = @EntityID) OPEN Action_Cursor; " + "\r\n";
            queryString = queryString + "                   FETCH NEXT FROM Action_Cursor INTO @UserID; " + "\r\n";
            queryString = queryString + "                   WHILE @@FETCH_STATUS = 0 " + "\r\n";
            queryString = queryString + "                       BEGIN " + "\r\n";

            queryString = queryString + "                           EXEC UserToggleVoid @UserID, @InActive " + "\r\n";

            queryString = queryString + "                           FETCH NEXT FROM Action_Cursor INTO @UserID; " + "\r\n";

            queryString = queryString + "                       END" + "\r\n";
            queryString = queryString + "                   CLOSE Action_Cursor; " + "\r\n";
            queryString = queryString + "                   DEALLOCATE Action_Cursor " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserControlToggleVoid", queryString);
        }


        private void GetUserControlGroups()
        {
            string queryString;

            queryString = " @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      UserGroupDetails.UserGroupDetailID, UserGroupDetails.UserGroupID, UserGroups.Code AS UserGroupCode, UserGroups.Name AS UserGroupName, N'Chevron Vietnam' AS GroupType " + "\r\n";
            queryString = queryString + "       FROM        UserGroupDetails INNER JOIN UserGroups ON UserGroupDetails.SecurityIdentifier = @SecurityIdentifier AND UserGroupDetails.UserGroupID = UserGroups.UserGroupID " + "\r\n";
            queryString = queryString + "       ORDER BY    UserGroupDetails.UserGroupDetailID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserControlGroups", queryString);
        }

        private void GetUserControlAvailableGroups()
        {
            string queryString = " @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           SELECT  UserGroupID, Code AS GroupCode, Name AS GroupName, Description, N'Chevron Vietnam' AS UserGroup FROM UserGroups WHERE UserGroupID NOT IN (SELECT UserGroupID FROM UserGroupDetails WHERE SecurityIdentifier = @SecurityIdentifier) ORDER BY Code, Name " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserControlAvailableGroups", queryString);
        }

        private void GetUserControlSalespersons()
        {
            string queryString;

            queryString = " @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      UserSalespersons.UserSalespersonID, UserSalespersons.EmployeeID, Employees.Code AS EmployeeCode, Employees.Name AS EmployeeName, N'Chevron Vietnam' AS EmployeeType " + "\r\n";
            queryString = queryString + "       FROM        UserSalespersons INNER JOIN Employees ON UserSalespersons.SecurityIdentifier = @SecurityIdentifier AND UserSalespersons.EmployeeID = Employees.EmployeeID " + "\r\n";
            queryString = queryString + "       ORDER BY    UserSalespersons.UserSalespersonID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserControlSalespersons", queryString);
        }

        private void GetUserControlAvailableSalespersons()
        {
            string queryString = " @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           SELECT  EmployeeID, Code AS EmployeeCode, Name AS EmployeeName, N'Chevron Vietnam' AS EmployeeType FROM Employees WHERE EmployeeID NOT IN (SELECT EmployeeID FROM UserSalespersons WHERE SecurityIdentifier = @SecurityIdentifier) AND EmployeeID IN (SELECT EmployeeID FROM EmployeeRoles WHERE RoleID = " + (int)GlobalEnums.RoleID.Saleperson + ") ORDER BY Code, Name " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserControlAvailableSalespersons", queryString);
        }

        private void UserControlAddSalesperson()
        {
            string queryString = " @SecurityIdentifier nvarchar(256), @EmployeeID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(SecurityIdentifier) FROM UserSalespersons WHERE EmployeeID = @EmployeeID AND SecurityIdentifier = @SecurityIdentifier) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE         @UserSalespersonID Int" + "\r\n";
            queryString = queryString + "                   INSERT INTO     UserSalespersons (SecurityIdentifier, EmployeeID, EntryDate) VALUES (@SecurityIdentifier, @EmployeeID, GetDate()); " + "\r\n";
            queryString = queryString + "                   SELECT          @UserSalespersonID = SCOPE_IDENTITY(); " + "\r\n";

            queryString = queryString + "                   SELECT          @UserSalespersonID AS UserSalespersonID " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Đăng ký trùng salesperon.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserControlAddSalesperson", queryString);
        }

        private void UserControlRemoveSalesperson()
        {
            string queryString = " @UserSalespersonID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(*) FROM UserSalespersons WHERE UserSalespersonID = @UserSalespersonID) = 1 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            //                                              WE ALSO CALL THIS BELOW STATEMENT TO REMOVE GROUP MEMBER WHEN UNREGISTER USER. SEE STORED: UserControlUnregister
            queryString = queryString + "                   DELETE FROM     UserSalespersons WHERE UserSalespersonID = @UserSalespersonID; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Không thể xóa salesperon khỏi user.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserControlRemoveSalesperson", queryString);
        }


    }
}
