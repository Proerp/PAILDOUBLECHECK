using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class CommodityCategoryService : GenericService<CommodityCategory, CommodityCategoryDTO, CommodityCategoryPrimitiveDTO>, ICommodityCategoryService
    {
        public CommodityCategoryService(ICommodityCategoryRepository commodityCategoryRepository)
            : base(commodityCategoryRepository, null, "CommodityCategorySaveRelative")
        {
        }
    }
}
