using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalDAL.Repositories.Sales
{
    public class ForecastRepository : GenericWithDetailRepository<Forecast, ForecastDetail>, IForecastRepository
    {
        public ForecastRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "ForecastEditable")
        {
        }
    }








    public class ForecastAPIRepository : GenericAPIRepository, IForecastAPIRepository
    {
        public ForecastAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetForecastIndexes")
        {
        }
    }


}
