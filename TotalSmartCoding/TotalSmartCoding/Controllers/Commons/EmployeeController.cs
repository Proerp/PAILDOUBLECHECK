using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class EmployeeController : GenericSimpleController<Employee, EmployeeDTO, EmployeePrimitiveDTO, EmployeeViewModel>
    {
        public EmployeeController(IEmployeeService employeeService, EmployeeViewModel employeeViewModel)
            : base(employeeService, employeeViewModel)
        {
        }
    }
}
