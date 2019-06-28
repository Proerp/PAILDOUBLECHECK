using System.ComponentModel;
using System.Collections.Generic;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;

namespace TotalDTO.Commons
{
    public class TeamPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Teams; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.TeamID; }
        public void SetID(int id) { this.TeamID = id; }


        private int teamID;
        [DefaultValue(0)]
        public int TeamID
        {
            get { return this.teamID; }
            set { ApplyPropertyChange<TeamPrimitiveDTO, int>(ref this.teamID, o => o.TeamID, value); }
        }


        public string Code { get { return this.Name; } }

        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<TeamPrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<TeamPrimitiveDTO>(p => p.Code), "Vui lòng nhập mã.", delegate { return (this.Code != null && this.Code.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<TeamPrimitiveDTO>(p => p.Name), "Vui lòng nhập tên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            return validationRules;
        }
    }

    public class TeamDTO : TeamPrimitiveDTO
    {
    }
}
