namespace TotalSmartCoding.Views.Mains
{
    partial class UserOUs
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonESC = new System.Windows.Forms.ToolStripButton();
            this.buttonOK = new System.Windows.Forms.ToolStripButton();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.labelNewLocationID = new System.Windows.Forms.Label();
            this.combexNewLocationID = new CustomControls.CombexBox();
            this.labelNewOrganizationalUnitID = new System.Windows.Forms.Label();
            this.combexOrganizationalUnitID = new CustomControls.CombexBox();
            this.labelOrganizationalUnitID = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textexNewOrganizationalUnitID = new CustomControls.TextexBox();
            this.toolStrip1.SuspendLayout();
            this.layoutTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 121);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(699, 39);
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
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.ColumnCount = 5;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.layoutTop.Controls.Add(this.labelNewLocationID, 2, 1);
            this.layoutTop.Controls.Add(this.combexNewLocationID, 3, 1);
            this.layoutTop.Controls.Add(this.labelNewOrganizationalUnitID, 2, 2);
            this.layoutTop.Controls.Add(this.combexOrganizationalUnitID, 3, 3);
            this.layoutTop.Controls.Add(this.labelOrganizationalUnitID, 2, 3);
            this.layoutTop.Controls.Add(this.pictureBox2, 1, 1);
            this.layoutTop.Controls.Add(this.textexNewOrganizationalUnitID, 3, 2);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.layoutTop.RowCount = 5;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.Size = new System.Drawing.Size(699, 121);
            this.layoutTop.TabIndex = 101;
            // 
            // labelNewLocationID
            // 
            this.labelNewLocationID.AutoSize = true;
            this.labelNewLocationID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNewLocationID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewLocationID.Location = new System.Drawing.Point(71, 27);
            this.labelNewLocationID.Name = "labelNewLocationID";
            this.labelNewLocationID.Size = new System.Drawing.Size(208, 31);
            this.labelNewLocationID.TabIndex = 90;
            this.labelNewLocationID.Text = "Location";
            this.labelNewLocationID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combexNewLocationID
            // 
            this.combexNewLocationID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexNewLocationID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexNewLocationID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexNewLocationID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combexNewLocationID.Editable = true;
            this.combexNewLocationID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexNewLocationID.FormattingEnabled = true;
            this.combexNewLocationID.Location = new System.Drawing.Point(283, 28);
            this.combexNewLocationID.Margin = new System.Windows.Forms.Padding(1);
            this.combexNewLocationID.Name = "combexNewLocationID";
            this.combexNewLocationID.ReadOnly = false;
            this.combexNewLocationID.Size = new System.Drawing.Size(397, 29);
            this.combexNewLocationID.TabIndex = 89;
            // 
            // labelNewOrganizationalUnitID
            // 
            this.labelNewOrganizationalUnitID.AutoSize = true;
            this.labelNewOrganizationalUnitID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNewOrganizationalUnitID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewOrganizationalUnitID.Location = new System.Drawing.Point(71, 58);
            this.labelNewOrganizationalUnitID.Name = "labelNewOrganizationalUnitID";
            this.labelNewOrganizationalUnitID.Size = new System.Drawing.Size(208, 30);
            this.labelNewOrganizationalUnitID.TabIndex = 78;
            this.labelNewOrganizationalUnitID.Text = "New Organizational Unit";
            this.labelNewOrganizationalUnitID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combexOrganizationalUnitID
            // 
            this.combexOrganizationalUnitID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexOrganizationalUnitID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexOrganizationalUnitID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexOrganizationalUnitID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combexOrganizationalUnitID.Editable = true;
            this.combexOrganizationalUnitID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexOrganizationalUnitID.FormattingEnabled = true;
            this.combexOrganizationalUnitID.Location = new System.Drawing.Point(283, 89);
            this.combexOrganizationalUnitID.Margin = new System.Windows.Forms.Padding(1);
            this.combexOrganizationalUnitID.Name = "combexOrganizationalUnitID";
            this.combexOrganizationalUnitID.ReadOnly = false;
            this.combexOrganizationalUnitID.Size = new System.Drawing.Size(397, 29);
            this.combexOrganizationalUnitID.TabIndex = 2;
            // 
            // labelOrganizationalUnitID
            // 
            this.labelOrganizationalUnitID.AutoSize = true;
            this.labelOrganizationalUnitID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOrganizationalUnitID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrganizationalUnitID.Location = new System.Drawing.Point(71, 88);
            this.labelOrganizationalUnitID.Name = "labelOrganizationalUnitID";
            this.labelOrganizationalUnitID.Size = new System.Drawing.Size(208, 31);
            this.labelOrganizationalUnitID.TabIndex = 83;
            this.labelOrganizationalUnitID.Text = "Organizational Unit";
            this.labelOrganizationalUnitID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TotalSmartCoding.Properties.Resources.hierarchical_structure;
            this.pictureBox2.Location = new System.Drawing.Point(33, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.layoutTop.SetRowSpan(this.pictureBox2, 3);
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 75;
            this.pictureBox2.TabStop = false;
            // 
            // textexNewOrganizationalUnitID
            // 
            this.textexNewOrganizationalUnitID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexNewOrganizationalUnitID.Editable = false;
            this.textexNewOrganizationalUnitID.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.textexNewOrganizationalUnitID.Location = new System.Drawing.Point(283, 59);
            this.textexNewOrganizationalUnitID.Margin = new System.Windows.Forms.Padding(1);
            this.textexNewOrganizationalUnitID.Name = "textexNewOrganizationalUnitID";
            this.textexNewOrganizationalUnitID.Size = new System.Drawing.Size(397, 28);
            this.textexNewOrganizationalUnitID.TabIndex = 88;
            // 
            // UserOUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 160);
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserOUs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add new organizational unit";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonESC;
        private System.Windows.Forms.ToolStripButton buttonOK;
        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private System.Windows.Forms.Label labelNewOrganizationalUnitID;
        private CustomControls.CombexBox combexOrganizationalUnitID;
        private System.Windows.Forms.Label labelOrganizationalUnitID;
        private System.Windows.Forms.PictureBox pictureBox2;
        private CustomControls.TextexBox textexNewOrganizationalUnitID;
        private System.Windows.Forms.Label labelNewLocationID;
        private CustomControls.CombexBox combexNewLocationID;
    }
}