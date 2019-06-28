using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalSmartCoding.Controllers.APIs.Sales
{
    public class ForecastAPIs
    {
        private readonly IForecastAPIRepository forecastAPIRepository;

        public ForecastAPIs(IForecastAPIRepository forecastAPIRepository)
        {
            this.forecastAPIRepository = forecastAPIRepository;
        }


        public ICollection<ForecastIndex> GetForecastIndexes()
        {
            return this.forecastAPIRepository.GetEntityIndexes<ForecastIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }
    }
}
