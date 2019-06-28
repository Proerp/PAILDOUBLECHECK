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
    public partial class UserRegister : Form
    {

        private UserAPIs userAPIs { get; set; }
        private OrganizationalUnitAPIs organizationalUnitAPIs { get; set; }

        public string UserName { get; set; }
        public int? OrganizationalUnitID { get; set; }

        public GlobalEnums.AccessLevel SameOUAccessLevel { get; set; }
        public GlobalEnums.AccessLevel SameLocationAccessLevel { get; set; }
        public GlobalEnums.AccessLevel OtherOUAccessLevel { get; set; }

        private Binding bindingUserName;
        private Binding bindingOrganizationalUnitID;

        public UserRegister(UserAPIs userAPIs, OrganizationalUnitAPIs organizationalUnitAPIs)
        {
            InitializeComponent();

            try
            {
                List<DomainUser> allUsers = new List<DomainUser>();

                //userAPIs.UpdateUserName("S-1-5-21-3775195119-1044016383-3360809325-1001", "NMVN\vendor");

                if (true)
                {
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "chevronvn.com"); //, "OU=SomeOU,dc=YourCompany,dc=com"// create your domain context and define the OU container to search in
                    UserPrincipal qbeUser = new UserPrincipal(ctx);// define a "query-by-example" principal - here, we search for a UserPrincipal (user)
                    PrincipalSearcher srch = new PrincipalSearcher(qbeUser); // create your principal searcher passing in the QBE principal    

                    foreach (var found in srch.FindAll())// find all matches
                    {// do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          

                        if (found.Sid.Value != null && found.Sid.Value != "" && found.SamAccountName != null && found.SamAccountName != "") userAPIs.UpdateUserName(found.Sid.Value, found.SamAccountName);

                        allUsers.Add(new DomainUser() { FirstName = "", LastName = "", UserName = found.SamAccountName, SecurityIdentifier = found.Sid.Value }); //found.UserPrincipalName: the same as SamAccountName, but with @chevron.com
                    }
                }
                else
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        allUsers.Add(new DomainUser() { FirstName = "FIST NAME" + i.ToString(), LastName = "FIST NAME" + i.ToString(), UserName = "CHEVRONVN\\Thi Thanh Giang Le", SecurityIdentifier = "S-1-5-21-2907738014-1953812902-1740135539-2131" });
                    }
                }

                this.combexUserID.DataSource = allUsers;
                this.combexUserID.DisplayMember = CommonExpressions.PropertyName<DomainUser>(p => p.UserName);
                this.combexUserID.ValueMember = CommonExpressions.PropertyName<DomainUser>(p => p.UserName);
                this.bindingUserName = this.combexUserID.DataBindings.Add("SelectedValue", this, CommonExpressions.PropertyName<DomainUser>(p => p.UserName), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingUserName.BindingComplete += binding_BindingComplete;

                this.userAPIs = userAPIs;
                this.organizationalUnitAPIs = organizationalUnitAPIs;

                this.combexOrganizationalUnitID.DataSource = this.organizationalUnitAPIs.GetOrganizationalUnitIndexes();
                this.combexOrganizationalUnitID.DisplayMember = CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.LocationOrganizationalUnitName);
                this.combexOrganizationalUnitID.ValueMember = CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.OrganizationalUnitID);
                this.bindingOrganizationalUnitID = this.combexOrganizationalUnitID.DataBindings.Add("SelectedValue", this, CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.OrganizationalUnitID), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingOrganizationalUnitID.BindingComplete += binding_BindingComplete;

                this.SameOUAccessLevel = GlobalEnums.AccessLevel.NoAccess;
                this.combexSameOUAccessLevels.DataSource = new List<ACL>() { new ACL() { AccessLevelID = GlobalEnums.AccessLevel.NoAccess }, new ACL() { AccessLevelID = GlobalEnums.AccessLevel.Readable }, new ACL() { AccessLevelID = GlobalEnums.AccessLevel.Editable } };
                this.combexSameOUAccessLevels.DisplayMember = CommonExpressions.PropertyName<ACL>(p => p.AccessLevelName);
                this.combexSameOUAccessLevels.ValueMember = CommonExpressions.PropertyName<ACL>(p => p.AccessLevelID);
                this.combexSameOUAccessLevels.DataBindings.Add("SelectedValue", this, "SameOUAccessLevel", true, DataSourceUpdateMode.OnPropertyChanged);

                this.SameLocationAccessLevel = GlobalEnums.AccessLevel.NoAccess;
                this.combexSameLocationAccessLevels.DataSource = new List<ACL>() { new ACL() { AccessLevelID = GlobalEnums.AccessLevel.NoAccess }, new ACL() { AccessLevelID = GlobalEnums.AccessLevel.Readable }, new ACL() { AccessLevelID = GlobalEnums.AccessLevel.Editable } };
                this.combexSameLocationAccessLevels.DisplayMember = CommonExpressions.PropertyName<ACL>(p => p.AccessLevelName);
                this.combexSameLocationAccessLevels.ValueMember = CommonExpressions.PropertyName<ACL>(p => p.AccessLevelID);
                this.combexSameLocationAccessLevels.DataBindings.Add("SelectedValue", this, "SameLocationAccessLevel", true, DataSourceUpdateMode.OnPropertyChanged);

                this.OtherOUAccessLevel = GlobalEnums.AccessLevel.NoAccess;
                this.combexOtherOUAccessLevels.DataSource = new List<ACL>() { new ACL() { AccessLevelID = GlobalEnums.AccessLevel.NoAccess }, new ACL() { AccessLevelID = GlobalEnums.AccessLevel.Readable } };
                this.combexOtherOUAccessLevels.DisplayMember = CommonExpressions.PropertyName<ACL>(p => p.AccessLevelName);
                this.combexOtherOUAccessLevels.ValueMember = CommonExpressions.PropertyName<ACL>(p => p.AccessLevelID);
                this.combexOtherOUAccessLevels.DataBindings.Add("SelectedValue", this, "OtherOUAccessLevel", true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
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
            this.buttonOK.Enabled = this.UserName != null && this.OrganizationalUnitID != null;

            if (this.UserName != null) this.labelAccessControl.Text = "Initialize the access controls for " + this.UserName + ":";
            if (this.OrganizationalUnitID != null)
            {
                OrganizationalUnitIndex organizationalUnitIndex = this.combexOrganizationalUnitID.SelectedItem as OrganizationalUnitIndex;
                if (organizationalUnitIndex != null && organizationalUnitIndex.OrganizationalUnitID == this.OrganizationalUnitID)
                {
                    this.labelSameOU.Text = "Access right applies to " + organizationalUnitIndex.LocationOrganizationalUnitName;
                    this.labelSameLocation.Text = "Access right applies to " + organizationalUnitIndex.LocationName;
                }
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.combexUserID.SelectedIndex >= 0 && this.UserName != null && this.OrganizationalUnitID != null)
                    {
                        DomainUser domainUser = this.combexUserID.SelectedItem as DomainUser;
                        if (domainUser != null)
                        {
                            OrganizationalUnitIndex organizationalUnitIndex = this.combexOrganizationalUnitID.SelectedItem as OrganizationalUnitIndex;
                            if (organizationalUnitIndex != null && organizationalUnitIndex.OrganizationalUnitID == this.OrganizationalUnitID)
                            {
                                this.userAPIs.UserRegister(organizationalUnitIndex.LocationID, organizationalUnitIndex.OrganizationalUnitID, domainUser.FirstName, domainUser.LastName, domainUser.UserName, domainUser.SecurityIdentifier, (int)this.SameOUAccessLevel, (int)this.SameLocationAccessLevel, (int)this.OtherOUAccessLevel);
                                this.DialogResult = DialogResult.OK;
                            }
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

    public class ACL
    {
        public GlobalEnums.AccessLevel AccessLevelID { get; set; }
        public string AccessLevelName { get { return this.AccessLevelID == GlobalEnums.AccessLevel.NoAccess ? "No Access" : this.AccessLevelID.ToString(); } }
    }
}
