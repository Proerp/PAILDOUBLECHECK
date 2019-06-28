using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class CommodityCategoryController : GenericSimpleController<CommodityCategory, CommodityCategoryDTO, CommodityCategoryPrimitiveDTO, CommodityCategoryViewModel>
    {
        public CommodityCategoryController(ICommodityCategoryService commodityCategoryService, CommodityCategoryViewModel commodityCategoryViewModel)
            : base(commodityCategoryService, commodityCategoryViewModel)
        {
        }
    }
}
