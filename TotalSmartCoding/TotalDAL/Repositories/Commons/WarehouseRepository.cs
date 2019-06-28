﻿using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "WarehouseEditable", null, "WarehouseDeletable")
        {
        }
    }





    public class WarehouseAPIRepository : GenericAPIRepository, IWarehouseAPIRepository
    {
        public WarehouseAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetWarehouseIndexes")
        {
        }

        public WarehouseBase GetWarehouseBase(int warehouseID)
        {
            return this.TotalSmartCodingEntities.GetWarehouseBase(warehouseID).FirstOrDefault();
        }

        public WarehouseBase GetWarehouseBase(string code)
        {
            return this.TotalSmartCodingEntities.GetWarehouseBaseByCode(code).FirstOrDefault();
        }

        public IList<WarehouseBase> GetWarehouseBases()
        {
            return this.TotalSmartCodingEntities.GetWarehouseBases().ToList();
        }

        public IList<WarehouseTree> GetWarehouseTrees(int? locationID)
        {
            return this.TotalSmartCodingEntities.GetWarehouseTrees(locationID).ToList();
        }

        public int? GetWarehouseLocationID(int? warehouseID)
        {
            return this.TotalSmartCodingEntities.GetWarehouseLocationID(warehouseID).Single();
        }
    }
}
