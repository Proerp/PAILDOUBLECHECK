namespace TotalSmartCoding.Views.Commons.Warehouses
{
    partial class Warehouses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Warehouses));
            this.toolStripChildForm = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.combexLocationID = new CustomControls.CombexBox();
            this.textexName = new CustomControls.TextexBox();
            this.textexRemarks = new CustomControls.TextexBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBookable = new System.Windows.Forms.CheckBox();
            this.checkIssuable = new System.Windows.Forms.CheckBox();
            this.checkIsDefault = new System.Windows.Forms.CheckBox();
            this.fastWarehouseIndex = new BrightIdeasSoftware.FastObjectListView();
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBlank = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvGlobalName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvWarehouseName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvRemarks = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripChildForm.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.layoutTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastWarehouseIndex)).BeginInit();
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
            this.toolStripButton2.Size = new System.Drawing.Size(102, 48);
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
            this.panelCenter.Controls.Add(this.layoutTop);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCenter.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelCenter.Location = new System.Drawing.Point(900, 0);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(368, 654);
            this.panelCenter.TabIndex = 76;
            // 
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.BackColor = System.Drawing.Color.Ivory;
            this.layoutTop.ColumnCount = 3;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.Controls.Add(this.label1, 1, 5);
            this.layoutTop.Controls.Add(this.combexLocationID, 1, 4);
            this.layoutTop.Controls.Add(this.textexName, 1, 2);
            this.layoutTop.Controls.Add(this.textexRemarks, 1, 6);
            this.layoutTop.Controls.Add(this.label14, 1, 1);
            this.layoutTop.Controls.Add(this.label12, 1, 3);
            this.layoutTop.Controls.Add(this.checkBookable, 1, 7);
            this.layoutTop.Controls.Add(this.checkIssuable, 1, 8);
            this.layoutTop.Controls.Add(this.checkIsDefault, 1, 9);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.RowCount = 11;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.Size = new System.Drawing.Size(368, 654);
            this.layoutTop.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(25, 122);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(318, 27);
            this.label1.TabIndex = 99;
            this.label1.Text = "Remarks";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // combexLocationID
            // 
            this.combexLocationID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexLocationID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexLocationID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexLocationID.Editable = true;
            this.combexLocationID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexLocationID.FormattingEnabled = true;
            this.combexLocationID.Location = new System.Drawing.Point(26, 96);
            this.combexLocationID.Margin = new System.Windows.Forms.Padding(1);
            this.combexLocationID.Name = "combexLocationID";
            this.combexLocationID.ReadOnly = false;
            this.combexLocationID.Size = new System.Drawing.Size(316, 25);
            this.combexLocationID.TabIndex = 98;
            // 
            // textexName
            // 
            this.textexName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexName.Editable = true;
            this.textexName.Location = new System.Drawing.Point(28, 43);
            this.textexName.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexName.Name = "textexName";
            this.textexName.Size = new System.Drawing.Size(314, 24);
            this.textexName.TabIndex = 86;
            // 
            // textexRemarks
            // 
            this.textexRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexRemarks.Editable = true;
            this.textexRemarks.Location = new System.Drawing.Point(28, 150);
            this.textexRemarks.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.textexRemarks.Name = "textexRemarks";
            this.textexRemarks.Size = new System.Drawing.Size(314, 24);
            this.textexRemarks.TabIndex = 88;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(25, 15);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label14.Size = new System.Drawing.Size(318, 27);
            this.label14.TabIndex = 97;
            this.label14.Text = "Name";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(25, 68);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label12.Size = new System.Drawing.Size(318, 27);
            this.label12.TabIndex = 94;
            this.label12.Text = "Location";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // checkBookable
            // 
            this.checkBookable.AutoSize = true;
            this.checkBookable.Location = new System.Drawing.Point(27, 178);
            this.checkBookable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 1);
            this.checkBookable.Name = "checkBookable";
            this.checkBookable.Size = new System.Drawing.Size(79, 21);
            this.checkBookable.TabIndex = 108;
            this.checkBookable.Text = "Bookable";
            this.checkBookable.UseVisualStyleBackColor = true;
            this.checkBookable.Visible = false;
            // 
            // checkIssuable
            // 
            this.checkIssuable.AutoSize = true;
            this.checkIssuable.Location = new System.Drawing.Point(27, 203);
            this.checkIssuable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 1);
            this.checkIssuable.Name = "checkIssuable";
            this.checkIssuable.Size = new System.Drawing.Size(72, 21);
            this.checkIssuable.TabIndex = 109;
            this.checkIssuable.Text = "Issuable";
            this.checkIssuable.UseVisualStyleBackColor = true;
            this.checkIssuable.Visible = false;
            // 
            // checkIsDefault
            // 
            this.checkIsDefault.AutoSize = true;
            this.checkIsDefault.Location = new System.Drawing.Point(27, 228);
            this.checkIsDefault.Margin = new System.Windows.Forms.Padding(2, 3, 2, 1);
            this.checkIsDefault.Name = "checkIsDefault";
            this.checkIsDefault.Size = new System.Drawing.Size(69, 21);
            this.checkIsDefault.TabIndex = 110;
            this.checkIsDefault.Text = "Default";
            this.checkIsDefault.UseVisualStyleBackColor = true;
            this.checkIsDefault.Visible = false;
            // 
            // fastWarehouseIndex
            // 
            this.fastWarehouseIndex.AllColumns.Add(this.olvID);
            this.fastWarehouseIndex.AllColumns.Add(this.olvBlank);
            this.fastWarehouseIndex.AllColumns.Add(this.olvGlobalName);
            this.fastWarehouseIndex.AllColumns.Add(this.olvWarehouseName);
            this.fastWarehouseIndex.AllColumns.Add(this.olvRemarks);
            this.fastWarehouseIndex.BackColor = System.Drawing.Color.Ivory;
            this.fastWarehouseIndex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvID,
            this.olvBlank,
            this.olvWarehouseName,
            this.olvRemarks});
            this.fastWarehouseIndex.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastWarehouseIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastWarehouseIndex.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.fastWarehouseIndex.FullRowSelect = true;
            this.fastWarehouseIndex.GroupImageList = this.imageList32;
            this.fastWarehouseIndex.HideSelection = false;
            this.fastWarehouseIndex.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastWarehouseIndex.Location = new System.Drawing.Point(0, 0);
            this.fastWarehouseIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastWarehouseIndex.Name = "fastWarehouseIndex";
            this.fastWarehouseIndex.OwnerDraw = true;
            this.fastWarehouseIndex.ShowGroups = false;
            this.fastWarehouseIndex.Size = new System.Drawing.Size(900, 654);
            this.fastWarehouseIndex.TabIndex = 68;
            this.fastWarehouseIndex.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastWarehouseIndex.UseCompatibleStateImageBehavior = false;
            this.fastWarehouseIndex.UseFiltering = true;
            this.fastWarehouseIndex.View = System.Windows.Forms.View.Details;
            this.fastWarehouseIndex.VirtualMode = true;
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
            // olvGlobalName
            // 
            this.olvGlobalName.AspectName = "LocationName";
            this.olvGlobalName.IsVisible = false;
            this.olvGlobalName.Text = "";
            // 
            // olvWarehouseName
            // 
            this.olvWarehouseName.AspectName = "Name";
            this.olvWarehouseName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvWarehouseName.Text = "Name";
            this.olvWarehouseName.Width = 360;
            // 
            // olvRemarks
            // 
            this.olvRemarks.AspectName = "Remarks";
            this.olvRemarks.FillsFreeSpace = true;
            this.olvRemarks.Text = "Remarks";
            this.olvRemarks.Width = 500;
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
            this.imageList32.Images.SetKeyName(9, "BinLocation-32");
            this.imageList32.Images.SetKeyName(10, "Warehouse");
            // 
            // Warehouses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 654);
            this.Controls.Add(this.fastWarehouseIndex);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.toolStripChildForm);
            this.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Warehouses";
            this.Text = "Warehouses";
            this.Controls.SetChildIndex(this.toolStripChildForm, 0);
            this.Controls.SetChildIndex(this.panelCenter, 0);
            this.Controls.SetChildIndex(this.fastWarehouseIndex, 0);
            this.toolStripChildForm.ResumeLayout(false);
            this.toolStripChildForm.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastWarehouseIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private System.Windows.Forms.ToolStrip toolStripChildForm;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private BrightIdeasSoftware.FastObjectListView fastWarehouseIndex;
        private BrightIdeasSoftware.OLVColumn olvID;
        private BrightIdeasSoftware.OLVColumn olvWarehouseName;
        private System.Windows.Forms.ImageList imageList32;
        private BrightIdeasSoftware.OLVColumn olvRemarks;
        private CustomControls.TextexBox textexName;
        private CustomControls.TextexBox textexRemarks;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panelCenter;
        private BrightIdeasSoftware.OLVColumn olvBlank;
        private System.Windows.Forms.Label label14;
        private BrightIdeasSoftware.OLVColumn olvGlobalName;
        private System.Windows.Forms.Label label1;
        private CustomControls.CombexBox combexLocationID;
        private System.Windows.Forms.CheckBox checkBookable;
        private System.Windows.Forms.CheckBox checkIssuable;
        private System.Windows.Forms.CheckBox checkIsDefault;

    }
}