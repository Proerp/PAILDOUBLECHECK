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
    
    public partial class UserGroupControl
    {
        public int UserGroupControlID { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public int ModuleDetailID { get; set; }
        public string ModuleDetailName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int AccessLevel { get; set; }
        public bool ApprovalPermitted { get; set; }
        public bool UnApprovalPermitted { get; set; }
        public bool VoidablePermitted { get; set; }
        public bool UnVoidablePermitted { get; set; }
        public bool ShowDiscount { get; set; }
    }
}