using System.ComponentModel;
using System.Collections.Generic;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;

namespace TotalDTO.Commons
{
    public class CommodityCategoryPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.CommodityCategories; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.CommodityCategoryID; }
        public void SetID(int id) { this.CommodityCategoryID = id; }


        private int commodityCategoryID;
        [DefaultValue(0)]
        public int CommodityCategoryID
        {
            get { return this.commodityCategoryID; }
            set { ApplyPropertyChange<CommodityCategoryPrimitiveDTO, int>(ref this.commodityCategoryID, o => o.CommodityCategoryID, value); }
        }


        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<CommodityCategoryPrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityCategoryPrimitiveDTO>(p => p.Name), "Vui lòng nhập tên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            return validationRules;
        }
    }

    public class CommodityCategoryDTO : CommodityCategoryPrimitiveDTO
    {
    }
}
