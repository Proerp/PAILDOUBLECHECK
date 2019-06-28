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
    public partial class UserGroupControls : Form
    {
        private UserGroupAPIs userGroupAPIs { get; set; }

        private BindingList<UserGroupControlDTO> bindingListUserGroupControls;
        private BindingList<UserGroupReportDTO> bindingListUserGroupReports;

        #region Contruction
        public UserGroupControls()
        {
            InitializeComponent();
            try
            {
                this.userGroupAPIs = new UserGroupAPIs(CommonNinject.Kernel.Get<IUserGroupAPIRepository>());

                this.fastUserGroups.ShowGroups = true;
                this.fastUserGroups.AboutToCreateGroups += fastGroups_AboutToCreateGroups;

                this.fastUserGroupDetails.ShowGroups = true;
                this.fastUserGroupDetails.AboutToCreateGroups += fastGroups_AboutToCreateGroups;

                this.gridexUserGroupControls.AutoGenerateColumns = false;
                this.gridexUserGroupControls.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                this.gridexUserGroupReports.AutoGenerateColumns = false;
                this.gridexUserGroupReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                this.bindingListUserGroupControls = new BindingList<UserGroupControlDTO>();
                this.gridexUserGroupControls.DataSource = this.bindingListUserGroupControls;
                this.bindingListUserGroupControls.ListChanged += bindingListUserGroupControls_ListChanged;

                this.bindingListUserGroupReports = new BindingList<UserGroupReportDTO>();
                this.gridexUserGroupReports.DataSource = this.bindingListUserGroupReports;
                this.bindingListUserGroupReports.ListChanged += bindingListUserGroupReports_ListChanged;

                StackedHeaderDecorator stackedHeaderDecoratorControl = new StackedHeaderDecorator(this.gridexUserGroupControls);
                StackedHeaderDecorator stackedHeaderDecoratorReport = new StackedHeaderDecorator(this.gridexUserGroupReports);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void UserGroupControls_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitializeTabControl();
                this.LoadUserGroups();
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

                customTabCenter.TabPages.Add("tabCenterAA", "Access Controls          ");
                customTabCenter.TabPages.Add("tabCenterAA", "Report Controls          ");
                customTabCenter.TabPages.Add("tabCenterAA", "Members                ");
                customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                customTabCenter.TabPages[1].BackColor = this.panelCenter.BackColor;
                customTabCenter.TabPages[2].BackColor = this.panelCenter.BackColor;

                customTabCenter.TabPages[0].Controls.Add(this.gridexUserGroupControls);
                customTabCenter.TabPages[1].Controls.Add(this.gridexUserGroupReports);
                customTabCenter.TabPages[2].Controls.Add(this.fastUserGroupDetails);
                customTabCenter.TabPages[2].Controls.Add(this.toolUserGroupDetails);

                this.gridexUserGroupControls.Dock = DockStyle.Fill;
                this.gridexUserGroupReports.Dock = DockStyle.Fill;
                this.fastUserGroupDetails.Dock = DockStyle.Fill;
                this.toolUserGroupDetails.Dock = DockStyle.Top;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
        #endregion Contruction


        #region Add, Remove UserGroup

        private void LoadUserGroups()
        {
            try
            {
                this.fastUserGroups.SetObjects(this.userGroupAPIs.GetUserGroupIndexes());
                this.fastUserGroups.Sort(this.olvUserGroupType, SortOrder.Ascending);

                fastUserGroups_SelectedIndexChanged(this.fastUserGroups, new EventArgs());
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonAddRemoveRenameUserGroup_Click(object sender, EventArgs e)
        {
            UserGroups wizardUserGroups = new UserGroups(this.userGroupAPIs, (sender.Equals(this.buttonRemoveUserGroup) || sender.Equals(this.buttonRenameUserGroup) ? this.SelectedUserGroupIndex : null), sender.Equals(this.buttonRemoveUserGroup));
            DialogResult dialogResult = wizardUserGroups.ShowDialog();

            wizardUserGroups.Dispose();
            if (dialogResult == DialogResult.OK) this.LoadUserGroups();
        }

        #endregion Add, Remove UserGroup

        #region Handle Task
        private void fastGroups_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = sender.Equals(this.fastUserGroups) ? "Assembly-32" : "UserGroupN";
                    olvGroup.Subtitle = olvGroup.Contents.Count.ToString() + (sender.Equals(this.fastUserGroups) ? " Group" : " User") + (olvGroup.Contents.Count > 1 ? "s" : "");
                }
            }
        }

        private UserGroupIndex selectedUserGroupIndex;
        private UserGroupIndex SelectedUserGroupIndex
        {
            get { return this.selectedUserGroupIndex; }
            set
            {
                if (this.selectedUserGroupIndex != value)
                {
                    this.selectedUserGroupIndex = value;
                    this.GetUserGroupControls();
                    this.GetUserGroupReports();
                    this.GetUserGroupMembers();
                }
            }
        }

        private void fastUserGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.fastUserGroups.SelectedObject != null)
            {
                UserGroupIndex userGroupIndex = (UserGroupIndex)this.fastUserGroups.SelectedObject;
                if (userGroupIndex != null)
                    this.SelectedUserGroupIndex = userGroupIndex;
            }
            else
            {
                this.GetUserGroupControls();
                this.GetUserGroupReports();
                this.GetUserGroupMembers();
            }
        }

        private void GetUserGroupMembers()
        {
            try
            {
                if (this.userGroupAPIs != null)
                {
                    this.fastUserGroupDetails.SetObjects(this.userGroupAPIs.GetUserGroupMembers(this.SelectedUserGroupIndex != null ? this.SelectedUserGroupIndex.UserGroupID : 0));
                    this.fastUserGroupDetails.Sort(this.olvUserType, SortOrder.Ascending);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void GetUserGroupControls()
        {
            try
            {
                if (this.userGroupAPIs != null && this.bindingListUserGroupControls != null)
                {
                    IList<UserGroupControl> userGroupControls = this.userGroupAPIs.GetUserGroupControls(this.SelectedUserGroupIndex != null ? this.SelectedUserGroupIndex.UserGroupID : 0);
                    this.bindingListUserGroupControls.RaiseListChangedEvents = false;
                    Mapper.Map<ICollection<UserGroupControl>, ICollection<UserGroupControlDTO>>(userGroupControls, this.bindingListUserGroupControls);
                    this.bindingListUserGroupControls.RaiseListChangedEvents = true;
                    this.bindingListUserGroupControls.ResetBindings();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void GetUserGroupReports()
        {
            try
            {
                if (this.userGroupAPIs != null && this.bindingListUserGroupReports != null)
                {
                    IList<UserGroupReport> userGroupReports = this.userGroupAPIs.GetUserGroupReports(this.SelectedUserGroupIndex != null ? this.SelectedUserGroupIndex.UserGroupID : 0);
                    this.bindingListUserGroupReports.RaiseListChangedEvents = false;
                    Mapper.Map<ICollection<UserGroupReport>, ICollection<UserGroupReportDTO>>(userGroupReports, this.bindingListUserGroupReports);
                    this.bindingListUserGroupReports.RaiseListChangedEvents = true;
                    this.bindingListUserGroupReports.ResetBindings();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void gridexUserGroupControls_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.gridexUserGroupControls.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridexUserGroupReports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.gridexUserGroupReports.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void bindingListUserGroupControls_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (this.SelectedUserGroupIndex != null && e.PropertyDescriptor != null && e.NewIndex >= 0 && e.NewIndex < this.bindingListUserGroupControls.Count)
                {
                    UserGroupControlDTO userGroupControlDTO = this.bindingListUserGroupControls[e.NewIndex];
                    if (userGroupControlDTO != null)
                    {
                        this.userGroupAPIs.SaveUserGroupControls(this.SelectedUserGroupIndex, userGroupControlDTO, e.PropertyDescriptor.Name);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void bindingListUserGroupReports_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (this.SelectedUserGroupIndex != null && e.PropertyDescriptor != null && e.NewIndex >= 0 && e.NewIndex < this.bindingListUserGroupReports.Count)
                {
                    UserGroupReportDTO userGroupReportDTO = this.bindingListUserGroupReports[e.NewIndex];
                    if (userGroupReportDTO != null)
                    {
                        this.userGroupAPIs.SaveUserGroupReports(userGroupReportDTO.UserGroupReportID, this.SelectedUserGroupIndex.UserGroupID, this.SelectedUserGroupIndex.Name, userGroupReportDTO.ReportID, userGroupReportDTO.ReportName, userGroupReportDTO.Enabled);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Handle Task

        #region Add, remove member
        private void buttonAddRemoveMember_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Cancel;
            if (sender.Equals(this.buttonAddMember) && this.SelectedUserGroupIndex != null)
            {
                UserGroupAvailableMembers wizardUserRegister = new UserGroupAvailableMembers(this.userGroupAPIs, this.SelectedUserGroupIndex.UserGroupID, this.SelectedUserGroupIndex.Name);
                dialogResult = wizardUserRegister.ShowDialog(); wizardUserRegister.Dispose();
            }
            if (sender.Equals(this.buttonRemoveMember) && this.SelectedUserGroupIndex != null && this.fastUserGroupDetails.SelectedObject != null)
            {
                UserGroupMember userGroupMember = (UserGroupMember)this.fastUserGroupDetails.SelectedObject;
                if (userGroupMember != null && CustomMsgBox.Show(this, "Are you sure you want to remove: " + "\r\n" + "\r\n" + userGroupMember.UserName + "\r\n" + "\r\n" + "from this group?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    this.userGroupAPIs.UserGroupRemoveMember(userGroupMember.UserGroupDetailID, this.SelectedUserGroupIndex.UserGroupID, this.SelectedUserGroupIndex.Name, userGroupMember.SecurityIdentifier, userGroupMember.UserName);
                    dialogResult = DialogResult.OK;
                }
            }

            if (dialogResult == DialogResult.OK) this.GetUserGroupMembers();
        }

        #endregion Add, remove member



        #region MERGE CELL
        private void gridexUserGroupControls_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0 || !(e.ColumnIndex == 0 || e.ColumnIndex == 1))
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void gridexUserGroupControls_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || !(e.ColumnIndex == 0 || e.ColumnIndex == 1))
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = gridexUserGroupControls.AdvancedCellBorderStyle.Top;
            }
        }

        private bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = gridexUserGroupControls[column, row];
            DataGridViewCell cell2 = gridexUserGroupControls[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        #endregion MERGE CELL

        #region MERGE CELL
        private void gridexUserGroupReports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0 || !(e.ColumnIndex == 0))
                return;
            if (IsGroupReportTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void gridexUserGroupReports_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || !(e.ColumnIndex == 0))
                return;
            if (IsGroupReportTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = gridexUserGroupReports.AdvancedCellBorderStyle.Top;
            }
        }

        private bool IsGroupReportTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = gridexUserGroupReports[column, row];
            DataGridViewCell cell2 = gridexUserGroupReports[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        #endregion MERGE CELL

    }
}
