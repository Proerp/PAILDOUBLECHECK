using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Commons;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class CommoditySettingAPIs
    {
        private readonly ICommoditySettingAPIRepository commoditySettingAPIRepository;

        public CommoditySettingAPIs(ICommoditySettingAPIRepository commoditySettingAPIRepository)
        {
            this.commoditySettingAPIRepository = commoditySettingAPIRepository;
        }


        public ICollection<CommoditySettingIndex> GetCommoditySettingIndexes()
        {
            return this.commoditySettingAPIRepository.GetEntityIndexes<CommoditySettingIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }
    }
}
