using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

using BrightIdeasSoftware;

using Ninject;
using Guifreaks.Navisuite;

using TotalBase;
using TotalBase.Enums;

using TotalCore.Repositories;
using TotalCore.Repositories.Generals;

using TotalModel.Models;
using TotalModel.Helpers;

using TotalSmartCoding.Properties;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Generals;

using TotalSmartCoding.Views.Generals;
using TotalSmartCoding.Views.Commons;

using TotalSmartCoding.Views.Productions;

using TotalSmartCoding.Views.Inventories.Pickups;
using TotalSmartCoding.Views.Inventories.GoodsIssues;
using TotalSmartCoding.Views.Inventories.WarehouseAdjustments;
using TotalSmartCoding.Views.Inventories.GoodsReceipts;

using TotalSmartCoding.Views.Sales.Forecasts;
using TotalSmartCoding.Views.Sales.SalesOrders;
using TotalSmartCoding.Views.Sales.DeliveryAdvices;
using TotalSmartCoding.Views.Sales.TransferOrders;
using TotalSmartCoding.Views.Sales.PendingOrders;

using TotalSmartCoding.Views.Commons.Customers;
using TotalSmartCoding.Views.Commons.CustomerTypes;
using TotalSmartCoding.Views.Commons.CustomerCategories;
using TotalSmartCoding.Views.Commons.BinLocations;
using TotalSmartCoding.Views.Commons.Territories;
using TotalSmartCoding.Views.Commons.Warehouses;

using TotalSmartCoding.Views.Commons.Teams;
using TotalSmartCoding.Views.Commons.Employees;

using TotalSmartCoding.Views.Commons.Commodities;
using TotalSmartCoding.Views.Commons.CommodityTypes;
using TotalSmartCoding.Views.Commons.CommodityCategories;
using TotalSmartCoding.Views.Commons.CommoditySettings;

using TotalSmartCoding.ViewModels.Helpers;


namespace TotalSmartCoding.Views.Mains
{
    public partial class MasterMDI : Form
    {
        #region Contractor

        Binding beginingDateBinding;
        Binding endingDateBinding;

        Binding buttonNaviBarHeaderVisibleBinding;

        private GlobalEnums.NmvnTaskID nmvnTaskID;

        private ModuleAPIs moduleAPIs;

        private bool isMainView;
        private readonly string searchPlaceHolder = "Enter a whole or any section of barcode ...";

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        public MasterMDI()
            : this(GlobalEnums.NmvnTaskID.UnKnown)
        { }

        public MasterMDI(GlobalEnums.NmvnTaskID nmvnTaskID)
            : this(nmvnTaskID, null)
        { }

        public MasterMDI(Form loadedView)
            : this(GlobalEnums.NmvnTaskID.UnKnown, loadedView)
        { }

        public MasterMDI(GlobalEnums.NmvnTaskID nmvnTaskID, Form loadedView)
            : this(nmvnTaskID, loadedView, true)
        { }

