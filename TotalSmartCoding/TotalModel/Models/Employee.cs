//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TotalModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int EmployeeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Nullable<int> TeamID { get; set; }
        public int LocationID { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public bool InActive { get; set; }
        public string EmployeeRoleIDs { get; set; }
        public string EmployeeLocationIDs { get; set; }
        public System.DateTime Birthday { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual Location Location { get; set; }
        public virtual Team Team { get; set; }
    }
}
