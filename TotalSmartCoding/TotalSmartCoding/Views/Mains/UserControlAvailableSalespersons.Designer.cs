namespace TotalSmartCoding.Views.Mains
{
    partial class UserControlAvailableSalespersons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlAvailableSalespersons));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonOK = new System.Windows.Forms.ToolStripButton();
            this.fastUserControlAvailableSalespersons = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvEmployeeType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvEmployeeCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvEmployeeName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserControlAvailableSalespersons)).BeginInit();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 493);
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
            this.buttonOK.Size = new System.Drawing.Size(66, 36);
            this.buttonOK.Text = "Add";
            this.buttonOK.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // fastUserControlAvailableSalespersons
            // 
            this.fastUserControlAvailableSalespersons.AllColumns.Add(this.olvID);
            this.fastUserControlAvailableSalespersons.AllColumns.Add(this.olvEmployeeType);
            this.fastUserControlAvailableSalespersons.AllColumns.Add(this.olvEmployeeCode);
            this.fastUserControlAvailableSalespersons.AllColumns.Add(this.olvEmployeeName);
            this.fastUserControlAvailableSalespersons.BackColor = System.Drawing.Color.Ivory;
            this.fastUserControlAvailableSalespersons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvEmployeeCode,
            this.olvEmployeeName});
            this.fastUserControlAvailableSalespersons.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastUserControlAvailableSalespersons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastUserControlAvailableSalespersons.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastUserControlAvailableSalespersons.FullRowSelect = true;
            this.fastUserControlAvailableSalespersons.GroupImageList = this.imageList32;
            this.fastUserControlAvailableSalespersons.HideSelection = false;
            this.fastUserControlAvailableSalespersons.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserControlAvailableSalespersons.Location = new System.Drawing.Point(0, 0);
            this.fastUserControlAvailableSalespersons.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.fastUserControlAvailableSalespersons.Name = "fastUserControlAvailableSalespersons";
            this.fastUserControlAvailableSalespersons.OwnerDraw = true;
            this.fastUserControlAvailableSalespersons.ShowGroups = false;
            this.fastUserControlAvailableSalespersons.Size = new System.Drawing.Size(534, 493);
            this.fastUserControlAvailableSalespersons.TabIndex = 101;
            this.fastUserControlAvailableSalespersons.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastUserControlAvailableSalespersons.UseCompatibleStateImageBehavior = false;
            this.fastUserControlAvailableSalespersons.UseFiltering = true;
            this.fastUserControlAvailableSalespersons.View = System.Windows.Forms.View.Details;
            this.fastUserControlAvailableSalespersons.VirtualMode = true;
            this.fastUserControlAvailableSalespersons.SelectedIndexChanged += new System.EventHandler(this.fastUserControlAvailableSalespersons_SelectedIndexChanged);
            // 
            // olvID
            // 
            this.olvID.Text = "";
            this.olvID.Width = 20;
            // 
            // olvEmployeeType
            // 
            this.olvEmployeeType.AspectName = "EmployeeType";
            this.olvEmployeeType.IsVisible = false;
            this.olvEmployeeType.Text = "";
            // 
            // olvEmployeeCode
            // 
            this.olvEmployeeCode.AspectName = "EmployeeCode";
            this.olvEmployeeCode.Text = "";
            this.olvEmployeeCode.Width = 208;
            // 
            // olvEmployeeName
            // 
            this.olvEmployeeName.AspectName = "EmployeeName";
            this.olvEmployeeName.FillsFreeSpace = true;
            this.olvEmployeeName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvEmployeeName.Sortable = false;
            this.olvEmployeeName.Text = "";
            this.olvEmployeeName.Width = 90;
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
            this.imageList32.Images.SetKeyName(12, "group-of-users");
            // 
            // UserControlAvailableSalespersons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 532);
            this.Controls.Add(this.fastUserControlAvailableSalespersons);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserControlAvailableSalespersons";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Available Salespersons";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastUserControlAvailableSalespersons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private BrightIdeasSoftware.FastObjectListView fastUserControlAvailableSalespersons;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvEmployeeName;
        private BrightIdeasSoftware.OLVColumn olvEmployeeType;
        private System.Windows.Forms.ImageList imageList32;
        private BrightIdeasSoftware.OLVColumn olvEmployeeCode;
    }
}