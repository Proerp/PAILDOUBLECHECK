using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class CustomerCategoryService : GenericService<CustomerCategory, CustomerCategoryDTO, CustomerCategoryPrimitiveDTO>, ICustomerCategoryService
    {
        public CustomerCategoryService(ICustomerCategoryRepository customerCategoryRepository)
            : base(customerCategoryRepository, null, "CustomerCategorySaveRelative")
        {
        }
    }
}
