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
    
    public partial class Pack
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pack()
        {
            this.PickupDetails = new HashSet<PickupDetail>();
            this.GoodsReceiptDetails = new HashSet<GoodsReceiptDetail>();
            this.WarehouseAdjustmentDetails = new HashSet<WarehouseAdjustmentDetail>();
        }
    
        public int PackID { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int FillingLineID { get; set; }
        public int BatchID { get; set; }
        public int LocationID { get; set; }
        public int QueueID { get; set; }
        public int CommodityID { get; set; }
        public Nullable<int> CartonID { get; set; }
        public string Code { get; set; }
        public decimal LineVolume { get; set; }
        public int EntryStatusID { get; set; }
        public string Label { get; set; }
    
        public virtual FillingLine FillingLine { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PickupDetail> PickupDetails { get; set; }
        public virtual Commodity Commodity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsReceiptDetail> GoodsReceiptDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseAdjustmentDetail> WarehouseAdjustmentDetails { get; set; }
        public virtual Carton Carton { get; set; }
    }
}
