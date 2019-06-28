using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Generals
{
    public interface IOrganizationalUnitRepository : IGenericRepository<OrganizationalUnit>
    {
    }

    public interface IOrganizationalUnitAPIRepository : IGenericAPIRepository
    {
        int OrganizationalUnitAdd(int? locationID, string code, string name);
        int OrganizationalUnitRemove(int? organizationalUnitID, string code);
    }

}
