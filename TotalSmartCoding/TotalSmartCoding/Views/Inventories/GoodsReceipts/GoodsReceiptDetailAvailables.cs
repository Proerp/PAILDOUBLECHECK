using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Ninject;


using TotalSmartCoding.Views.Mains;


using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalCore.Repositories.Inventories;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalBase;
using TotalModel.Models;
using TotalSmartCoding.ViewModels.Helpers;
using TotalSmartCoding.ViewModels.Inventories;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using BrightIdeasSoftware;


namespace TotalSmartCoding.Views.Inventories.GoodsReceipts
{
    public partial class GoodsReceiptDetailAvailables : BaseView
    {
        private CustomTabControl customTabBatch;

        private GoodsReceiptAPIs goodsReceiptAPIs;

        public GoodsReceiptDetailAvailables()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;

            this.goodsReceiptAPIs = new GoodsReceiptAPIs(CommonNinject.Kernel.Get<IGoodsReceiptAPIRepository>());

            this.baseDTO = new GoodsReceiptDetailAvailableViewModel();
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                this.customTabBatch = new CustomTabControl();

                this.customTabBatch.Font = this.fastAvailablePallets.Font;
                this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
                this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

                this.customTabBatch.TabPages.Add("tabPendingPallets", "Pending pallets");
                this.customTabBatch.TabPages.Add("tabPendingCartons", "Pending cartons");
                this.customTabBatch.TabPages[0].Controls.Add(this.fastAvailablePallets);
                this.customTabBatch.TabPages[1].Controls.Add(this.fastAvailableCartons);


                this.customTabBatch.Dock = DockStyle.Fill;
                this.fastAvailablePallets.Dock = DockStyle.Fill;
                this.fastAvailableCartons.Dock = DockStyle.Fill;
                this.Controls.Add(this.customTabBatch);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingLocationID;
        Binding bindingOnlyIssuable;
        Binding bindingOnlyApproved;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            //JUST INIT THE local parameter only. DON'T INIT VIA: this.LocationID: BECAUSE IT WILL RAISE THE LocationID CHANGE EVENT
            this.locationID = ContextAttributes.User.LocationID;
            this.onlyApproved = true; this.onlyIssuable = true;

            this.comboSummaryVersusDetail.ComboBox.Items.AddRange(new string[] { "Summary only", "Show detail" });
            this.comboSummaryVersusDetail.ComboBox.SelectedIndex = 0;

            LocationAPIs locationAPIs = new LocationAPIs(CommonNinject.Kernel.Get<ILocationAPIRepository>());
            this.comboLocationID.ComboBox.DataSource = locationAPIs.GetLocationBases();
            this.comboLocationID.ComboBox.DisplayMember = CommonExpressions.PropertyName<LocationBase>(p => p.Name);
            this.comboLocationID.ComboBox.ValueMember = CommonExpressions.PropertyName<LocationBase>(p => p.LocationID);
            this.bindingLocationID = this.comboLocationID.ComboBox.DataBindings.Add("SelectedValue", this, "LocationID", true, DataSourceUpdateMode.OnPropertyChanged);

            this.comboOnlyIssuable.ComboBox.DataSource = new List<OptionBool>() { new OptionBool() { OptionValue = true, OptionDescription = "Issuable Only" }, new OptionBool() { OptionValue = false, OptionDescription = "Include Hold Items" } };
            this.comboOnlyIssuable.ComboBox.DisplayMember = CommonExpressions.PropertyName<OptionBool>(p => p.OptionDescription);
            this.comboOnlyIssuable.ComboBox.ValueMember = CommonExpressions.PropertyName<OptionBool>(p => p.OptionValue);
            this.bindingOnlyIssuable = this.comboOnlyIssuable.ComboBox.DataBindings.Add("SelectedValue", this, "OnlyIssuable", true, DataSourceUpdateMode.OnPropertyChanged);

            this.comboOnlyApproved.ComboBox.DataSource = new List<OptionBool>() { new OptionBool() { OptionValue = true, OptionDescription = "Verified Only" }, new OptionBool() { OptionValue = false, OptionDescription = "Incclude not Verified Items" } };
            this.comboOnlyApproved.ComboBox.DisplayMember = CommonExpressions.PropertyName<OptionBool>(p => p.OptionDescription);
            this.comboOnlyApproved.ComboBox.ValueMember = CommonExpressions.PropertyName<OptionBool>(p => p.OptionValue);
            this.bindingOnlyApproved = this.comboOnlyApproved.ComboBox.DataBindings.Add("SelectedValue", this, "OnlyApproved", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingLocationID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingOnlyIssuable.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingOnlyApproved.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastAvailablePallets.AboutToCreateGroups += fastAvailableItems_AboutToCreateGroups;
            this.fastAvailableCartons.AboutToCreateGroups += fastAvailableItems_AboutToCreateGroups;
            this.fastAvailablePallets.ShowGroups = true;
            this.fastAvailableCartons.ShowGroups = true;
        }

