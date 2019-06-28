using System;
using System.Windows.Forms;

using Ninject;

using TotalCore.Extensions;
using TotalCore.Repositories;
using TotalModel.Helpers;
using TotalSmartCoding.Libraries;

namespace TotalSmartCoding.Views.Mains
{
    public partial class ConnectServer : Form
    {
        private bool specifyNewRole;

        public ConnectServer(bool specifyNewRole)
        {
            InitializeComponent();

            this.specifyNewRole = specifyNewRole;
        }

        private void ConnectServer_Load(object sender, EventArgs e)
        {
            this.textexApplicationRoleName.Text = ApplicationRoles.Name;
            this.textexApplicationRolePassword.Text = ApplicationRoles.Password;

            this.textexApplicationRoleName.Visible = this.specifyNewRole;
            this.textexApplicationRolePassword.Visible = this.specifyNewRole;
            this.labelApplicationRolePassword.Visible = this.specifyNewRole;
            this.buttonUpdate.Visible = this.specifyNewRole;
            this.buttonApplicationRoleRequired.Visible = !this.specifyNewRole && !ApplicationRoles.Required;
            this.buttonApplicationRoleIgnored.Visible = !this.specifyNewRole && ApplicationRoles.Required;

            if (!this.specifyNewRole)
            {
                this.Text = "Fail to connect to server";
                this.labelApplicationRoleName.Text = "The program on this computer can not connect to server." + "\r\n" + "\r\n" + "Please contact your administrator for more information.";
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.buttonUpdate))
            {
                if (this.textexApplicationRoleName.Text.Trim() != "" && this.textexApplicationRolePassword.Text.Trim() != "")
                {
                    IBaseRepository baseRepository = CommonNinject.Kernel.Get<IBaseRepository>();
                    if (baseRepository.UpdateApplicationRole(SecurePassword.Encrypt(this.textexApplicationRoleName.Text.Trim()), SecurePassword.Encrypt(this.textexApplicationRolePassword.Text.Trim())) == 1)
                        this.DialogResult = DialogResult.OK;
                    else
                        throw new Exception("Fail to update application role.");
                }
            }
            else
                if (sender.Equals(this.buttonApplicationRoleRequired) || sender.Equals(this.buttonApplicationRoleIgnored))
                {
                    CommonConfigs.AddUpdateAppSetting("ApplicationRoleRequired", sender.Equals(this.buttonApplicationRoleRequired) ? "true" : "false");

                    CustomMsgBox.Show(this, "Please open your program again in order to take new effect.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.DialogResult = DialogResult.Cancel;
                }
                else
                    this.DialogResult = DialogResult.Cancel;
        }

        private void textexApplicationRole_TextChanged(object sender, EventArgs e)
        {
            this.buttonUpdate.Enabled = this.textexApplicationRoleName.Text.Trim() != "" && this.textexApplicationRolePassword.Text.Trim() != "";
        }
    }
}
