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
using TotalSmartCoding.ViewModels.Sales;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalCore.Repositories.Sales;


namespace TotalSmartCoding.Views.Sales.PendingOrders
{
    public partial class PendingOrders : BaseView
    {
        private CustomTabControl customTabBatch;

        private DeliveryAdviceAPIs deliveryAdviceAPIs;
        private GoodsIssueAPIs goodsIssueAPIs;

        public PendingOrders()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;

            this.deliveryAdviceAPIs = new DeliveryAdviceAPIs(CommonNinject.Kernel.Get<IDeliveryAdviceAPIRepository>());
            this.goodsIssueAPIs = new GoodsIssueAPIs(CommonNinject.Kernel.Get<IGoodsIssueAPIRepository>());

            this.baseDTO = new PendingOrderViewModel();
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                this.customTabBatch = new CustomTabControl();

                this.customTabBatch.Font = this.fastWholePendingDeliveryAdviceDetails.Font;
                this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
                this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

                this.customTabBatch.TabPages.Add("tabPendingSalesOrders", "Pending sales orders");
                this.customTabBatch.TabPages.Add("tabPendingDeliveryAdvices", "Pending delivery advices");
                this.customTabBatch.TabPages.Add("tabPendingTransferOrders", "Pending transfer orders");
                this.customTabBatch.TabPages[0].Controls.Add(this.fastWholePendingSalesOrderDetails);
                this.customTabBatch.TabPages[1].Controls.Add(this.fastWholePendingDeliveryAdviceDetails);
                this.customTabBatch.TabPages[2].Controls.Add(this.fastWholePendingTransferOrderDetails);


                this.customTabBatch.Dock = DockStyle.Fill;
                this.fastWholePendingSalesOrderDetails.Dock = DockStyle.Fill;
                this.fastWholePendingDeliveryAdviceDetails.Dock = DockStyle.Fill;
                this.fastWholePendingTransferOrderDetails.Dock = DockStyle.Fill;
                this.Controls.Add(this.customTabBatch);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingLocationID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.locationID = ContextAttributes.User.LocationID; //JUST INIT THE local parameter only. DON'T INIT VIA: this.LocationID: BECAUSE IT WILL RAISE THE LocationID CHANGE EVENT
            LocationAPIs locationAPIs = new LocationAPIs(CommonNinject.Kernel.Get<ILocationAPIRepository>());

            this.comboLocationID.ComboBox.DataSource = locationAPIs.GetLocationBases();
            this.comboLocationID.ComboBox.DisplayMember = CommonExpressions.PropertyName<LocationBase>(p => p.Name);
            this.comboLocationID.ComboBox.ValueMember = CommonExpressions.PropertyName<LocationBase>(p => p.LocationID);
            this.bindingLocationID = this.comboLocationID.ComboBox.DataBindings.Add("SelectedValue", this, "LocationID", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingLocationID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastWholePendingSalesOrderDetails.AboutToCreateGroups += fastAvailableItems_AboutToCreateGroups;
            this.fastWholePendingDeliveryAdviceDetails.AboutToCreateGroups += fastAvailableItems_AboutToCreateGroups;
            this.fastWholePendingTransferOrderDetails.AboutToCreateGroups += fastAvailableItems_AboutToCreateGroups;
            this.fastWholePendingSalesOrderDetails.ShowGroups = true;
            this.fastWholePendingDeliveryAdviceDetails.ShowGroups = true;
            this.fastWholePendingTransferOrderDetails.ShowGroups = true;
        }

