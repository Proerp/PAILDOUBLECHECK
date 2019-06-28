using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class WarehouseService : GenericService<Warehouse, WarehouseDTO, WarehousePrimitiveDTO>, IWarehouseService
    {
        public WarehouseService(IWarehouseRepository warehouseRepository)
            : base(warehouseRepository, null, "WarehouseSaveRelative")
        {
        }
    }
}
