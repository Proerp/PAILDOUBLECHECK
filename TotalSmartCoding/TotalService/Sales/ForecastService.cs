using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Sales;
using TotalCore.Repositories.Sales;
using TotalCore.Services.Sales;

namespace TotalService.Sales
{
    public class ForecastService : GenericWithViewDetailService<Forecast, ForecastDetail, ForecastViewDetail, ForecastDTO, ForecastPrimitiveDTO, ForecastDetailDTO>, IForecastService
    {
        public ForecastService(IForecastRepository forecastRepository)
            : base(forecastRepository, "ForecastPostSaveValidate", "ForecastSaveRelative", null, null, null, "GetForecastViewDetails")
        {
        }

        public override ICollection<ForecastViewDetail> GetViewDetails(int forecastID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("ForecastID", forecastID) };
            return this.GetViewDetails(parameters);
        }
    }
}
