using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Commons;

namespace TotalDAL.Repositories.Commons
{
    public class CommoditySettingRepository : GenericWithDetailRepository<CommoditySetting, CommoditySettingDetail>, ICommoditySettingRepository
    {
        public CommoditySettingRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "CommoditySettingEditable", null, "CommoditySettingDeletable")
        {
        }
    }








    public class CommoditySettingAPIRepository : GenericAPIRepository, ICommoditySettingAPIRepository
    {
        public CommoditySettingAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetCommoditySettingIndexes")
        {
        }
    }


}
