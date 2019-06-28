using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class CustomerTypeService : GenericService<CustomerType, CustomerTypeDTO, CustomerTypePrimitiveDTO>, ICustomerTypeService
    {
        public CustomerTypeService(ICustomerTypeRepository customerTypeRepository)
            : base(customerTypeRepository, null, "CustomerTypeSaveRelative")
        {
        }
    }
}
