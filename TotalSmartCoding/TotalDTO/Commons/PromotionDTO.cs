﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalBase.Enums;
using TotalModel;

namespace TotalDTO.Commons
{
    public class PromotionDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Promotions; } }

        public override int GetID() { return (int)this.PromotionID; }
        public void SetID(int id) { this.PromotionID = id; }

        public Nullable<int> PromotionID { get; set; }
        [Display(Name = "Chương trình khuyến mãi")]
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        [Display(Name = "Ngày kết thúc")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Display(Name = "Tỷ lệ chiết khấu")]
        public Nullable<decimal> DiscountPercent { get; set; }
        [Display(Name = "Tặng quà")]
        public Nullable<decimal> ControlFreeQuantity { get; set; }
        public bool ApplyToAllCustomers { get; set; }
        public bool ApplyToAllCommodities { get; set; }

        
    }
}
