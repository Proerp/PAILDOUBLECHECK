using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Libraries;


namespace TotalSmartCoding.Views.Mains
{
    public partial class UserControlRegister : Form
    {

        private UserControlAPIs userControlAPIs { get; set; }

        public string UserName { get; set; }

        private Binding bindingUserName;

        public UserControlRegister(UserControlAPIs userControlAPIs)
        {
            InitializeComponent();

            try
            {
                this.userControlAPIs = userControlAPIs;

                List<UserControlIndex> userControlIndexes = this.userControlAPIs.GetUserControlIndexes(GlobalEnums.ActiveOption.Both);

                List<DomainUser> allUsers = new List<DomainUser>();

                //userAPIs.UpdateUserName("S-1-5-21-3775195119-1044016383-3360809325-1001", "NMVN\vendor");

                if (true)
                {
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "chevronvn.com"); //, "OU=SomeOU,dc=YourCompany,dc=com"// create your domain context and define the OU container to search in
                    UserPrincipal qbeUser = new UserPrincipal(ctx);// define a "query-by-example" principal - here, we search for a UserPrincipal (user)
                    PrincipalSearcher srch = new PrincipalSearcher(qbeUser); // create your principal searcher passing in the QBE principal    

                    foreach (var found in srch.FindAll())// find all matches
                    {// do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          

                        if (found.Sid.Value != null && found.Sid.Value != "" && found.SamAccountName != null && found.SamAccountName != "")
                        {
                            userControlAPIs.UpdateUserName(found.Sid.Value, found.SamAccountName);

                            if (!this.UserExisted(userControlIndexes, found.Sid.Value))
                                allUsers.Add(new DomainUser() { FirstName = "", LastName = "", DistinguishedName = this.GetWindowsIdentityName(found.DistinguishedName), UserName = found.SamAccountName, SecurityIdentifier = found.Sid.Value }); //found.UserPrincipalName: the same as SamAccountName, but with @chevron.com
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (!this.UserExisted(userControlIndexes, "S-1-5-21-290773801108-TEST-" + i.ToString()))
                            allUsers.Add(new DomainUser() { FirstName = "FIST NAME" + i.ToString(), LastName = "LAST NAME " + i.ToString(), DistinguishedName = "Distinguished Name " + i.ToString(), UserName = "CHEVRONVN\\Vendor " + i.ToString(), SecurityIdentifier = "S-1-5-21-290773801108-TEST-" + i.ToString() });
                    }
                }

                this.combexUserID.DataSource = allUsers;
                this.combexUserID.DisplayMember = CommonExpressions.PropertyName<DomainUser>(p => p.UserName);
                this.combexUserID.ValueMember = CommonExpressions.PropertyName<DomainUser>(p => p.UserName);
                this.bindingUserName = this.combexUserID.DataBindings.Add("SelectedValue", this, CommonExpressions.PropertyName<DomainUser>(p => p.UserName), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingUserName.BindingComplete += binding_BindingComplete;

            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private bool UserExisted(List<UserControlIndex> userControlIndexes, string securityIdentifier)
        {
            return userControlIndexes.Find(w => w.SecurityIdentifier == securityIdentifier) != null;
        }

        private string GetWindowsIdentityName(string distinguishedName)
        {
            string windowsIdentityName = "";

            string[] arrayName = distinguishedName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string stringName in arrayName)
            {
                if (stringName.IndexOf("CN=") >= 0)
                    windowsIdentityName = windowsIdentityName + "\\" + stringName.Substring(stringName.IndexOf("CN=") + "CN=".Length);
                if (stringName.IndexOf("DC=") >= 0 && stringName.IndexOf("DC=com") < 0)
                    windowsIdentityName = (stringName.Substring(stringName.IndexOf("DC=") + "DC=".Length)).ToUpper() + windowsIdentityName;
            }

            return windowsIdentityName;
        }

        private void binding_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            this.buttonOK.Enabled = this.UserName != null;
            if (this.combexUserID.SelectedIndex >= 0 && this.UserName != null)
            {
                DomainUser domainUser = this.combexUserID.SelectedItem as DomainUser;
                if (domainUser != null) this.textexDistinguishedName.Text = domainUser.DistinguishedName;
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.combexUserID.SelectedIndex >= 0 && this.UserName != null)
                    {
                        DomainUser domainUser = this.combexUserID.SelectedItem as DomainUser;
                        if (domainUser != null)
                        {
                            this.userControlAPIs.UserControlRegister(domainUser.FirstName, domainUser.LastName, domainUser.UserName, domainUser.SecurityIdentifier);
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

    public class DomainUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string DistinguishedName { get; set; }
        public string SecurityIdentifier { get; set; }
    }
}
