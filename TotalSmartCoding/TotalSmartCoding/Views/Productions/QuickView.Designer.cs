namespace TotalSmartCoding.Views.Productions
{
    partial class QuickView
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
            this.fastBarcodes = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvLabel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCheckedOut = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvRemarks = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.textFilter = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureTesaLabel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fastBarcodes)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTesaLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // fastBarcodes
            // 
            this.fastBarcodes.AllColumns.Add(this.olvColumn1);
            this.fastBarcodes.AllColumns.Add(this.olvCode);
            this.fastBarcodes.AllColumns.Add(this.olvLabel);
            this.fastBarcodes.AllColumns.Add(this.olvCheckedOut);
            this.fastBarcodes.AllColumns.Add(this.olvRemarks);
            this.fastBarcodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvCode,
            this.olvLabel,
            this.olvCheckedOut,
            this.olvRemarks});
            this.fastBarcodes.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastBarcodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastBarcodes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastBarcodes.FullRowSelect = true;
            this.fastBarcodes.HideSelection = false;
            this.fastBarcodes.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBarcodes.Location = new System.Drawing.Point(73, 23);
            this.fastBarcodes.Margin = new System.Windows.Forms.Padding(2);
            this.fastBarcodes.Name = "fastBarcodes";
            this.fastBarcodes.OwnerDraw = true;
            this.fastBarcodes.ShowGroups = false;
            this.fastBarcodes.Size = new System.Drawing.Size(633, 658);
            this.fastBarcodes.TabIndex = 71;
            this.fastBarcodes.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.fastBarcodes.UseCompatibleStateImageBehavior = false;
            this.fastBarcodes.UseFiltering = true;
            this.fastBarcodes.View = System.Windows.Forms.View.Details;
            this.fastBarcodes.VirtualMode = true;
            this.fastBarcodes.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastBarcodes_FormatRow);
            this.fastBarcodes.DoubleClick += new System.EventHandler(this.pictureTesaLabel_Click);
            this.fastBarcodes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fastBarcodes_MouseDown);
            // 
            // olvColumn1
            // 
            this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Text = "";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Width = 35;
            // 
            // olvCode
            // 
            this.olvCode.AspectName = "Code";
            this.olvCode.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCode.Text = "Barcode";
            this.olvCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCode.Width = 260;
            // 
            // olvLabel
            // 
            this.olvLabel.AspectName = "Label";
            this.olvLabel.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvLabel.Text = "Tesa Label";
            this.olvLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvLabel.Width = 108;
            // 
            // olvCheckedOut
            // 
            this.olvCheckedOut.AspectName = "DoubleChecked";
            this.olvCheckedOut.CheckBoxes = true;
            this.olvCheckedOut.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvCheckedOut.Text = "Checked";
            this.olvCheckedOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvRemarks
            // 
            this.olvRemarks.AspectName = "Remarks";
            this.olvRemarks.FillsFreeSpace = true;
            this.olvRemarks.Text = "Remarks";
            this.olvRemarks.Width = 108;
            // 
            // textFilter
            // 
            this.textFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.textFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFilter.Location = new System.Drawing.Point(73, 0);
            this.textFilter.Margin = new System.Windows.Forms.Padding(2);
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(633, 23);
            this.textFilter.TabIndex = 72;
            this.textFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textFilter.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureTesaLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(73, 681);
            this.panel1.TabIndex = 73;
            // 
            // pictureTesaLabel
            // 
            this.pictureTesaLabel.Image = global::TotalSmartCoding.Properties.Resources.cloud_Download_Blue;
            this.pictureTesaLabel.Location = new System.Drawing.Point(16, 22);
            this.pictureTesaLabel.Margin = new System.Windows.Forms.Padding(2);
            this.pictureTesaLabel.Name = "pictureTesaLabel";
            this.pictureTesaLabel.Size = new System.Drawing.Size(32, 32);
            this.pictureTesaLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureTesaLabel.TabIndex = 7;
            this.pictureTesaLabel.TabStop = false;
            this.pictureTesaLabel.Click += new System.EventHandler(this.pictureTesaLabel_Click);
            // 
            // QuickView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 681);
            this.Controls.Add(this.fastBarcodes);
            this.Controls.Add(this.textFilter);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick View";
            ((System.ComponentModel.ISupportInitialize)(this.fastBarcodes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTesaLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView fastBarcodes;
        private BrightIdeasSoftware.OLVColumn olvCode;
        private System.Windows.Forms.TextBox textFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureTesaLabel;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvLabel;
        private BrightIdeasSoftware.OLVColumn olvRemarks;
        private BrightIdeasSoftware.OLVColumn olvCheckedOut;
    }
}