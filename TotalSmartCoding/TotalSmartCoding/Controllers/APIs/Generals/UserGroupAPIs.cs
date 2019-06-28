using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalDTO.Generals;
using TotalModel.Models;

using TotalCore.Repositories.Generals;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class UserGroupAPIs
    {
        private readonly IUserGroupAPIRepository userGroupAPIRepository;

        public UserGroupAPIs(IUserGroupAPIRepository userGroupAPIRepository)
        {
            this.userGroupAPIRepository = userGroupAPIRepository;
        }


        public ICollection<UserGroupIndex> GetUserGroupIndexes()
        {
            return this.userGroupAPIRepository.GetEntityIndexes<UserGroupIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public int UserGroupAdd(string code, string name, string description)
        {
            int userGroupID = this.userGroupAPIRepository.UserGroupAdd(code, name, description);

            this.AddDataLogs("Add new group", userGroupID, code, name, description);

            return userGroupID;
        }

        public int UserGroupRename(int userGroupID, string code, string name, string description)
        {
            this.userGroupAPIRepository.UserGroupRename(userGroupID, code, name, description);

            this.AddDataLogs("Rename group", userGroupID, code, name, description);

            return userGroupID;
        }

        public int UserGroupRemove(int userGroupID, string code, string name, string description)
        {
            int affectedRows = this.userGroupAPIRepository.UserGroupRemove(userGroupID, code);

            this.AddDataLogs("Delete group", userGroupID, code, name, description);

            return affectedRows;
        }

        public int UserGroupAddMember(int userGroupID, string userGroupName, string securityIdentifier, string userName)
        {
            int userGroupDetailID = this.userGroupAPIRepository.UserGroupAddMember(userGroupID, securityIdentifier);

            this.AddDataLogs("Add new group member", userGroupDetailID, userGroupID, userGroupName, securityIdentifier, userName);

            return userGroupDetailID;
        }

        public int UserGroupRemoveMember(int userGroupDetailID, int userGroupID, string userGroupName, string securityIdentifier, string userName)
        {
            int affectedRows = this.userGroupAPIRepository.UserGroupRemoveMember(userGroupDetailID);

            this.AddDataLogs("Remove group member", userGroupDetailID, userGroupID, userGroupName, securityIdentifier, userName);

            return affectedRows;
        }

        public IList<UserGroupAvailableMember> GetUserGroupAvailableMembers(int? userGroupID)
        {
            return this.userGroupAPIRepository.GetUserGroupAvailableMembers(userGroupID);
        }

        public IList<UserGroupMember> GetUserGroupMembers(int? userGroupID)
        {
            return this.userGroupAPIRepository.GetUserGroupMembers(userGroupID);
        }

        public IList<UserGroupControl> GetUserGroupControls(int? userGroupID)
        {
            return this.userGroupAPIRepository.GetUserGroupControls(userGroupID);
        }

        public IList<UserGroupReport> GetUserGroupReports(int? userGroupID)
        {
            return this.userGroupAPIRepository.GetUserGroupReports(userGroupID);
        }

        public int SaveUserGroupControls(UserGroupIndex userGroupIndex, UserGroupControlDTO userGroupControlDTO, string propertyName)
        {
            int affectedRows = this.userGroupAPIRepository.SaveUserGroupControls(userGroupControlDTO.UserGroupControlID, userGroupControlDTO.AccessLevel, userGroupControlDTO.ApprovalPermitted, userGroupControlDTO.UnApprovalPermitted, userGroupControlDTO.VoidablePermitted, userGroupControlDTO.UnVoidablePermitted, userGroupControlDTO.ShowDiscount);

            if (propertyName == "Editable" || propertyName == "ApprovalPermitted" || propertyName == "UnApprovalPermitted" || propertyName == "VoidablePermitted" || propertyName == "UnVoidablePermitted")
            {
                string propertyValue = "";
                if (propertyName == "Editable") { propertyName = "AccessLevel"; propertyValue = ((GlobalEnums.AccessLevel)userGroupControlDTO.AccessLevel).ToString(); }
                if (propertyName == "ApprovalPermitted") { propertyName = "Verify"; propertyValue = userGroupControlDTO.ApprovalPermitted ? "Allowed" : "Disallowed"; }
                if (propertyName == "UnApprovalPermitted") { propertyName = "Unverify"; propertyValue = userGroupControlDTO.UnApprovalPermitted ? "Allowed" : "Disallowed"; }
                if (propertyName == "VoidablePermitted") { propertyName = "Void"; propertyValue = userGroupControlDTO.VoidablePermitted ? "Allowed" : "Disallowed"; }
                if (propertyName == "UnVoidablePermitted") { propertyName = "Unvoid"; propertyValue = userGroupControlDTO.UnVoidablePermitted ? "Allowed" : "Disallowed"; }

                this.AddDataLogs("Update access control", userGroupControlDTO.UserGroupControlID, userGroupIndex.UserGroupID, userGroupIndex.Name, userGroupControlDTO.ModuleDetailID, userGroupControlDTO.ModuleDetailName, userGroupControlDTO.LocationID, userGroupControlDTO.LocationName, propertyName, propertyValue);
            }

            return affectedRows;
        }

        public int SaveUserGroupReports(int userGroupReportID, int userGroupID, string userGroupName, int reportID, string reportName, bool enabled)
        {
            int affectedRows = this.userGroupAPIRepository.SaveUserGroupReports(userGroupReportID, enabled);

            this.AddDataLogs("Update report control", userGroupReportID, userGroupID, userGroupName, reportID, reportName, enabled);

            return affectedRows;
        }


        private void AddDataLogs(string actionType, int userGroupID, string userGroupCode, string userGroupName, string description)
        {
            if (!this.userGroupAPIRepository.GetOnDataLogs()) return;// DO NOTHING

            DateTime entryDate = DateTime.Now;

            this.userGroupAPIRepository.AddDataLogs(userGroupID, null, entryDate, "UserControls", actionType, "UserGroup", "UserGroupID", userGroupID.ToString());
            this.userGroupAPIRepository.AddDataLogs(userGroupID, null, entryDate, "UserControls", actionType, "UserGroup", "UserGroupCode", userGroupCode);
            this.userGroupAPIRepository.AddDataLogs(userGroupID, null, entryDate, "UserControls", actionType, "UserGroup", "UserGroupName", userGroupName);
            this.userGroupAPIRepository.AddDataLogs(userGroupID, null, entryDate, "UserControls", actionType, "UserGroup", "Description", description);
        }

        private void AddDataLogs(string actionType, int userGroupDetailID, int userGroupID, string userGroupName, string securityIdentifier, string userName)
        {
            if (!this.userGroupAPIRepository.GetOnDataLogs()) return;// DO NOTHING

            DateTime entryDate = DateTime.Now;

            this.userGroupAPIRepository.AddDataLogs(userGroupDetailID, null, entryDate, "UserControls", actionType, "UserGroupDetail", "UserGroupID", userGroupID.ToString());
            this.userGroupAPIRepository.AddDataLogs(userGroupDetailID, null, entryDate, "UserControls", actionType, "UserGroupDetail", "UserGroupName", userGroupName);
            this.userGroupAPIRepository.AddDataLogs(userGroupDetailID, null, entryDate, "UserControls", actionType, "UserGroupDetail", "SecurityIdentifier", securityIdentifier);
            this.userGroupAPIRepository.AddDataLogs(userGroupDetailID, null, entryDate, "UserControls", actionType, "UserGroupDetail", "UserName", userName);
        }

        private void AddDataLogs(string actionType, int userGroupReportID, int userGroupID, string userGroupName, int reportID, string reportName, bool enabled)
        {
            if (!this.userGroupAPIRepository.GetOnDataLogs()) return;// DO NOTHING

            DateTime entryDate = DateTime.Now;

            this.userGroupAPIRepository.AddDataLogs(userGroupReportID, null, entryDate, "UserControls", actionType, "ReportControl", "UserGroupID", userGroupID.ToString());
            this.userGroupAPIRepository.AddDataLogs(userGroupReportID, null, entryDate, "UserControls", actionType, "ReportControl", "UserGroupName", userGroupName);
            this.userGroupAPIRepository.AddDataLogs(userGroupReportID, null, entryDate, "UserControls", actionType, "ReportControl", "ReportID", reportID.ToString());
            this.userGroupAPIRepository.AddDataLogs(userGroupReportID, null, entryDate, "UserControls", actionType, "ReportControl", "ReportName", reportName);
            this.userGroupAPIRepository.AddDataLogs(userGroupReportID, null, entryDate, "UserControls", actionType, "ReportControl", "Enabled", enabled ? "Enabled" : "Disabled");
        }

        private void AddDataLogs(string actionType, int userGroupControlID, int userGroupID, string userGroupName, int moduleDetailID, string moduleDetailName, int locationID, string locationName, string propertyName, string propertyValue)
        {
            if (!this.userGroupAPIRepository.GetOnDataLogs()) return;// DO NOTHING

            DateTime entryDate = DateTime.Now;

            this.userGroupAPIRepository.AddDataLogs(userGroupControlID, null, entryDate, "UserControls", actionType, "AccessControl", "UserGroupID", userGroupID.ToString());
            this.userGroupAPIRepository.AddDataLogs(userGroupControlID, null, entryDate, "UserControls", actionType, "AccessControl", "UserGroupName", userGroupName);
            this.userGroupAPIRepository.AddDataLogs(userGroupControlID, null, entryDate, "UserControls", actionType, "AccessControl", "ModuleDetailID", moduleDetailID.ToString());
            this.userGroupAPIRepository.AddDataLogs(userGroupControlID, null, entryDate, "UserControls", actionType, "AccessControl", "ModuleDetailName", moduleDetailName);
            if (locationName != "")
            {
                this.userGroupAPIRepository.AddDataLogs(userGroupControlID, null, entryDate, "UserControls", actionType, "AccessControl", "LocationID", locationName != "" ? locationID.ToString() : "");
                this.userGroupAPIRepository.AddDataLogs(userGroupControlID, null, entryDate, "UserControls", actionType, "AccessControl", "LocationName", locationName);
            }
            this.userGroupAPIRepository.AddDataLogs(userGroupControlID, null, entryDate, "UserControls", actionType, "AccessControl", propertyName, propertyValue);
        }

    }
}