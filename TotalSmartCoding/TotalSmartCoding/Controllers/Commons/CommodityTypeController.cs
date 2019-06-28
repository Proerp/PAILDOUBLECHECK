using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class CommodityTypeController : GenericSimpleController<CommodityType, CommodityTypeDTO, CommodityTypePrimitiveDTO, CommodityTypeViewModel>
    {
        public CommodityTypeController(ICommodityTypeService commodityTypeService, CommodityTypeViewModel commodityTypeViewModel)
            : base(commodityTypeService, commodityTypeViewModel)
        {
        }
    }
}
