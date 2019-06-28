using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "TeamEditable", null, "TeamDeletable")
        {
        }
    }





    public class TeamAPIRepository : GenericAPIRepository, ITeamAPIRepository
    {
        public TeamAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetTeamIndexes")
        {
        }

        public IList<TeamBase> GetTeamBases()
        {
            return this.TotalSmartCodingEntities.GetTeamBases().ToList();
        }

        public IList<TeamTree> GetTeamTrees()
        {
            return this.TotalSmartCodingEntities.GetTeamTrees().ToList();
        }
    }
}
