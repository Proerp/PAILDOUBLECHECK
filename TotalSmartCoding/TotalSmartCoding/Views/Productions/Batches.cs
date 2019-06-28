using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ninject;



using TotalSmartCoding.Views.Mains;


using TotalBase;
using TotalBase.Enums;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalCore.Repositories.Productions;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalCore.Services.Productions;
using TotalSmartCoding.ViewModels.Productions;
using TotalSmartCoding.Controllers.Productions;
using TotalDTO.Productions;
using AutoMapper;
using TotalModel.Models;

using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using TotalModel.Interfaces;
using BrightIdeasSoftware;
using TotalSmartCoding.Properties;



namespace TotalSmartCoding.Views.Productions
{
    public partial class Batches : BaseView
    {
        private SmartCoding smartCoding;
        private bool allQueueEmpty;

        private BatchAPIs batchAPIs;
        private BatchViewModel batchViewModel { get; set; }

        public Batches(SmartCoding smartCoding, bool allQueueEmpty)
            : base()
        {
            InitializeComponent();


            this.smartCoding = smartCoding;
            this.allQueueEmpty = allQueueEmpty;

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastBatchIndex;


            this.olvIsDefault.AspectGetter = delegate(object row)
            {// IsDefault indicator column
                if (((BatchIndex)row).IsDefault)
                    return "IsDefault";
                return "";
            };
            this.olvIsDefault.Renderer = new MappedImageRenderer(new Object[] { "IsDefault", Resources.Play_Normal_16 });
            this.buttonApply.Enabled = allQueueEmpty;

            this.batchAPIs = new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>());

            this.batchViewModel = CommonNinject.Kernel.Get<BatchViewModel>();
            this.batchViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.batchViewModel;
        }


