namespace TotalSmartCoding.Views.Mains
{
    partial class UserGroupControls
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserGroupControls));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolUserGroupDetails = new System.Windows.Forms.ToolStrip();
            this.buttonAddMember = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonRemoveMember = new System.Windows.Forms.ToolStripButton();
            this.fastUserGroups = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUserGroupType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUserGroupName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.gridexUserGroupControls = new CustomControls.DataGridexView();
            this.ModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleDetailName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoAccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ReadOnly = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Editable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ApprovalPermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UnApprovalPermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VoidablePermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UnVoidablePermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolUserGroups = new System.Windows.Forms.ToolStrip();
            this.buttonAddUserGroup = new System.Windows.Forms.ToolStripButton();
            this.buttonRemoveUserGroup = new System.Windows.Forms.ToolStripButton();
            this.buttonRenameUserGroup = new System.Windows.Forms.ToolStripButton();
            this.olvTreePrimaryID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreeAncestorID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreeCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreeParameterName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panelCenter = new System.Windows.Forms.Panel();
            this.gridexUserGroupReports = new CustomControls.DataGridexView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fastUserGroupDetails = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUserType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUserName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolUserGroupDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridexUserGroupControls)).BeginInit();
            this.toolUserGroups.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridexUserGroupReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserGroupDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // toolUserGroupDetails
            // 
            this.toolUserGroupDetails.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolUserGroupDetails.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolUserGroupDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAddMember,
            this.toolStripSeparator1,
            this.buttonRemoveMember});
            this.toolUserGroupDetails.Location = new System.Drawing.Point(0, 0);
            this.toolUserGroupDetails.Name = "toolUserGroupDetails";
            this.toolUserGroupDetails.Size = new System.Drawing.Size(1016, 39);
            this.toolUserGroupDetails.TabIndex = 0;
            this.toolUserGroupDetails.Text = "toolStrip1";
            // 
            // buttonAddMember
            // 
            this.buttonAddMember.Image = global::TotalSmartCoding.Properties.Resources.add_user;
            this.buttonAddMember.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAddMember.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddMember.Name = "buttonAddMember";
            this.buttonAddMember.Size = new System.Drawing.Size(147, 36);
            this.buttonAddMember.Text = "Add a new member";
            this.buttonAddMember.Click += new System.EventHandler(this.buttonAddRemoveMember_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // buttonRemoveMember
            // 
            this.buttonRemoveMember.Image = global::TotalSmartCoding.Properties.Resources.remove_user;
            this.buttonRemoveMember.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRemoveMember.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRemoveMember.Name = "buttonRemoveMember";
            this.buttonRemoveMember.Size = new System.Drawing.Size(180, 36);
            this.buttonRemoveMember.Text = "Remove selected member";
            this.buttonRemoveMember.Click += new System.EventHandler(this.buttonAddRemoveMember_Click);
            // 
            // fastUserGroups
            // 
            this.fastUserGroups.AllColumns.Add(this.olvID);
            this.fastUserGroups.AllColumns.Add(this.olvUserGroupType);
            this.fastUserGroups.AllColumns.Add(this.olvUserGroupName);
            this.fastUserGroups.BackColor = System.Drawing.Color.Ivory;
            this.fastUserGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvUserGroupName});
            this.fastUserGroups.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastUserGroups.Dock = System.Windows.Forms.DockStyle.Left;
            this.fastUserGroups.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastUserGroups.FullRowSelect = true;
            this.fastUserGroups.GroupImageList = this.imageList32;
            this.fastUserGroups.HideSelection = false;
            this.fastUserGroups.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserGroups.Location = new System.Drawing.Point(0, 39);
            this.fastUserGroups.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastUserGroups.Name = "fastUserGroups";
            this.fastUserGroups.OwnerDraw = true;
            this.fastUserGroups.ShowGroups = false;
            this.fastUserGroups.Size = new System.Drawing.Size(225, 610);
            this.fastUserGroups.TabIndex = 69;
            this.fastUserGroups.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserGroups.UseCompatibleStateImageBehavior = false;
            this.fastUserGroups.UseFiltering = true;
            this.fastUserGroups.View = System.Windows.Forms.View.Details;
            this.fastUserGroups.VirtualMode = true;
            this.fastUserGroups.SelectedIndexChanged += new System.EventHandler(this.fastUserGroups_SelectedIndexChanged);
            // 
            // olvID
            // 
            this.olvID.Text = "";
            this.olvID.Width = 20;
            // 
            // olvUserGroupType
            // 
            this.olvUserGroupType.AspectName = "UserGroupType";
            this.olvUserGroupType.DisplayIndex = 1;
            this.olvUserGroupType.IsVisible = false;
            // 
            // olvUserGroupName
            // 
            this.olvUserGroupName.AspectName = "Name";
            this.olvUserGroupName.FillsFreeSpace = true;
            this.olvUserGroupName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvUserGroupName.Sortable = false;
            this.olvUserGroupName.Text = "";
            this.olvUserGroupName.Width = 90;
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "Forklift");
            this.imageList32.Images.SetKeyName(1, "ForkliftYellow");
            this.imageList32.Images.SetKeyName(2, "ForkliftOrange");
            this.imageList32.Images.SetKeyName(3, "ForkliftJapan");
            this.imageList32.Images.SetKeyName(4, "Placeholder32");
            this.imageList32.Images.SetKeyName(5, "Storage32");
            this.imageList32.Images.SetKeyName(6, "Sales-Order-32");
            this.imageList32.Images.SetKeyName(7, "Sign_Order_32");
            this.imageList32.Images.SetKeyName(8, "Assembly-32");
            this.imageList32.Images.SetKeyName(9, "UserGroupN");
            // 
            // gridexUserGroupControls
            // 
            this.gridexUserGroupControls.AllowAddRow = false;
            this.gridexUserGroupControls.AllowDeleteRow = false;
            this.gridexUserGroupControls.AllowUserToAddRows = false;
            this.gridexUserGroupControls.AllowUserToDeleteRows = false;
            this.gridexUserGroupControls.BackgroundColor = System.Drawing.Color.Ivory;
            this.gridexUserGroupControls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridexUserGroupControls.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridexUserGroupControls.ColumnHeadersHeight = 24;
            this.gridexUserGroupControls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ModuleName,
            this.ModuleDetailName,
            this.LocationName,
            this.NoAccess,
            this.ReadOnly,
            this.Editable,
            this.ApprovalPermitted,
            this.UnApprovalPermitted,
            this.VoidablePermitted,
            this.UnVoidablePermitted});
            this.gridexUserGroupControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridexUserGroupControls.Editable = true;
            this.gridexUserGroupControls.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.gridexUserGroupControls.Location = new System.Drawing.Point(0, 39);
            this.gridexUserGroupControls.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridexUserGroupControls.MultiSelect = false;
            this.gridexUserGroupControls.Name = "gridexUserGroupControls";
            this.gridexUserGroupControls.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridexUserGroupControls.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridexUserGroupControls.RowTemplate.Height = 24;
            this.gridexUserGroupControls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridexUserGroupControls.Size = new System.Drawing.Size(1016, 177);
            this.gridexUserGroupControls.TabIndex = 70;
            this.gridexUserGroupControls.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridexUserGroupControls_CellContentClick);
            this.gridexUserGroupControls.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridexUserGroupControls_CellFormatting);
            this.gridexUserGroupControls.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridexUserGroupControls_CellPainting);
            // 
            // ModuleName
            // 
            this.ModuleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ModuleName.DataPropertyName = "ModuleName";
            this.ModuleName.FillWeight = 19F;
            this.ModuleName.HeaderText = "Features.Tasks";
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.ReadOnly = true;
            // 
            // ModuleDetailName
            // 
            this.ModuleDetailName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ModuleDetailName.DataPropertyName = "ModuleDetailName";
            this.ModuleDetailName.FillWeight = 21F;
            this.ModuleDetailName.HeaderText = "Features.Modules";
            this.ModuleDetailName.Name = "ModuleDetailName";
            this.ModuleDetailName.ReadOnly = true;
            // 
            // LocationName
            // 
            this.LocationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocationName.DataPropertyName = "LocationName";
            this.LocationName.FillWeight = 11F;
            this.LocationName.HeaderText = "Locations";
            this.LocationName.Name = "LocationName";
            this.LocationName.ReadOnly = true;
            // 
            // NoAccess
            // 
            this.NoAccess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoAccess.DataPropertyName = "NoAccess";
            this.NoAccess.FillWeight = 7F;
            this.NoAccess.HeaderText = "Access Controls.No Access";
            this.NoAccess.Name = "NoAccess";
            // 
            // ReadOnly
            // 
            this.ReadOnly.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReadOnly.DataPropertyName = "ReadOnly";
            this.ReadOnly.FillWeight = 7F;
            this.ReadOnly.HeaderText = "Access Controls.Read Only";
            this.ReadOnly.Name = "ReadOnly";
            // 
            // Editable
            // 
            this.Editable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Editable.DataPropertyName = "Editable";
            this.Editable.FillWeight = 7F;
            this.Editable.HeaderText = "Access Controls.Editable";
            this.Editable.Name = "Editable";
            // 
            // ApprovalPermitted
            // 
            this.ApprovalPermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ApprovalPermitted.DataPropertyName = "ApprovalPermitted";
            this.ApprovalPermitted.FillWeight = 7F;
            this.ApprovalPermitted.HeaderText = "Verify Permissions.Verify";
            this.ApprovalPermitted.Name = "ApprovalPermitted";
            // 
            // UnApprovalPermitted
            // 
            this.UnApprovalPermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnApprovalPermitted.DataPropertyName = "UnApprovalPermitted";
            this.UnApprovalPermitted.FillWeight = 7F;
            this.UnApprovalPermitted.HeaderText = "Verify Permissions.Unverify";
            this.UnApprovalPermitted.Name = "UnApprovalPermitted";
            // 
            // VoidablePermitted
            // 
            this.VoidablePermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VoidablePermitted.DataPropertyName = "VoidablePermitted";
            this.VoidablePermitted.FillWeight = 7F;
            this.VoidablePermitted.HeaderText = "Void Permissions.Void";
            this.VoidablePermitted.Name = "VoidablePermitted";
            // 
            // UnVoidablePermitted
            // 
            this.UnVoidablePermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnVoidablePermitted.DataPropertyName = "UnVoidablePermitted";
            this.UnVoidablePermitted.FillWeight = 7F;
            this.UnVoidablePermitted.HeaderText = "Void Permissions.Unvoid";
            this.UnVoidablePermitted.Name = "UnVoidablePermitted";
            // 
            // toolUserGroups
            // 
            this.toolUserGroups.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolUserGroups.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolUserGroups.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAddUserGroup,
            this.buttonRemoveUserGroup,
            this.buttonRenameUserGroup});
            this.toolUserGroups.Location = new System.Drawing.Point(0, 0);
            this.toolUserGroups.Name = "toolUserGroups";
            this.toolUserGroups.Size = new System.Drawing.Size(1241, 39);
            this.toolUserGroups.TabIndex = 0;
            this.toolUserGroups.Text = "toolStrip2";
            // 
            // buttonAddUserGroup
            // 
            this.buttonAddUserGroup.Image = global::TotalSmartCoding.Properties.Resources.Add_UserGroup;
            this.buttonAddUserGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAddUserGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddUserGroup.Name = "buttonAddUserGroup";
            this.buttonAddUserGroup.Size = new System.Drawing.Size(134, 36);
            this.buttonAddUserGroup.Text = "Add a new group";
            this.buttonAddUserGroup.Click += new System.EventHandler(this.buttonAddRemoveRenameUserGroup_Click);
            // 
            // buttonRemoveUserGroup
            // 
            this.buttonRemoveUserGroup.Image = global::TotalSmartCoding.Properties.Resources.Remove_UserGroup;
            this.buttonRemoveUserGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRemoveUserGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRemoveUserGroup.Name = "buttonRemoveUserGroup";
            this.buttonRemoveUserGroup.Size = new System.Drawing.Size(157, 36);
            this.buttonRemoveUserGroup.Text = "Delete selected group";
            this.buttonRemoveUserGroup.Click += new System.EventHandler(this.buttonAddRemoveRenameUserGroup_Click);
            // 
            // buttonRenameUserGroup
            // 
            this.buttonRenameUserGroup.Image = global::TotalSmartCoding.Properties.Resources.Cloud_sync;
            this.buttonRenameUserGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRenameUserGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRenameUserGroup.Name = "buttonRenameUserGroup";
            this.buttonRenameUserGroup.Size = new System.Drawing.Size(167, 36);
            this.buttonRenameUserGroup.Text = "Rename selected group";
            this.buttonRenameUserGroup.Click += new System.EventHandler(this.buttonAddRemoveRenameUserGroup_Click);
            // 
            // olvTreePrimaryID
            // 
            this.olvTreePrimaryID.AspectName = "PrimaryID";
            this.olvTreePrimaryID.IsVisible = false;
            // 
            // olvTreeAncestorID
            // 
            this.olvTreeAncestorID.AspectName = "AncestorID";
            this.olvTreeAncestorID.IsVisible = false;
            // 
            // olvTreeCode
            // 
            this.olvTreeCode.AspectName = "Code";
            this.olvTreeCode.IsVisible = false;
            // 
            // olvTreeParameterName
            // 
            this.olvTreeParameterName.AspectName = "ParameterName";
            this.olvTreeParameterName.IsVisible = false;
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.gridexUserGroupReports);
            this.panelCenter.Controls.Add(this.fastUserGroupDetails);
            this.panelCenter.Controls.Add(this.gridexUserGroupControls);
            this.panelCenter.Controls.Add(this.toolUserGroupDetails);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(225, 39);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(1016, 610);
            this.panelCenter.TabIndex = 74;
            // 
            // gridexUserGroupReports
            // 
            this.gridexUserGroupReports.AllowAddRow = false;
            this.gridexUserGroupReports.AllowDeleteRow = false;
            this.gridexUserGroupReports.AllowUserToAddRows = false;
            this.gridexUserGroupReports.AllowUserToDeleteRows = false;
            this.gridexUserGroupReports.BackgroundColor = System.Drawing.Color.Ivory;
            this.gridexUserGroupReports.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridexUserGroupReports.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridexUserGroupReports.ColumnHeadersHeight = 24;
            this.gridexUserGroupReports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Enabled});
            this.gridexUserGroupReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridexUserGroupReports.Editable = true;
            this.gridexUserGroupReports.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.gridexUserGroupReports.Location = new System.Drawing.Point(0, 216);
            this.gridexUserGroupReports.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridexUserGroupReports.MultiSelect = false;
            this.gridexUserGroupReports.Name = "gridexUserGroupReports";
            this.gridexUserGroupReports.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridexUserGroupReports.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridexUserGroupReports.RowTemplate.Height = 24;
            this.gridexUserGroupReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridexUserGroupReports.Size = new System.Drawing.Size(1016, 177);
            this.gridexUserGroupReports.TabIndex = 103;
            this.gridexUserGroupReports.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridexUserGroupReports_CellContentClick);
            this.gridexUserGroupReports.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridexUserGroupReports_CellFormatting);
            this.gridexUserGroupReports.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridexUserGroupReports_CellPainting);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ReportGroupName";
            this.dataGridViewTextBoxColumn1.FillWeight = 30F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Reports.Groups";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ReportName";
            this.dataGridViewTextBoxColumn2.FillWeight = 60F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Reports.Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // Enabled
            // 
            this.Enabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Enabled.DataPropertyName = "Enabled";
            this.Enabled.FillWeight = 10F;
            this.Enabled.HeaderText = "Enabled";
            this.Enabled.Name = "Enabled";
            // 
            // fastUserGroupDetails
            // 
            this.fastUserGroupDetails.AllColumns.Add(this.olvColumn1);
            this.fastUserGroupDetails.AllColumns.Add(this.olvUserType);
            this.fastUserGroupDetails.AllColumns.Add(this.olvUserName);
            this.fastUserGroupDetails.BackColor = System.Drawing.Color.Ivory;
            this.fastUserGroupDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvUserName});
            this.fastUserGroupDetails.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastUserGroupDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fastUserGroupDetails.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastUserGroupDetails.FullRowSelect = true;
            this.fastUserGroupDetails.GroupImageList = this.imageList32;
            this.fastUserGroupDetails.HideSelection = false;
            this.fastUserGroupDetails.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserGroupDetails.Location = new System.Drawing.Point(0, 440);
            this.fastUserGroupDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastUserGroupDetails.Name = "fastUserGroupDetails";
            this.fastUserGroupDetails.OwnerDraw = true;
            this.fastUserGroupDetails.ShowGroups = false;
            this.fastUserGroupDetails.Size = new System.Drawing.Size(1016, 170);
            this.fastUserGroupDetails.TabIndex = 102;
            this.fastUserGroupDetails.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserGroupDetails.UseCompatibleStateImageBehavior = false;
            this.fastUserGroupDetails.UseFiltering = true;
            this.fastUserGroupDetails.View = System.Windows.Forms.View.Details;
            this.fastUserGroupDetails.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.Text = "";
            this.olvColumn1.Width = 20;
            // 
            // olvUserType
            // 
            this.olvUserType.AspectName = "UserType";
            this.olvUserType.IsVisible = false;
            this.olvUserType.Text = "UserType";
            // 
            // olvUserName
            // 
            this.olvUserName.AspectName = "UserName";
            this.olvUserName.FillsFreeSpace = true;
            this.olvUserName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvUserName.Sortable = false;
            this.olvUserName.Text = "";
            this.olvUserName.Width = 90;
            // 
            // UserGroupControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 649);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.fastUserGroups);
            this.Controls.Add(this.toolUserGroups);
            this.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserGroupControls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Groups";
            this.Load += new System.EventHandler(this.UserGroupControls_Load);
            this.toolUserGroupDetails.ResumeLayout(false);
            this.toolUserGroupDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridexUserGroupControls)).EndInit();
            this.toolUserGroups.ResumeLayout(false);
            this.toolUserGroups.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridexUserGroupReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserGroupDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolUserGroupDetails;
        private System.Windows.Forms.ToolStripButton buttonAddMember;
        private System.Windows.Forms.ToolStripButton buttonRemoveMember;
        private BrightIdeasSoftware.FastObjectListView fastUserGroups;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvUserGroupName;
        private CustomControls.DataGridexView gridexUserGroupControls;
        private System.Windows.Forms.ImageList imageList32;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolUserGroups;
        private BrightIdeasSoftware.OLVColumn olvTreePrimaryID;
        private BrightIdeasSoftware.OLVColumn olvTreeAncestorID;
        private BrightIdeasSoftware.OLVColumn olvTreeCode;
        private BrightIdeasSoftware.OLVColumn olvTreeParameterName;
        private System.Windows.Forms.ToolStripButton buttonAddUserGroup;
        private System.Windows.Forms.ToolStripButton buttonRemoveUserGroup;
        private BrightIdeasSoftware.OLVColumn olvUserGroupType;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleDetailName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NoAccess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ReadOnly;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Editable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ApprovalPermitted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UnApprovalPermitted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn VoidablePermitted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UnVoidablePermitted;
        private BrightIdeasSoftware.FastObjectListView fastUserGroupDetails;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvUserName;
        private BrightIdeasSoftware.OLVColumn olvUserType;
        private CustomControls.DataGridexView gridexUserGroupReports;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enabled;
        private System.Windows.Forms.ToolStripButton buttonRenameUserGroup;
    }
}