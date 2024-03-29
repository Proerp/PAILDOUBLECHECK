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
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.DeliveryAdviceDetails = new HashSet<DeliveryAdviceDetail>();
            this.DeliveryAdvices = new HashSet<DeliveryAdvice>();
            this.SalesOrders = new HashSet<SalesOrder>();
            this.DeliveryAdviceDetails1 = new HashSet<DeliveryAdviceDetail>();
            this.DeliveryAdvices1 = new HashSet<DeliveryAdvice>();
            this.SalesOrders1 = new HashSet<SalesOrder>();
            this.GoodsIssueDetails = new HashSet<GoodsIssueDetail>();
            this.GoodsIssueDetails1 = new HashSet<GoodsIssueDetail>();
            this.GoodsIssues = new HashSet<GoodsIssue>();
            this.GoodsIssues1 = new HashSet<GoodsIssue>();
        }
    
        public int CustomerID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string ContactInfo { get; set; }
        public int SalespersonID { get; set; }
        public int CustomerCategoryID { get; set; }
        public int CustomerTypeID { get; set; }
        public int TerritoryID { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string VATCode { get; set; }
        public string Telephone { get; set; }
        public string Facsimile { get; set; }
        public string AttentionName { get; set; }
        public string Remarks { get; set; }
        public bool InActive { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsReceiver { get; set; }
        public string Email { get; set; }
    
        public virtual CustomerCategory CustomerCategory { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual EntireTerritory EntireTerritory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryAdviceDetail> DeliveryAdviceDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryAdvice> DeliveryAdvices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryAdviceDetail> DeliveryAdviceDetails1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryAdvice> DeliveryAdvices1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrder> SalesOrders1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsIssueDetail> GoodsIssueDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsIssueDetail> GoodsIssueDetails1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsIssue> GoodsIssues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsIssue> GoodsIssues1 { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
