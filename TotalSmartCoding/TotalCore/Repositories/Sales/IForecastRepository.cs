using System;
using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Sales
{
    public interface IForecastRepository : IGenericWithDetailRepository<Forecast, ForecastDetail>
    {
    }

    public interface IForecastAPIRepository : IGenericAPIRepository
    {
    }
}

