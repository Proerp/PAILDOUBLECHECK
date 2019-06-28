using System.ComponentModel;
using System.Collections.Generic;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;

namespace TotalDTO.Commons
{
    public class CustomerCategoryPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.CustomerCategories; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.CustomerCategoryID; }
        public void SetID(int id) { this.CustomerCategoryID = id; }


        private int customerCategoryID;
        [DefaultValue(0)]
        public int CustomerCategoryID
        {
            get { return this.customerCategoryID; }
            set { ApplyPropertyChange<CustomerCategoryPrimitiveDTO, int>(ref this.customerCategoryID, o => o.CustomerCategoryID, value); }
        }


        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<CustomerCategoryPrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerCategoryPrimitiveDTO>(p => p.Name), "Vui lòng nhập tên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            return validationRules;
        }
    }

    public class CustomerCategoryDTO : CustomerCategoryPrimitiveDTO
    {
    }
}