        public MasterMDI(GlobalEnums.NmvnTaskID nmvnTaskID, Form loadedView, bool isMainView)
        {
            InitializeComponent();

            try
            {
                this.nmvnTaskID = nmvnTaskID;
                IModuleAPIRepository moduleAPIRepository = CommonNinject.Kernel.Get<IModuleAPIRepository>();
                this.moduleAPIs = new ModuleAPIs(moduleAPIRepository);

                if (GlobalEnums.NMVNOnly) this.panelTopRight.Visible = false;

                switch (this.nmvnTaskID)
                {
                    case GlobalEnums.NmvnTaskID.SmartCoding:
                        this.buttonEscape.Visible = false;
                        this.buttonLoading.Visible = false;
                        this.buttonNew.Visible = false;
                        this.buttonEdit.Visible = false;
                        this.buttonSave.Visible = false;
                        this.buttonDelete.Visible = false;
                        this.buttonImport.Visible = false;
                        this.buttonExport.Visible = false;
                        this.toolStripSeparatorImport.Visible = false;
                        this.buttonApprove.Visible = false;
                        this.buttonVoid.Visible = false;
                        this.toolStripSeparatorApprove.Visible = false;
                        this.toolStripSeparatorVoid.Visible = false;
                        this.buttonPrint.Visible = false;
                        this.buttonPrintPreview.Visible = false;
                        this.toolStripSeparatorPrint.Visible = false;
                        this.separatorInputData.Visible = false;
                        this.labelSearchBarcode.Visible = false;
                        this.toolStripTopHead.Visible = false;
                        break;
                    case GlobalEnums.NmvnTaskID.Batch:
                        this.Size = new Size(1180, 732);
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                        this.MinimizeBox = false; this.MaximizeBox = false; this.WindowState = FormWindowState.Normal;

                        this.panelTopRight.Visible = false;
                        this.panelTopLeft.Dock = DockStyle.Fill;
                        break;
                    case GlobalEnums.NmvnTaskID.FillingLine:
                        this.Size = new Size(620, 360);
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                        this.MinimizeBox = false; this.MaximizeBox = false; this.WindowState = FormWindowState.Normal;

                        this.toolStripTopHead.Visible = false;
                        this.panelTopRight.Visible = false;
                        this.panelTopLeft.Dock = DockStyle.Fill;
                        this.statusStrip.Visible = false;
                        break;
                    default:
                        break;
                }

                this.beginingDateBinding = this.dateTimexLowerFillterDate.DataBindings.Add("Value", GlobalEnums.GlobalOptionSetting, CommonExpressions.PropertyName<OptionSetting>(p => p.LowerFillterDate), true, DataSourceUpdateMode.OnPropertyChanged);
                this.endingDateBinding = this.dateTimexUpperFillterDate.DataBindings.Add("Value", GlobalEnums.GlobalOptionSetting, CommonExpressions.PropertyName<OptionSetting>(p => p.UpperFillterDate), true, DataSourceUpdateMode.OnPropertyChanged);

                this.beginingDateBinding.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.endingDateBinding.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.buttonNaviBarHeaderVisibleBinding = this.buttonNaviBarHeader.DataBindings.Add("Visible", this.naviBarModuleMaster, "Collapsed", true, DataSourceUpdateMode.OnPropertyChanged);
                this.buttonNaviBarHeaderVisibleBinding.Parse += new ConvertEventHandler(buttonNaviBarHeaderVisibleBinding_Parse);
                this.buttonNaviBarHeaderVisibleBinding.Format += new ConvertEventHandler(buttonNaviBarHeaderVisibleBinding_Format);

                #region fastNMVNTask
                this.fastNMVNTask.AboutToCreateGroups += fastNMVNTask_AboutToCreateGroups;
                this.fastNMVNTask.ShowGroups = true;

                //this.fastNMVNTask.UseTranslucentHotItem = true; //DEFAULT HotItem
                fastNMVNTask.UseTranslucentHotItem = false;
                fastNMVNTask.UseHotItem = true;
                fastNMVNTask.UseExplorerTheme = false;

                RowBorderDecoration rbd = new RowBorderDecoration();
                rbd.BorderPen = new Pen(Color.SeaGreen, 2);
                rbd.FillBrush = null;
                rbd.CornerRounding = 4.0f;
                HotItemStyle hotItemStyle2 = new HotItemStyle();
                hotItemStyle2.Decoration = rbd;
                fastNMVNTask.HotItemStyle = hotItemStyle2;
                #endregion fastNMVNTask


                if (loadedView != null)
                {
                    this.naviBarModuleMaster.Visible = false;
                    this.OpenView(loadedView);
                }
                else
                {
                    this.InitializeModuleMaster();
                    //this.buttonNaviBarHeader_Click(this.buttonNaviBarHeader, new EventArgs());
                }

                DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                this.statusVersion.Text = "Version 1.0." + GlobalVariables.ConfigVersionID(GlobalVariables.ConfigID).ToString() + ", Date: " + buildDate.ToString("dd/MM/yyyy HH:mm:ss");
                this.labelDataSource.Text = this.moduleAPIs.DataSource;
                this.labelApplicationRole.Text = ApplicationRoles.Required && ApplicationRoles.Name != "" && ApplicationRoles.ExceptionMessage == "" ? "[Application Role]" : "[Windows Authentication]";

                this.comboSearchBarcode.Text = this.searchPlaceHolder;
                this.toolUserReferences.Visible = ContextAttributes.User.IsDatabaseAdmin && false; //HIDE AT CHEVRON
                this.toolUserGroupControls.Visible = ContextAttributes.User.IsDatabaseAdmin;
                this.statusUserDescription.Text = ContextAttributes.User.FullyQualifiedUserName;

                this.panelTopRight.Width = (this.nmvnTaskID == GlobalEnums.NmvnTaskID.SmartCoding ? 10 : this.labelSearchBarcode.Width) + this.comboSearchBarcode.Width + this.buttonSearchBarcode.Width + 10;
                this.panelTop.Height = this.nmvnTaskID == GlobalEnums.NmvnTaskID.SmartCoding ? 61 : 39;

                #region JUST DISABLE FOR CHEVRON
                int? accessLevel = moduleAPIRepository.TotalSmartCodingEntities.GetAccessLevel(ContextAttributes.User.UserID, (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.MonthEnd, 0).Single();
                if (accessLevel < (int)TotalBase.Enums.GlobalEnums.AccessLevel.Readable) this.buttonLockedDate.Enabled = false;

                this.txtLockedDate.Visible = false;
                //this.buttonLockedDate.Visible = false;
                #endregion

                this.isMainView = isMainView;
                if (this.isMainView) this.AddEventLogs("Log on application");
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void MasterMDI_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.naviBarModuleMaster.Bands.Count > 0) this.naviBarModuleMaster.ActiveBand = this.naviBarModuleMaster.Bands[1];
                this.naviBarModuleMaster.PopupHeight = this.naviBarModuleMaster.Height + this.naviBarModuleMaster.HeaderHeight - (this.naviBarModuleMaster.ButtonHeight + 6) * (this.naviBarModuleMaster.Bands.Count + 2) - 7;
                //this.naviBarModuleMaster.PopupMinWidth = this.naviBarModuleMaster.Width;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void MasterMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                for (int i = 0; i < this.MdiChildren.Length; i++)
                {
                    IToolstripChild mdiChildCallToolStrip = this.MdiChildren[i] as IToolstripChild;
                    if (mdiChildCallToolStrip != null)
                    {
                        if (mdiChildCallToolStrip.ReadonlyMode) ((Form)mdiChildCallToolStrip).Close();
                    }
                    else
                        this.MdiChildren[i].Close();
                }

                if (this.MdiChildren.Length > 0)
                    e.Cancel = true;
                else
                    if (this.isMainView) this.AddEventLogs("Exit application");
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void fastNMVNTask_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    if (olvGroup.Key != null)
                    {
                        olvGroup.TitleImage = olvGroup.Key.ToString() == "LOGISTICS ADMIN" ? "Warehouse_Navy-32" : (olvGroup.Key.ToString() == "WAREHOUSE CONTROLS" ? "Warehouse_Navy-32" : (olvGroup.Key.ToString() == "CUSTOMER MANAGEMENT" ? "Member-32" : (olvGroup.Key.ToString() == "WAREHOUSE RESOURCES" ? "Member-32" : "Report-Yellow-32")));
                        olvGroup.Subtitle = olvGroup.Key.ToString() == "LOGISTICS ADMIN" ? "Focuses on Orders" : (olvGroup.Key.ToString() == "WAREHOUSE CONTROLS" ? "Regulator for Warehousing Activities" : (olvGroup.Key.ToString() == "CUSTOMER MANAGEMENT" ? "Sales Governance & Organization" : (olvGroup.Key.ToString() == "WAREHOUSE RESOURCES" ? "Commodities & Related Resources" : null)));
                    }
                }
            }
        }