        protected override void InitializeTabControl()
        {
            try
            {
                this.comboDiscontinued.SelectedIndex = 0;

                this.checkAutoBarcode.Visible = GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail && GlobalEnums.NMVNOnly; //IF WE WANT AUTO AutoBarcode: JUST REMOVE: && GlobalEnums.NMVNOnly
                this.checkAutoCarton.Visible = GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Import;
                if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Medium4L || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Import || (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum && !GlobalEnums.DrumWithDigit)) { this.labelNextPackNo.Visible = false; this.textexNextPackNo.Visible = false; this.labelBatchPackNo.Visible = false; this.textexBatchPackNo.Visible = false; this.olvNextPackNo.IsVisible = false; ; this.olvBatchPackNo.IsVisible = false; }
                if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum) { this.labelNextCartonNo.Visible = false; this.textexNextCartonNo.Visible = false; }
                if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Drum && GlobalEnums.DrumWithDigit) { this.labelNextPalletNo.Visible = false; this.textexNextPalletNo.Visible = false; }
                if (GlobalEnums.CBPP) { this.labelCommodityAPICode.Visible = false; this.textexCommodityAPICode.Visible = false; this.olvCommodityAPICode.IsVisible = false; this.labelNextPalletNo.Visible = false; this.textexNextPalletNo.Visible = false; this.labelBatchPalletNo.Visible = false; this.textexBatchPalletNo.Visible = false; this.olvNextPalletNo.IsVisible = false; this.olvBatchPalletNo.IsVisible = false; }

                CustomTabControl customTabBatch = new CustomTabControl();
                //customTabControlCustomerChannel.ImageList = this.imageListTabControl;

                customTabBatch.Font = this.textexCode.Font;
                customTabBatch.DisplayStyle = TabStyle.VisualStudio;
                customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

                customTabBatch.TabPages.Add("Batch", "Batch Information    ");
                customTabBatch.TabPages[0].Controls.Add(this.layoutMaster);

                this.naviBarMaster.Bands[0].ClientArea.Controls.Add(customTabBatch);

                customTabBatch.Dock = DockStyle.Fill;
                this.layoutMaster.Dock = DockStyle.Fill;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingEntryDate;
        Binding bindingCode;

        Binding bindingNextPackNo;
        Binding bindingNextCartonNo;
        Binding bindingNextPalletNo;

        Binding bindingBatchPackNo;
        Binding bindingBatchCartonNo;
        Binding bindingBatchPalletNo;

        Binding bindingFinalCartonNo;

        Binding bindingAutoBarcode;
        Binding bindingAutoCarton;

        Binding bindingRemarks;

        Binding bindingCommodityID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.batchViewModel, "EntryDate", true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCode = this.textexCode.DataBindings.Add("Text", this.batchViewModel, "Code", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingNextPackNo = this.textexNextPackNo.DataBindings.Add("Text", this.batchViewModel, "NextPackNo", true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingNextCartonNo = this.textexNextCartonNo.DataBindings.Add("Text", this.batchViewModel, "NextCartonNo", true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingNextPalletNo = this.textexNextPalletNo.DataBindings.Add("Text", this.batchViewModel, "NextPalletNo", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingBatchPackNo = this.textexBatchPackNo.DataBindings.Add("Text", this.batchViewModel, "BatchPackNo", true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingBatchCartonNo = this.textexBatchCartonNo.DataBindings.Add("Text", this.batchViewModel, "BatchCartonNo", true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingBatchPalletNo = this.textexBatchPalletNo.DataBindings.Add("Text", this.batchViewModel, "BatchPalletNo", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingFinalCartonNo = this.textexFinalCartonNo.DataBindings.Add("Text", this.batchViewModel, "FinalCartonNo", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingAutoCarton = this.checkAutoCarton.DataBindings.Add("Checked", this.batchViewModel, "AutoCarton", true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingAutoBarcode = this.checkAutoBarcode.DataBindings.Add("Checked", this.batchViewModel, "AutoBarcode", true, DataSourceUpdateMode.OnPropertyChanged);
            this.labelFinalCartonNo.DataBindings.Add("Visible", this.batchViewModel, "AutoBarcode", true, DataSourceUpdateMode.OnPropertyChanged);
            this.textexFinalCartonNo.DataBindings.Add("Visible", this.batchViewModel, "AutoBarcode", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.batchViewModel, "Remarks", true, DataSourceUpdateMode.OnPropertyChanged);

            this.textexCommodityName.DataBindings.Add("Text", this.batchViewModel, CommonExpressions.PropertyName<BatchViewModel>(p => p.CommodityName), true);
            this.textexCommodityAPICode.DataBindings.Add("Text", this.batchViewModel, CommonExpressions.PropertyName<BatchViewModel>(p => p.CommodityAPICode), true);

            CommodityAPIs commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>());
            commodityAPIs.ImportCommodities(); //IMPORT Commodities FROM EXISTING SYSTEM

            this.combexCommodityID.DataSource = commodityAPIs.GetCommodityBases().Where(p => p.FillingLineIDs.Contains(((int)GlobalVariables.FillingLineID).ToString()) || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Import).ToList();
            this.combexCommodityID.DisplayMember = CommonExpressions.PropertyName<CommodityBase>(p => p.DisplayCode);
            this.combexCommodityID.ValueMember = CommonExpressions.PropertyName<CommodityBase>(p => p.CommodityID);
            this.bindingCommodityID = this.combexCommodityID.DataBindings.Add("SelectedValue", this.batchViewModel, CommonExpressions.PropertyName<BatchViewModel>(p => p.CommodityID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingNextPackNo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingNextCartonNo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingNextPalletNo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingBatchPackNo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingBatchCartonNo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingBatchPalletNo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingFinalCartonNo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingAutoBarcode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingAutoCarton.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingCommodityID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
        }

        protected override void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            base.CommonControl_BindingComplete(sender, e);
            if (sender.Equals(this.bindingCommodityID))
            {
                if (this.combexCommodityID.SelectedItem != null)
                {
                    CommodityBase commodityBase = (CommodityBase)this.combexCommodityID.SelectedItem;
                    this.batchViewModel.CommodityName = commodityBase.Name;
                    this.batchViewModel.CommodityAPICode = commodityBase.APICode;
                }
            }
        }


        protected override Controllers.BaseController myController
        {
            get { return new BatchController(CommonNinject.Kernel.Get<IBatchService>(), this.batchViewModel); }
        }

        public override void Loading()
        {
            this.fastBatchIndex.SetObjects(this.batchAPIs.GetBatchIndexes(this.comboDiscontinued.SelectedIndex == 0 ? GlobalEnums.ActiveOption.Active : GlobalEnums.ActiveOption.Both));
            base.Loading();

            this.smartCoding.Initialize();
        }

        protected override void invokeEdit(int? id)
        {
            base.invokeEdit(id);

            this.textexNextPackNo.BackColor = textexCommodityAPICode.BackColor;
            this.textexNextCartonNo.BackColor = textexCommodityAPICode.BackColor;
            this.textexNextPalletNo.BackColor = textexCommodityAPICode.BackColor;

            this.textexBatchPackNo.BackColor = textexCommodityAPICode.BackColor;
            this.textexBatchCartonNo.BackColor = textexCommodityAPICode.BackColor;
            this.textexBatchPalletNo.BackColor = textexCommodityAPICode.BackColor;
        }

        protected override void NotifyPropertyChanged(string propertyName)
        {
            base.NotifyPropertyChanged(propertyName);

            if (propertyName == "ReadonlyMode")
            {
                this.buttonApply.Enabled = this.allQueueEmpty && this.ReadonlyMode;
                this.buttonDiscontinued.Enabled = this.Newable && this.ReadonlyMode;
            }

            if (propertyName == "EntryDate") this.batchViewModel.EntryMonthID = CommonExpressions.GetEntryMonthID(this.batchViewModel.EntryDate);
            if (propertyName == "EntryMonthID") this.GetBatchMaxNo(null, this.batchViewModel.EntryMonthID);
            if (propertyName == "Code") this.GetBatchMaxNo(this.batchViewModel.Code, null);
            if (propertyName == "CommodityID") this.GetBatchMaxNo(this.batchViewModel.Code, this.batchViewModel.EntryMonthID);

            if (propertyName == "NextPackNo") this.textexNextPackNo.BackColor = Color.LightSalmon;
            if (propertyName == "NextCartonNo") this.textexNextCartonNo.BackColor = Color.LightSalmon;
            if (propertyName == "NextPalletNo") this.textexNextPalletNo.BackColor = Color.LightSalmon;

            if (propertyName == "BatchPackNo") this.textexBatchPackNo.BackColor = Color.LightSalmon;
            if (propertyName == "BatchCartonNo") this.textexBatchCartonNo.BackColor = Color.LightSalmon;
            if (propertyName == "BatchPalletNo") this.textexBatchPalletNo.BackColor = Color.LightSalmon;
        }

        private void GetBatchMaxNo(string code, int? entryMonthID)
        {
            if (code != null)
            {
                List<BatchMaxNo> batchMaxNoes = this.batchAPIs.GetBatchMaxNo(this.batchViewModel.FillingLineID, this.batchViewModel.CommodityID, code);
                if (batchMaxNoes.Count > 0 && batchMaxNoes[0].BatchPackNo != null)
                { this.batchViewModel.BatchPackNo = batchMaxNoes[0].BatchPackNo; this.batchViewModel.BatchCartonNo = batchMaxNoes[0].BatchCartonNo; this.batchViewModel.BatchPalletNo = batchMaxNoes[0].BatchPalletNo; }
                else
                { this.batchViewModel.BatchPackNo = "000001"; this.batchViewModel.BatchCartonNo = (this.batchViewModel.FillingLineID == (int)GlobalVariables.FillingLine.Pail ? "000001" : "900001"); this.batchViewModel.BatchPalletNo = "900001"; }
            }
            if (entryMonthID != null)
            {
                List<BatchMaxNo> batchMaxNoes = this.batchAPIs.GetBatchMaxNo(this.batchViewModel.FillingLineID, this.batchViewModel.CommodityID, entryMonthID);
                if (batchMaxNoes.Count > 0 && batchMaxNoes[0].NextPackNo != null)
                { this.batchViewModel.NextPackNo = batchMaxNoes[0].NextPackNo; this.batchViewModel.NextCartonNo = batchMaxNoes[0].NextCartonNo; this.batchViewModel.NextPalletNo = batchMaxNoes[0].NextPalletNo; }
                else
                { this.batchViewModel.NextPackNo = "000001"; this.batchViewModel.NextCartonNo = (this.batchViewModel.FillingLineID == (int)GlobalVariables.FillingLine.Pail ? "000001" : "900001"); this.batchViewModel.NextPalletNo = "900001"; }
            }
        }

        private void comboDiscontinued_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.batchAPIs != null) this.Loading();
        }

        private void fastBatchIndex_FormatRow(object sender, FormatRowEventArgs e)
        {
            BatchIndex batchIndex = (BatchIndex)e.Model;
            if (batchIndex.InActive) e.Item.ForeColor = Color.Gray;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (this.allQueueEmpty) this.Approve();
        }

        protected override bool ApproveCheck(int id)
        {
            return !this.batchViewModel.IsDefault && !this.batchViewModel.InActive;
        }

        private void buttonDiscontinued_Click(object sender, EventArgs e)
        {
            this.Void();
        }

        protected override bool VoidCheck(int id)
        {
            this.batchViewModel.VoidTypeID = 1;
            return !this.batchViewModel.IsDefault;
        }

        private void timerEverySecond_Tick(object sender, EventArgs e)
        {
            if (this.batchViewModel.EntryMonthID != CommonExpressions.GetEntryMonthID())
                this.iconNewMonth.Visible = !this.iconNewMonth.Visible;
            else
                this.iconNewMonth.Visible = false;
        }


    }
}
