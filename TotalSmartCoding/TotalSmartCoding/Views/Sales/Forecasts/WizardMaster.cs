using System;
using System.Windows.Forms;
using System.ComponentModel;

using Ninject;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Commons;

using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Sales;
using TotalSmartCoding.Controllers.APIs.Commons;

namespace TotalSmartCoding.Views.Sales.Forecasts
{
    public partial class WizardMaster : Form
    {
        private ForecastViewModel forecastViewModel;

        Binding bindingForecastLocationID;

        Binding bindingEntryDate;
        Binding bindingVoucherCode;
        Binding bindingDescription;
        Binding bindingRemarks;

        public WizardMaster(ForecastViewModel forecastViewModel)
        {
            InitializeComponent();

            this.forecastViewModel = forecastViewModel;
        }

        private void WizardMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.forecastViewModel.PropertyChanged += forecastDetailDTO_PropertyChanged;

                LocationAPIs locationAPIs = new LocationAPIs(CommonNinject.Kernel.Get<ILocationAPIRepository>());
                this.combexForecastLocationID.DataSource = locationAPIs.GetLocationBases();
                this.combexForecastLocationID.DisplayMember = CommonExpressions.PropertyName<LocationBase>(p => p.Name);
                this.combexForecastLocationID.ValueMember = CommonExpressions.PropertyName<LocationBase>(p => p.LocationID);
                this.bindingForecastLocationID = this.combexForecastLocationID.DataBindings.Add("SelectedValue", this.forecastViewModel, CommonExpressions.PropertyName<ForecastViewModel>(p => p.ForecastLocationID), true, DataSourceUpdateMode.OnPropertyChanged);


                this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.forecastViewModel, CommonExpressions.PropertyName<ForecastViewModel>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingVoucherCode = this.textexVoucherCode.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastViewModel>(p => p.VoucherCode), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastViewModel>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.forecastViewModel, CommonExpressions.PropertyName<ForecastViewModel>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingForecastLocationID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingVoucherCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.buttonOK.Enabled = this.forecastViewModel.IsValid;
                this.errorProviderMaster.DataSource = this.forecastViewModel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void forecastDetailDTO_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.buttonOK.Enabled = this.forecastViewModel.IsValid;
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception) { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
            if (sender.Equals(this.bindingForecastLocationID) && this.combexForecastLocationID.SelectedItem != null)
            {
                LocationBase locationBase = (LocationBase)this.combexForecastLocationID.SelectedItem;
                this.forecastViewModel.ForecastLocationName = locationBase.Name;
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.forecastViewModel.ForecastLocationID != null)
                    {
                        this.forecastViewModel.EntryDate = new DateTime(((DateTime)this.forecastViewModel.EntryDate).Year, ((DateTime)this.forecastViewModel.EntryDate).Month, 1);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                        CustomMsgBox.Show(this, "Vui lòng chọn forecast location.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                }

                if (sender.Equals(this.buttonESC))
                    this.DialogResult = DialogResult.Cancel;

            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
    }
}
