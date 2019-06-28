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

using TotalSmartCoding.Controllers.Sales;
using TotalCore.Repositories.Sales;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalCore.Services.Sales;
using TotalSmartCoding.ViewModels.Sales;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using TotalBase;
using TotalModel.Models;
using TotalDTO.Sales;
using BrightIdeasSoftware;
using TotalSmartCoding.Libraries.StackedHeaders;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalCore.Repositories.Generals;
using TotalModel.Helpers;
using TotalSmartCoding.ViewModels.Commons;
using TotalSmartCoding.Controllers.Commons;


namespace TotalSmartCoding.Views.Sales.Forecasts
{
    public partial class Forecasts : BaseView
    {
        private CustomTabControl customTabLeft;
        private CustomTabControl customTabCenter;

        private ForecastAPIs forecastAPIs;
        private ForecastViewModel forecastViewModel { get; set; }

        private CommodityAPIs commodityAPIs;
        private WarehouseAPIs _warehouseAPIs;
        private WarehouseAPIs warehouseAPIs
        {
            get
            {
                if (_warehouseAPIs == null) _warehouseAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());
                return _warehouseAPIs;
            }
        }

        public Forecasts()
            : base()
        {
            InitializeComponent();


            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastForecastIndex;

            this.forecastAPIs = new ForecastAPIs(CommonNinject.Kernel.Get<IForecastAPIRepository>());

            this.forecastViewModel = CommonNinject.Kernel.Get<ForecastViewModel>();
            this.forecastViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.forecastViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                #region TabLeft
                this.customTabLeft = new CustomTabControl();
                this.customTabLeft.DisplayStyle = TabStyle.VisualStudio;

                this.customTabLeft.TabPages.Add("tabLeftAA", "Onward Forecast  ");
                this.customTabLeft.TabPages[0].BackColor = this.panelLeft.BackColor;
                this.customTabLeft.TabPages[0].Padding = new Padding(15, 0, 0, 0);
                this.customTabLeft.TabPages[0].Controls.Add(this.layoutLeft);

                this.customTabLeft.Dock = DockStyle.Fill;
                this.panelLeft.Controls.Add(this.customTabLeft);

                this.layoutLeft.ColumnStyles[this.layoutLeft.ColumnCount - 1].SizeType = SizeType.Absolute; this.layoutLeft.ColumnStyles[this.layoutLeft.ColumnCount - 1].Width = 15;
                #endregion TabLeft

                #region TabCenter
                this.customTabCenter = new CustomTabControl();
                this.customTabCenter.DisplayStyle = TabStyle.VisualStudio;

                this.customTabCenter.TabPages.Add("tabCenterAA", "Forecast Details            ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Description            ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Remarks                    ");

                this.customTabCenter.TabPages[0].Controls.Add(this.gridexViewDetails);
                this.customTabCenter.TabPages[1].Controls.Add(this.textexDescription);
                this.customTabCenter.TabPages[2].Controls.Add(this.textexRemarks);
                this.customTabCenter.TabPages[1].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[2].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.gridexViewDetails.Dock = DockStyle.Fill;
                this.textexDescription.Dock = DockStyle.Fill;
                this.textexRemarks.Dock = DockStyle.Fill;

                this.customTabCenter.Dock = DockStyle.Fill;
                this.panelCenter.Controls.Add(this.customTabCenter);
                #endregion TabCenter

                this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].SizeType = SizeType.Absolute; this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].Width = 15;

                this.buttonExpandTop.Visible = this.naviGroupTop.Tag.ToString() == "Expandable";
                this.buttonExpandTop_Click(this.buttonExpandTop, new EventArgs());
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingEntryDate;
        Binding bindingReference;
        Binding bindingVoucherCode;
        Binding bindingDescription;
        Binding bindingRemarks;
        Binding bindingCaption;

        Binding bindingForecastLocationID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.forecastViewModel, CommonExpressions.PropertyName<ForecastDTO>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingReference = this.textexReference.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastDTO>(p => p.Reference), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVoucherCode = this.textexVoucherCode.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastDTO>(p => p.VoucherCode), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastDTO>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCaption = this.labelCaption.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastDTO>(p => p.Caption));

            LocationAPIs locationAPIs = new LocationAPIs(CommonNinject.Kernel.Get<ILocationAPIRepository>());
            this.combexForecastLocationID.DataSource = locationAPIs.GetLocationBases();
            this.combexForecastLocationID.DisplayMember = CommonExpressions.PropertyName<LocationBase>(p => p.Name);
            this.combexForecastLocationID.ValueMember = CommonExpressions.PropertyName<LocationBase>(p => p.LocationID);
            this.bindingForecastLocationID = this.combexForecastLocationID.DataBindings.Add("SelectedValue", this.forecastViewModel, CommonExpressions.PropertyName<ForecastViewModel>(p => p.ForecastLocationID), true, DataSourceUpdateMode.OnPropertyChanged);


            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingReference.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVoucherCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCaption.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingForecastLocationID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastForecastIndex.AboutToCreateGroups += fastForecastIndex_AboutToCreateGroups;

            this.fastForecastIndex.ShowGroups = true;
            this.naviGroupDetails.ExpandedHeight = this.naviGroupDetails.Size.Height;
        }

        private Nullable<DateTime> forecastDate;
        protected override void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            base.CommonControl_BindingComplete(sender, e);
            if (this.EditableMode)
            {
                if (sender.Equals(this.bindingForecastLocationID) && this.combexForecastLocationID.SelectedItem != null)
                {
                    LocationBase locationBase = (LocationBase)this.combexForecastLocationID.SelectedItem;
                    this.forecastViewModel.ForecastLocationName = locationBase.Name;
                }
            }
            if (sender.Equals(this.bindingEntryDate))
            {
                if (this.forecastDate != this.forecastViewModel.EntryDate)
                {
                    this.forecastDate = this.forecastViewModel.EntryDate;

                    this.Quantity.HeaderText = "Forecast." + ((DateTime)this.forecastViewModel.EntryDate).ToString("MMM-yy");
                    this.QuantityM1.HeaderText = "Forecast." + ((DateTime)this.forecastViewModel.EntryDate).AddMonths(1).ToString("MMM-yy");
                    this.QuantityM2.HeaderText = "Forecast." + ((DateTime)this.forecastViewModel.EntryDate).AddMonths(2).ToString("MMM-yy");
                    this.QuantityM3.HeaderText = "Forecast." + ((DateTime)this.forecastViewModel.EntryDate).AddMonths(3).ToString("MMM-yy");

                    this.LineVolume.HeaderText = this.Quantity.HeaderText;
                    this.LineVolumeM1.HeaderText = this.QuantityM1.HeaderText;
                    this.LineVolumeM2.HeaderText = this.QuantityM2.HeaderText;
                    this.LineVolumeM3.HeaderText = this.QuantityM3.HeaderText;

                    StackedHeaderDecorator stackedHeaderDecorator = new StackedHeaderDecorator(this.gridexViewDetails);
                }
            }
        }

        private void fastForecastIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "pay-per-click";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Forecast(s)";
                }
            }
        }

        private BindingSource bindingSourceViewDetails = new BindingSource();

        protected override void InitializeDataGridBinding()
        {
            base.InitializeDataGridBinding();
            this.InitializeDataGridReadonlyColumns(this.gridexViewDetails);

            this.gridexViewDetails.AutoGenerateColumns = false;
            this.gridexViewDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.bindingSourceViewDetails.DataSource = this.forecastViewModel.ViewDetails;
            this.gridexViewDetails.DataSource = this.bindingSourceViewDetails;

            this.bindingSourceViewDetails.AddingNew += bindingSourceViewDetails_AddingNew;
            this.forecastViewModel.ViewDetails.ListChanged += ViewDetails_ListChanged;
            this.gridexViewDetails.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dataGridViewDetails_EditingControlShowing);
            this.gridexViewDetails.ReadOnlyChanged += new System.EventHandler(this.dataGrid_ReadOnlyChanged);

            DataGridViewComboBoxColumn comboBoxColumn;
            this.commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>());

            comboBoxColumn = (DataGridViewComboBoxColumn)this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.CommodityID)];
            comboBoxColumn.DataSource = this.commodityAPIs.GetCommodityBases(true);
            comboBoxColumn.DisplayMember = CommonExpressions.PropertyName<CommodityBase>(p => p.Code);
            comboBoxColumn.ValueMember = CommonExpressions.PropertyName<CommodityBase>(p => p.CommodityID);

            StackedHeaderDecorator stackedHeaderDecorator = new StackedHeaderDecorator(this.gridexViewDetails);
        }

        private void bindingSourceViewDetails_AddingNew(object sender, AddingNewEventArgs e)
        {   //ONLY WHEN USING COMBOBOX TO ADD NEW ROW TO datagridview => WE SHOULD USE BindingSource => THEN WE HANDLE AN EVENT HANDLER FOR AddingNew EVENT
            //In this form, the datagridview using a combobox for add new item => add new row to the datagridview
            //If user cancel the combobox => the datagridview will not cancel new adding row (event no new row added???)
            //This will raise error when user move the cursor to the new row (means: the datagridview will add new row again!!!)
            //I find an workarround to handle this error from this https://stackoverflow.com/questions/2359124/datagridview-throwing-invalidoperationexception-operation-is-not-valid-whe
            //The following code: will remove current pending new row => in order add another new row again
            if (this.gridexViewDetails.Rows.Count == this.bindingSourceViewDetails.Count)
                this.bindingSourceViewDetails.RemoveAt(this.bindingSourceViewDetails.Count - 1);
            //-----------The following is explanation from internet (the link above): The reason it works is because on a DataGridView where AllowUserToAddRows is true, it adds an empty row at the end of its rows which if bound to a list creates a null element at the end of your list. Your code removes that element and then the AddNew in the BindingList will trigger the DataGridView to add it again. 
            //This code bypass the error, BUT NOT SOLVE THE PROBLEM COMPLETELY. SO: WE SHOULD ADVICE USER NOT CANCEL THE COMBOBOX => INSTEAD: CANCEL THE ROW AFTER SELECT THE COMBOBOX
        }

        private void ViewDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
                this.customTabCenter.TabPages[0].Text = "Forecast Details [" + this.forecastViewModel.ViewDetails.Count.ToString("N0") + " Line(s)]             ";

            if (this.EditableMode && e.PropertyDescriptor != null && e.NewIndex >= 0 && e.NewIndex < this.forecastViewModel.ViewDetails.Count)
            {
                ForecastDetailDTO forecastDetailDTO = this.forecastViewModel.ViewDetails[e.NewIndex];
                if (forecastDetailDTO != null)
                    this.CalculateDetailDTO(forecastDetailDTO, e.PropertyDescriptor.Name);
            }
        }


        #region Helper Method
        private void CalculateDetailDTO(ForecastDetailDTO forecastDetailDTO, string propertyName)
        {
            if (propertyName == CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.CommodityID))
            {
                IList<SearchCommodity> searchCommodities = this.commodityAPIs.SearchCommodities(forecastDetailDTO.CommodityID, ContextAttributes.User.LocationID, null, null, null); ////WE MUST USE ContextAttributes.User.LocationID, INSTEAD OF quantityDetailDTO.LocationID, BECAUSE AT FIRST quantityDetailDTO.LocationID = 0. WHEN SAVE: GenericService.PreSaveRoutines WILL UPDATE DTO.LocationID = ContextAttributes.User.LocationID. SEE GenericService.PreSaveRoutines FOR MORE INFORMATION!!!
                if (searchCommodities.Count > 0)
                {
                    forecastDetailDTO.CommodityCode = searchCommodities[0].Code;
                    forecastDetailDTO.CommodityName = searchCommodities[0].Name;
                    forecastDetailDTO.CommodityCategoryName = searchCommodities[0].CommodityCategoryName;
                }
            }
        }
        #endregion Helper Method

        protected override Controllers.BaseController myController
        {
            get { return new ForecastController(CommonNinject.Kernel.Get<IForecastService>(), this.forecastViewModel); }
        }

        public override void Loading()
        {
            this.fastForecastIndex.SetObjects(this.forecastAPIs.GetForecastIndexes());

            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastForecastIndex.Sort(this.olvEntryDate, SortOrder.Descending);
        }

        protected override void invokeEdit(int? id)
        {
            base.invokeEdit(id);

            bool byQuantity = this.forecastViewModel.QuantityVersusVolume == 0;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.TotalQuantity)].Visible = byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.TotalLineVolume)].Visible = !byQuantity;

            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.Quantity)].Visible = byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.LineVolume)].Visible = !byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.QuantityM1)].Visible = byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.LineVolumeM1)].Visible = !byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.QuantityM2)].Visible = byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.LineVolumeM2)].Visible = !byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.QuantityM3)].Visible = byQuantity;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<ForecastDetailDTO>(p => p.LineVolumeM3)].Visible = !byQuantity;
        }

        private Dictionary<string, object> optionDictionary;
        private DataTable excelDataTable;
        public override void Import()
        {
            this.ImportExcel(GlobalEnums.MappingTaskID.Forecast);
        }

        protected override Dictionary<string, object> OptionDictionary()
        {
            this.optionDictionary = base.OptionDictionary();
            return this.optionDictionary;
        }

        protected override void DoImportExcel(string fileName, string sheetName)
        {
            base.DoImportExcel(fileName, sheetName);

            if (!this.Editable) throw new System.ArgumentException("Import", "Permission conflict");

            OleDbAPIs oleDbAPIs = new OleDbAPIs(CommonNinject.Kernel.Get<IOleDbAPIRepository>(), GlobalEnums.MappingTaskID.Forecast);
            this.excelDataTable = oleDbAPIs.OpenExcelSheet(fileName, sheetName);

            this.New();

            this.excelDataTable = null;
        }

        protected override DialogResult wizardMaster()
        {
            if (this.optionDictionary != null)
            { //WHEN IMPORT, this.optionDictionary WILL BE != null
                object objectValue; DateTime entryDate;
                if (this.optionDictionary.TryGetValue("EntryDate", out objectValue) && DateTime.TryParse(objectValue.ToString(), out entryDate)) this.forecastViewModel.EntryDate = entryDate;
                this.optionDictionary = null;

                if (this.excelDataTable != null && this.excelDataTable.Rows.Count > 0 && this.excelDataTable.Rows[0]["WarehouseCode"].ToString().Trim() != "")
                {
                    WarehouseBase warehouseBase = this.warehouseAPIs.GetWarehouseBase(this.excelDataTable.Rows[0]["WarehouseCode"].ToString().Trim());
                    if (warehouseBase != null) this.forecastViewModel.ForecastLocationID = warehouseBase.LocationID;
                }
            }

            WizardMaster wizardMaster = new WizardMaster(this.forecastViewModel);
            DialogResult dialogResult = wizardMaster.ShowDialog();

            wizardMaster.Dispose();
            return dialogResult;
        }

        protected override void wizardDetail()
        {
            try
            {
                ExceptionTable exceptionTable = new ExceptionTable(new string[2, 2] { { "ExceptionCategory", "System.String" }, { "Description", "System.String" } }); decimal decimalValue;

                if (this.excelDataTable != null && this.excelDataTable.Rows.Count > 0)
                {
                    this.forecastViewModel.ViewDetails.RaiseListChangedEvents = false;
                    foreach (DataRow excelDataRow in this.excelDataTable.Rows)
                    {
                        if (excelDataRow["WarehouseCode"] != null && excelDataRow["WarehouseCode"].ToString().Trim() != "")
                        {
                            WarehouseBase warehouseBase = this.warehouseAPIs.GetWarehouseBase(excelDataRow["WarehouseCode"].ToString().Trim());
                            if (warehouseBase != null && warehouseBase.LocationID == this.forecastViewModel.ForecastLocationID)
                            {
                                CommodityBase commodityBase = this.commodityAPIs.GetCommodityBase(excelDataRow["CommodityCode"].ToString());
                                if (commodityBase != null)
                                {
                                    ForecastDetailDTO forecastDetailDTO = new ForecastDetailDTO();

                                    forecastDetailDTO.CommodityID = commodityBase.CommodityID;
                                    forecastDetailDTO.CommodityCode = commodityBase.Code;
                                    forecastDetailDTO.CommodityName = commodityBase.Name;
                                    forecastDetailDTO.CommodityCategoryName = commodityBase.CommodityCategoryName;

                                    if (decimal.TryParse(excelDataRow["CurrentMonth"].ToString(), out decimalValue)) { if (this.forecastViewModel.QuantityVersusVolume == 0) forecastDetailDTO.Quantity = decimalValue; else forecastDetailDTO.LineVolume = decimalValue; } else exceptionTable.AddException(new string[] { "Type mismatch; number value required", "Column current month: " + excelDataRow["CurrentMonth"].ToString() });
                                    if (decimal.TryParse(excelDataRow["NextMonth"].ToString(), out decimalValue)) { if (this.forecastViewModel.QuantityVersusVolume == 0) forecastDetailDTO.QuantityM1 = decimalValue; else forecastDetailDTO.LineVolumeM1 = decimalValue; } else exceptionTable.AddException(new string[] { "Type mismatch; number value required", "Column next month: " + excelDataRow["NextMonth"].ToString() });
                                    if (decimal.TryParse(excelDataRow["NextTwoMonth"].ToString(), out decimalValue)) { if (this.forecastViewModel.QuantityVersusVolume == 0) forecastDetailDTO.QuantityM2 = decimalValue; else forecastDetailDTO.LineVolumeM2 = decimalValue; } else exceptionTable.AddException(new string[] { "Type mismatch; number value required", "Column next two month: " + excelDataRow["NextTwoMonth"].ToString() });
                                    if (decimal.TryParse(excelDataRow["NextThreeMonth"].ToString(), out decimalValue)) { if (this.forecastViewModel.QuantityVersusVolume == 0) forecastDetailDTO.QuantityM3 = decimalValue; else forecastDetailDTO.LineVolumeM3 = decimalValue; } else exceptionTable.AddException(new string[] { "Type mismatch; number value required", "Column next three month: " + excelDataRow["NextThreeMonth"].ToString() });

                                    if (forecastDetailDTO.Quantity != 0 || forecastDetailDTO.QuantityM1 != 0 || forecastDetailDTO.QuantityM2 != 0 || forecastDetailDTO.QuantityM3 != 0 || forecastDetailDTO.LineVolume != 0 || forecastDetailDTO.LineVolumeM1 != 0 || forecastDetailDTO.LineVolumeM2 != 0 || forecastDetailDTO.LineVolumeM3 != 0) this.forecastViewModel.ViewDetails.Add(forecastDetailDTO);
                                }
                                else
                                    exceptionTable.AddException(new string[] { "Item not found", "Item: " + excelDataRow["CommodityCode"].ToString() });
                            }
                            else
                                exceptionTable.AddException(new string[] { "Warehouse does not match", "Warehouse: " + excelDataRow["WarehouseCode"].ToString() });
                        }
                    }
                }

                if (exceptionTable.Table.Rows.Count > 0)
                    throw new CustomException("Lỗi import file excel. Vui lòng xem danh sách đính kèm. Click vào từng nội dung để xem chi tiết.", exceptionTable.Table);
            }
            catch (System.Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
            finally
            {
                this.forecastViewModel.ViewDetails.RaiseListChangedEvents = true;
                this.forecastViewModel.ViewDetails.ResetBindings();
            }
        }

        private void naviGroupDetails_HeaderMouseClick(object sender, MouseEventArgs e)
        {
            this.toolStripNaviGroup.Visible = this.naviGroupDetails.Expanded;
        }

        private void buttonExpandTop_Click(object sender, EventArgs e)
        {
            if (this.naviGroupTop.Tag.ToString() == "Expandable" || this.naviGroupTop.Expanded)
            {
                this.naviGroupTop.Expanded = !this.naviGroupTop.Expanded;
                this.naviGroupTop.Padding = new Padding(0, 0, 0, 0);
                this.buttonExpandTop.Image = this.naviGroupTop.Expanded ? Resources.chevron : Resources.chevron_expand;
            }
        }

    }
}
