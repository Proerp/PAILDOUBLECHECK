using System.ComponentModel;
using System.Collections.Generic;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;

namespace TotalDTO.Commons
{
    public class CommodityTypePrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.CommodityTypes; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.CommodityTypeID; }
        public void SetID(int id) { this.CommodityTypeID = id; }


        private int commodityTypeID;
        [DefaultValue(0)]
        public int CommodityTypeID
        {
            get { return this.commodityTypeID; }
            set { ApplyPropertyChange<CommodityTypePrimitiveDTO, int>(ref this.commodityTypeID, o => o.CommodityTypeID, value); }
        }


        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<CommodityTypePrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityTypePrimitiveDTO>(p => p.Name), "Vui lòng nhập tên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            return validationRules;
        }
    }

    public class CommodityTypeDTO : CommodityTypePrimitiveDTO
    {
    }
}
