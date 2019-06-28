using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{
    public class UserGroup
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public UserGroup(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetUserGroupIndexes();

            this.UserGroupEditable();

            this.UserGroupAdd();
            this.UserGroupRename();
            this.UserGroupRemove();

           

            this.GetUserGroupMembers();
            this.GetUserGroupAvailableMembers();

            this.UserGroupAddMember();
            this.UserGroupRemoveMember();
        }

        private void GetUserGroupIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      UserGroupID, Code, Name, Description, Remarks, N'Chevron Vietnam' AS UserGroupType " + "\r\n";
            queryString = queryString + "       FROM        UserGroups " + "\r\n";
            queryString = queryString + "       ORDER BY    Code " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserGroupIndexes", queryString);
        }

        private void UserGroupEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = UserGroupID FROM UserGroupDetails WHERE UserGroupID = @EntityID ";
        
            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("UserGroupEditable", queryArray);
        }


        private void UserGroupAdd()
        {
            string queryString = " @Code nvarchar(60), @Name nvarchar(100), @Description nvarchar(100) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(UserGroupID) FROM UserGroups WHERE Code = @Code OR Name = @Name) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE         @UserGroupID Int" + "\r\n";
            queryString = queryString + "                   INSERT INTO     UserGroups (Code, Name, Description, Remarks) VALUES (@Code, @Name, @Description, NULL); " + "\r\n";
            queryString = queryString + "                   SELECT          @UserGroupID = SCOPE_IDENTITY(); " + "\r\n";

            queryString = queryString + "                   INSERT INTO     UserGroupControls (UserGroupID, ModuleDetailID, LocationID, AccessLevel, ApprovalPermitted, UnApprovalPermitted, VoidablePermitted, UnVoidablePermitted, ShowDiscount, InActive) " + "\r\n";
            queryString = queryString + "                   SELECT          @UserGroupID, ModuleDetails.ModuleDetailID, Locations.LocationID, 0 AS AccessLevel, 0 AS ApprovalPermitted, 0 AS UnApprovalPermitted, 0 AS VoidablePermitted, 0 AS UnVoidablePermitted, 0 AS ShowDiscount, 0 AS InActive " + "\r\n";
            queryString = queryString + "                   FROM            ModuleDetails CROSS JOIN Locations " + "\r\n";
            queryString = queryString + "                   WHERE           ModuleDetails.ControlTypeID <> 0 OR (ModuleDetails.ControlTypeID = 0 AND Locations.LocationID = 1); " + "\r\n";

            queryString = queryString + "                   INSERT INTO     UserGroupReports (UserGroupID, ReportID, Enabled) " + "\r\n";
            queryString = queryString + "                   SELECT          @UserGroupID, ReportID, 0 AS Enabled " + "\r\n";
            queryString = queryString + "                   FROM            Reports " + "\r\n";

            queryString = queryString + "                   SELECT          @UserGroupID AS UserGroupID " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Thêm mới trùng tên.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserGroupAdd", queryString);
        }

        private void UserGroupRename()
        {
            string queryString = " @UserGroupID int, @Code nvarchar(60), @Name nvarchar(100), @Description nvarchar(100) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(UserGroupID) FROM UserGroups WHERE (Code = @Code OR Name = @Name) AND UserGroupID <> @UserGroupID) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   UPDATE          UserGroups SET Code = @Code, Name = @Name, Description = @Description WHERE UserGroupID = @UserGroupID; " + "\r\n";
            queryString = queryString + "                   SELECT          @UserGroupID AS UserGroupID " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Trùng tên group.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserGroupRename", queryString);
        }

        private void UserGroupRemove()
        {
            string queryString = " @UserGroupID int, @Code nvarchar(256)" + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           DECLARE     @FoundEntitys TABLE (FoundEntity int NULL) " + "\r\n";
            queryString = queryString + "           INSERT INTO @FoundEntitys EXEC UserGroupEditable @UserGroupID " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(*) FROM @FoundEntitys WHERE NOT FoundEntity IS NULL) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DELETE FROM     UserGroupReports WHERE UserGroupID = @UserGroupID " + "\r\n";
            queryString = queryString + "                   DELETE FROM     UserGroupControls WHERE UserGroupID = @UserGroupID " + "\r\n";
            queryString = queryString + "                   DELETE FROM     UserGroupDetails WHERE UserGroupID = @UserGroupID " + "\r\n";
            queryString = queryString + "                   DELETE FROM     UserGroups WHERE UserGroupID = @UserGroupID " + "\r\n";
            queryString = queryString + "                   EXEC            SubmitUserAccessControls " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Không thể xóa ' + @Code + '. Vui lòng remove user trước khi xóa group.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserGroupRemove", queryString);
        }








        private void GetUserGroupMembers()
        {
            string queryString;

            queryString = " @UserGroupID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      UserGroupDetails.UserGroupDetailID, UserGroupDetails.UserGroupID, UserGroupDetails.SecurityIdentifier, Users.UserName, N'Chevron Vietnam' AS UserType " + "\r\n";
            queryString = queryString + "       FROM        UserGroupDetails INNER JOIN (SELECT DISTINCT SecurityIdentifier, UserName FROM Users) Users ON UserGroupDetails.UserGroupID = @UserGroupID AND UserGroupDetails.SecurityIdentifier = Users.SecurityIdentifier " + "\r\n";
            queryString = queryString + "       ORDER BY    UserGroupDetails.UserGroupDetailID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserGroupMembers", queryString);
        }

        private void GetUserGroupAvailableMembers()
        {
            string queryString = " @UserGroupID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";
            queryString = queryString + "           SELECT DISTINCT     SecurityIdentifier, UserName, N'Chevron Vietnam' AS UserType FROM Users WHERE SecurityIdentifier NOT IN (SELECT SecurityIdentifier FROM UserGroupDetails WHERE UserGroupID = @UserGroupID) ORDER BY UserName " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserGroupAvailableMembers", queryString);
        }

        private void UserGroupAddMember()
        {
            string queryString = " @UserGroupID int, @SecurityIdentifier nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(SecurityIdentifier) FROM UserGroupDetails WHERE UserGroupID = @UserGroupID AND SecurityIdentifier = @SecurityIdentifier) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE         @UserGroupDetailID Int" + "\r\n";
            queryString = queryString + "                   INSERT INTO     UserGroupDetails (UserGroupID, SecurityIdentifier, EntryDate) VALUES (@UserGroupID, @SecurityIdentifier, GetDate()); " + "\r\n";
            queryString = queryString + "                   SELECT          @UserGroupDetailID = SCOPE_IDENTITY(); " + "\r\n";

            queryString = queryString + "                   EXEC            SubmitUserAccessControls " + "\r\n";

            queryString = queryString + "                   SELECT          @UserGroupDetailID AS UserGroupDetailID " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Đăng ký trùng user.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserGroupAddMember", queryString);
        }

        private void UserGroupRemoveMember()
        {
            string queryString = " @UserGroupDetailID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(*) FROM UserGroupDetails WHERE UserGroupDetailID = @UserGroupDetailID) = 1 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            //                                              WE ALSO CALL THIS BELOW STATEMENT TO REMOVE GROUP MEMBER WHEN UNREGISTER USER. SEE STORED: UserControlUnregister
            queryString = queryString + "                   DELETE FROM     UserGroupDetails WHERE UserGroupDetailID = @UserGroupDetailID; " + "\r\n";
            queryString = queryString + "                   EXEC            SubmitUserAccessControls " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Không thể xóa user khỏi group.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UserGroupRemoveMember", queryString);
        }

    }
}
