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


namespace TotalSmartCoding.Views.Commons.CommodityCategories
{
    public partial class CommodityCategories : BaseView
    {
        private CustomTabControl customTabCenter;

        private CommodityCategoryAPIs commodityCategoryAPIs;
        private CommodityCategoryViewModel commodityCategoryViewModel { get; set; }

        public CommodityCategories()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastCommodityCategoryIndex;

            this.commodityCategoryAPIs = new CommodityCategoryAPIs(CommonNinject.Kernel.Get<ICommodityCategoryAPIRepository>());

            this.commodityCategoryViewModel = CommonNinject.Kernel.Get<CommodityCategoryViewModel>();
            this.commodityCategoryViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.commodityCategoryViewModel;
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Commodity Category      ");

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

            this.bindingName = this.textexName.DataBindings.Add("Text", this.commodityCategoryViewModel, CommonExpressions.PropertyName<CommodityCategoryDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.commodityCategoryViewModel, CommonExpressions.PropertyName<CommodityCategoryDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastCommodityCategoryIndex.AboutToCreateGroups += fastCommodityCategoryIndex_AboutToCreateGroups;
            this.fastCommodityCategoryIndex.ShowGroups = true;
        }

        private void fastCommodityCategoryIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "2-squares";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Category(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new CommodityCategoryController(CommonNinject.Kernel.Get<ICommodityCategoryService>(), this.commodityCategoryViewModel); }
        }

        public override void Loading()
        {
            this.fastCommodityCategoryIndex.SetObjects(this.commodityCategoryAPIs.GetCommodityCategoryIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastCommodityCategoryIndex.Sort(this.olvGlobalName, SortOrder.Descending);
        }
    }
}
