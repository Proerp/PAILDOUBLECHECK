using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class TeamService : GenericService<Team, TeamDTO, TeamPrimitiveDTO>, ITeamService
    {
        public TeamService(ITeamRepository teamRepository)
            : base(teamRepository, null, "TeamSaveRelative")
        {
        }
    }
}
