using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalCore.Repositories.Generals;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class OrganizationalUnitAPIs
    {
        private readonly IOrganizationalUnitAPIRepository organizationalUnitAPIRepository;

        public OrganizationalUnitAPIs(IOrganizationalUnitAPIRepository organizationalUnitAPIRepository)
        {
            this.organizationalUnitAPIRepository = organizationalUnitAPIRepository;
        }


        public ICollection<OrganizationalUnitIndex> GetOrganizationalUnitIndexes()
        {
            return this.organizationalUnitAPIRepository.GetEntityIndexes<OrganizationalUnitIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public int OrganizationalUnitAdd(int? locationID, string code, string name)
        {
            return this.organizationalUnitAPIRepository.OrganizationalUnitAdd(locationID, code, name);
        }

        public int OrganizationalUnitRemove(int? organizationalUnitID, string code)
        {
            return this.organizationalUnitAPIRepository.OrganizationalUnitRemove(organizationalUnitID, code);
        }

    }
}