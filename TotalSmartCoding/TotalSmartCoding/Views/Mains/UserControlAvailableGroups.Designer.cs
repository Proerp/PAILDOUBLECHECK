namespace TotalSmartCoding.Views.Mains
{
    partial class UserControlAvailableGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlAvailableGroups));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonOK = new System.Windows.Forms.ToolStripButton();
            this.fastUserControlAvailableGroups = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUserGroup = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvGroupCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvGroupName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserControlAvailableGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonESC,
            this.buttonOK});
            this.toolStrip1.Location = new System.Drawing.Point(0, 366);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(534, 39);
            this.toolStrip1.TabIndex = 100;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonESC
            // 
            this.buttonESC.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonESC.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;
            this.buttonESC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonESC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonESC.Size = new System.Drawing.Size(74, 36);
            this.buttonESC.Text = "Cancel";
            this.buttonESC.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Image = global::TotalSmartCoding.Properties.Resources.networking;
            this.buttonOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonOK.Size = new System.Drawing.Size(65, 36);
            this.buttonOK.Text = "Join";
            this.buttonOK.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // fastUserControlAvailableGroups
            // 
            this.fastUserControlAvailableGroups.AllColumns.Add(this.olvID);
            this.fastUserControlAvailableGroups.AllColumns.Add(this.olvUserGroup);
            this.fastUserControlAvailableGroups.AllColumns.Add(this.olvGroupCode);
            this.fastUserControlAvailableGroups.AllColumns.Add(this.olvGroupName);
            this.fastUserControlAvailableGroups.BackColor = System.Drawing.Color.Ivory;
            this.fastUserControlAvailableGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvGroupCode,
            this.olvGroupName});
            this.fastUserControlAvailableGroups.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastUserControlAvailableGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastUserControlAvailableGroups.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastUserControlAvailableGroups.FullRowSelect = true;
            this.fastUserControlAvailableGroups.GroupImageList = this.imageList32;
            this.fastUserControlAvailableGroups.HideSelection = false;
            this.fastUserControlAvailableGroups.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserControlAvailableGroups.Location = new System.Drawing.Point(0, 0);
            this.fastUserControlAvailableGroups.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.fastUserControlAvailableGroups.Name = "fastUserControlAvailableGroups";
            this.fastUserControlAvailableGroups.OwnerDraw = true;
            this.fastUserControlAvailableGroups.ShowGroups = false;
            this.fastUserControlAvailableGroups.Size = new System.Drawing.Size(534, 366);
            this.fastUserControlAvailableGroups.TabIndex = 101;
            this.fastUserControlAvailableGroups.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserControlAvailableGroups.UseCompatibleStateImageBehavior = false;
            this.fastUserControlAvailableGroups.UseFiltering = true;
            this.fastUserControlAvailableGroups.View = System.Windows.Forms.View.Details;
            this.fastUserControlAvailableGroups.VirtualMode = true;
            this.fastUserControlAvailableGroups.SelectedIndexChanged += new System.EventHandler(this.fastUserControlAvailableGroups_SelectedIndexChanged);
            // 
            // olvID
            // 
            this.olvID.Text = "";
            this.olvID.Width = 20;
            // 
            // olvUserGroup
            // 
            this.olvUserGroup.AspectName = "UserGroup";
            this.olvUserGroup.IsVisible = false;
            this.olvUserGroup.Text = "";
            // 
            // olvGroupCode
            // 
            this.olvGroupCode.AspectName = "GroupCode";
            this.olvGroupCode.Text = "";
            this.olvGroupCode.Width = 208;
            // 
            // olvGroupName
            // 
            this.olvGroupName.AspectName = "GroupName";
            this.olvGroupName.FillsFreeSpace = true;
            this.olvGroupName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvGroupName.Sortable = false;
            this.olvGroupName.Text = "";
            this.olvGroupName.Width = 90;
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
            this.imageList32.Images.SetKeyName(9, "UserGroupM");
            this.imageList32.Images.SetKeyName(10, "UserGroupR");
            this.imageList32.Images.SetKeyName(11, "Manage_group");
            // 
            // UserControlAvailableGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 405);
            this.Controls.Add(this.fastUserControlAvailableGroups);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserControlAvailableGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Available Groups";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserControlAvailableGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private BrightIdeasSoftware.FastObjectListView fastUserControlAvailableGroups;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvGroupName;
        private BrightIdeasSoftware.OLVColumn olvUserGroup;
        private System.Windows.Forms.ImageList imageList32;
        private BrightIdeasSoftware.OLVColumn olvGroupCode;
    }
}