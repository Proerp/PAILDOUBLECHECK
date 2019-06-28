using TotalModel.Models;

using TotalDTO.Sales;

namespace TotalCore.Services.Sales
{
    public interface IForecastService : IGenericWithViewDetailService<Forecast, ForecastDetail, ForecastViewDetail, ForecastDTO, ForecastPrimitiveDTO, ForecastDetailDTO>
    {
    }
}