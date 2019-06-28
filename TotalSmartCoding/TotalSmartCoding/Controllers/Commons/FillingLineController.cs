using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class FillingLineController : GenericSimpleController<FillingLine, FillingLineDTO, FillingLinePrimitiveDTO, FillingLineViewModel>
    {
        public FillingLineController(IFillingLineService commoditySettingService, FillingLineViewModel commoditySettingViewModel)
            : base(commoditySettingService, commoditySettingViewModel)
        {
        }
    }
}
