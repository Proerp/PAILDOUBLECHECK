using System;
using System.Windows.Forms;

using BrightIdeasSoftware;

using TotalModel.Models;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Libraries.Helpers;

namespace TotalSmartCoding.Views.Mains
{
    public partial class UserControlAvailableGroups : Form
    {
        private string securityIdentifier;
        private string userName;

        private UserControlAPIs userControlAPIs;
        private UserGroupAPIs userGroupAPIs;


        public UserControlAvailableGroups(UserControlAPIs userControlAPIs, UserGroupAPIs userGroupAPIs, string securityIdentifier, string userName)
        {
            InitializeComponent();

            try
            {
                this.securityIdentifier = securityIdentifier;
                this.userName = userName;

                this.userControlAPIs = userControlAPIs;
                this.userGroupAPIs = userGroupAPIs;

                this.fastUserControlAvailableGroups.ShowGroups = true;
                this.fastUserControlAvailableGroups.AboutToCreateGroups += fastUserControlAvailableGroups_AboutToCreateGroups;

                this.fastUserControlAvailableGroups.SetObjects(this.userControlAPIs.GetUserControlAvailableGroups(this.securityIdentifier));
                this.fastUserControlAvailableGroups.Sort(this.olvUserGroup, SortOrder.Ascending);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void fastUserControlAvailableGroups_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Assembly-32";
                    olvGroup.Subtitle = "Available " + olvGroup.Contents.Count.ToString() + " Group" + (olvGroup.Contents.Count > 1 ? "s" : "");
                }
            }
        }

        private void fastUserControlAvailableGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.fastUserControlAvailableGroups.SelectedObject != null;
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.fastUserControlAvailableGroups.SelectedObject != null)
                    {
                        UserControlAvailableGroup userControlAvailableGroup = (UserControlAvailableGroup)this.fastUserControlAvailableGroups.SelectedObject;
                        if (userControlAvailableGroup != null)
                        {
                            this.userGroupAPIs.UserGroupAddMember(userControlAvailableGroup.UserGroupID, userControlAvailableGroup.GroupCode, this.securityIdentifier, this.userName);
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
