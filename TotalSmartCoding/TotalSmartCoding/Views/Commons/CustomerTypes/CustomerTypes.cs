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


namespace TotalSmartCoding.Views.Commons.CustomerTypes
{
    public partial class CustomerTypes : BaseView
    {
        private CustomTabControl customTabCenter;

        private CustomerTypeAPIs customerTypeAPIs;
        private CustomerTypeViewModel customerTypeViewModel { get; set; }

        public CustomerTypes()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastCustomerTypeIndex;

            this.customerTypeAPIs = new CustomerTypeAPIs(CommonNinject.Kernel.Get<ICustomerTypeAPIRepository>());

            this.customerTypeViewModel = CommonNinject.Kernel.Get<CustomerTypeViewModel>();
            this.customerTypeViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.customerTypeViewModel;
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Customer Type      ");

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

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingName = this.textexName.DataBindings.Add("Text", this.customerTypeViewModel, CommonExpressions.PropertyName<CustomerTypeDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.customerTypeViewModel, CommonExpressions.PropertyName<CustomerTypeDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastCustomerTypeIndex.AboutToCreateGroups += fastCustomerTypeIndex_AboutToCreateGroups;
            this.fastCustomerTypeIndex.ShowGroups = true;
        }

        private void fastCustomerTypeIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "google-drive-blue";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Type(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new CustomerTypeController(CommonNinject.Kernel.Get<ICustomerTypeService>(), this.customerTypeViewModel); }
        }

        public override void Loading()
        {
            this.fastCustomerTypeIndex.SetObjects(this.customerTypeAPIs.GetCustomerTypeIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastCustomerTypeIndex.Sort(this.olvGlobalName, SortOrder.Descending);
        }
    }
}
