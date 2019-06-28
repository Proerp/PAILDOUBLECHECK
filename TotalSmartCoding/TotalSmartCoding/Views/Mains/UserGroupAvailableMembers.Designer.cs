namespace TotalSmartCoding.Views.Mains
{
    partial class UserGroupAvailableMembers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserGroupAvailableMembers));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fastUserGroupAvailableMembers = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUserType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUserName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonOK = new System.Windows.Forms.ToolStripButton();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserGroupAvailableMembers)).BeginInit();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 518);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(500, 39);
            this.toolStrip1.TabIndex = 100;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fastUserGroupAvailableMembers
            // 
            this.fastUserGroupAvailableMembers.AllColumns.Add(this.olvID);
            this.fastUserGroupAvailableMembers.AllColumns.Add(this.olvUserType);
            this.fastUserGroupAvailableMembers.AllColumns.Add(this.olvUserName);
            this.fastUserGroupAvailableMembers.BackColor = System.Drawing.Color.Ivory;
            this.fastUserGroupAvailableMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvUserName});
            this.fastUserGroupAvailableMembers.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastUserGroupAvailableMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastUserGroupAvailableMembers.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastUserGroupAvailableMembers.FullRowSelect = true;
            this.fastUserGroupAvailableMembers.GroupImageList = this.imageList32;
            this.fastUserGroupAvailableMembers.HideSelection = false;
            this.fastUserGroupAvailableMembers.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserGroupAvailableMembers.Location = new System.Drawing.Point(0, 0);
            this.fastUserGroupAvailableMembers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastUserGroupAvailableMembers.Name = "fastUserGroupAvailableMembers";
            this.fastUserGroupAvailableMembers.OwnerDraw = true;
            this.fastUserGroupAvailableMembers.ShowGroups = false;
            this.fastUserGroupAvailableMembers.Size = new System.Drawing.Size(500, 518);
            this.fastUserGroupAvailableMembers.TabIndex = 101;
            this.fastUserGroupAvailableMembers.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserGroupAvailableMembers.UseCompatibleStateImageBehavior = false;
            this.fastUserGroupAvailableMembers.UseFiltering = true;
            this.fastUserGroupAvailableMembers.View = System.Windows.Forms.View.Details;
            this.fastUserGroupAvailableMembers.VirtualMode = true;
            this.fastUserGroupAvailableMembers.SelectedIndexChanged += new System.EventHandler(this.fastUserGroupAvailableMembers_SelectedIndexChanged);
            // 
            // olvID
            // 
            this.olvID.Text = "";
            this.olvID.Width = 20;
            // 
            // olvUserType
            // 
            this.olvUserType.AspectName = "UserType";
            this.olvUserType.IsVisible = false;
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
            // buttonESC
            // 
            this.buttonESC.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonESC.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;
            this.buttonESC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonESC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonESC.Name = "buttonESC";
            this.buttonESC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonESC.Size = new System.Drawing.Size(83, 36);
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
            this.buttonOK.Size = new System.Drawing.Size(74, 36);
            this.buttonOK.Text = "Add";
            this.buttonOK.Click += new System.EventHandler(this.buttonOKESC_Click);
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
            // 
            // UserGroupAvailableMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 557);
            this.Controls.Add(this.fastUserGroupAvailableMembers);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserGroupAvailableMembers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Available Users";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserGroupAvailableMembers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private BrightIdeasSoftware.FastObjectListView fastUserGroupAvailableMembers;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvUserName;
        private BrightIdeasSoftware.OLVColumn olvUserType;
        private System.Windows.Forms.ImageList imageList32;
    }
}