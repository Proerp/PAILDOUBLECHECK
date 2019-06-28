using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class TeamController : GenericSimpleController<Team, TeamDTO, TeamPrimitiveDTO, TeamViewModel>
    {
        public TeamController(ITeamService teamService, TeamViewModel teamViewModel)
            : base(teamService, teamViewModel)
        {
        }
    }
}
