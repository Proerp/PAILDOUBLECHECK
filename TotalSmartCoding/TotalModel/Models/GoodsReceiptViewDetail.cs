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
    
    public partial class GoodsReceiptViewDetail
    {
        public int GoodsReceiptDetailID { get; set; }
        public int GoodsReceiptID { get; set; }
        public Nullable<int> PickupID { get; set; }
        public Nullable<int> PickupDetailID { get; set; }
        public string PickupReference { get; set; }
        public Nullable<System.DateTime> PickupEntryDate { get; set; }
        public int CommodityID { get; set; }
        public string CommodityCode { get; set; }
        public string CommodityName { get; set; }
        public Nullable<int> PalletID { get; set; }
        public string PalletCode { get; set; }
        public string Remarks { get; set; }
        public int BinLocationID { get; set; }
        public string BinLocationCode { get; set; }
        public Nullable<int> PackID { get; set; }
        public string PackCode { get; set; }
        public Nullable<int> CartonID { get; set; }
        public string CartonCode { get; set; }
        public decimal Quantity { get; set; }
        public int PackCounts { get; set; }
        public int CartonCounts { get; set; }
        public int PalletCounts { get; set; }
        public decimal LineVolume { get; set; }
        public int BatchID { get; set; }
        public System.DateTime BatchEntryDate { get; set; }
        public Nullable<int> GoodsIssueID { get; set; }
        public Nullable<int> GoodsIssueTransferDetailID { get; set; }
        public string GoodsIssueReference { get; set; }
        public Nullable<System.DateTime> GoodsIssueEntryDate { get; set; }
        public Nullable<int> WarehouseAdjustmentID { get; set; }
        public Nullable<int> WarehouseAdjustmentDetailID { get; set; }
        public string WarehouseAdjustmentReference { get; set; }
        public Nullable<System.DateTime> WarehouseAdjustmentEntryDate { get; set; }
        public Nullable<int> LocationIssueID { get; set; }
        public Nullable<int> WarehouseIssueID { get; set; }
        public Nullable<int> WarehouseAdjustmentTypeID { get; set; }
    }
}
