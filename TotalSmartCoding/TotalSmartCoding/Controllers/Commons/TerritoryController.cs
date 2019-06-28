using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class TerritoryController : GenericSimpleController<Territory, TerritoryDTO, TerritoryPrimitiveDTO, TerritoryViewModel>
    {
        public TerritoryController(ITerritoryService territoryService, TerritoryViewModel territoryViewModel)
            : base(territoryService, territoryViewModel)
        {
        }
    }
}
