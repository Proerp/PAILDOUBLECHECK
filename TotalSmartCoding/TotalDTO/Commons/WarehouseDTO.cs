using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;


namespace TotalDTO.Commons
{
    /// <summary>
    /// NOTES: THIS IWarehouseBaseDTO AND ITS CLASS WarehouseBaseDTO IS USED TO REPRESENT OF FOREIGN RELATIONSHIP OF SalesOrders/ DeliveryAdvices/ GoodsIssues/ SalesReturn TO Wareshouses
    /// AT CURRENT: IN THE DATABASE DESIGN: THE RELATIONSHIP IS MANDATORY (MEANS: THE FOREIGN KEY WarehouseID IN THESES TABLE SalesOrders/ DeliveryAdvices/ GoodsIssues/ SalesReturn ARE REQUIRED - NOT ALLOW NULL)
    /// BUT AT DTO LAYER: WE STILL KEEP:     Nullable<int> WarehouseID
    /// THE TRUST IS THAT: THE WarehouseID IN THESE TABLES SalesOrders/ DeliveryAdvices/ GoodsIssues/ SalesReturn IS NOT VERY IMPORTANT. WE CAN ALLOW WarehouseID NULLABLE
    /// THIS WarehouseID DOES NOT AFFECT THE WAREHOUSE OUTPUT. IT JUST CONTROL ALL WarehouseID IN THE DETAIL TABLES AS THE SAME AS THE MASTER TABLES
    /// ------->IN BRIEF: THE WAREHOUSE OUTPUT IS BASED ON THE DETAIL TABLES. THIS ALLOWS MLUTIPLE WarehouseID ON THE DETAIL TABLES. THE WarehouseID IN THE MASTER TABLES JUST CONTROL: ONLY 1 WarehouseID FOR EVERY DETAIL(S) RECORDS PER EACH MASTER RECORD
    /// LASTER: IF WE ALLOW MULTIPLE WarehouseID IN DETAIL TABLES: JUST MODIFY THE DATABASE, THEN MODIFY APPRORIATE CODING!!! GENERALLY, THIS IS ACCEPTABLE!!! BUT NEED TO MODIFY :) !!!
    /// </summary>
    public interface IWarehouseBaseDTO
    {
        Nullable<int> WarehouseID { get; set; }
        string Code { get; set; }
        [Display(Name = "Kho hàng")]
        [UIHint("AutoCompletes/WarehouseBase")]
        [Required(ErrorMessage = "Vui lòng nhập kho hàng")]
        string Name { get; set; }
    }

    public class WarehouseBaseDTO : BaseDTO, IWarehouseBaseDTO
    {
        public Nullable<int> WarehouseID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }



    public class WarehousePrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Warehouses; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.WarehouseID; }
        public void SetID(int id) { this.WarehouseID = id; }


        private int warehouseID;
        [DefaultValue(0)]
        public int WarehouseID
        {
            get { return this.warehouseID; }
            set { ApplyPropertyChange<WarehousePrimitiveDTO, int>(ref this.warehouseID, o => o.WarehouseID, value); }
        }


        public string Code { get { return this.Name; } }

        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<WarehousePrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        private Nullable<int> locationID;
        [DefaultValue(null)]
        public Nullable<int> LocationID
        {
            get { return this.locationID; }
            set { ApplyPropertyChange<WarehousePrimitiveDTO, Nullable<int>>(ref this.locationID, o => o.LocationID, value); }
        }

        public int WarehouseCategoryID { get { return 1; } }

        private bool bookable;
        [DefaultValue(false)]
        public bool Bookable
        {
            get { return this.bookable; }
            set { ApplyPropertyChange<WarehousePrimitiveDTO, bool>(ref this.bookable, o => o.Bookable, value); }
        }

        private bool issuable;
        [DefaultValue(false)]
        public bool Issuable
        {
            get { return this.issuable; }
            set { ApplyPropertyChange<WarehousePrimitiveDTO, bool>(ref this.issuable, o => o.Issuable, value); }
        }

        private bool isDefault;
        [DefaultValue(false)]
        public bool IsDefault
        {
            get { return this.isDefault; }
            set { ApplyPropertyChange<WarehousePrimitiveDTO, bool>(ref this.isDefault, o => o.IsDefault, value); }
        }


        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehousePrimitiveDTO>(p => p.Code), "Vui lòng nhập mã.", delegate { return (this.Code != null && this.Code.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehousePrimitiveDTO>(p => p.Name), "Vui lòng nhập tên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehousePrimitiveDTO>(p => p.LocationID), "Vui lòng chọn Location.", delegate { return (this.LocationID != null && this.LocationID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehousePrimitiveDTO>(p => p.WarehouseCategoryID), "Vui lòng chọn Location.", delegate { return (this.WarehouseCategoryID != null && this.WarehouseCategoryID > 0); }));
            return validationRules;
        }
    }

    public class WarehouseDTO : WarehousePrimitiveDTO
    {
    }

}
