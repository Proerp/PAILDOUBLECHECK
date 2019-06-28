namespace TotalSmartCoding.Views.Mains
{
    partial class UserReferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserReferences));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.comboUserID = new System.Windows.Forms.ToolStripComboBox();
            this.comboOrganizationalUnit = new System.Windows.Forms.ToolStripComboBox();
            this.comboInActive = new System.Windows.Forms.ToolStripComboBox();
            this.buttonUserRegister = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonUserUnregister = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonUserToggleVoid = new System.Windows.Forms.ToolStripButton();
            this.fastNMVNTasks = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvModuleName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvModuleDetailName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.gridexUserAccessControl = new CustomControls.DataGridexView();
            this.LocationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrganizationalUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoAccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ReadOnly = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Editable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ApprovalPermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UnApprovalPermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VoidablePermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UnVoidablePermitted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.comboActiveOption = new System.Windows.Forms.ToolStripComboBox();
            this.buttonAddOU = new System.Windows.Forms.ToolStripButton();
            this.buttonRemoveOU = new System.Windows.Forms.ToolStripButton();
            this.treeUserID = new BrightIdeasSoftware.DataTreeListView();
            this.olvTreeName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreeSelected = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreePrimaryID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreeAncestorID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreeCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTreeParameterName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.naviIndex = new Guifreaks.Navisuite.NaviBar(this.components);
            this.navibandUserTrees = new Guifreaks.Navisuite.NaviBand(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pictureTopleft = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastNMVNTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridexUserAccessControl)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeUserID)).BeginInit();
            this.naviIndex.SuspendLayout();
            this.navibandUserTrees.ClientArea.SuspendLayout();
            this.navibandUserTrees.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTopleft)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboUserID,
            this.comboOrganizationalUnit,
            this.comboInActive,
            this.buttonUserRegister,
            this.toolStripSeparator1,
            this.buttonUserUnregister,
            this.toolStripSeparator2,
            this.buttonUserToggleVoid});
            this.toolStrip1.Location = new System.Drawing.Point(33, 55);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1208, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // comboUserID
            // 
            this.comboUserID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUserID.Enabled = false;
            this.comboUserID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboUserID.Name = "comboUserID";
            this.comboUserID.Size = new System.Drawing.Size(396, 55);
            this.comboUserID.SelectedIndexChanged += new System.EventHandler(this.comboUserID_SelectedIndexChanged);
            // 
            // comboOrganizationalUnit
            // 
            this.comboOrganizationalUnit.Enabled = false;
            this.comboOrganizationalUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboOrganizationalUnit.Name = "comboOrganizationalUnit";
            this.comboOrganizationalUnit.Size = new System.Drawing.Size(196, 55);
            // 
            // comboInActive
            // 
            this.comboInActive.Enabled = false;
            this.comboInActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboInActive.Name = "comboInActive";
            this.comboInActive.Size = new System.Drawing.Size(88, 55);
            // 
            // buttonUserRegister
            // 
            this.buttonUserRegister.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonUserRegister.Image = global::TotalSmartCoding.Properties.Resources.add_user;
            this.buttonUserRegister.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonUserRegister.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonUserRegister.Name = "buttonUserRegister";
            this.buttonUserRegister.Size = new System.Drawing.Size(52, 52);
            this.buttonUserRegister.ToolTipText = "Register user for new location";
            this.buttonUserRegister.Click += new System.EventHandler(this.buttonUserRegister_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // buttonUserUnregister
            // 
            this.buttonUserUnregister.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonUserUnregister.Image = global::TotalSmartCoding.Properties.Resources.remove_user;
            this.buttonUserUnregister.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonUserUnregister.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonUserUnregister.Name = "buttonUserUnregister";
            this.buttonUserUnregister.Size = new System.Drawing.Size(52, 52);
            this.buttonUserUnregister.ToolTipText = "Cancel user registration";
            this.buttonUserUnregister.Click += new System.EventHandler(this.buttonUserUnregister_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // buttonUserToggleVoid
            // 
            this.buttonUserToggleVoid.Image = global::TotalSmartCoding.Properties.Resources.no_persons_2;
            this.buttonUserToggleVoid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonUserToggleVoid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonUserToggleVoid.Name = "buttonUserToggleVoid";
            this.buttonUserToggleVoid.Size = new System.Drawing.Size(151, 52);
            this.buttonUserToggleVoid.Text = "Set active status";
            this.buttonUserToggleVoid.Click += new System.EventHandler(this.buttonUserToggleVoid_Click);
            // 
            // fastNMVNTasks
            // 
            this.fastNMVNTasks.AllColumns.Add(this.olvID);
            this.fastNMVNTasks.AllColumns.Add(this.olvModuleName);
            this.fastNMVNTasks.AllColumns.Add(this.olvModuleDetailName);
            this.fastNMVNTasks.BackColor = System.Drawing.Color.Ivory;
            this.fastNMVNTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvModuleDetailName});
            this.fastNMVNTasks.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastNMVNTasks.Dock = System.Windows.Forms.DockStyle.Left;
            this.fastNMVNTasks.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastNMVNTasks.FullRowSelect = true;
            this.fastNMVNTasks.GroupImageList = this.imageList32;
            this.fastNMVNTasks.HideSelection = false;
            this.fastNMVNTasks.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastNMVNTasks.Location = new System.Drawing.Point(33, 110);
            this.fastNMVNTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastNMVNTasks.Name = "fastNMVNTasks";
            this.fastNMVNTasks.OwnerDraw = true;
            this.fastNMVNTasks.ShowGroups = false;
            this.fastNMVNTasks.Size = new System.Drawing.Size(289, 539);
            this.fastNMVNTasks.TabIndex = 69;
            this.fastNMVNTasks.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastNMVNTasks.UseCompatibleStateImageBehavior = false;
            this.fastNMVNTasks.UseFiltering = true;
            this.fastNMVNTasks.View = System.Windows.Forms.View.Details;
            this.fastNMVNTasks.VirtualMode = true;
            this.fastNMVNTasks.SelectedIndexChanged += new System.EventHandler(this.fastNMVNTasks_SelectedIndexChanged);
            // 
            // olvID
            // 
            this.olvID.Text = "";
            this.olvID.Width = 20;
            // 
            // olvModuleName
            // 
            this.olvModuleName.AspectName = "ModuleName";
            this.olvModuleName.IsVisible = false;
            // 
            // olvModuleDetailName
            // 
            this.olvModuleDetailName.AspectName = "ModuleDetailName";
            this.olvModuleDetailName.FillsFreeSpace = true;
            this.olvModuleDetailName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvModuleDetailName.Sortable = false;
            this.olvModuleDetailName.Text = "";
            this.olvModuleDetailName.Width = 90;
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
            // 
            // gridexUserAccessControl
            // 
            this.gridexUserAccessControl.AllowAddRow = false;
            this.gridexUserAccessControl.AllowDeleteRow = false;
            this.gridexUserAccessControl.AllowUserToAddRows = false;
            this.gridexUserAccessControl.AllowUserToDeleteRows = false;
            this.gridexUserAccessControl.BackgroundColor = System.Drawing.Color.Ivory;
            this.gridexUserAccessControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridexUserAccessControl.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridexUserAccessControl.ColumnHeadersHeight = 24;
            this.gridexUserAccessControl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocationName,
            this.OrganizationalUnitName,
            this.NoAccess,
            this.ReadOnly,
            this.Editable,
            this.ApprovalPermitted,
            this.UnApprovalPermitted,
            this.VoidablePermitted,
            this.UnVoidablePermitted});
            this.gridexUserAccessControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridexUserAccessControl.Editable = true;
            this.gridexUserAccessControl.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.gridexUserAccessControl.Location = new System.Drawing.Point(322, 110);
            this.gridexUserAccessControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridexUserAccessControl.Name = "gridexUserAccessControl";
            this.gridexUserAccessControl.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridexUserAccessControl.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridexUserAccessControl.RowTemplate.Height = 24;
            this.gridexUserAccessControl.Size = new System.Drawing.Size(919, 539);
            this.gridexUserAccessControl.TabIndex = 70;
            this.gridexUserAccessControl.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridexAccessControls_CellContentClick);
            // 
            // LocationName
            // 
            this.LocationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocationName.DataPropertyName = "LocationName";
            this.LocationName.FillWeight = 15F;
            this.LocationName.HeaderText = "Organizational Units.Location";
            this.LocationName.Name = "LocationName";
            this.LocationName.ReadOnly = true;
            // 
            // OrganizationalUnitName
            // 
            this.OrganizationalUnitName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OrganizationalUnitName.DataPropertyName = "OrganizationalUnitName";
            this.OrganizationalUnitName.FillWeight = 22F;
            this.OrganizationalUnitName.HeaderText = "Organizational Units.OU";
            this.OrganizationalUnitName.Name = "OrganizationalUnitName";
            this.OrganizationalUnitName.ReadOnly = true;
            // 
            // NoAccess
            // 
            this.NoAccess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoAccess.DataPropertyName = "NoAccess";
            this.NoAccess.FillWeight = 9F;
            this.NoAccess.HeaderText = "Access Controls.No Access";
            this.NoAccess.Name = "NoAccess";
            // 
            // ReadOnly
            // 
            this.ReadOnly.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReadOnly.DataPropertyName = "ReadOnly";
            this.ReadOnly.FillWeight = 9F;
            this.ReadOnly.HeaderText = "Access Controls.Read Only";
            this.ReadOnly.Name = "ReadOnly";
            // 
            // Editable
            // 
            this.Editable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Editable.DataPropertyName = "Editable";
            this.Editable.FillWeight = 9F;
            this.Editable.HeaderText = "Access Controls.Editable";
            this.Editable.Name = "Editable";
            // 
            // ApprovalPermitted
            // 
            this.ApprovalPermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ApprovalPermitted.DataPropertyName = "ApprovalPermitted";
            this.ApprovalPermitted.FillWeight = 9F;
            this.ApprovalPermitted.HeaderText = "Verify Permissions.Verify";
            this.ApprovalPermitted.Name = "ApprovalPermitted";
            // 
            // UnApprovalPermitted
            // 
            this.UnApprovalPermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnApprovalPermitted.DataPropertyName = "UnApprovalPermitted";
            this.UnApprovalPermitted.FillWeight = 9F;
            this.UnApprovalPermitted.HeaderText = "Verify Permissions.Unverify";
            this.UnApprovalPermitted.Name = "UnApprovalPermitted";
            // 
            // VoidablePermitted
            // 
            this.VoidablePermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VoidablePermitted.DataPropertyName = "VoidablePermitted";
            this.VoidablePermitted.FillWeight = 9F;
            this.VoidablePermitted.HeaderText = "Void Permissions.Void";
            this.VoidablePermitted.Name = "VoidablePermitted";
            // 
            // UnVoidablePermitted
            // 
            this.UnVoidablePermitted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnVoidablePermitted.DataPropertyName = "UnVoidablePermitted";
            this.UnVoidablePermitted.FillWeight = 9F;
            this.UnVoidablePermitted.HeaderText = "Void Permissions.Unvoid";
            this.UnVoidablePermitted.Name = "UnVoidablePermitted";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboActiveOption,
            this.buttonAddOU,
            this.buttonRemoveOU});
            this.toolStrip2.Location = new System.Drawing.Point(33, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1208, 55);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // comboActiveOption
            // 
            this.comboActiveOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboActiveOption.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.comboActiveOption.Items.AddRange(new object[] {
            "Active only",
            "Show Inactive"});
            this.comboActiveOption.Name = "comboActiveOption";
            this.comboActiveOption.Size = new System.Drawing.Size(139, 55);
            this.comboActiveOption.SelectedIndexChanged += new System.EventHandler(this.comboActiveOption_SelectedIndexChanged);
            // 
            // buttonAddOU
            // 
            this.buttonAddOU.Image = global::TotalSmartCoding.Properties.Resources.Data_add;
            this.buttonAddOU.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAddOU.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAddOU.Name = "buttonAddOU";
            this.buttonAddOU.Size = new System.Drawing.Size(284, 52);
            this.buttonAddOU.Text = "Add new organizational unit [OU]";
            this.buttonAddOU.Click += new System.EventHandler(this.buttonAddRemoveOU_Click);
            // 
            // buttonRemoveOU
            // 
            this.buttonRemoveOU.Image = global::TotalSmartCoding.Properties.Resources.RemoveOU;
            this.buttonRemoveOU.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRemoveOU.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRemoveOU.Name = "buttonRemoveOU";
            this.buttonRemoveOU.Size = new System.Drawing.Size(124, 52);
            this.buttonRemoveOU.Text = "Remove OU";
            this.buttonRemoveOU.Click += new System.EventHandler(this.buttonAddRemoveOU_Click);
            // 
            // treeUserID
            // 
            this.treeUserID.AllColumns.Add(this.olvTreeName);
            this.treeUserID.AllColumns.Add(this.olvTreeSelected);
            this.treeUserID.AllColumns.Add(this.olvTreePrimaryID);
            this.treeUserID.AllColumns.Add(this.olvTreeAncestorID);
            this.treeUserID.AllColumns.Add(this.olvTreeCode);
            this.treeUserID.AllColumns.Add(this.olvTreeParameterName);
            this.treeUserID.BackColor = System.Drawing.Color.Wheat;
            this.treeUserID.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvTreeName,
            this.olvTreeSelected});
            this.treeUserID.DataSource = null;
            this.treeUserID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeUserID.FullRowSelect = true;
            this.treeUserID.KeyAspectName = "NodeID";
            this.treeUserID.Location = new System.Drawing.Point(0, 0);
            this.treeUserID.Name = "treeUserID";
            this.treeUserID.OwnerDraw = true;
            this.treeUserID.ParentKeyAspectName = "ParentNodeID";
            this.treeUserID.RootKeyValueString = "";
            this.treeUserID.ShowGroups = false;
            this.treeUserID.ShowKeyColumns = false;
            this.treeUserID.Size = new System.Drawing.Size(1, 1);
            this.treeUserID.TabIndex = 71;
            this.treeUserID.UseCompatibleStateImageBehavior = false;
            this.treeUserID.UseFilterIndicator = true;
            this.treeUserID.UseFiltering = true;
            this.treeUserID.View = System.Windows.Forms.View.Details;
            this.treeUserID.VirtualMode = true;
            // 
            // olvTreeName
            // 
            this.olvTreeName.AspectName = "Name";
            this.olvTreeName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvTreeName.IsEditable = false;
            this.olvTreeName.Sortable = false;
            this.olvTreeName.Text = "All Organizational Units";
            this.olvTreeName.Width = 275;
            // 
            // olvTreeSelected
            // 
            this.olvTreeSelected.AspectName = "Selected";
            this.olvTreeSelected.CheckBoxes = true;
            this.olvTreeSelected.FillsFreeSpace = true;
            this.olvTreeSelected.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvTreeSelected.IsEditable = false;
            this.olvTreeSelected.Sortable = false;
            this.olvTreeSelected.Text = "Inactive";
            this.olvTreeSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvTreeSelected.Width = 79;
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
            // naviIndex
            // 
            this.naviIndex.ActiveBand = this.navibandUserTrees;
            this.naviIndex.Collapsed = true;
            this.naviIndex.Controls.Add(this.navibandUserTrees);
            this.naviIndex.Controls.Add(this.button1);
            this.naviIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.naviIndex.HeaderHeight = 42;
            this.naviIndex.LayoutStyle = Guifreaks.Navisuite.NaviLayoutStyle.Office2010Blue;
            this.naviIndex.Location = new System.Drawing.Point(0, 36);
            this.naviIndex.Margin = new System.Windows.Forms.Padding(4);
            this.naviIndex.Name = "naviIndex";
            this.naviIndex.PopupHeight = 554;
            this.naviIndex.PopupMinWidth = 368;
            this.naviIndex.ShowMoreOptionsButton = false;
            this.naviIndex.Size = new System.Drawing.Size(33, 613);
            this.naviIndex.TabIndex = 72;
            // 
            // navibandUserTrees
            // 
            // 
            // navibandUserTrees.ClientArea
            // 
            this.navibandUserTrees.ClientArea.Controls.Add(this.treeUserID);
            this.navibandUserTrees.ClientArea.LayoutStyle = Guifreaks.Navisuite.NaviLayoutStyle.StyleFromOwner;
            this.navibandUserTrees.ClientArea.Location = new System.Drawing.Point(0, 0);
            this.navibandUserTrees.ClientArea.Margin = new System.Windows.Forms.Padding(0);
            this.navibandUserTrees.ClientArea.Name = "ClientArea";
            this.navibandUserTrees.ClientArea.Size = new System.Drawing.Size(1, 1);
            this.navibandUserTrees.ClientArea.TabIndex = 0;
            this.navibandUserTrees.LargeImageIndex = 0;
            this.navibandUserTrees.LayoutStyle = Guifreaks.Navisuite.NaviLayoutStyle.StyleFromOwner;
            this.navibandUserTrees.Location = new System.Drawing.Point(1, 42);
            this.navibandUserTrees.Margin = new System.Windows.Forms.Padding(4);
            this.navibandUserTrees.Name = "navibandUserTrees";
            this.navibandUserTrees.Size = new System.Drawing.Size(0, 0);
            this.navibandUserTrees.SmallImageIndex = 0;
            this.navibandUserTrees.TabIndex = 72;
            this.navibandUserTrees.Text = "Click here to expand";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Image = global::TotalSmartCoding.Properties.Resources.Man_2;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 43);
            this.button1.TabIndex = 77;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.naviIndex);
            this.panelLeft.Controls.Add(this.pictureTopleft);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(33, 649);
            this.panelLeft.TabIndex = 73;
            // 
            // pictureTopleft
            // 
            this.pictureTopleft.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureTopleft.Image = global::TotalSmartCoding.Properties.Resources.refresh;
            this.pictureTopleft.Location = new System.Drawing.Point(0, 0);
            this.pictureTopleft.Name = "pictureTopleft";
            this.pictureTopleft.Size = new System.Drawing.Size(33, 36);
            this.pictureTopleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureTopleft.TabIndex = 73;
            this.pictureTopleft.TabStop = false;
            // 
            // UserReferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 649);
            this.Controls.Add(this.gridexUserAccessControl);
            this.Controls.Add(this.fastNMVNTasks);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.panelLeft);
            this.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserReferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User References [Determine the tasks each user can perform]";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastNMVNTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridexUserAccessControl)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeUserID)).EndInit();
            this.naviIndex.ResumeLayout(false);
            this.navibandUserTrees.ClientArea.ResumeLayout(false);
            this.navibandUserTrees.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureTopleft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox comboUserID;
        private System.Windows.Forms.ToolStripButton buttonUserRegister;
        private System.Windows.Forms.ToolStripButton buttonUserUnregister;
        private BrightIdeasSoftware.FastObjectListView fastNMVNTasks;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvModuleDetailName;
        private CustomControls.DataGridexView gridexUserAccessControl;
        private System.Windows.Forms.ImageList imageList32;
        private BrightIdeasSoftware.OLVColumn olvModuleName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private BrightIdeasSoftware.DataTreeListView treeUserID;
        private BrightIdeasSoftware.OLVColumn olvTreeName;
        private BrightIdeasSoftware.OLVColumn olvTreeSelected;
        private BrightIdeasSoftware.OLVColumn olvTreePrimaryID;
        private BrightIdeasSoftware.OLVColumn olvTreeAncestorID;
        private BrightIdeasSoftware.OLVColumn olvTreeCode;
        private BrightIdeasSoftware.OLVColumn olvTreeParameterName;
        private Guifreaks.Navisuite.NaviBar naviIndex;
        private Guifreaks.Navisuite.NaviBand navibandUserTrees;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripComboBox comboOrganizationalUnit;
        private System.Windows.Forms.ToolStripButton buttonAddOU;
        private System.Windows.Forms.ToolStripComboBox comboInActive;
        private System.Windows.Forms.ToolStripComboBox comboActiveOption;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.PictureBox pictureTopleft;
        private System.Windows.Forms.ToolStripButton buttonRemoveOU;
        private System.Windows.Forms.ToolStripButton buttonUserToggleVoid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrganizationalUnitName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NoAccess;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ReadOnly;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Editable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ApprovalPermitted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UnApprovalPermitted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn VoidablePermitted;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UnVoidablePermitted;
    }
}