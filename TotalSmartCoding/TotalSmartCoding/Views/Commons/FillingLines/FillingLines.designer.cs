namespace TotalSmartCoding.Views.Commons.FillingLines
{
    partial class FillingLines
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillingLines));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripChildForm = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.gridexViewDetails = new CustomControls.DataGridexView();
            this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPv4Byte1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPv4Byte2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPv4Byte3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPv4Byte4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fastFillingLineIndex = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBlank = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvFillingLineType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvFillingLineCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvFillingLineName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.toolBottom = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripChildForm.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridexViewDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastFillingLineIndex)).BeginInit();
            this.toolBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripChildForm
            // 
            this.toolStripChildForm.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripChildForm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripChildForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripSeparator1});
            this.toolStripChildForm.Location = new System.Drawing.Point(0, 0);
            this.toolStripChildForm.Name = "toolStripChildForm";
            this.toolStripChildForm.Size = new System.Drawing.Size(1722, 51);
            this.toolStripChildForm.TabIndex = 29;
            this.toolStripChildForm.Text = "toolStrip1";
            this.toolStripChildForm.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(118, 48);
            this.toolStripButton2.Text = "Disconnect";
            this.toolStripButton2.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            this.toolStripSeparator1.Visible = false;
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.Ivory;
            this.panelCenter.BackgroundImage = global::TotalSmartCoding.Properties.Resources.Blue2010Large;
            this.panelCenter.Controls.Add(this.toolBottom);
            this.panelCenter.Controls.Add(this.gridexViewDetails);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCenter.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelCenter.Location = new System.Drawing.Point(1500, 0);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(368, 858);
            this.panelCenter.TabIndex = 76;
            // 
            // gridexViewDetails
            // 
            this.gridexViewDetails.AllowAddRow = true;
            this.gridexViewDetails.AllowDeleteRow = true;
            this.gridexViewDetails.AllowUserToAddRows = false;
            this.gridexViewDetails.AllowUserToDeleteRows = false;
            this.gridexViewDetails.BackgroundColor = System.Drawing.Color.White;
            this.gridexViewDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridexViewDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridexViewDetails.ColumnHeadersHeight = 24;
            this.gridexViewDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeviceName,
            this.IPv4Byte1,
            this.IPv4Byte2,
            this.IPv4Byte3,
            this.IPv4Byte4});
            this.gridexViewDetails.Editable = true;
            this.gridexViewDetails.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.gridexViewDetails.Location = new System.Drawing.Point(6, 34);
            this.gridexViewDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridexViewDetails.Name = "gridexViewDetails";
            this.gridexViewDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridexViewDetails.RowHeadersVisible = false;
            this.gridexViewDetails.RowHeadersWidth = 15;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridexViewDetails.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridexViewDetails.RowTemplate.Height = 24;
            this.gridexViewDetails.Size = new System.Drawing.Size(359, 105);
            this.gridexViewDetails.TabIndex = 66;
            // 
            // DeviceName
            // 
            this.DeviceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeviceName.DataPropertyName = "DeviceName";
            this.DeviceName.FillWeight = 52F;
            this.DeviceName.HeaderText = "Device";
            this.DeviceName.Name = "DeviceName";
            this.DeviceName.ReadOnly = true;
            this.DeviceName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // IPv4Byte1
            // 
            this.IPv4Byte1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPv4Byte1.DataPropertyName = "IPv4Byte1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "N0";
            this.IPv4Byte1.DefaultCellStyle = dataGridViewCellStyle1;
            this.IPv4Byte1.FillWeight = 12F;
            this.IPv4Byte1.HeaderText = "P1";
            this.IPv4Byte1.Name = "IPv4Byte1";
            // 
            // IPv4Byte2
            // 
            this.IPv4Byte2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPv4Byte2.DataPropertyName = "IPv4Byte2";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N0";
            this.IPv4Byte2.DefaultCellStyle = dataGridViewCellStyle2;
            this.IPv4Byte2.FillWeight = 12F;
            this.IPv4Byte2.HeaderText = "P2";
            this.IPv4Byte2.Name = "IPv4Byte2";
            // 
            // IPv4Byte3
            // 
            this.IPv4Byte3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPv4Byte3.DataPropertyName = "IPv4Byte3";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "N0";
            this.IPv4Byte3.DefaultCellStyle = dataGridViewCellStyle3;
            this.IPv4Byte3.FillWeight = 12F;
            this.IPv4Byte3.HeaderText = "P3";
            this.IPv4Byte3.Name = "IPv4Byte3";
            // 
            // IPv4Byte4
            // 
            this.IPv4Byte4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPv4Byte4.DataPropertyName = "IPv4Byte4";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "N0";
            this.IPv4Byte4.DefaultCellStyle = dataGridViewCellStyle4;
            this.IPv4Byte4.FillWeight = 12F;
            this.IPv4Byte4.HeaderText = "P4";
            this.IPv4Byte4.Name = "IPv4Byte4";
            // 
            // fastFillingLineIndex
            // 
            this.fastFillingLineIndex.AllColumns.Add(this.olvID);
            this.fastFillingLineIndex.AllColumns.Add(this.olvBlank);
            this.fastFillingLineIndex.AllColumns.Add(this.olvFillingLineType);
            this.fastFillingLineIndex.AllColumns.Add(this.olvFillingLineCode);
            this.fastFillingLineIndex.AllColumns.Add(this.olvFillingLineName);
            this.fastFillingLineIndex.BackColor = System.Drawing.Color.White;
            this.fastFillingLineIndex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvBlank,
            this.olvFillingLineCode,
            this.olvFillingLineName});
            this.fastFillingLineIndex.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastFillingLineIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastFillingLineIndex.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastFillingLineIndex.FullRowSelect = true;
            this.fastFillingLineIndex.GroupImageList = this.imageList32;
            this.fastFillingLineIndex.HideSelection = false;
            this.fastFillingLineIndex.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastFillingLineIndex.Location = new System.Drawing.Point(0, 0);
            this.fastFillingLineIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastFillingLineIndex.Name = "fastFillingLineIndex";
            this.fastFillingLineIndex.OwnerDraw = true;
            this.fastFillingLineIndex.ShowGroups = false;
            this.fastFillingLineIndex.Size = new System.Drawing.Size(1500, 858);
            this.fastFillingLineIndex.TabIndex = 68;
            this.fastFillingLineIndex.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastFillingLineIndex.UseCompatibleStateImageBehavior = false;
            this.fastFillingLineIndex.UseFiltering = true;
            this.fastFillingLineIndex.View = System.Windows.Forms.View.Details;
            this.fastFillingLineIndex.VirtualMode = true;
            // 
            // olvID
            // 
            this.olvID.Text = "";
            this.olvID.Width = 0;
            // 
            // olvBlank
            // 
            this.olvBlank.Text = "";
            this.olvBlank.Width = 15;
            // 
            // olvFillingLineType
            // 
            this.olvFillingLineType.AspectName = "FillingLineType";
            this.olvFillingLineType.IsVisible = false;
            this.olvFillingLineType.Text = "Type";
            // 
            // olvFillingLineCode
            // 
            this.olvFillingLineCode.AspectName = "Code";
            this.olvFillingLineCode.Text = "Code";
            // 
            // olvFillingLineName
            // 
            this.olvFillingLineName.AspectName = "Name";
            this.olvFillingLineName.FillsFreeSpace = true;
            this.olvFillingLineName.Text = "Name";
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
            this.imageList32.Images.SetKeyName(8, "CustomerRed");
            this.imageList32.Images.SetKeyName(9, "price-tag-32");
            // 
            // toolBottom
            // 
            this.toolBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolBottom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripLabel1});
            this.toolBottom.Location = new System.Drawing.Point(0, 831);
            this.toolBottom.Name = "toolBottom";
            this.toolBottom.Size = new System.Drawing.Size(368, 27);
            this.toolBottom.TabIndex = 68;
            this.toolBottom.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::TotalSmartCoding.Properties.Resources.Warning;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(417, 20);
            this.toolStripLabel1.Text = "Please restart your software after change IP to take new effect";
            // 
            // FillingLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1868, 858);
            this.Controls.Add(this.fastFillingLineIndex);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.toolStripChildForm);
            this.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FillingLines";
            this.Text = "IP Settings";
            this.Controls.SetChildIndex(this.toolStripChildForm, 0);
            this.Controls.SetChildIndex(this.panelCenter, 0);
            this.Controls.SetChildIndex(this.fastFillingLineIndex, 0);
            this.toolStripChildForm.ResumeLayout(false);
            this.toolStripChildForm.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridexViewDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastFillingLineIndex)).EndInit();
            this.toolBottom.ResumeLayout(false);
            this.toolBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripChildForm;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private BrightIdeasSoftware.FastObjectListView fastFillingLineIndex;
        private BrightIdeasSoftware.OLVColumn olvID;
        private System.Windows.Forms.ImageList imageList32;
        private System.Windows.Forms.Panel panelCenter;
        private BrightIdeasSoftware.OLVColumn olvBlank;
        private CustomControls.DataGridexView gridexViewDetails;
        private BrightIdeasSoftware.OLVColumn olvFillingLineCode;
        private BrightIdeasSoftware.OLVColumn olvFillingLineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPv4Byte1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPv4Byte2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPv4Byte3;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPv4Byte4;
        private BrightIdeasSoftware.OLVColumn olvFillingLineType;
        private System.Windows.Forms.ToolStrip toolBottom;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;

    }
}