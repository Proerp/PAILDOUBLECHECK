using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TotalBase;
using TotalBase.Enums;
using TotalModel;
using TotalModel.Helpers;
using TotalDTO.Helpers;
using TotalDTO.Commons;

namespace TotalDTO.Commons
{
    public class CommoditySettingPrimitiveDTO : BaseWithDetailDTO<CommoditySettingDetailDTO>, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.CommoditySettings; } }
        public override bool NoApprovable { get { return true; } }

        public CommoditySettingPrimitiveDTO() { this.Initialize(); }

        public override void Init()
        {
            base.Init();
            this.Initialize();
        }

        protected virtual void Initialize() { }

        public override int GetID() { return this.CommoditySettingID; }
        public void SetID(int id) { this.CommoditySettingID = id; }

        public override DateTime? EntryDate { get { return DateTime.Now; } set { } }

        private int commoditySettingID;
        [DefaultValue(0)]
        public int CommoditySettingID
        {
            get { return this.commoditySettingID; }
            set { ApplyPropertyChange<CommoditySettingPrimitiveDTO, int>(ref this.commoditySettingID, o => o.CommoditySettingID, value); }
        }

        private Nullable<int> commodityID;
        [DefaultValue(null)]
        public Nullable<int> CommodityID
        {
            get { return this.commodityID; }
            set { ApplyPropertyChange<CommoditySettingPrimitiveDTO, Nullable<int>>(ref this.commodityID, o => o.CommodityID, value); }
        }
        private string commodityCode;
        [DefaultValue(null)]
        public string CommodityCode
        {
            get { return this.commodityCode; }
            set { ApplyPropertyChange<CommoditySettingDTO, string>(ref this.commodityCode, o => o.CommodityCode, value, false); }
        }

        private string commodityName;
        [DefaultValue(null)]
        public string CommodityName
        {
            get { return this.commodityName; }
            set { ApplyPropertyChange<CommoditySettingDTO, string>(ref this.commodityName, o => o.CommodityName, value, false); }
        }

        private string commodityCategoryName;
        [DefaultValue(null)]
        public string CommodityCategoryName
        {
            get { return this.commodityCategoryName; }
            set { ApplyPropertyChange<CommoditySettingDTO, string>(ref this.commodityCategoryName, o => o.CommodityCategoryName, value, false); }
        }


        private string packageSize;
        [DefaultValue(null)]
        public string PackageSize
        {
            get { return this.packageSize; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.packageSize, o => o.PackageSize, value); }
        }

        private decimal packageVolume;
        //[DefaultValue(0.0)]
        [Range(1, 99999999999, ErrorMessage = "Volume không hợp lệ")]
        public virtual decimal PackageVolume
        {
            get { return this.packageVolume; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, decimal>(ref this.packageVolume, o => o.PackageVolume, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();

            this.DtoDetails().ToList().ForEach(e => { e.CommodityID = this.CommodityID; });
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommoditySettingPrimitiveDTO>(p => p.CommodityID), "Vui lòng chọn mặt hàng.", delegate { return (this.CommodityID != null && this.CommodityID > 0); }));

            return validationRules;
        }
    }


    public class CommoditySettingDTO : CommoditySettingPrimitiveDTO, IBaseDetailEntity<CommoditySettingDetailDTO>
    {
        public CommoditySettingDTO()
        {
            this.CommoditySettingDetails = new BindingList<CommoditySettingDetailDTO>();

            this.CommoditySettingDetails.ListChanged += CommoditySettingDetails_ListChanged;
        }


        public BindingList<CommoditySettingDetailDTO> CommoditySettingDetails { get; set; }
        public BindingList<CommoditySettingDetailDTO> ViewDetails { get { return this.CommoditySettingDetails; } set { this.CommoditySettingDetails = value; } }

        public ICollection<CommoditySettingDetailDTO> GetDetails() { return this.CommoditySettingDetails; }

        protected override IEnumerable<CommoditySettingDetailDTO> DtoDetails() { return this.CommoditySettingDetails; }






        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommoditySettingDTO>(p => p.Remarks), "Vui lòng nhập Low, High && Alert Details.", delegate { return (this.GetDetails().Count > 0); }));

            return validationRules;
        }






        #region USE GenericSimpleController WITH DETAIL
        protected override void Initialize()
        {
            base.Initialize(); //BECAUSE THIS CommoditySettingDTO IS POPULATED BY GenericSimpleController => WHEN INIT: THERE IS NO FUNCTION TO CLEAR ViewDetails WHEN GenericSimpleController.Init()
            if (this.CommoditySettingDetails != null) this.CommoditySettingDetails.Clear(); //NOTE: WITH GenericViewDetailController.Init(): WILL CALL: ViewDetails.Clear()
        }

        private void CommoditySettingDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.SetDirty();
        }
        #endregion  USE GenericSimpleController WITH DETAIL
    }
}
