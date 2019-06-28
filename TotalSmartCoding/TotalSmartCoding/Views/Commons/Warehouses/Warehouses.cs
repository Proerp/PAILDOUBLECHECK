using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;

using Ninject;


using TotalBase;
using TotalBase.Enums;
using TotalDTO.Commons;
using TotalModel.Models;

using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

using TotalSmartCoding.Properties;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalSmartCoding.ViewModels.Commons;

using TotalSmartCoding.Controllers.Commons;
using TotalSmartCoding.Controllers.APIs.Commons;

using TotalSmartCoding.Views.Mains;
using TotalSmartCoding.Libraries.StackedHeaders;


namespace TotalSmartCoding.Views.Commons.Warehouses
{
    public partial class Warehouses : BaseView
    {
        private CustomTabControl customTabCenter;

        private WarehouseAPIs teamAPIs;
        private WarehouseViewModel teamViewModel { get; set; }

        public Warehouses()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastWarehouseIndex;

            this.teamAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());

            this.teamViewModel = CommonNinject.Kernel.Get<WarehouseViewModel>();
            this.teamViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.teamViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();
                
                #region TabCenter
                this.customTabCenter = new CustomTabControl();
                this.customTabCenter.DisplayStyle = TabStyle.VisualStudio;
                this.customTabCenter.Font = this.panelCenter.Font;

                this.customTabCenter.TabPages.Add("tabCenterAA", "Warehouse       ");

                this.customTabCenter.TabPages[0].Controls.Add(this.layoutTop);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.layoutTop.Dock = DockStyle.Fill;

                this.panelCenter.Controls.Add(this.customTabCenter);
                this.customTabCenter.Dock = DockStyle.Fill;
                #endregion TabCenter

                this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].SizeType = SizeType.Absolute; this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].Width = 15;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingName;
        Binding bindingRemarks;
        Binding bindingBookable;
        Binding bindingIssuable;
        Binding bindingIsDefault;
        
        Binding bindingLocationID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingName = this.textexName.DataBindings.Add("Text", this.teamViewModel, CommonExpressions.PropertyName<WarehouseDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.teamViewModel, CommonExpressions.PropertyName<WarehouseDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingBookable = this.checkBookable.DataBindings.Add("Checked", this.teamViewModel, CommonExpressions.PropertyName<WarehouseDTO>(p => p.Bookable), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingIssuable = this.checkIssuable.DataBindings.Add("Checked", this.teamViewModel, CommonExpressions.PropertyName<WarehouseDTO>(p => p.Issuable), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingIsDefault = this.checkIsDefault.DataBindings.Add("Checked", this.teamViewModel, CommonExpressions.PropertyName<WarehouseDTO>(p => p.IsDefault), true, DataSourceUpdateMode.OnPropertyChanged);

            LocationAPIs locationAPIs = new LocationAPIs(CommonNinject.Kernel.Get<ILocationAPIRepository>());
            this.combexLocationID.DataSource = locationAPIs.GetLocationBases(true);
            this.combexLocationID.DisplayMember = CommonExpressions.PropertyName<LocationBase>(p => p.Name);
            this.combexLocationID.ValueMember = CommonExpressions.PropertyName<LocationBase>(p => p.LocationID);
            this.bindingLocationID = this.combexLocationID.DataBindings.Add("SelectedValue", this.teamViewModel, CommonExpressions.PropertyName<WarehouseDTO>(p => p.LocationID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingBookable.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingIssuable.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingIsDefault.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingLocationID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastWarehouseIndex.AboutToCreateGroups += fastWarehouseIndex_AboutToCreateGroups;
            this.fastWarehouseIndex.ShowGroups = true;
        }

        private void fastWarehouseIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Warehouse";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Warehouse(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new WarehouseController(CommonNinject.Kernel.Get<IWarehouseService>(), this.teamViewModel); }
        }

        public override void Loading()
        {
            this.fastWarehouseIndex.SetObjects(this.teamAPIs.GetWarehouseIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastWarehouseIndex.Sort(this.olvGlobalName, SortOrder.Descending);
        }
    }
}
