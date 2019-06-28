using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;
using TotalBase;

namespace TotalDTO.Commons
{
    public interface IEmployeeBaseDTO
    {
        int EmployeeID { get; set; }
        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Vui lòng nhập tên nhân viên")]
        string Name { get; set; }
    }

    public class EmployeeBaseDTO : BaseDTO, IEmployeeBaseDTO
    {
        private int employeeID;
        [DefaultValue(0)]
        public int EmployeeID
        {
            get { return this.employeeID; }
            set { ApplyPropertyChange<EmployeeBaseDTO, int>(ref this.employeeID, o => o.EmployeeID, value); }
        }

        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<EmployeeBaseDTO, string>(ref this.name, o => o.Name, value); }
        }
    }

    public class EmployeePrimitiveDTO : EmployeeBaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Employees; } }

        public override int GetID() { return this.EmployeeID; }
        public void SetID(int id) { this.EmployeeID = id; }

        private string code;
        [DefaultValue(null)]
        public string Code
        {
            get { return this.code; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, string>(ref this.code, o => o.Code, value); }
        }

        private Nullable<int> teamID;
        [DefaultValue(null)]
        public Nullable<int> TeamID
        {
            get { return this.teamID; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, Nullable<int>>(ref this.teamID, o => o.TeamID, value); }
        }

        private string title;
        [DefaultValue(null)]
        public string Title
        {
            get { return this.title; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, string>(ref this.title, o => o.Title, value); }
        }

        private DateTime? birthday;
        public DateTime? Birthday
        {
            get { return this.birthday; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, DateTime?>(ref this.birthday, o => o.Birthday, value); }
        }

        private string telephone;
        [DefaultValue(null)]
        public string Telephone
        {
            get { return this.telephone; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, string>(ref this.telephone, o => o.Telephone, value); }
        }

        private string address;
        [DefaultValue(null)]
        public string Address
        {
            get { return this.address; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, string>(ref this.address, o => o.Address, value); }
        }


        private string employeeLocationIDs;
        [DefaultValue(null)]
        public string EmployeeLocationIDs
        {
            get { return this.employeeLocationIDs; }
            set
            {
                ApplyPropertyChange<EmployeePrimitiveDTO, string>(ref this.employeeLocationIDs, o => o.EmployeeLocationIDs, value);
                this.checkLocation1 = this.CheckLocation1;
                this.checkLocation2 = this.CheckLocation2;
                this.checkLocation3 = this.CheckLocation3;
                this.checkLocation4 = this.CheckLocation4;
            }
        }

        private bool checkLocation1;
        [DefaultValue(false)]
        public bool CheckLocation1
        {
            get { return this.EmployeeLocationIDs != null ? this.EmployeeLocationIDs.IndexOf("1") >= 0 : false; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, bool>(ref this.checkLocation1, o => o.CheckLocation1, value); this.setEmployeeLocationIDs(); }
        }

        private bool checkLocation2;
        [DefaultValue(false)]
        public bool CheckLocation2
        {
            get { return this.EmployeeLocationIDs != null ? this.EmployeeLocationIDs.IndexOf("2") >= 0 : false; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, bool>(ref this.checkLocation2, o => o.CheckLocation2, value); this.setEmployeeLocationIDs(); }
        }

        private bool checkLocation3;
        [DefaultValue(false)]
        public bool CheckLocation3
        {
            get { return this.EmployeeLocationIDs != null ? this.EmployeeLocationIDs.IndexOf("3") >= 0 : false; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, bool>(ref this.checkLocation3, o => o.CheckLocation3, value); this.setEmployeeLocationIDs(); }
        }

        private bool checkLocation4;
        [DefaultValue(false)]
        public bool CheckLocation4
        {
            get { return this.EmployeeLocationIDs != null ? this.EmployeeLocationIDs.IndexOf("4") >= 0 : false; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, bool>(ref this.checkLocation4, o => o.CheckLocation4, value); this.setEmployeeLocationIDs(); }
        }

        private void setEmployeeLocationIDs()
        {
            this.EmployeeLocationIDs = (this.checkLocation1 ? "1," : "") + (this.checkLocation2 ? "2," : "") + (this.checkLocation3 ? "3," : "") + (this.checkLocation4 ? "4," : "");
            if (this.EmployeeLocationIDs != null && this.EmployeeLocationIDs != "" && this.EmployeeLocationIDs.Substring(this.EmployeeLocationIDs.Length - 1) == ",") this.EmployeeLocationIDs = this.EmployeeLocationIDs.Substring(0, this.EmployeeLocationIDs.Length - 1);
        }

        private string employeeRoleIDs;
        [DefaultValue(null)]
        public string EmployeeRoleIDs
        {
            get { return this.employeeRoleIDs; }
            set
            {
                ApplyPropertyChange<EmployeePrimitiveDTO, string>(ref this.employeeRoleIDs, o => o.EmployeeRoleIDs, value);
                this.checkRole1 = this.CheckRole1;
                this.checkRole2 = this.CheckRole2;
                this.checkRole3 = this.CheckRole3;
            }
        }

        private bool checkRole1;
        [DefaultValue(false)]
        public bool CheckRole1
        {
            get { return this.EmployeeRoleIDs != null ? this.EmployeeRoleIDs.IndexOf("1") >= 0 : false; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, bool>(ref this.checkRole1, o => o.CheckRole1, value); this.setEmployeeRoleIDs(); }
        }

        private bool checkRole2;
        [DefaultValue(false)]
        public bool CheckRole2
        {
            get { return this.EmployeeRoleIDs != null ? this.EmployeeRoleIDs.IndexOf("2") >= 0 : false; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, bool>(ref this.checkRole2, o => o.CheckRole2, value); this.setEmployeeRoleIDs(); }
        }

        private bool checkRole3;
        [DefaultValue(false)]
        public bool CheckRole3
        {
            get { return this.EmployeeRoleIDs != null ? this.EmployeeRoleIDs.IndexOf("3") >= 0 : false; }
            set { ApplyPropertyChange<EmployeePrimitiveDTO, bool>(ref this.checkRole3, o => o.CheckRole3, value); this.setEmployeeRoleIDs(); }
        }

        private void setEmployeeRoleIDs()
        {
            this.EmployeeRoleIDs = (this.checkRole1 ? "1," : "") + (this.checkRole2 ? "2," : "") + (this.checkRole3 ? "3," : "");
            if (this.EmployeeRoleIDs != null && this.EmployeeRoleIDs != "" && this.EmployeeRoleIDs.Substring(this.EmployeeRoleIDs.Length - 1) == ",") this.EmployeeRoleIDs = this.EmployeeRoleIDs.Substring(0, this.EmployeeRoleIDs.Length - 1);
        }

        public override string LogRemarks { get { return this.Code != null && this.Code != "" ? "Code: " + this.Code : null; } }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<EmployeeDTO>(p => p.Code), "Vui lòng nhập mã nhân viên.", delegate { return (this.Code != null && this.Code.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<EmployeeDTO>(p => p.Name), "Vui lòng nhập tên nhân viên.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<EmployeeDTO>(p => p.Title), "Vui lòng nhập chức vụ.", delegate { return (this.Title != null && this.Title.Length > 0); }));
            return validationRules;
        }
    }

    public class EmployeeDTO : EmployeePrimitiveDTO
    {
    }
}
