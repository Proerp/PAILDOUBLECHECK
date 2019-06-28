using System;
using System.Threading;
using System.Net.Http;
using System.Windows.Forms;

using TotalSmartCoding.Libraries;
using TotalSmartCoding.Controllers.Generals;
using TotalSmartCoding.Libraries.Helpers;
using System.ComponentModel;


namespace TotalSmartCoding.Views.Generals
{
    public partial class WebapiGettsa : Form
    {
        private Thread dataServerThread;
        private DataServerController dataServerController;

        delegate void propertyChangedThread(object sender, PropertyChangedEventArgs e);

        public WebapiGettsa(string q_id1)
        {
            InitializeComponent();

            this.dataServerController = new DataServerController(q_id1);
            dataServerController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
        }



        private void WebapiGettsa_Shown(object sender, EventArgs e)
        {
            try
            {
                if (dataServerThread != null && dataServerThread.IsAlive) dataServerThread.Abort();
                dataServerThread = new Thread(new ThreadStart(dataServerController.ThreadGet));

                dataServerThread.Start();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void WebapiGettsa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataServerThread != null && dataServerThread.IsAlive) { e.Cancel = true; return; }
        }

        private void controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                propertyChangedThread propertyChangedDelegate = new propertyChangedThread(propertyChangedHandler);
                this.Invoke(propertyChangedDelegate, new object[] { sender, e });
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void propertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (sender.Equals(this.dataServerController))
                {
                    if (e.PropertyName == "MainStatus")
                    {
                        string mainStatus = this.dataServerController.MainStatus.Trim();
                        if (mainStatus != "" && mainStatus.IndexOf("@") != -1)
                        {
                            string attributeName = mainStatus.Substring(0, mainStatus.IndexOf("@"));
                            string attributeValue = mainStatus.Substring(mainStatus.IndexOf("@") + 1);

                            if (attributeName == "label") this.textexLabel.Text = attributeValue;
                            if (attributeName == "production_date") this.textexProduction_date.Text = attributeValue;
                            if (attributeName == "production_line") this.textexProduction_line.Text = attributeValue;
                            if (attributeName == "product_id") this.textexProduct_id.Text = attributeValue;
                            if (attributeName == "batch_number") this.textexBatch_number.Text = attributeValue;
                            if (attributeName == "batch_serial") this.textexBatch_serial.Text = attributeValue;
                            if (attributeName == "domino_code") this.textexDomino_code.Text = attributeValue;
                            if (attributeName == "valid") this.textexValid.Text = attributeValue;
                        }
                        else
                            this.labelMainStatus.Text = mainStatus;
                    }
                }

            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
