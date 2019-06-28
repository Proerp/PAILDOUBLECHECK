using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

using Ninject;
using AutoMapper;

using BrightIdeasSoftware;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalDTO.Generals;

using TotalCore.Repositories.Generals;

using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Libraries.StackedHeaders;


namespace TotalSmartCoding.Views.Mains
{
    public partial class UserControls : Form
    {
        private IUserControlRepository userControlRepository { get; set; }
        private UserControlAPIs userControlAPIs { get; set; }
        private UserGroupAPIs userGroupAPIs { get; set; }

        private Binding bindingOnDataLogs;
        private Binding bindingOnEventLogs;

        #region Contruction
        public UserControls()
        {
            InitializeComponent();
            try
            {
                this.userControlRepository = CommonNinject.Kernel.Get<IUserControlRepository>();
                this.userControlAPIs = new UserControlAPIs(CommonNinject.Kernel.Get<IUserControlAPIRepository>());
                this.userGroupAPIs = new UserGroupAPIs(CommonNinject.Kernel.Get<IUserGroupAPIRepository>());

                this.comboActiveOption.SelectedIndex = 0;

                this.fastUserControlIndexes.ShowGroups = true;
                this.fastUserControlIndexes.AboutToCreateGroups += fastGroups_AboutToCreateGroups;

                this.fastUserGroupDetails.ShowGroups = true;
                this.fastUserGroupDetails.AboutToCreateGroups += fastGroups_AboutToCreateGroups;

                this.fastUserSalespersons.ShowGroups = true;
                this.fastUserSalespersons.AboutToCreateGroups += fastGroups_AboutToCreateGroups;

                this.onDataLogs = this.userControlRepository.GetOnDataLogs();
                this.onEventLogs = this.userControlRepository.GetOnEventLogs();
                this.bindingOnDataLogs = this.checkOnDataLogs.DataBindings.Add("Checked", this, "OnDataLogs", true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingOnEventLogs = this.checkOnEventLogs.DataBindings.Add("Checked", this, "OnEventLogs", true, DataSourceUpdateMode.OnPropertyChanged);

                this.textexLegalNotice.Text = this.userControlRepository.GetLegalNotice();
                this.labelUpdateSuccessfullly.Text = ""; this.separatorLegalNotice.Visible = false;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void UserControls_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitializeTabControl();
                this.LoadUserControls();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected void InitializeTabControl()
        {
            try
            {
                CustomTabControl customTabCenter = new CustomTabControl();
                customTabCenter.DisplayStyle = TabStyle.VisualStudio;
                customTabCenter.Font = this.panelCenter.Font;
                panelCenter.Controls.Add(customTabCenter);
                customTabCenter.Dock = DockStyle.Fill;

                customTabCenter.TabPages.Add("tabCenterAA", "Member of Groups            ");
                customTabCenter.TabPages.Add("tabCenterBB", "Salespersons Filtering          ");
                customTabCenter.TabPages.Add("tabCenterCC", "Legal Notice          ");
                customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                customTabCenter.TabPages[1].BackColor = this.panelCenter.BackColor;
                customTabCenter.TabPages[2].BackColor = this.panelCenter.BackColor;

                customTabCenter.TabPages[0].Controls.Add(this.fastUserGroupDetails);
                customTabCenter.TabPages[0].Controls.Add(this.toolUserGroupDetails);
                customTabCenter.TabPages[1].Controls.Add(this.fastUserSalespersons);
                customTabCenter.TabPages[1].Controls.Add(this.toolUserSalespersons);
                customTabCenter.TabPages[2].Controls.Add(this.textexLegalNotice);
                customTabCenter.TabPages[2].Controls.Add(this.toolLegalNotice);

                this.fastUserGroupDetails.Dock = DockStyle.Fill;
                this.toolUserGroupDetails.Dock = DockStyle.Top;
                this.fastUserSalespersons.Dock = DockStyle.Fill;
                this.toolUserSalespersons.Dock = DockStyle.Top;
                this.textexLegalNotice.Dock = DockStyle.Fill;
                this.toolLegalNotice.Dock = DockStyle.Top;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private bool onDataLogs;
        public bool OnDataLogs
        {
            get { return this.onDataLogs; }
            set
            {
                if (this.onDataLogs != value)
                {
                    this.onDataLogs = value;
                    this.userControlAPIs.UpdateOnDataLogs(this.onDataLogs ? 1 : 0);
                }
            }
        }
        private bool onEventLogs;
        public bool OnEventLogs
        {
            get { return this.onEventLogs; }
            set
            {
                if (this.onEventLogs != value)
                {
                    this.onEventLogs = value;
                    this.userControlAPIs.UpdateOnEventLogs(this.onEventLogs ? 1 : 0);
                }
            }
        }
        #endregion Contruction


        #region Register, Unuegister, ToggleVoid

        private void comboActiveOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.LoadUserControls();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }

        }

        private void LoadUserControls()
        {
            try
            {
                this.fastUserControlIndexes.SetObjects(this.userControlAPIs.GetUserControlIndexes(this.comboActiveOption.SelectedIndex == 0 ? GlobalEnums.ActiveOption.Active : GlobalEnums.ActiveOption.Both));
                this.fastUserControlIndexes.Sort(this.olvUserControlType, SortOrder.Ascending);

                fastControlGroups_SelectedIndexChanged(this.fastUserControlIndexes, new EventArgs());
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonUserRegister_Click(object sender, EventArgs e)
        {
            UserControlRegister wizardUserControlRegister = new UserControlRegister(this.userControlAPIs);
            DialogResult dialogResult = wizardUserControlRegister.ShowDialog();

            wizardUserControlRegister.Dispose();
            if (dialogResult == DialogResult.OK) this.LoadUserControls();
        }

        private void buttonUserUnregister_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedUserControlIndex != null && this.SelectedUserControlIndex.UserID > 0 && this.SelectedUserControlIndex.SecurityIdentifier != ContextAttributes.User.SecurityIdentifier)
                {
                    if (CustomMsgBox.Show(this, "Are you sure you want to deregister this user?" + "\r\n" + "\r\nUser:  " + this.SelectedUserControlIndex.UserName, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        if (this.userControlRepository.GetEditable((int)this.selectedUserControlIndex.UserID))
                        {
                            this.userControlAPIs.UserControlUnregister(this.SelectedUserControlIndex.UserID, this.SelectedUserControlIndex.UserName, this.SelectedUserControlIndex.SecurityIdentifier);
                            this.LoadUserControls();
                        }
                        else
                            throw new Exception("Can not deregister this user. Please check again!");
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonUserAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedUserControlIndex != null && this.SelectedUserControlIndex.UserID > 0 && this.SelectedUserControlIndex.SecurityIdentifier != ContextAttributes.User.SecurityIdentifier)
                {
                    if (CustomMsgBox.Show(this, "Are you sure you want to " + (this.SelectedUserControlIndex.IsDatabaseAdmin ? "unset" : "set") + " as admin for this user?" + "\r\n" + "\r\nUser:  " + this.SelectedUserControlIndex.UserName, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                    {
                        this.userControlAPIs.UserControlSetAdmin(this.SelectedUserControlIndex.UserID, this.SelectedUserControlIndex.UserName, this.SelectedUserControlIndex.SecurityIdentifier, !this.SelectedUserControlIndex.IsDatabaseAdmin);
                        this.LoadUserControls();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonUserToggleVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedUserControlIndex != null && this.SelectedUserControlIndex.UserID > 0 && this.SelectedUserControlIndex.SecurityIdentifier != ContextAttributes.User.SecurityIdentifier)
                {
                    if (CustomMsgBox.Show(this, "Are you sure you want to " + (this.SelectedUserControlIndex.InActive ? "enable" : "disable") + " this user registration?" + "\r\n" + "\r\nUser:  " + this.SelectedUserControlIndex.UserName, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                    {
                        this.userControlAPIs.UserControlToggleVoid(this.SelectedUserControlIndex.UserID, this.SelectedUserControlIndex.UserName, this.SelectedUserControlIndex.SecurityIdentifier, !this.SelectedUserControlIndex.InActive);
                        this.LoadUserControls();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Register, Unuegister, ToggleVoid

        #region Handle Task
        private void fastGroups_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = sender.Equals(this.fastUserControlIndexes) ? "UserGroupN" : (sender.Equals(this.fastUserGroupDetails) ? "Assembly-32" : "group-of-users-silhouette");
                    olvGroup.Subtitle = olvGroup.Contents.Count.ToString() + (sender.Equals(this.fastUserControlIndexes) ? " User" : (sender.Equals(this.fastUserGroupDetails) ? " Group" : " Salesperson")) + (olvGroup.Contents.Count > 1 ? "s" : "");
                }
            }
        }

        private UserControlIndex selectedUserControlIndex;
        private UserControlIndex SelectedUserControlIndex
        {
            get { return this.selectedUserControlIndex; }
            set
            {
                if (this.selectedUserControlIndex != value)
                {
                    this.selectedUserControlIndex = value;
                    this.GetUserControlSalespersons();
                    this.GetUserControlGroups();

                    this.buttonUserToggleVoid.Enabled = this.selectedUserControlIndex.SecurityIdentifier != ContextAttributes.User.SecurityIdentifier;
                    this.buttonUserUnregister.Enabled = this.selectedUserControlIndex.SecurityIdentifier != ContextAttributes.User.SecurityIdentifier && this.userControlRepository.GetEditable((int)this.selectedUserControlIndex.UserID);

                    this.buttonUserAdmin.Enabled = this.selectedUserControlIndex.SecurityIdentifier != ContextAttributes.User.SecurityIdentifier;
                }
            }
        }

        private void fastControlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.fastUserControlIndexes.SelectedObject != null)
            {
                UserControlIndex userGroupIndex = (UserControlIndex)this.fastUserControlIndexes.SelectedObject;
                if (userGroupIndex != null)
                    this.SelectedUserControlIndex = userGroupIndex;
            }
            else
            {
                this.GetUserControlSalespersons();
                this.GetUserControlGroups();
            }
        }

        private void GetUserControlGroups()
        {
            try
            {
                if (this.userControlAPIs != null)
                {
                    this.fastUserGroupDetails.SetObjects(this.userControlAPIs.GetUserControlGroups(this.SelectedUserControlIndex != null ? this.SelectedUserControlIndex.SecurityIdentifier : null));
                    this.fastUserGroupDetails.Sort(this.olvGroupType, SortOrder.Ascending);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void GetUserControlSalespersons()
        {
            try
            {
                if (this.userControlAPIs != null)
                {
                    this.fastUserSalespersons.SetObjects(this.userControlAPIs.GetUserControlSalespersons(this.SelectedUserControlIndex != null ? this.SelectedUserControlIndex.SecurityIdentifier : null));
                    this.fastUserSalespersons.Sort(this.olvEmployeeType, SortOrder.Ascending);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Handle Task

        #region Add, remove member
        private void buttonJoinLeaveGroup_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Cancel;
            if (sender.Equals(this.buttonJoinGroup) && this.SelectedUserControlIndex != null)
            {
                UserControlAvailableGroups wizardUserControlAvailableGroups = new UserControlAvailableGroups(this.userControlAPIs, this.userGroupAPIs, this.SelectedUserControlIndex.SecurityIdentifier, this.SelectedUserControlIndex.UserName);
                dialogResult = wizardUserControlAvailableGroups.ShowDialog(); wizardUserControlAvailableGroups.Dispose();
            }
            if (sender.Equals(this.buttonLeaveGroup) && this.SelectedUserControlIndex != null && this.fastUserGroupDetails.SelectedObject != null)
            {
                UserControlGroup userControlGroup = (UserControlGroup)this.fastUserGroupDetails.SelectedObject;
                if (userControlGroup != null && CustomMsgBox.Show(this, "Are you sure you want to leave this group: " + "\r\n" + "\r\n" + userControlGroup.UserGroupCode + "\r\n" + userControlGroup.UserGroupName, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    this.userGroupAPIs.UserGroupRemoveMember(userControlGroup.UserGroupDetailID, userControlGroup.UserGroupID, userControlGroup.UserGroupCode, this.SelectedUserControlIndex.SecurityIdentifier, this.SelectedUserControlIndex.UserName);
                    dialogResult = DialogResult.OK;
                }
            }

            if (dialogResult == DialogResult.OK) this.GetUserControlGroups();
        }

        private void buttonManageGroups_Click(object sender, EventArgs e)
        {
            UserGroupControls userGroupControls = new UserGroupControls();
            DialogResult dialogResult = userGroupControls.ShowDialog();

            userGroupControls.Dispose();
            this.GetUserControlGroups();
        }

        #endregion Add, remove member

        #region Add, remove salesperson
        private void buttonAddRemoveSalesperson_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Cancel;
            if (sender.Equals(this.buttonAddSalesperson) && this.SelectedUserControlIndex != null)
            {
                UserControlAvailableSalespersons wizardUserControlAvailableSalespersons = new UserControlAvailableSalespersons(this.userControlAPIs, this.SelectedUserControlIndex.UserName, this.SelectedUserControlIndex.SecurityIdentifier);
                dialogResult = wizardUserControlAvailableSalespersons.ShowDialog(); wizardUserControlAvailableSalespersons.Dispose();
            }
            if (sender.Equals(this.buttonRemoveSalesperson) && this.SelectedUserControlIndex != null && this.fastUserSalespersons.SelectedObject != null)
            {
                UserControlSalesperson userControlSalesperson = (UserControlSalesperson)this.fastUserSalespersons.SelectedObject;
                if (userControlSalesperson != null && CustomMsgBox.Show(this, "Are you sure you want to remove this salesperson: " + "\r\n" + "\r\n" + userControlSalesperson.EmployeeCode + "\r\n" + userControlSalesperson.EmployeeName, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    this.userControlAPIs.UserControlRemoveSalesperson(userControlSalesperson.UserSalespersonID, this.SelectedUserControlIndex.UserName, this.SelectedUserControlIndex.SecurityIdentifier, userControlSalesperson.EmployeeID, userControlSalesperson.EmployeeName);
                    dialogResult = DialogResult.OK;
                }
            }

            if (dialogResult == DialogResult.OK) this.GetUserControlSalespersons();
        }
        #endregion Add, remove salesperson

        private void buttonUpdateLegalNotice_Click(object sender, EventArgs e)
        {
            try
            {
                this.userControlRepository.UpdateLegalNotice(this.textexLegalNotice.Text);
                this.labelUpdateSuccessfullly.Text = "Update Successfully!";
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void textexLegalNotice_TextChanged(object sender, EventArgs e)
        {
            this.labelUpdateSuccessfullly.Text = "Legal notice changed. Click save to update!"; this.separatorLegalNotice.Visible = true;
        }

    }
}
