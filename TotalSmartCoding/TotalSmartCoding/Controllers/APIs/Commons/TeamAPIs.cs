using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;
using TotalCore.Repositories.Commons;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class TeamAPIs
    {
        private readonly ITeamAPIRepository teamAPIRepository;

        public TeamAPIs(ITeamAPIRepository teamAPIRepository)
        {
            this.teamAPIRepository = teamAPIRepository;
        }


        public ICollection<TeamIndex> GetTeamIndexes()
        {
            return this.teamAPIRepository.GetEntityIndexes<TeamIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<TeamBase> GetTeamBases()
        {
            return this.teamAPIRepository.GetTeamBases();
        }

        public IList<TeamTree> GetTeamTrees()
        {
            return this.teamAPIRepository.GetTeamTrees();
        }
    }
}
