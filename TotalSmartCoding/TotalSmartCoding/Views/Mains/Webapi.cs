using System;
using System.Windows.Forms;

using Ninject;

using TotalCore.Extensions;
using TotalCore.Repositories;
using TotalModel.Helpers;
using TotalSmartCoding.Libraries;

namespace TotalSmartCoding.Views.Mains
{
    public partial class Webapi : Form
    {
        public Webapi()
        {
            InitializeComponent();
        }

        private void Webapi_Load(object sender, EventArgs e)
        {
            this.textexBaseUri.Text = Webapis.BaseUri;
            this.textexConsumerKey.Text = Webapis.ConsumerKey;
            this.textexConsumerSecret.Text = Webapis.ConsumerSecret;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.buttonUpdate))
            {
                if (this.textexBaseUri.Text.Trim() != "" && this.textexConsumerKey.Text.Trim() != "" && this.textexConsumerSecret.Text.Trim() != "")
                {
                    IBaseRepository baseRepository = CommonNinject.Kernel.Get<IBaseRepository>();
                    if (baseRepository.UpdateWebapi(SecurePassword.Encrypt(this.textexBaseUri.Text.Trim()), SecurePassword.Encrypt(this.textexConsumerKey.Text.Trim()), SecurePassword.Encrypt(this.textexConsumerSecret.Text.Trim())) == 1)
                        this.DialogResult = DialogResult.OK;
                    else
                        throw new Exception("Fail to update application role.");
                }
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void textexWebapi_TextChanged(object sender, EventArgs e)
        {
            this.buttonUpdate.Enabled = this.textexBaseUri.Text.Trim() != "" && this.textexConsumerKey.Text.Trim() != "" && this.textexConsumerSecret.Text.Trim() != "";
        }
    }
}
