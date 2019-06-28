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


namespace TotalSmartCoding.Views.Commons.Territories
{
    public partial class Territories : BaseView
    {
        private CustomTabControl customTabCenter;

        private TerritoryAPIs territoryAPIs;
        private TerritoryViewModel territoryViewModel { get; set; }

        public Territories()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastTerritoryIndex;

            this.territoryAPIs = new TerritoryAPIs(CommonNinject.Kernel.Get<ITerritoryAPIRepository>());

            this.territoryViewModel = CommonNinject.Kernel.Get<TerritoryViewModel>();
            this.territoryViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.territoryViewModel;
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Territory Info      ");

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

            this.bindingName = this.textexName.DataBindings.Add("Text", this.territoryViewModel, CommonExpressions.PropertyName<TerritoryDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.territoryViewModel, CommonExpressions.PropertyName<TerritoryDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastTerritoryIndex.AboutToCreateGroups += fastTerritoryIndex_AboutToCreateGroups;
            this.fastTerritoryIndex.ShowGroups = true;
        }

        private void fastTerritoryIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "earth-globe";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Territories";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new TerritoryController(CommonNinject.Kernel.Get<ITerritoryService>(), this.territoryViewModel); }
        }

        public override void Loading()
        {
            this.fastTerritoryIndex.SetObjects(this.territoryAPIs.GetTerritoryIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastTerritoryIndex.Sort(this.olvGlobalName, SortOrder.Descending);
        }
    }
}
