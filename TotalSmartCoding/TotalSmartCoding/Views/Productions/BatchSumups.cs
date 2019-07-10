using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using BrightIdeasSoftware;

using Ninject;
using AutoMapper;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalDTO.Sales;
using TotalDTO.Productions;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Sales;
using TotalSmartCoding.Views.Mains;
using TotalDTO.Helpers.Interfaces;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalCore.Repositories.Productions;


namespace TotalSmartCoding.Views.Productions
{
    public partial class BatchSumups : Form
    {
        private BatchAPIs batchAPIs;

        private Binding beginingDateBinding;
        private Binding endingDateBinding;

        private DateTime lowerFillterDate;
        public DateTime LowerFillterDate
        {
            get { return this.lowerFillterDate; }
            set { if (this.lowerFillterDate != value) { this.lowerFillterDate = value; this.BatchSumups_Load(this, new EventArgs()); } }
        }

        private DateTime upperFillterDate;
        public DateTime UpperFillterDate
        {
            get { return this.upperFillterDate; }
            set { if (this.upperFillterDate != value) { this.upperFillterDate = value; this.BatchSumups_Load(this, new EventArgs()); } }
        }

        public BatchSumups()
        {
            InitializeComponent();

            this.batchAPIs = new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>());


            this.lowerFillterDate = DateTime.Today.AddDays(-7); this.upperFillterDate = DateTime.Today.AddDays(10);
            this.beginingDateBinding = this.dateTimexLowerFillterDate.DataBindings.Add("Value", this, "LowerFillterDate", true, DataSourceUpdateMode.OnPropertyChanged);
            this.endingDateBinding = this.dateTimexUpperFillterDate.DataBindings.Add("Value", this, "UpperFillterDate", true, DataSourceUpdateMode.OnPropertyChanged);

            this.beginingDateBinding.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.endingDateBinding.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
        }


        private void BatchSumups_Load(object sender, EventArgs e)
        {
            try
            {
                List<BatchSumup> batchSumups = null;
                batchSumups = this.batchAPIs.GetBatchSumups(this.LowerFillterDate, this.UpperFillterDate, (int)GlobalVariables.BarcodeStatus.Freshnew + "," + (int)GlobalVariables.BarcodeStatus.Readytoset + "," + (int)GlobalVariables.BarcodeStatus.Wrapped);

                if (batchSumups != null) this.fastBatchSumups.SetObjects(batchSumups);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (sender.Equals(this.beginingDateBinding) || sender.Equals(this.endingDateBinding))
            {
            }
        }

        private void textexFilters_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OLVHelpers.ApplyFilters(this.fastBatchSumups, this.textexFilters.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonClearFilters_Click(object sender, EventArgs e)
        {
            this.textexFilters.Text = "";
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.BatchSumups_Load(this, new EventArgs());
        }

        private void fastBatchSumups_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.fastBatchSumups.SelectedObject != null)
                {
                    BatchSumup batchSumup = this.fastBatchSumups.SelectedObject as BatchSumup;
                    if (batchSumup != null)
                    {
                        IList<BarcodeDTO> barcodeList = new List<BarcodeDTO>();
                        IList<Carton> cartons = this.batchAPIs.GetCartonCheckedOuts(batchSumup.BatchID, false);

                        if (cartons.Count > 0)
                        {
                            Mapper.Map<IList<Carton>, IList<BarcodeDTO>>(cartons, barcodeList);

                            QuickView quickView = new QuickView(barcodeList, barcodeList.Count.ToString("N0") + " carton" + (barcodeList.Count > 1 ? "s" : "") + " NOT checked of batch: " + batchSumup.BatchCode);
                            quickView.ShowDialog(); quickView.Dispose();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
    }
}
