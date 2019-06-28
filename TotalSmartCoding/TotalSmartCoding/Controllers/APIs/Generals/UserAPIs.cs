using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;
using TotalCore.Repositories.Generals;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class UserAPIs
    {
        private readonly IUserAPIRepository userAPIRepository;

        public UserAPIs(IUserAPIRepository userAPIRepository)
        {
            this.userAPIRepository = userAPIRepository;
        }


        public ICollection<UserIndex> GetUserIndexes(GlobalEnums.ActiveOption activeOption)
        {
            this.userAPIRepository.RepositoryBag["ActiveOption"] = (int)activeOption;
            return this.userAPIRepository.GetEntityIndexes<UserIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public List<UserTree> GetUserTrees(GlobalEnums.ActiveOption activeOption)
        {
            return this.userAPIRepository.GetUserTrees((int)activeOption);
        }

        public IList<ActiveUser> GetActiveUsers(string securityIdentifier)
        {
            return this.userAPIRepository.GetActiveUsers(securityIdentifier);
        }

        public IList<UserAccessControl> GetUserAccessControls(int? userID, int? nmvnTaskID)
        {
            return this.userAPIRepository.GetUserAccessControls(userID, nmvnTaskID);
        }

        public int UserRegister(int? locationID, int? organizationalUnitID, string firstName, string lastName, string userName, string securityIdentifier, int? sameOUAccessLevel, int? sameLocationAccessLevel, int? otherOUAccessLevel)
        {
            return this.userAPIRepository.UserRegister(locationID, organizationalUnitID, firstName, lastName, userName, securityIdentifier, sameOUAccessLevel, sameLocationAccessLevel, otherOUAccessLevel);
        }

        public int UserUnregister(int? userID, string userName, string organizationalUnitName)
        {
            return this.userAPIRepository.UserUnregister(userID, userName, organizationalUnitName);
        }

        public int UserToggleVoid(int? userID, bool? inActive)
        {
            return this.userAPIRepository.UserToggleVoid(userID, inActive);
        }

        public int UpdateUserName(string securityIdentifier, string userName)
        {
            return this.userAPIRepository.UpdateUserName(securityIdentifier, userName);
        }

        public int SaveUserAccessControls(int? accessControlID, int? accessLevel, bool? approvalPermitted, bool? unApprovalPermitted, bool? voidablePermitted, bool? unVoidablePermitted, bool? showDiscount)
        {
            return this.userAPIRepository.SaveUserAccessControls(accessControlID, accessLevel, approvalPermitted, unApprovalPermitted, voidablePermitted, unVoidablePermitted, showDiscount);
        }
    }
}
