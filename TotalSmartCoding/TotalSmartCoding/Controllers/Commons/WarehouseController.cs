using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class WarehouseController : GenericSimpleController<Warehouse, WarehouseDTO, WarehousePrimitiveDTO, WarehouseViewModel>
    {
        public WarehouseController(IWarehouseService warehouseService, WarehouseViewModel warehouseViewModel)
            : base(warehouseService, warehouseViewModel)
        {
        }
    }
}
