using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Generals;

namespace TotalDAL.Repositories.Generals
{
    public class OrganizationalUnitRepository : GenericRepository<OrganizationalUnit>, IOrganizationalUnitRepository
    {
        public OrganizationalUnitRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "OrganizationalUnitEditable")
        {
        }
    }





    public class OrganizationalUnitAPIRepository : GenericAPIRepository, IOrganizationalUnitAPIRepository
    {
        public OrganizationalUnitAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetOrganizationalUnitIndexes")
        {
        }

        public int OrganizationalUnitAdd(int? locationID, string code, string name)
        {
            return this.TotalSmartCodingEntities.OrganizationalUnitAdd(locationID, code, name);
        }

        public int OrganizationalUnitRemove(int? organizationalUnitID, string code)
        {
            return this.TotalSmartCodingEntities.OrganizationalUnitRemove(organizationalUnitID, code);
        }
    }
}
