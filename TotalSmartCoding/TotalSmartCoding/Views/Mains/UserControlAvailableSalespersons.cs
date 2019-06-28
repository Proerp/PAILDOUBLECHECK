using System;
using System.Windows.Forms;

using BrightIdeasSoftware;

using TotalModel.Models;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Libraries.Helpers;

namespace TotalSmartCoding.Views.Mains
{
    public partial class UserControlAvailableSalespersons : Form
    {
        private string userName;
        private string securityIdentifier;
        private UserControlAPIs userControlAPIs;


        public UserControlAvailableSalespersons(UserControlAPIs userControlAPIs, string userName, string securityIdentifier)
        {
            InitializeComponent();

            try
            {
                this.userName = userName;
                this.securityIdentifier = securityIdentifier;
                this.userControlAPIs = userControlAPIs;

                this.fastUserControlAvailableSalespersons.ShowGroups = true;
                this.fastUserControlAvailableSalespersons.AboutToCreateGroups += fastUserControlAvailableSalespersons_AboutToCreateGroups;

                this.fastUserControlAvailableSalespersons.SetObjects(this.userControlAPIs.GetUserControlAvailableSalespersons(this.securityIdentifier));
                this.fastUserControlAvailableSalespersons.Sort(this.olvEmployeeType, SortOrder.Ascending);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void fastUserControlAvailableSalespersons_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "group-of-users";
                    olvGroup.Subtitle = "Available " + olvGroup.Contents.Count.ToString() + " Salesperson" + (olvGroup.Contents.Count > 1 ? "s" : "");
                }
            }
        }

        private void fastUserControlAvailableSalespersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.fastUserControlAvailableSalespersons.SelectedObject != null;
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.fastUserControlAvailableSalespersons.SelectedObject != null)
                    {
                        UserControlAvailableSalesperson userControlAvailableSalesperson = (UserControlAvailableSalesperson)this.fastUserControlAvailableSalespersons.SelectedObject;
                        if (userControlAvailableSalesperson != null)
                        {
                            this.userControlAPIs.UserControlAddSalesperson(this.userName, this.securityIdentifier, userControlAvailableSalesperson.EmployeeID, userControlAvailableSalesperson.EmployeeName);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
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