        private void buttonNaviBarHeaderVisibleBinding_Parse(object sender, ConvertEventArgs e)
        {
            e.Value = !(bool)e.Value;
        }

        private void buttonNaviBarHeaderVisibleBinding_Format(object sender, ConvertEventArgs e)
        {
            e.Value = !(bool)e.Value;
        }

        private void buttonNaviBarHeader_Click(object sender, EventArgs e)
        {
            this.naviBarModuleMaster.Collapsed = true;
        }

        private void naviBarModuleMaster_CollapsedChanged(object sender, EventArgs e)
        {
            this.olvModuleDetailName.Width = 239;// this.naviBarModuleMaster.Width - this.fastNMVNTask.Columns[0].Width + (this.naviBarModuleMaster.Collapsed ? -4 : 4);
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception) { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
        }

        #endregion Contractor




        #region Load and Open Module, Task

        private NaviBand lastActiveBand;
        private void naviBarModuleMaster_ActiveBandChanged(object sender, EventArgs e)
        {
            try
            {
                int moduleID;
                if (int.TryParse(this.naviBarModuleMaster.ActiveBand.Tag.ToString(), out  moduleID))
                {
                    if (moduleID == 9)
                    {
                        this.OpenModuleDetail((int)GlobalEnums.NmvnTaskID.Reports);
                        if (lastActiveBand != null) this.naviBarModuleMaster.ActiveBand = lastActiveBand;
                    }
                    else
                    {
                        this.buttonNaviBarHeader.Text = this.naviBarModuleMaster.ActiveBand.Text;

                        this.fastNMVNTask.Parent = null;
                        this.naviBarModuleMaster.ActiveBand.ClientArea.Controls.Add(this.fastNMVNTask);
                        this.fastNMVNTask.Dock = DockStyle.Fill;
                        this.fastNMVNTask.Visible = true;

                        lastActiveBand = this.naviBarModuleMaster.ActiveBand;

                        InitializeTaskMaster(moduleID);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void InitializeModuleMaster()
        {
            try
            {
                ICollection<ModuleIndex> moduleIndexs = this.moduleAPIs.GetModuleIndexes();

                foreach (ModuleIndex moduleIndex in moduleIndexs)
                {
                    if (moduleIndex.ModuleID == 1 || moduleIndex.ModuleID == 6 || moduleIndex.ModuleID == 9)
                    {
                        NaviBand naviBand = new NaviBand();

                        naviBand.Text = moduleIndex.Code;
                        naviBand.Tag = moduleIndex.ModuleID.ToString();

                        naviBand.SmallImage = this.imageListModuleMasterSmall.Images[moduleIndex.ModuleID == 1 ? 0 : (moduleIndex.ModuleID == 6 ? 1 : 2)];
                        naviBand.LargeImage = this.imageListModuleMasterLarge.Images[moduleIndex.ModuleID == 1 ? 0 : (moduleIndex.ModuleID == 6 ? 1 : 2)];

                        this.naviBarModuleMaster.Bands.Add(naviBand);
                    }
                }

                this.naviBarModuleMaster.VisibleLargeButtons = this.naviBarModuleMaster.Bands.Count;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void InitializeTaskMaster(int moduleID)
        {
            try
            {
                this.fastNMVNTask.SetObjects(this.moduleAPIs.GetModuleViewDetails(moduleID));
                this.fastNMVNTask.Sort(this.olvModuleDetailController, SortOrder.Ascending);

                this.olvModuleDetailName.Width = 239;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }

        }

        private void fastNMVNTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.fastNMVNTask.SelectedObject != null)
                {
                    ModuleViewDetail moduleViewDetail = (ModuleViewDetail)this.fastNMVNTask.SelectedObject;
                    if (moduleViewDetail != null)
                        this.OpenModuleDetail(moduleViewDetail.ModuleDetailID);

                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void OpenModuleDetail(int moduleDetailID)
        {
            if (moduleDetailID > 0)
            {
                Form openingView = null;//Form to open

                for (int i = 0; i < this.MdiChildren.Length; i++)
                {//Find and active the current form
                    IToolstripChild mdiChildCallToolStrip = this.MdiChildren[i] as IToolstripChild;
                    if (mdiChildCallToolStrip != null)
                    {
                        if (moduleDetailID == (int)mdiChildCallToolStrip.NMVNTaskID)
                        {
                            openingView = (Form)this.MdiChildren[i];
                            break;
                        }
                    }
                }

                if (openingView != null)
                {
                    openingView.Activate();
                    IToolstripChild mdiChildCallToolStrip = openingView as IToolstripChild;
                    if (mdiChildCallToolStrip != null) mdiChildCallToolStrip.DoAfterActivate();
                }
                else
                { //OPEN NEW VIEW
                    switch (moduleDetailID)
                    {
                        case (int)GlobalEnums.NmvnTaskID.Customers:
                            openingView = new Customers();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.CustomerTypes:
                            openingView = new CustomerTypes();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.CustomerCategories:
                            openingView = new CustomerCategories();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.Commodities:
                            openingView = new Commodities();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.CommoditySettings:
                            openingView = new CommoditySettings();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.CommodityTypes:
                            openingView = new CommodityTypes();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.CommodityCategories:
                            openingView = new CommodityCategories();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.BinLocations:
                            openingView = new BinLocations();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.Teams:
                            openingView = new Teams();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.Territories:
                            openingView = new Territories();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.Warehouses:
                            openingView = new Warehouses();
                            break;
                        case (int)GlobalEnums.NmvnTaskID.Employees:
                            openingView = new Employees();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.Pickups:
                            openingView = new Pickups();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.GoodsReceipts:
                            openingView = new GoodsReceipts();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.Forecasts:
                            openingView = new Forecasts();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.SalesOrders:
                            openingView = new SalesOrders();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.DeliveryAdvices:
                            openingView = new DeliveryAdvices();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.TransferOrders:
                            openingView = new TransferOrders();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.GoodsIssues:
                            openingView = new GoodsIssues();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.WarehouseAdjustments:
                            openingView = new WarehouseAdjustments();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.AvailableItems:
                            openingView = new GoodsReceiptDetailAvailables();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.PendingOrders:
                            openingView = new PendingOrders();
                            break;

                        case (int)GlobalEnums.NmvnTaskID.Reports:
                            openingView = new Reports();
                            break;

                        default:
                            openingView = new BlankView();
                            break;
                    }

                    if (openingView != null) this.OpenView(openingView);
                }

                if (!this.naviBarModuleMaster.Collapsed)
                    buttonNaviBarHeader_Click(this.buttonNaviBarHeader, new EventArgs());

            }
        }

        private void OpenView(Form openingView)
        {
            openingView.MdiParent = this; //childForm.Owner = this;
            if (!(openingView is SmartCoding))
                openingView.WindowState = FormWindowState.Maximized;
            openingView.Show();
        }


        #endregion Load and Open Module, Task






        #region Form Events: Merge toolstrip & Set toolbar context
        private void MasterMdi_MdiChildActivate(object sender, EventArgs e)
        {
            try
            {
                ToolStripManager.RevertMerge(this.toolstripMain);
                IToolstripMerge mergeToolstrip = ActiveMdiChild as IToolstripMerge;
                if (mergeToolstrip != null)
                {
                    ToolStripManager.Merge(mergeToolstrip.toolstripChild, toolstripMain);
                }

                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null)
                {
                    toolstripChild.PropertyChanged -= new PropertyChangedEventHandler(toolstripChild_PropertyChanged);
                    toolstripChild.PropertyChanged += new PropertyChangedEventHandler(toolstripChild_PropertyChanged);

                    toolstripChild_PropertyChanged(toolstripChild, new PropertyChangedEventArgs("IsDirty"));
                }

                if (ActiveMdiChild != null)
                    ActiveMdiChild.WindowState = FormWindowState.Maximized;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void toolstripChild_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = sender as IToolstripChild;
                if (toolstripChild != null)
                {

                    bool closable = toolstripChild.Closable;
                    bool loadable = toolstripChild.Loadable;
                    bool newable = toolstripChild.Newable;
                    bool editable = toolstripChild.Editable;
                    bool isDirty = toolstripChild.IsDirty;
                    bool deletable = toolstripChild.Deletable;
                    bool importable = toolstripChild.Importable;
                    bool exportable = toolstripChild.Exportable;
                    bool approvable = toolstripChild.Approvable;
                    bool unapprovable = toolstripChild.Unapprovable;
                    bool voidable = toolstripChild.Voidable;
                    bool unvoidable = toolstripChild.Unvoidable;

                    bool printable = toolstripChild.Printable;
                    bool printVisible = toolstripChild.PrintVisible;

                    bool readonlyMode = toolstripChild.ReadonlyMode;
                    bool editableMode = toolstripChild.EditableMode;

                    bool isValid = toolstripChild.IsValid;


                    this.buttonEscape.Enabled = closable;
                    this.buttonLoading.Enabled = loadable && readonlyMode;

                    this.separatorInputData.Visible = toolstripChild.AllowDataInput;
                    this.buttonNew.Visible = toolstripChild.AllowDataInput;
                    this.buttonEdit.Visible = toolstripChild.AllowDataInput;
                    this.buttonSave.Visible = toolstripChild.AllowDataInput;
                    this.buttonDelete.Visible = toolstripChild.AllowDataInput;

                    this.buttonNew.Enabled = newable && readonlyMode;
                    this.buttonEdit.Enabled = editable && readonlyMode;
                    this.buttonSave.Enabled = isDirty && isValid && editableMode;
                    this.buttonDelete.Enabled = deletable && readonlyMode;

                    this.buttonImport.Visible = importable;
                    this.buttonImport.Enabled = importable && newable && readonlyMode;
                    this.buttonExport.Visible = exportable;
                    this.buttonExport.Enabled = exportable;//&& !isDirty && readonlyMode;
                    this.toolStripSeparatorImport.Visible = importable || exportable;

                    this.buttonApprove.Visible = sender is Batches ? false : (approvable || unapprovable);
                    this.buttonApprove.Enabled = (approvable || unapprovable) && readonlyMode;
                    this.buttonApprove.Text = approvable ? "Verify" : "Un-verify";
                    this.buttonApprove.Image = approvable ? Resources.Check_Saki_Ok : Resources.Cross_UnVerify;

                    this.toolStripSeparatorApprove.Visible = sender is Batches ? false : (approvable || unapprovable);

                    this.buttonVoid.Visible = sender is Batches ? false : (voidable || unvoidable);
                    this.buttonVoid.Enabled = (voidable || unvoidable) && readonlyMode;
                    this.buttonVoid.Text = voidable ? "Void" : "Un-void";
                    this.buttonVoid.Image = voidable ? Resources.Void_24 : Resources.Cross_UnVerify;

                    this.toolStripSeparatorVoid.Visible = sender is Batches ? false : (voidable || unvoidable);

                    this.buttonPrint.Enabled = printable;
                    this.buttonPrint.Visible = printVisible;
                    this.buttonPrintPreview.Enabled = printable;
                    this.buttonPrintPreview.Visible = printVisible;
                    this.toolStripSeparatorPrint.Visible = printVisible;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Form Events


        #region <Call Tool Strip>
        private void buttonEscape_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Escape();
                if (this.MdiChildren.Length <= 0) this.naviBarModuleMaster.Collapsed = false;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonLoading_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Loading();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void comboFilterTexts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null)
                {
                    if (sender.Equals(this.comboFilterTexts))
                        toolstripChild.ApplyFilter(this.comboFilterTexts.Text);
                    if (sender.Equals(this.comboDetailFilterTexts))
                        toolstripChild.ApplyDetailFilter(this.comboDetailFilterTexts.Text);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonClearFilters_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.buttonClearFilters))
                this.comboFilterTexts.Text = "";
            if (sender.Equals(this.buttonClearDetailFilters))
                this.comboDetailFilterTexts.Text = "";
        }


        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Import();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.New();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Edit();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Save();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Delete();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Import();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Export();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonApprove_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Approve();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonVoid_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Void();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Print(GlobalEnums.PrintDestination.Print);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                IToolstripChild toolstripChild = ActiveMdiChild as IToolstripChild;
                if (toolstripChild != null) toolstripChild.Print(GlobalEnums.PrintDestination.PrintPreview);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion <Call Tool Strip>

        private void toolUserReferences_Click(object sender, EventArgs e)
        {
            this.AddEventLogs("Open user controls");

            if (sender.Equals(this.toolUserReferences))
            {
                UserReferences userReferences = new UserReferences();
                DialogResult dialogResult = userReferences.ShowDialog();

                userReferences.Dispose();
            }

            if (sender.Equals(this.toolUserGroupControls))
            {
                UserControls userControls = new UserControls();
                DialogResult dialogResult = userControls.ShowDialog();

                userControls.Dispose();
            }

            this.AddEventLogs("Close user controls");
        }

        private void buttonLockedDate_Click(object sender, EventArgs e)
        {
            this.AddEventLogs("Open month-end closing");

            LockedDates lockedDates = new LockedDates();
            DialogResult dialogResult = lockedDates.ShowDialog();

            lockedDates.Dispose();

            this.AddEventLogs("Close month-end closing");
        }

        #region Search barcode
        private void comboSearchBarcode_Enter(object sender, EventArgs e)
        {
            if (this.comboSearchBarcode.Text == this.searchPlaceHolder)
            {
                this.comboSearchBarcode.Text = "";
                this.comboSearchBarcode.ForeColor = SystemColors.ControlText;
            }
        }

        private void comboSearchBarcode_Leave(object sender, EventArgs e)
        {
            if (this.comboSearchBarcode.Text.Trim() == "")
            {
                this.comboSearchBarcode.Text = this.searchPlaceHolder;
                this.comboSearchBarcode.ForeColor = SystemColors.ControlDark;
            }
        }

        private void comboSearchBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) this.buttonSearchBarcode_Click(this.buttonSearchBarcode, new EventArgs());
        }

        private void buttonSearchBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                this.comboSearchBarcode.Text = this.comboSearchBarcode.Text.Trim();
                if (this.comboSearchBarcode.Text.Length > 0 && (this.comboSearchBarcode.Text != this.searchPlaceHolder))
                {
                    if (this.comboSearchBarcode.Items.IndexOf(this.comboSearchBarcode.Text) == -1)
                        this.comboSearchBarcode.Items.Add(this.comboSearchBarcode.Text);

                    SearchBarcode quickView = new SearchBarcode(this.comboSearchBarcode.Text);
                    quickView.ShowDialog(); quickView.Dispose();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
        #endregion Search barcode


        private void buttonLicenseTerms_Click(object sender, EventArgs e)
        {
            if (!GlobalEnums.CBPP)
            {
                LicenseTerms licenseTerms = new LicenseTerms();
                licenseTerms.ShowDialog(); licenseTerms.Dispose();
            }
        }

        private void buttonUserManuals_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Path.GetDirectoryName(Application.ExecutablePath) + "\\User Manuals.pdf");
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }




        #region Smart Logs
        public void AddEventLogs(string actionType)
        {
            try
            {
                IBaseRepository baseRepository = CommonNinject.Kernel.Get<IBaseRepository>();
                if (baseRepository.GetOnEventLogs()) baseRepository.AddEventLogs("Application", actionType, null, null);
            }
            catch (Exception ex) { }
        }
        #endregion Smart Logs

    }
}
