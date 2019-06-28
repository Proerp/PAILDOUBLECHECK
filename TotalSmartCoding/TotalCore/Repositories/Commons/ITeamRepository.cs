using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ITeamRepository : IGenericRepository<Team>
    {

    }

    public interface ITeamAPIRepository : IGenericAPIRepository
    {
        IList<TeamBase> GetTeamBases();
        IList<TeamTree> GetTeamTrees();
    }
}
