using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class CustomerTypeController : GenericSimpleController<CustomerType, CustomerTypeDTO, CustomerTypePrimitiveDTO, CustomerTypeViewModel>
    {
        public CustomerTypeController(ICustomerTypeService customerTypeService, CustomerTypeViewModel customerTypeViewModel)
            : base(customerTypeService, customerTypeViewModel)
        {
        }
    }
}