        private void fastAvailableItems_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = sender.Equals(this.fastWholePendingSalesOrderDetails) ? "Sign_Order_32" : (sender.Equals(this.fastWholePendingDeliveryAdviceDetails) ? "Schedule-32" : "filetransfer32");
                    olvGroup.Subtitle = "List count: " + olvGroup.Contents.Count.ToString();
                }
            }
        }

        public override void Loading()
        {
            try
            {
                this.fastWholePendingSalesOrderDetails.SelectedObject = null; this.fastWholePendingSalesOrderDetails.SelectedObjects = null;
                this.fastWholePendingDeliveryAdviceDetails.SelectedObject = null; this.fastWholePendingDeliveryAdviceDetails.SelectedObjects = null;
                this.fastWholePendingTransferOrderDetails.SelectedObject = null; this.fastWholePendingTransferOrderDetails.SelectedObjects = null;

                this.fastWholePendingSalesOrderDetails.SetObjects(this.deliveryAdviceAPIs.GetWholePendingSalesOrderDetails(this.LocationID));
                this.fastWholePendingDeliveryAdviceDetails.SetObjects(this.goodsIssueAPIs.GetWholePendingDeliveryAdviceDetails(this.LocationID));
                this.fastWholePendingTransferOrderDetails.SetObjects(this.goodsIssueAPIs.GetWholePendingTransferOrderDetails(this.LocationID));

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
            this.fastWholePendingSalesOrderDetails.Sort(this.olvSalesOrderCustomerName);
            this.fastWholePendingDeliveryAdviceDetails.Sort(this.olvDeliveryAdviceCustomerName);
            this.fastWholePendingTransferOrderDetails.Sort(this.olvTransferOrderWarehouseCode);
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

        private string FilterTexts { get; set; }
        public override void ApplyFilter(string filterTexts)
        {
            this.FilterTexts = filterTexts;

            OLVHelpers.ApplyFilters(this.fastWholePendingSalesOrderDetails, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            OLVHelpers.ApplyFilters(this.fastWholePendingDeliveryAdviceDetails, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            OLVHelpers.ApplyFilters(this.fastWholePendingTransferOrderDetails, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

            this.ShowRowCount();
        }

        private void ShowRowCount()
        {
            decimal? totalQuantityRemains = this.fastWholePendingSalesOrderDetails.FilteredObjects.Cast<WholePendingSalesOrderDetail>().Select(o => o.QuantityRemains).Sum();
            decimal? totalLineVolumeRemains = this.fastWholePendingSalesOrderDetails.FilteredObjects.Cast<WholePendingSalesOrderDetail>().Select(o => o.LineVolumeRemains).Sum();
            this.customTabBatch.TabPages[0].Text = "Pending sales orders: " + this.fastWholePendingSalesOrderDetails.GetItemCount().ToString("N0") + " line" + (this.fastWholePendingSalesOrderDetails.GetItemCount() > 1 ? "s" : "") + ", quantity: " + (totalQuantityRemains != null ? ((decimal)totalQuantityRemains).ToString("N0") : "0") + ", volume: " + (totalLineVolumeRemains != null ? ((decimal)totalLineVolumeRemains).ToString("N2") : "0.00") + "       ";

            totalQuantityRemains = this.fastWholePendingDeliveryAdviceDetails.FilteredObjects.Cast<WholePendingDeliveryAdviceDetail>().Select(o => o.QuantityRemains).Sum();
            totalLineVolumeRemains = this.fastWholePendingDeliveryAdviceDetails.FilteredObjects.Cast<WholePendingDeliveryAdviceDetail>().Select(o => o.LineVolumeRemains).Sum();
            this.customTabBatch.TabPages[1].Text = "Pending delivery advices: " + this.fastWholePendingDeliveryAdviceDetails.GetItemCount().ToString("N0") + " line" + (this.fastWholePendingDeliveryAdviceDetails.GetItemCount() > 1 ? "s" : "") + ", quantity: " + (totalQuantityRemains != null ? ((decimal)totalQuantityRemains).ToString("N0") : "0") + ", volume: " + (totalLineVolumeRemains != null ? ((decimal)totalLineVolumeRemains).ToString("N2") : "0.00") + "       ";

            totalQuantityRemains = this.fastWholePendingTransferOrderDetails.FilteredObjects.Cast<WholePendingTransferOrderDetail>().Select(o => o.QuantityRemains).Sum();
            totalLineVolumeRemains = this.fastWholePendingTransferOrderDetails.FilteredObjects.Cast<WholePendingTransferOrderDetail>().Select(o => o.LineVolumeRemains).Sum();
            this.customTabBatch.TabPages[2].Text = "Pending transfer orders: " + this.fastWholePendingTransferOrderDetails.GetItemCount().ToString("N0") + " line" + (this.fastWholePendingTransferOrderDetails.GetItemCount() > 1 ? "s" : "") + ", quantity: " + (totalQuantityRemains != null ? ((decimal)totalQuantityRemains).ToString("N0") : "0") + ", volume: " + (totalLineVolumeRemains != null ? ((decimal)totalLineVolumeRemains).ToString("N2") : "0.00") + "       ";
        }

        protected override PrintViewModel InitPrintViewModel()
        {
            PrintViewModel printViewModel = base.InitPrintViewModel();
            printViewModel.ReportPath = "PendingOrders";

            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("LocationID", this.LocationID.ToString()));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("LocationCode", this.comboLocationID.Text));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("CommodityCode", this.FilterTexts == null ? "" : this.FilterTexts));

            return printViewModel;
        }
    } 
}
