using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TotalBase;

namespace TotalSmartCoding.Views.Mains
{
    public partial class LicenseTerms : Form
    {
        public LicenseTerms()
        {
            InitializeComponent();
        }

        private void LicenseTerms_Load(object sender, EventArgs e)
        {
            DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            this.labelVersion.Text = "Version 1.0." + GlobalVariables.ConfigVersionID(GlobalVariables.ConfigID).ToString() + ", Date: " + buildDate.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
