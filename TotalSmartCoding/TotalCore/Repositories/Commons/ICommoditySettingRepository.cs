using System;
using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICommoditySettingRepository : IGenericWithDetailRepository<CommoditySetting, CommoditySettingDetail>
    {
    }

    public interface ICommoditySettingAPIRepository : IGenericAPIRepository
    {
    }
}