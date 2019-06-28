using System.ComponentModel;
using System.Collections.Generic;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;

namespace TotalDTO.Commons
{
    public class CustomerTypePrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.CustomerTypes; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.CustomerTypeID; }
        public void SetID(int id) { this.CustomerTypeID = id; }


        private int customerTypeID;
        [DefaultValue(0)]
        public int CustomerTypeID
        {
            get { return this.customerTypeID; }
            set { ApplyPropertyChange<CustomerTypePrimitiveDTO, int>(ref this.customerTypeID, o => o.CustomerTypeID, value); }
        }


        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<CustomerTypePrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerTypePrimitiveDTO>(p => p.Name), "Vui lòng nhập tên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            return validationRules;
        }
    }

    public class CustomerTypeDTO : CustomerTypePrimitiveDTO
    {
    }
}
