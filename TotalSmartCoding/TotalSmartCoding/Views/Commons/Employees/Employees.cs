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



using TotalBase.Enums;
using TotalSmartCoding.Properties;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalSmartCoding.Controllers.Commons;
using TotalCore.Repositories.Commons;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;
using TotalBase;
using TotalModel.Models;
using TotalDTO.Commons;
using BrightIdeasSoftware;
using TotalSmartCoding.Libraries.StackedHeaders;


namespace TotalSmartCoding.Views.Commons.Employees
{
    public partial class Employees : BaseView
    {
        private CustomTabControl customTabCenter;

        private EmployeeAPIs employeeAPIs;
        private EmployeeViewModel employeeViewModel { get; set; }

        public Employees()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastEmployeeIndex;

            this.employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

            this.employeeViewModel = CommonNinject.Kernel.Get<EmployeeViewModel>();
            this.employeeViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.employeeViewModel;
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Detail Info      ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Location && Roles");

                this.customTabCenter.TabPages[0].Controls.Add(this.layoutTop);
                this.customTabCenter.TabPages[1].Controls.Add(this.layoutRight);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.customTabCenter.TabPages[1].BackColor = this.panelCenter.BackColor;
                this.layoutTop.Dock = DockStyle.Fill;
                this.layoutRight.Dock = DockStyle.Fill;

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

        Binding bindingCode;
        Binding bindingName;

        Binding bindingTitle;
        Binding bindingBirthday;

        Binding bindingTeamID;

        Binding bindingTelephone;
        Binding bindingAddress;

        Binding bindingRemarks;

        Binding bindingCheckLocation1;
        Binding bindingCheckLocation2;
        Binding bindingCheckLocation3;
        Binding bindingCheckLocation4;

        Binding bindingCheckRole1;
        Binding bindingCheckRole2;
        Binding bindingCheckRole3;


        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingCode = this.textexCode.DataBindings.Add("Text", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.Code), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingName = this.textexName.DataBindings.Add("Text", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingTitle = this.textexTitle.DataBindings.Add("Text", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.Title), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingBirthday = this.dateTimexBirthday.DataBindings.Add("Value", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.Birthday), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingTelephone = this.textexTelephone.DataBindings.Add("Text", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.Telephone), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingAddress = this.textexAddress.DataBindings.Add("Text", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.Address), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingCheckLocation1 = this.CheckLocation1.DataBindings.Add("Checked", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.CheckLocation1), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCheckLocation2 = this.CheckLocation2.DataBindings.Add("Checked", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.CheckLocation2), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCheckLocation3 = this.CheckLocation3.DataBindings.Add("Checked", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.CheckLocation3), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCheckLocation4 = this.CheckLocation4.DataBindings.Add("Checked", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.CheckLocation4), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingCheckRole1 = this.CheckRole1.DataBindings.Add("Checked", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.CheckRole1), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCheckRole2 = this.CheckRole2.DataBindings.Add("Checked", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.CheckRole2), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCheckRole3 = this.CheckRole3.DataBindings.Add("Checked", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeDTO>(p => p.CheckRole3), true, DataSourceUpdateMode.OnPropertyChanged);

            TeamAPIs teamAPIs = new TeamAPIs(CommonNinject.Kernel.Get<ITeamAPIRepository>());
            this.combexTeamID.DataSource = teamAPIs.GetTeamBases();
            this.combexTeamID.DisplayMember = CommonExpressions.PropertyName<TeamBase>(p => p.Name);
            this.combexTeamID.ValueMember = CommonExpressions.PropertyName<TeamBase>(p => p.TeamID);
            this.bindingTeamID = this.combexTeamID.DataBindings.Add("SelectedValue", this.employeeViewModel, CommonExpressions.PropertyName<EmployeeViewModel>(p => p.TeamID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingTitle.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingBirthday.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingTelephone.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingAddress.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingCheckLocation1.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCheckLocation2.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCheckLocation3.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCheckLocation4.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingCheckRole1.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCheckRole2.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCheckRole3.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingTeamID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastEmployeeIndex.AboutToCreateGroups += fastEmployeeIndex_AboutToCreateGroups;

            this.fastEmployeeIndex.ShowGroups = true;
            this.olvInActive.Renderer = new MappedImageRenderer(new Object[] { 1, Resources.Void_16 });
        }

        private void fastEmployeeIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Emp-32";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Employee(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new EmployeeController(CommonNinject.Kernel.Get<IEmployeeService>(), this.employeeViewModel); }
        }

        public override void Loading()
        {
            this.fastEmployeeIndex.SetObjects(this.employeeAPIs.GetEmployeeIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastEmployeeIndex.Sort(this.olvTeamCode, SortOrder.Descending);
        }
    }
}
