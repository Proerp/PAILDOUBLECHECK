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
    
    public partial class CommodityBase
    {
        public int CommodityID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Volume { get; set; }
        public string PackageSize { get; set; }
        public decimal PackageVolume { get; set; }
        public string APICode { get; set; }
        public string FillingLineIDs { get; set; }
        public int CommodityCategoryID { get; set; }
        public string CommodityCategoryName { get; set; }
        public string OfficialCode { get; set; }
        public string DisplayCode { get; set; }
    }
}