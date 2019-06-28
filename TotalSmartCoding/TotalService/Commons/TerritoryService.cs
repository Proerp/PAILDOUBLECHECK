using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class TerritoryService : GenericService<Territory, TerritoryDTO, TerritoryPrimitiveDTO>, ITerritoryService
    {
        public TerritoryService(ITerritoryRepository territoryRepository)
            : base(territoryRepository, "TerritoryPostSaveValidate", "TerritorySaveRelative")
        {
        }
    }
}
