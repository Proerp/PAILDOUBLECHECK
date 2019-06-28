using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Generals;

namespace TotalDAL.Repositories.Generals
{
    public class UserGroupRepository : GenericRepository<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "UserGroupEditable")
        {
        }
    }





    public class UserGroupAPIRepository : GenericAPIRepository, IUserGroupAPIRepository
    {
        public UserGroupAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetUserGroupIndexes")
        {
        }

        public int UserGroupAdd(string code, string name, string description)
        {
            return (int)this.TotalSmartCodingEntities.UserGroupAdd(code, name, description).FirstOrDefault();
        }

        public int UserGroupRename(int? userGroupID, string code, string name, string description)
        {
            return (int)this.TotalSmartCodingEntities.UserGroupRename(userGroupID, code, name, description).FirstOrDefault();
        }

        public int UserGroupRemove(int? userGroupID, string code)
        {
            return this.TotalSmartCodingEntities.UserGroupRemove(userGroupID, code);
        }

        public int UserGroupAddMember(int? userGroupID, string securityIdentifier)
        {
            return (int)this.TotalSmartCodingEntities.UserGroupAddMember(userGroupID, securityIdentifier).FirstOrDefault();
        }

        public int UserGroupRemoveMember(int? userGroupDetailID)
        {
            return this.TotalSmartCodingEntities.UserGroupRemoveMember(userGroupDetailID);
        }

        public IList<UserGroupAvailableMember> GetUserGroupAvailableMembers(int? userGroupID)
        {
            return this.TotalSmartCodingEntities.GetUserGroupAvailableMembers(userGroupID).ToList();
        }

        public IList<UserGroupMember> GetUserGroupMembers(int? userGroupID)
        {
            return this.TotalSmartCodingEntities.GetUserGroupMembers(userGroupID).ToList();
        }

        public IList<UserGroupControl> GetUserGroupControls(int? userGroupID)
        {
            return this.TotalSmartCodingEntities.GetUserGroupControls(userGroupID).ToList();
        }

        public IList<UserGroupReport> GetUserGroupReports(int? userGroupID)
        {
            return this.TotalSmartCodingEntities.GetUserGroupReports(userGroupID).ToList();
        }

        public int SaveUserGroupControls(int? userGroupControlID, int? accessLevel, bool? approvalPermitted, bool? unApprovalPermitted, bool? voidablePermitted, bool? unVoidablePermitted, bool? showDiscount)
        {
            return this.TotalSmartCodingEntities.SaveUserGroupControls(userGroupControlID, accessLevel, approvalPermitted, unApprovalPermitted, voidablePermitted, unVoidablePermitted, showDiscount);
        }

        public int SaveUserGroupReports(int? userGroupReportID, bool? enabled)
        {
            return this.TotalSmartCodingEntities.SaveUserGroupReports(userGroupReportID, enabled);
        }
    }
}