        private void fastAvailableItems_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = sender.Equals(this.fastAvailablePallets) ? "Pallet-32-O" : "Carton-32";
                    olvGroup.Subtitle = "List count: " + olvGroup.Contents.Count.ToString();
                }
            }
        }

        public override void Loading()
        {
            try
            {
                List<GoodsReceiptDetailAvailable> goodsReceiptDetailAvailables = goodsReceiptAPIs.GetGoodsReceiptDetailAvailables(this.LocationID, null, null, null, null, null, this.OnlyApproved, this.OnlyIssuable);

                this.fastAvailablePallets.SelectedObject = null; this.fastAvailablePallets.SelectedObjects = null;
                this.fastAvailableCartons.SelectedObject = null; this.fastAvailableCartons.SelectedObjects = null;

                this.fastAvailablePallets.SetObjects(goodsReceiptDetailAvailables.Where(w => w.PalletID != null));
                this.fastAvailableCartons.SetObjects(goodsReceiptDetailAvailables.Where(w => w.CartonID != null));

                this.ShowRowCount();

                base.Loading();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastAvailablePallets.Sort(this.olvPalletCommodityCode);
            this.fastAvailableCartons.Sort(this.olvCartonCommodityCode);
        }

        public override void DoAfterActivate()
        {
            base.DoAfterActivate();
            this.Loading();
        }

        private int locationID;
        public int LocationID
        {
            get { return this.locationID; }
            set
            {
                if (this.locationID != value)
                {
                    this.locationID = value;
                    if (this.locationID > 0) this.Loading();
                }
            }
        }

        private bool onlyApproved;
        public bool OnlyApproved
        {
            get { return this.onlyApproved; }
            set
            {
                if (this.onlyApproved != value)
                {
                    this.onlyApproved = value;
                    this.Loading();
                }
            }
        }

        private bool onlyIssuable;
        public bool OnlyIssuable
        {
            get { return this.onlyIssuable; }
            set
            {
                if (this.onlyIssuable != value)
                {
                    this.onlyIssuable = value;
                    this.Loading();
                }
            }
        }

        private string FilterTexts { get; set; }
        public override void ApplyFilter(string filterTexts)
        {
            this.FilterTexts = filterTexts;

            OLVHelpers.ApplyFilters(this.fastAvailablePallets, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            OLVHelpers.ApplyFilters(this.fastAvailableCartons, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

            this.ShowRowCount();
        }

        private void ShowRowCount()
        {
            decimal? totalQuantityAvailables = this.fastAvailablePallets.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.QuantityAvailable).Sum();
            decimal? totalLineVolumeAvailables = this.fastAvailablePallets.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.LineVolumeAvailable).Sum();
            this.customTabBatch.TabPages[0].Text = "Available " + this.fastAvailablePallets.GetItemCount().ToString("N0") + " pallet" + (this.fastAvailablePallets.GetItemCount() > 1 ? "s" : "") + ", Total quantity: " + (totalQuantityAvailables != null ? ((decimal)totalQuantityAvailables).ToString("N0") : "0") + ", Total volume: " + (totalLineVolumeAvailables != null ? ((decimal)totalLineVolumeAvailables).ToString("N2") : "0") + "       ";

            totalQuantityAvailables = this.fastAvailableCartons.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.QuantityAvailable).Sum();
            totalLineVolumeAvailables = this.fastAvailableCartons.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.LineVolumeAvailable).Sum();
            this.customTabBatch.TabPages[1].Text = "Available " + this.fastAvailableCartons.GetItemCount().ToString("N0") + " carton" + (this.fastAvailableCartons.GetItemCount() > 1 ? "s" : "") + ", Total quantity: " + (totalQuantityAvailables != null ? ((decimal)totalQuantityAvailables).ToString("N0") : "0") + ", Total volume: " + (totalLineVolumeAvailables != null ? ((decimal)totalLineVolumeAvailables).ToString("N2") : "0") + "       ";
        }

        protected override PrintViewModel InitPrintViewModel()
        {
            PrintViewModel printViewModel = base.InitPrintViewModel();
            printViewModel.ReportPath = "AvailableItems";
            
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("LocationID", this.LocationID.ToString()));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("LocationCode", this.comboLocationID.Text));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("SummaryVersusDetail", this.comboSummaryVersusDetail.ComboBox.SelectedIndex.ToString()));

            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("CommodityCode", this.FilterTexts == null? "": this.FilterTexts));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("OnlyIssuable", this.OnlyIssuable.ToString()));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("OnlyApproved", this.OnlyApproved.ToString()));

            return printViewModel;
        }

    }
}
