using TotalModel.Models;

using TotalDTO.Commons;

namespace TotalCore.Services.Commons
{
    public interface IEmployeeService : IGenericService<Employee, EmployeeDTO, EmployeePrimitiveDTO>
    {
    }
}
