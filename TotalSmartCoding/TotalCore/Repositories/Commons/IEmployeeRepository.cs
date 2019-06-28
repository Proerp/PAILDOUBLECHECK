using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }

    public interface IEmployeeAPIRepository : IGenericAPIRepository
    {
        IList<EmployeeBase> GetEmployeeBases(int? userID, int? nmvnTaskID, int? roleID);
        IList<EmployeeTree> GetEmployeeTrees();
    }
}
