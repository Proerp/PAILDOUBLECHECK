using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Ninject;

using TotalBase;
using TotalDTO.Productions;
using TotalCore.Repositories.Productions;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Views.Generals;

namespace TotalSmartCoding.Views.Productions
{
    public partial class QuickView : Form
    {
        private ScannerAPIs scannerAPIs;

        public QuickView(IList<BarcodeDTO> barcodeList, string caption)
        {
            InitializeComponent();

            this.scannerAPIs = new ScannerAPIs(CommonNinject.Kernel.Get<IPackRepository>(), CommonNinject.Kernel.Get<ICartonRepository>(), CommonNinject.Kernel.Get<IPalletRepository>());

            this.fastBarcodes.SetObjects(barcodeList);

            this.Text = caption;
        }

        private void textFilter_TextChanged(object sender, EventArgs e)
        {
            OLVHelpers.ApplyFilters(this.fastBarcodes, textFilter.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void fastBarcodes_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            e.Item.SubItems[0].Text = (e.RowIndex + 1).ToString();
        }

        private void fastBarcodes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    if (this.fastBarcodes.SelectedObject != null)
                    {
                        CartonDTO cartonDTO = this.fastBarcodes.SelectedObject as CartonDTO;
                        if (cartonDTO != null)
                        {
                            QuickView quickView = new QuickView(this.scannerAPIs.GetBarcodeList((GlobalVariables.FillingLine)cartonDTO.FillingLineID, cartonDTO.CartonID, 0), "Carton: " + cartonDTO.Code);
                            quickView.ShowDialog(); quickView.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        private void pictureTesaLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fastBarcodes.SelectedObject != null)
                {
                    BarcodeDTO barcodeDTO = this.fastBarcodes.SelectedObject as BarcodeDTO;
                    if (barcodeDTO != null)
                    {
                        WebapiGettsa webapiGettsa = new WebapiGettsa(barcodeDTO.Label);
                        webapiGettsa.ShowDialog(); webapiGettsa.Dispose();
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
