using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class CommoditySettingService : GenericWithDetailService<CommoditySetting, CommoditySettingDetail, CommoditySettingDTO, CommoditySettingPrimitiveDTO, CommoditySettingDetailDTO>, ICommoditySettingService
    {
        public CommoditySettingService(ICommoditySettingRepository commoditySettingRepository)
            : base(commoditySettingRepository)
        {
        }
    }
}