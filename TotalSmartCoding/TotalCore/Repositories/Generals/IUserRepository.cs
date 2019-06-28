using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Generals
{
    public interface IUserRepository : IGenericRepository<User>
    {

    }

    public interface IUserAPIRepository : IGenericAPIRepository
    {
        List<UserTree> GetUserTrees(int? activeOption);

        IList<ActiveUser> GetActiveUsers(string securityIdentifier);

        IList<UserAccessControl> GetUserAccessControls(int? userID, int? nmvnTaskID);

        int UserRegister(int? locationID, int? organizationalUnitID, string firstName, string lastName, string userName, string securityIdentifier, int? sameOUAccessLevel, int? sameLocationAccessLevel, int? otherOUAccessLevel);
        int UserUnregister(int? userID, string userName, string organizationalUnitName);
        int UserToggleVoid(int? userID, bool? inActive);

        int UpdateUserName(string securityIdentifier, string userName);

        int SaveUserAccessControls(int? accessControlID, int? accessLevel, bool? approvalPermitted, bool? unApprovalPermitted, bool? voidablePermitted, bool? unVoidablePermitted, bool? showDiscount);
    }
}
