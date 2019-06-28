using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class CommoditySettingController : GenericSimpleController<CommoditySetting, CommoditySettingDTO, CommoditySettingPrimitiveDTO, CommoditySettingViewModel>
    {
        public CommoditySettingController(ICommoditySettingService commoditySettingService, CommoditySettingViewModel commoditySettingViewModel)
            : base(commoditySettingService, commoditySettingViewModel)
        {
        }
    }
}
