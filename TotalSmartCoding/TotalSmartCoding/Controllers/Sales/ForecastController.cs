using TotalModel.Models;
using TotalDTO.Sales;

using TotalCore.Services.Sales;
using TotalSmartCoding.ViewModels.Sales;

namespace TotalSmartCoding.Controllers.Sales
{
    public class ForecastController : GenericViewDetailController<Forecast, ForecastDetail, ForecastViewDetail, ForecastDTO, ForecastPrimitiveDTO, ForecastDetailDTO, ForecastViewModel>
    {
        public ForecastController(IForecastService forecastService, ForecastViewModel forecastViewModel)
            : base(forecastService, forecastViewModel)
        {
        }
    }
}
