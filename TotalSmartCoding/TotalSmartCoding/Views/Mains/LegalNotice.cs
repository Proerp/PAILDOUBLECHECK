using System;
using System.Windows.Forms;

using Ninject;

using TotalCore.Repositories;
using TotalSmartCoding.Libraries;

namespace TotalSmartCoding.Views.Mains
{
    public partial class LegalNotice : Form
    {
        public LegalNotice()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void LegalNotice_Load(object sender, EventArgs e)
        {
            IBaseRepository baseRepository = CommonNinject.Kernel.Get<IBaseRepository>();
            this.labelLegalNotice.Text = baseRepository.GetLegalNotice();
        }
    }
}
