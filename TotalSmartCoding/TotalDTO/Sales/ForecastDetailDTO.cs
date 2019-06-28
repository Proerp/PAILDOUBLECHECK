using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TotalBase;
using TotalBase.Enums;
using TotalModel;
using TotalModel.Helpers;

namespace TotalDTO.Sales
{
    public class ForecastDetailDTO : BaseModel, IBaseModel, IPrimitiveEntity
    {
        public int GetID() { return this.ForecastDetailID; }

        public int ForecastDetailID { get; set; }
        public int ForecastID { get; set; }

        public string VoucherCode { get; set; }
        public Nullable<int> ForecastLocationID { get; set; }



        private int commodityID;
        [DefaultValue(null)]
        public int CommodityID
        {
            get { return this.commodityID; }
            set { ApplyPropertyChange<ForecastDetailDTO, int>(ref this.commodityID, o => o.CommodityID, value); }
        }

        private string commodityCode;
        [DefaultValue(null)]
        public virtual string CommodityCode
        {
            get { return this.commodityCode; }
            set { ApplyPropertyChange<ForecastDetailDTO, string>(ref this.commodityCode, o => o.CommodityCode, value); }
        }

        private string commodityName;
        [DefaultValue(null)]
        public virtual string CommodityName
        {
            get { return this.commodityName; }
            set { ApplyPropertyChange<ForecastDetailDTO, string>(ref this.commodityName, o => o.CommodityName, value); }
        }

        private string commodityCategoryName;
        [DefaultValue(null)]
        public virtual string CommodityCategoryName
        {
            get { return this.commodityCategoryName; }
            set { ApplyPropertyChange<ForecastDetailDTO, string>(ref this.commodityCategoryName, o => o.CommodityCategoryName, value); }
        }





        private decimal quantity;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal Quantity
        {
            get { return this.quantity; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.quantity, o => o.Quantity, Math.Round(value, (int)GlobalEnums.rndQuantity)); this.NotifyPropertyChanged("TotalQuantity"); }
        }

        private decimal lineVolume;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "LineVolume không hợp lệ")]
        public virtual decimal LineVolume
        {
            get { return this.lineVolume; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.lineVolume, o => o.LineVolume, Math.Round(value, (int)GlobalEnums.rndVolume)); this.NotifyPropertyChanged("TotalLineVolume"); }
        }

        private decimal quantityM1;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal QuantityM1
        {
            get { return this.quantityM1; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.quantityM1, o => o.QuantityM1, Math.Round(value, (int)GlobalEnums.rndQuantity)); this.NotifyPropertyChanged("TotalQuantity"); }
        }

        private decimal lineVolumeM1;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "LineVolumeM1 không hợp lệ")]
        public virtual decimal LineVolumeM1
        {
            get { return this.lineVolumeM1; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.lineVolumeM1, o => o.LineVolumeM1, Math.Round(value, (int)GlobalEnums.rndVolume)); this.NotifyPropertyChanged("TotalLineVolume"); }
        }

        private decimal quantityM2;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal QuantityM2
        {
            get { return this.quantityM2; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.quantityM2, o => o.QuantityM2, Math.Round(value, (int)GlobalEnums.rndQuantity)); this.NotifyPropertyChanged("TotalQuantity"); }
        }

        private decimal lineVolumeM2;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "LineVolumeM2 không hợp lệ")]
        public virtual decimal LineVolumeM2
        {
            get { return this.lineVolumeM2; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.lineVolumeM2, o => o.LineVolumeM2, Math.Round(value, (int)GlobalEnums.rndVolume)); this.NotifyPropertyChanged("TotalLineVolume"); }
        }

        private decimal quantityM3;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal QuantityM3
        {
            get { return this.quantityM3; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.quantityM3, o => o.QuantityM3, Math.Round(value, (int)GlobalEnums.rndQuantity)); this.NotifyPropertyChanged("TotalQuantity"); }
        }

        private decimal lineVolumeM3;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "LineVolumeM3 không hợp lệ")]
        public virtual decimal LineVolumeM3
        {
            get { return this.lineVolumeM3; }
            set { ApplyPropertyChange<ForecastDetailDTO, decimal>(ref this.lineVolumeM3, o => o.LineVolumeM3, Math.Round(value, (int)GlobalEnums.rndVolume)); this.NotifyPropertyChanged("TotalLineVolume"); }
        }

        public virtual decimal TotalQuantity
        {
            get { return this.quantity + this.quantityM1 + this.quantityM2 + this.quantityM3; }
        }

        public virtual decimal TotalLineVolume
        {
            get { return this.lineVolume + this.lineVolumeM1 + this.lineVolumeM2 + this.lineVolumeM3; }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.CommodityID), "Vui lòng chọn mặt hàng.", delegate { return (this.CommodityID > 0); }));

            return validationRules;

        }
    }
}
