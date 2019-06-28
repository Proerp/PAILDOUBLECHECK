using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface IWarehouseRepository : IGenericRepository<Warehouse>
    {

    }

    public interface IWarehouseAPIRepository : IGenericAPIRepository
    {
        WarehouseBase GetWarehouseBase(string code);
        WarehouseBase GetWarehouseBase(int warehouseID);
        IList<WarehouseBase> GetWarehouseBases();
        IList<WarehouseTree> GetWarehouseTrees(int? locationID);

        int? GetWarehouseLocationID(int? warehouseID);
    }
}
