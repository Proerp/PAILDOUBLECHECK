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


namespace TotalSmartCoding.Views.Commons.CustomerCategories
{
    public partial class CustomerCategories : BaseView
    {
        private CustomTabControl customTabCenter;

        private CustomerCategoryAPIs customerCategoryAPIs;
        private CustomerCategoryViewModel customerCategoryViewModel { get; set; }

        public CustomerCategories()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastCustomerCategoryIndex;

            this.customerCategoryAPIs = new CustomerCategoryAPIs(CommonNinject.Kernel.Get<ICustomerCategoryAPIRepository>());

            this.customerCategoryViewModel = CommonNinject.Kernel.Get<CustomerCategoryViewModel>();
            this.customerCategoryViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.customerCategoryViewModel;
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Customer Category      ");

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

            this.bindingName = this.textexName.DataBindings.Add("Text", this.customerCategoryViewModel, CommonExpressions.PropertyName<CustomerCategoryDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.customerCategoryViewModel, CommonExpressions.PropertyName<CustomerCategoryDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastCustomerCategoryIndex.AboutToCreateGroups += fastCustomerCategoryIndex_AboutToCreateGroups;
            this.fastCustomerCategoryIndex.ShowGroups = true;
        }

        private void fastCustomerCategoryIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "2-squares-blue";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Category(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new CustomerCategoryController(CommonNinject.Kernel.Get<ICustomerCategoryService>(), this.customerCategoryViewModel); }
        }

        public override void Loading()
        {
            this.fastCustomerCategoryIndex.SetObjects(this.customerCategoryAPIs.GetCustomerCategoryIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastCustomerCategoryIndex.Sort(this.olvGlobalName, SortOrder.Descending);
        }
    }
}
