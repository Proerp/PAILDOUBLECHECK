namespace TotalSmartCoding.Views.Productions
{
    partial class BatchSumups
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
            this.fastBatchSumups = new BrightIdeasSoftware.FastObjectListView();
            this.olvPalletSelected = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvEntryDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBatchCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCommodityCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCommodityOfficialCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCommodityName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCartonCounts = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvUniqueCartonCounts = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panelMaster = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelBottomLeft = new System.Windows.Forms.Panel();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.textexFilters = new System.Windows.Forms.ToolStripTextBox();
            this.buttonClearFilters = new System.Windows.Forms.ToolStripButton();
            this.dateTimexUpperFillterDate = new CustomControls.DateTimexPicker();
            this.dateTimexLowerFillterDate = new CustomControls.DateTimexPicker();
            ((System.ComponentModel.ISupportInitialize)(this.fastBatchSumups)).BeginInit();
            this.panelMaster.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelBottomLeft.SuspendLayout();
            this.layoutTop.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastBatchSumups
            // 
            this.fastBatchSumups.AllColumns.Add(this.olvPalletSelected);
            this.fastBatchSumups.AllColumns.Add(this.olvEntryDate);
            this.fastBatchSumups.AllColumns.Add(this.olvBatchCode);
            this.fastBatchSumups.AllColumns.Add(this.olvCommodityCode);
            this.fastBatchSumups.AllColumns.Add(this.olvCommodityOfficialCode);
            this.fastBatchSumups.AllColumns.Add(this.olvCommodityName);
            this.fastBatchSumups.AllColumns.Add(this.olvCartonCounts);
            this.fastBatchSumups.AllColumns.Add(this.olvUniqueCartonCounts);
            this.fastBatchSumups.CheckedAspectName = "";
            this.fastBatchSumups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvPalletSelected,
            this.olvEntryDate,
            this.olvBatchCode,
            this.olvCommodityCode,
            this.olvCommodityOfficialCode,
            this.olvCommodityName,
            this.olvCartonCounts,
            this.olvUniqueCartonCounts});
            this.fastBatchSumups.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastBatchSumups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastBatchSumups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastBatchSumups.FullRowSelect = true;
            this.fastBatchSumups.HideSelection = false;
            this.fastBatchSumups.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBatchSumups.Location = new System.Drawing.Point(0, 0);
            this.fastBatchSumups.Margin = new System.Windows.Forms.Padding(2);
            this.fastBatchSumups.Name = "fastBatchSumups";
            this.fastBatchSumups.OwnerDraw = true;
            this.fastBatchSumups.RowHeight = 36;
            this.fastBatchSumups.ShowGroups = false;
            this.fastBatchSumups.ShowImagesOnSubItems = true;
            this.fastBatchSumups.Size = new System.Drawing.Size(924, 445);
            this.fastBatchSumups.TabIndex = 69;
            this.fastBatchSumups.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBatchSumups.UseCompatibleStateImageBehavior = false;
            this.fastBatchSumups.UseFiltering = true;
            this.fastBatchSumups.View = System.Windows.Forms.View.Details;
            this.fastBatchSumups.VirtualMode = true;
            // 
            // olvPalletSelected
            // 
            this.olvPalletSelected.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletSelected.Sortable = false;
            this.olvPalletSelected.Text = "";
            this.olvPalletSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvPalletSelected.Width = 20;
            // 
            // olvEntryDate
            // 
            this.olvEntryDate.AspectName = "EntryDate";
            this.olvEntryDate.AspectToStringFormat = "{0:d}";
            this.olvEntryDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvEntryDate.Text = "Date";
            this.olvEntryDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvEntryDate.Width = 88;
            // 
            // olvBatchCode
            // 
            this.olvBatchCode.AspectName = "BatchCode";
            this.olvBatchCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBatchCode.Text = "Batch";
            this.olvBatchCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvBatchCode.Width = 96;
            // 
            // olvCommodityCode
            // 
            this.olvCommodityCode.AspectName = "CommodityCode";
            this.olvCommodityCode.Text = "Item";
            // 
            // olvCommodityOfficialCode
            // 
            this.olvCommodityOfficialCode.AspectName = "CommodityOfficialCode";
            this.olvCommodityOfficialCode.Text = "Code";
            this.olvCommodityOfficialCode.Width = 96;
            // 
            // olvCommodityName
            // 
            this.olvCommodityName.AspectName = "CommodityName";
            this.olvCommodityName.Text = "Description";
            this.olvCommodityName.Width = 290;
            // 
            // olvCartonCounts
            // 
            this.olvCartonCounts.AspectName = "CartonCounts";
            this.olvCartonCounts.AspectToStringFormat = "{0:#,#}";
            this.olvCartonCounts.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvCartonCounts.Text = "Include duplicate labels";
            this.olvCartonCounts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvCartonCounts.Width = 153;
            // 
            // olvUniqueCartonCounts
            // 
            this.olvUniqueCartonCounts.AspectName = "UniqueCartonCounts";
            this.olvUniqueCartonCounts.AspectToStringFormat = "{0:#,#}";
            this.olvUniqueCartonCounts.FillsFreeSpace = true;
            this.olvUniqueCartonCounts.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvUniqueCartonCounts.Text = "Unique labels";
            this.olvUniqueCartonCounts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvUniqueCartonCounts.Width = 90;
            // 
            // panelMaster
            // 
            this.panelMaster.BackColor = System.Drawing.Color.Ivory;
            this.panelMaster.Controls.Add(this.fastBatchSumups);
            this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaster.Location = new System.Drawing.Point(0, 45);
            this.panelMaster.Margin = new System.Windows.Forms.Padding(2);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Size = new System.Drawing.Size(924, 445);
            this.panelMaster.TabIndex = 71;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.panelBottomLeft);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBottom.Location = new System.Drawing.Point(0, 0);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(2);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(924, 45);
            this.panelBottom.TabIndex = 72;
            // 
            // panelBottomLeft
            // 
            this.panelBottomLeft.Controls.Add(this.layoutTop);
            this.panelBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.panelBottomLeft.Margin = new System.Windows.Forms.Padding(2);
            this.panelBottomLeft.Name = "panelBottomLeft";
            this.panelBottomLeft.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelBottomLeft.Size = new System.Drawing.Size(924, 45);
            this.panelBottomLeft.TabIndex = 1;
            // 
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.BackColor = System.Drawing.Color.Transparent;
            this.layoutTop.ColumnCount = 4;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.Controls.Add(this.toolStrip2, 0, 0);
            this.layoutTop.Controls.Add(this.dateTimexUpperFillterDate, 2, 0);
            this.layoutTop.Controls.Add(this.dateTimexLowerFillterDate, 3, 0);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Right;
            this.layoutTop.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutTop.Location = new System.Drawing.Point(306, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.layoutTop.RowCount = 1;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.layoutTop.Size = new System.Drawing.Size(618, 45);
            this.layoutTop.TabIndex = 94;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRefresh,
            this.textexFilters,
            this.buttonClearFilters});
            this.toolStrip2.Location = new System.Drawing.Point(240, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(378, 39);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonRefresh.Image = global::TotalSmartCoding.Properties.Resources.refresh;
            this.buttonRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(36, 36);
            this.buttonRefresh.Text = "Cập nhật số liệu mới nhất";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // textexFilters
            // 
            this.textexFilters.AutoSize = false;
            this.textexFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textexFilters.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textexFilters.Name = "textexFilters";
            this.textexFilters.Size = new System.Drawing.Size(270, 24);
            this.textexFilters.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textexFilters.TextChanged += new System.EventHandler(this.textexFilters_TextChanged);
            // 
            // buttonClearFilters
            // 
            this.buttonClearFilters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClearFilters.Image = global::TotalSmartCoding.Properties.Resources.Edit_clear;
            this.buttonClearFilters.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonClearFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClearFilters.Name = "buttonClearFilters";
            this.buttonClearFilters.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonClearFilters.Size = new System.Drawing.Size(36, 36);
            this.buttonClearFilters.Text = "Clear current filters";
            this.buttonClearFilters.Click += new System.EventHandler(this.buttonClearFilters_Click);
            // 
            // dateTimexUpperFillterDate
            // 
            this.dateTimexUpperFillterDate.CustomFormat = "dd/MMM/yyyy";
            this.dateTimexUpperFillterDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimexUpperFillterDate.Editable = true;
            this.dateTimexUpperFillterDate.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimexUpperFillterDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimexUpperFillterDate.Location = new System.Drawing.Point(122, 8);
            this.dateTimexUpperFillterDate.Margin = new System.Windows.Forms.Padding(0, 8, 1, 1);
            this.dateTimexUpperFillterDate.Name = "dateTimexUpperFillterDate";
            this.dateTimexUpperFillterDate.ReadOnly = false;
            this.dateTimexUpperFillterDate.Size = new System.Drawing.Size(118, 24);
            this.dateTimexUpperFillterDate.TabIndex = 91;
            // 
            // dateTimexLowerFillterDate
            // 
            this.dateTimexLowerFillterDate.CustomFormat = "dd/MMM/yyyy";
            this.dateTimexLowerFillterDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimexLowerFillterDate.Editable = true;
            this.dateTimexLowerFillterDate.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimexLowerFillterDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimexLowerFillterDate.Location = new System.Drawing.Point(3, 8);
            this.dateTimexLowerFillterDate.Margin = new System.Windows.Forms.Padding(0, 8, 3, 1);
            this.dateTimexLowerFillterDate.Name = "dateTimexLowerFillterDate";
            this.dateTimexLowerFillterDate.ReadOnly = false;
            this.dateTimexLowerFillterDate.Size = new System.Drawing.Size(118, 24);
            this.dateTimexLowerFillterDate.TabIndex = 90;
            // 
            // BatchSumups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 490);
            this.Controls.Add(this.panelMaster);
            this.Controls.Add(this.panelBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchSumups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Sum-Ups";
            this.Load += new System.EventHandler(this.BatchSumups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastBatchSumups)).EndInit();
            this.panelMaster.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottomLeft.ResumeLayout(false);
            this.panelBottomLeft.PerformLayout();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView fastBatchSumups;
        private System.Windows.Forms.Panel panelMaster;
        private BrightIdeasSoftware.OLVColumn olvCommodityCode;
        private BrightIdeasSoftware.OLVColumn olvPalletSelected;
        private BrightIdeasSoftware.OLVColumn olvCommodityName;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelBottomLeft;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.ToolStripTextBox textexFilters;
        private System.Windows.Forms.ToolStripButton buttonClearFilters;
        private BrightIdeasSoftware.OLVColumn olvCartonCounts;
        private BrightIdeasSoftware.OLVColumn olvUniqueCartonCounts;
        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private CustomControls.DateTimexPicker dateTimexLowerFillterDate;
        private CustomControls.DateTimexPicker dateTimexUpperFillterDate;
        private BrightIdeasSoftware.OLVColumn olvEntryDate;
        private BrightIdeasSoftware.OLVColumn olvBatchCode;
        private BrightIdeasSoftware.OLVColumn olvCommodityOfficialCode;
    }
}