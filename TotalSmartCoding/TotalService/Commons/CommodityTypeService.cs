using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class CommodityTypeService : GenericService<CommodityType, CommodityTypeDTO, CommodityTypePrimitiveDTO>, ICommodityTypeService
    {
        public CommodityTypeService(ICommodityTypeRepository commodityTypeRepository)
            : base(commodityTypeRepository, null, "CommodityTypeSaveRelative")
        {
        }
    }
}
