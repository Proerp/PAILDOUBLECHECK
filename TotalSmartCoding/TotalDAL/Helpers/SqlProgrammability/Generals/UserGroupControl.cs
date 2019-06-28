using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{
    public class UserGroupControl
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public UserGroupControl(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetUserGroupControls();
            this.GetUserGroupReports();
            this.SubmitUserAccessControls();//MUST CREATE SubmitUserAccessControls BEFORE SaveUserGroupControls, BECAUSE SubmitUserAccessControls WILL BE CALLED BY SaveUserGroupControls

            this.SaveUserGroupControls();
            this.SaveUserGroupReports();
        }

        private void GetUserGroupControls()
        {
            string queryString;

            queryString = " @UserGroupID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      UserGroupControls.UserGroupControlID, Modules.ModuleID, IIF(ModuleDetails.Controller <> N'#', ModuleDetails.Controller, Modules.Code) AS ModuleName, ModuleDetails.ModuleDetailID, ModuleDetails.Name AS ModuleDetailName, UserGroupControls.LocationID, IIF(ModuleDetails.ControlTypeID = 0, ModuleDetails.FullName, Locations.Name) AS LocationName, UserGroupControls.AccessLevel, UserGroupControls.ApprovalPermitted, UserGroupControls.UnApprovalPermitted, UserGroupControls.VoidablePermitted, UserGroupControls.UnVoidablePermitted, UserGroupControls.ShowDiscount " + "\r\n";
            queryString = queryString + "       FROM        UserGroupControls INNER JOIN ModuleDetails ON UserGroupControls.UserGroupID = @UserGroupID AND UserGroupControls.ModuleDetailID = ModuleDetails.ModuleDetailID INNER JOIN Modules ON ModuleDetails.ModuleID = Modules.ModuleID INNER JOIN Locations ON UserGroupControls.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "       WHERE       UserGroupControls.ModuleDetailID NOT IN (" + (int)GlobalEnums.NmvnTaskID.SmartCoding + ")" + "\r\n"; //HARD CODE HERE: TO EXCLUDE: SmartCoding FROM THE CONTROL LIST
            queryString = queryString + "       ORDER BY    Modules.Name, ModuleName, ModuleDetails.SerialID, Locations.LocationID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserGroupControls", queryString);
        }

        private void GetUserGroupReports()
        {
            string queryString;

            queryString = " @UserGroupID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      UserGroupReports.UserGroupReportID, UserGroupReports.ReportID, UPPER(Reports.ReportGroupName) AS ReportGroupName, Reports.ReportName, UserGroupReports.Enabled " + "\r\n";
            queryString = queryString + "       FROM        UserGroupReports INNER JOIN Reports ON UserGroupReports.UserGroupID = @UserGroupID AND UserGroupReports.ReportID = Reports.ReportID " + "\r\n";
            queryString = queryString + "       ORDER BY    Reports.ReportGroupName, Reports.SerialID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserGroupReports", queryString);
        }


        private void SaveUserGroupControls()
        {
            string queryString = " @UserGroupControlID int, @AccessLevel Int, @ApprovalPermitted bit, @UnApprovalPermitted bit, @VoidablePermitted bit, @UnVoidablePermitted bit, @ShowDiscount bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           UPDATE          UserGroupControls " + "\r\n";
            queryString = queryString + "           SET             AccessLevel = @AccessLevel, ApprovalPermitted = @ApprovalPermitted, UnApprovalPermitted = @UnApprovalPermitted, VoidablePermitted = @VoidablePermitted, UnVoidablePermitted = @UnVoidablePermitted, ShowDiscount = @ShowDiscount " + "\r\n";
            queryString = queryString + "           WHERE           UserGroupControlID = @UserGroupControlID " + "\r\n";

            queryString = queryString + "           IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Unknow error: Save User Access Controls. Please exit then open and try again.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           EXEC            SubmitUserAccessControls " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SaveUserGroupControls", queryString);
        }

        private void SaveUserGroupReports()
        {
            string queryString = " @UserGroupReportID int, @Enabled bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           UPDATE          UserGroupReports " + "\r\n";
            queryString = queryString + "           SET             Enabled = @Enabled " + "\r\n";
            queryString = queryString + "           WHERE           UserGroupReportID = @UserGroupReportID " + "\r\n";

            queryString = queryString + "           IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   DECLARE     @msg NVARCHAR(300) = N'Unknow error: Update UserGroupReports. Please exit then open and try again.' ; " + "\r\n";
            queryString = queryString + "                   THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";
            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SaveUserGroupReports", queryString);
        }

        private void SubmitUserAccessControls()
        {
            string queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       BEGIN " + "\r\n";

            queryString = queryString + "           DECLARE     @MasterAccessControls TABLE (UserID int NULL, NMVNTaskID int NULL, OrganizationalUnitID int NULL, AccessLevel int NULL, ApprovalPermitted bit NULL, UnApprovalPermitted bit NULL, VoidablePermitted bit NULL, UnVoidablePermitted bit NULL, ShowDiscount bit NULL) " + "\r\n";

            queryString = queryString + "           INSERT INTO @MasterAccessControls (UserID, NMVNTaskID, OrganizationalUnitID, AccessLevel, ApprovalPermitted, UnApprovalPermitted, VoidablePermitted, UnVoidablePermitted, ShowDiscount) " + "\r\n";
            queryString = queryString + "           SELECT      MASTERUserGroupControls.UserID, MASTERUserGroupControls.ModuleDetailID, MASTERUserGroupControls.OrganizationalUnitID, " + "\r\n";
            queryString = queryString + "                       MAX(MASTERUserGroupControls.AccessLevel) AS MAXAccessLevel, " + "\r\n";
            queryString = queryString + "                       MAX(CAST(MASTERUserGroupControls.ApprovalPermitted AS int)) AS ApprovalPermitted, " + "\r\n";
            queryString = queryString + "                       MAX(CAST(MASTERUserGroupControls.UnApprovalPermitted AS int)) AS UnApprovalPermitted, " + "\r\n";
            queryString = queryString + "                       MAX(CAST(MASTERUserGroupControls.VoidablePermitted AS int)) AS VoidablePermitted, " + "\r\n";
            queryString = queryString + "                       MAX(CAST(MASTERUserGroupControls.UnVoidablePermitted AS int)) AS UnVoidablePermitted, " + "\r\n";
            queryString = queryString + "                       MAX(CAST(MASTERUserGroupControls.ShowDiscount AS int)) AS ShowDiscount" + "\r\n";            
            queryString = queryString + "           FROM        (SELECT     MasterUsers.UserID, UserGroupControls.ModuleDetailID, OrganizationalUnits.OrganizationalUnitID, " + "\r\n";
            //queryString = queryString + "                                   IIF(UserGroupControls.LocationID = MasterUsers.LocationID OR ModuleDetails.ControlTypeID = 0, UserGroupControls.AccessLevel, IIF(UserGroupControls.AccessLevel >= 1, 1, 0) ) AS AccessLevel, " + "\r\n"; //NOW: AccessLevel HAVE A FEATURE CALLED AdvancedPermission: TO ALLOW 1 USER HAVE A 'READONLY ACCESS' TO 'OTHER LOCATION' FROM 'ANY LOCATION'. TO DISABLE THIS: JUST MODIFY THE STATEMENT HERE: SET AccessLevel = 0 WHEN UserGroupControls.LocationID <> MasterUsers.LocationID!!! JUST ONLY HERE - VERY EASY!!!
            queryString = queryString + "                                   IIF(UserGroupControls.LocationID = MasterUsers.LocationID OR ModuleDetails.ControlTypeID = 0, UserGroupControls.AccessLevel, IIF(UserGroupControls.AccessLevel >= 1, 0, 0) ) AS AccessLevel, " + "\r\n"; //NOW: AccessLevel HAVE A FEATURE CALLED AdvancedPermission: TO ALLOW 1 USER HAVE A 'READONLY ACCESS' TO 'OTHER LOCATION' FROM 'ANY LOCATION'. TO DISABLE THIS: JUST MODIFY THE STATEMENT HERE: SET AccessLevel = 0 WHEN UserGroupControls.LocationID <> MasterUsers.LocationID!!! JUST ONLY HERE - VERY EASY!!!
            queryString = queryString + "                                   IIF(UserGroupControls.LocationID = MasterUsers.LocationID, UserGroupControls.ApprovalPermitted, 0) AS ApprovalPermitted, " + "\r\n";
            queryString = queryString + "                                   IIF(UserGroupControls.LocationID = MasterUsers.LocationID, UserGroupControls.UnApprovalPermitted, 0) AS UnApprovalPermitted, " + "\r\n";
            queryString = queryString + "                                   IIF(UserGroupControls.LocationID = MasterUsers.LocationID, UserGroupControls.VoidablePermitted, 0) AS VoidablePermitted, " + "\r\n";
            queryString = queryString + "                                   IIF(UserGroupControls.LocationID = MasterUsers.LocationID, UserGroupControls.UnVoidablePermitted, 0) AS UnVoidablePermitted, " + "\r\n";
            queryString = queryString + "                                   IIF(UserGroupControls.LocationID = MasterUsers.LocationID, UserGroupControls.ShowDiscount, 0) AS ShowDiscount " + "\r\n";
            queryString = queryString + "                       FROM        UserGroupControls " + "\r\n";
            queryString = queryString + "                                   INNER JOIN OrganizationalUnits ON UserGroupControls.LocationID = OrganizationalUnits.LocationID " + "\r\n";
            queryString = queryString + "                                   INNER JOIN ModuleDetails ON UserGroupControls.ModuleDetailID = ModuleDetails.ModuleDetailID " + "\r\n";
            queryString = queryString + "                                   INNER JOIN UserGroupDetails ON UserGroupControls.UserGroupID = UserGroupDetails.UserGroupID " + "\r\n";
            queryString = queryString + "                                   INNER JOIN (SELECT Users.UserID, Users.SecurityIdentifier, UserOUs.LocationID FROM Users INNER JOIN OrganizationalUnits AS UserOUs ON Users.OrganizationalUnitID = UserOUs.OrganizationalUnitID) AS MasterUsers ON UserGroupDetails.SecurityIdentifier = MasterUsers.SecurityIdentifier " + "\r\n";
            queryString = queryString + "                       ) MASTERUserGroupControls " + "\r\n";
            queryString = queryString + "           GROUP BY    MASTERUserGroupControls.UserID, MASTERUserGroupControls.ModuleDetailID, MASTERUserGroupControls.OrganizationalUnitID " + "\r\n";

            queryString = queryString + "           UPDATE      AccessControls " + "\r\n";
            queryString = queryString + "           SET         AccessControls.AccessLevel = 0, AccessControls.ApprovalPermitted = 0, AccessControls.UnApprovalPermitted = 0, " + "\r\n";
            queryString = queryString + "                       AccessControls.VoidablePermitted = 0, AccessControls.UnVoidablePermitted = 0, AccessControls.ShowDiscount = 0 " + "\r\n";
            queryString = queryString + "           FROM        AccessControls " + "\r\n";
            queryString = queryString + "                       LEFT JOIN @MasterAccessControls AS MasterAccessControls ON AccessControls.UserID = MasterAccessControls.UserID AND AccessControls.NMVNTaskID = MasterAccessControls.NMVNTaskID AND AccessControls.OrganizationalUnitID = MasterAccessControls.OrganizationalUnitID " + "\r\n";
            queryString = queryString + "           WHERE       MasterAccessControls.UserID IS NULL " + "\r\n";

            queryString = queryString + "           UPDATE      AccessControls " + "\r\n";
            queryString = queryString + "           SET         AccessControls.AccessLevel = MasterAccessControls.AccessLevel, AccessControls.ApprovalPermitted = MasterAccessControls.ApprovalPermitted, AccessControls.UnApprovalPermitted = MasterAccessControls.UnApprovalPermitted, " + "\r\n";
            queryString = queryString + "                       AccessControls.VoidablePermitted = MasterAccessControls.VoidablePermitted, AccessControls.UnVoidablePermitted = MasterAccessControls.UnVoidablePermitted, AccessControls.ShowDiscount = MasterAccessControls.ShowDiscount " + "\r\n";
            queryString = queryString + "           FROM        AccessControls " + "\r\n";
            queryString = queryString + "                       INNER JOIN @MasterAccessControls AS MasterAccessControls ON AccessControls.UserID = MasterAccessControls.UserID AND AccessControls.NMVNTaskID = MasterAccessControls.NMVNTaskID AND AccessControls.OrganizationalUnitID = MasterAccessControls.OrganizationalUnitID " + "\r\n";

            queryString = queryString + "       END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SubmitUserAccessControls", queryString);
        }

    }
}
