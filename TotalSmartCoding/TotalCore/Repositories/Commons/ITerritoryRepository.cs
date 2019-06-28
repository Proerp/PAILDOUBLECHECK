using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ITerritoryRepository : IGenericRepository<Territory>
    {

    }

    public interface ITerritoryAPIRepository : IGenericAPIRepository
    {
        IList<TerritoryBase> GetTerritoryBases();
    }
}
