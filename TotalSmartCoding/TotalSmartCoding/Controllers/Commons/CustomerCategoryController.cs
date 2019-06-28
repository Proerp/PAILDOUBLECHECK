using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class CustomerCategoryController : GenericSimpleController<CustomerCategory, CustomerCategoryDTO, CustomerCategoryPrimitiveDTO, CustomerCategoryViewModel>
    {
        public CustomerCategoryController(ICustomerCategoryService customerCategoryService, CustomerCategoryViewModel customerCategoryViewModel)
            : base(customerCategoryService, customerCategoryViewModel)
        {
        }
    }
}
