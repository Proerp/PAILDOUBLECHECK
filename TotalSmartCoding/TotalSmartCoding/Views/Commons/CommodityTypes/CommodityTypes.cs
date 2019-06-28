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


namespace TotalSmartCoding.Views.Commons.CommodityTypes
{
    public partial class CommodityTypes : BaseView
    {
        private CustomTabControl customTabCenter;

        private CommodityTypeAPIs commodityTypeAPIs;
        private CommodityTypeViewModel commodityTypeViewModel { get; set; }

        public CommodityTypes()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastCommodityTypeIndex;

            this.commodityTypeAPIs = new CommodityTypeAPIs(CommonNinject.Kernel.Get<ICommodityTypeAPIRepository>());

            this.commodityTypeViewModel = CommonNinject.Kernel.Get<CommodityTypeViewModel>();
            this.commodityTypeViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.commodityTypeViewModel;
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Commodity Type       ");

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

            this.bindingName = this.textexName.DataBindings.Add("Text", this.commodityTypeViewModel, CommonExpressions.PropertyName<CommodityTypeDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.commodityTypeViewModel, CommonExpressions.PropertyName<CommodityTypeDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastCommodityTypeIndex.AboutToCreateGroups += fastCommodityTypeIndex_AboutToCreateGroups;
            this.fastCommodityTypeIndex.ShowGroups = true;
        }

        private void fastCommodityTypeIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "google-drive";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Type(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new CommodityTypeController(CommonNinject.Kernel.Get<ICommodityTypeService>(), this.commodityTypeViewModel); }
        }

        public override void Loading()
        {
            this.fastCommodityTypeIndex.SetObjects(this.commodityTypeAPIs.GetCommodityTypeIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastCommodityTypeIndex.Sort(this.olvGlobalName, SortOrder.Descending);
        }
    }
}
