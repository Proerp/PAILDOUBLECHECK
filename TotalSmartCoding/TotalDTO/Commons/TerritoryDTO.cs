using System.ComponentModel;
using System.Collections.Generic;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;

namespace TotalDTO.Commons
{
    public class TerritoryPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Territories; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.TerritoryID; }
        public void SetID(int id) { this.TerritoryID = id; }


        private int territoryID;
        [DefaultValue(0)]
        public int TerritoryID
        {
            get { return this.territoryID; }
            set { ApplyPropertyChange<TerritoryPrimitiveDTO, int>(ref this.territoryID, o => o.TerritoryID, value); }
        }


        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<TerritoryPrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<TerritoryPrimitiveDTO>(p => p.Name), "Vui lòng nhập tên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            return validationRules;
        }
    }

    public class TerritoryDTO : TerritoryPrimitiveDTO
    {
    }
}
