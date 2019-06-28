using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Generals
{
    public interface IUserControlRepository : IGenericRepository<User>
    {
    }

    public interface IUserControlAPIRepository : IGenericAPIRepository
    {
        IList<UserControlGroup> GetUserControlGroups(string securityIdentifier);
        IList<UserControlAvailableGroup> GetUserControlAvailableGroups(string securityIdentifier);

        IList<UserControlSalesperson> GetUserControlSalespersons(string securityIdentifier);
        IList<UserControlAvailableSalesperson> GetUserControlAvailableSalespersons(string securityIdentifier);

        int UserControlAddSalesperson(string securityIdentifier, int? employeeID);
        int UserControlRemoveSalesperson(int? userSalespersonID);





        int UserControlRegister(string firstName, string lastName, string userName, string securityIdentifier);
        int UserControlUnregister(int? userID);
        int UserControlSetAdmin(int? userID, bool? isDatabaseAdmin);
        int UserControlToggleVoid(int? userID, bool? inActive);

        int UpdateUserName(string securityIdentifier, string userName);
        int UpdateOnDataLogs(int onDataLogs);
        int UpdateOnEventLogs(int onEventLogs);
    }
}
