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
    public class FillingLinePrimitiveDTO : BaseWithDetailDTO<FillingLineDetailDTO>, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.FillingLine; } }
        public override bool Newable { get { return false; } set { base.Newable = value; } }
        public override bool Deletable { get { return false; } set { base.Deletable = value; } }
        public override bool NoApprovable { get { return true; } }

        public FillingLinePrimitiveDTO() { this.Initialize(); }

        public override void Init()
        {
            base.Init();
            this.Initialize();
        }

        protected virtual void Initialize() { }

        public override int GetID() { return this.FillingLineID; }
        public void SetID(int id) { this.FillingLineID = id; }

        private int fillingLineID;
        [DefaultValue(0)]
        public int FillingLineID
        {
            get { return this.fillingLineID; }
            set { ApplyPropertyChange<FillingLinePrimitiveDTO, int>(ref this.fillingLineID, o => o.FillingLineID, value); }
        }


        private string fillingLineCode;
        [DefaultValue(null)]
        public string FillingLineCode
        {
            get { return this.fillingLineCode; }
            set { ApplyPropertyChange<FillingLineDTO, string>(ref this.fillingLineCode, o => o.FillingLineCode, value, false); }
        }

        private string fillingLineName;
        [DefaultValue(null)]
        public string FillingLineName
        {
            get { return this.fillingLineName; }
            set { ApplyPropertyChange<FillingLineDTO, string>(ref this.fillingLineName, o => o.FillingLineName, value, false); }
        }





        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<FillingLinePrimitiveDTO>(p => p.FillingLineID), "Vui lòng chọn dây chuyền.", delegate { return (this.FillingLineID != null && this.FillingLineID > 0); })); //FillingLineID LÀ PRIMARYKEY => Đ/K NÀY SẼ NGĂN KHÔNG CHO ADD NEW

            return validationRules;
        }
    }


    public class FillingLineDTO : FillingLinePrimitiveDTO, IBaseDetailEntity<FillingLineDetailDTO>
    {
        public FillingLineDTO()
        {
            this.FillingLineDetails = new BindingList<FillingLineDetailDTO>();

            this.FillingLineDetails.ListChanged += FillingLineDetails_ListChanged;
        }


        public BindingList<FillingLineDetailDTO> FillingLineDetails { get; set; }
        public BindingList<FillingLineDetailDTO> ViewDetails { get { return this.FillingLineDetails; } set { this.FillingLineDetails = value; } }

        public ICollection<FillingLineDetailDTO> GetDetails() { return this.FillingLineDetails; }

        protected override IEnumerable<FillingLineDetailDTO> DtoDetails() { return this.FillingLineDetails; }






        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<FillingLineDTO>(p => p.Remarks), "Vui lòng nhập IP address.", delegate { return (this.GetDetails().Count > 0); }));

            return validationRules;
        }






        #region USE GenericSimpleController WITH DETAIL
        protected override void Initialize()
        {
            base.Initialize(); //BECAUSE THIS FillingLineDTO IS POPULATED BY GenericSimpleController => WHEN INIT: THERE IS NO FUNCTION TO CLEAR ViewDetails WHEN GenericSimpleController.Init()
            if (this.FillingLineDetails != null) this.FillingLineDetails.Clear(); //NOTE: WITH GenericViewDetailController.Init(): WILL CALL: ViewDetails.Clear()
        }

        private void FillingLineDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.SetDirty();
        }
        #endregion  USE GenericSimpleController WITH DETAIL
    }
}
