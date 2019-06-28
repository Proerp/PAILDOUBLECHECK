namespace TotalSmartCoding.Views.Mains
{
    partial class UserGroups
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
            this.textexDescription = new CustomControls.TextexBox();
            this.textexCode = new CustomControls.TextexBox();
            this.labelNewLocationID = new System.Windows.Forms.Label();
            this.labelNewOrganizationalUnitID = new System.Windows.Forms.Label();
            this.labelOrganizationalUnitID = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textexName = new CustomControls.TextexBox();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 156);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(658, 39);
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
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.layoutTop.Controls.Add(this.textexDescription, 3, 3);
            this.layoutTop.Controls.Add(this.textexCode, 3, 1);
            this.layoutTop.Controls.Add(this.labelNewLocationID, 2, 1);
            this.layoutTop.Controls.Add(this.labelNewOrganizationalUnitID, 2, 2);
            this.layoutTop.Controls.Add(this.labelOrganizationalUnitID, 2, 3);
            this.layoutTop.Controls.Add(this.pictureBox2, 1, 1);
            this.layoutTop.Controls.Add(this.textexName, 3, 2);
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
            this.layoutTop.Size = new System.Drawing.Size(658, 156);
            this.layoutTop.TabIndex = 101;
            // 
            // textexDescription
            // 
            this.textexDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexDescription.Editable = false;
            this.textexDescription.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.textexDescription.Location = new System.Drawing.Point(212, 88);
            this.textexDescription.Margin = new System.Windows.Forms.Padding(1);
            this.textexDescription.Name = "textexDescription";
            this.textexDescription.Size = new System.Drawing.Size(427, 28);
            this.textexDescription.TabIndex = 3;
            // 
            // textexCode
            // 
            this.textexCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexCode.Editable = false;
            this.textexCode.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.textexCode.Location = new System.Drawing.Point(212, 28);
            this.textexCode.Margin = new System.Windows.Forms.Padding(1);
            this.textexCode.Name = "textexCode";
            this.textexCode.Size = new System.Drawing.Size(427, 28);
            this.textexCode.TabIndex = 1;
            // 
            // labelNewLocationID
            // 
            this.labelNewLocationID.AutoSize = true;
            this.labelNewLocationID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNewLocationID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewLocationID.Location = new System.Drawing.Point(71, 27);
            this.labelNewLocationID.Name = "labelNewLocationID";
            this.labelNewLocationID.Size = new System.Drawing.Size(137, 30);
            this.labelNewLocationID.TabIndex = 90;
            this.labelNewLocationID.Text = "Code";
            this.labelNewLocationID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNewOrganizationalUnitID
            // 
            this.labelNewOrganizationalUnitID.AutoSize = true;
            this.labelNewOrganizationalUnitID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNewOrganizationalUnitID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewOrganizationalUnitID.Location = new System.Drawing.Point(71, 57);
            this.labelNewOrganizationalUnitID.Name = "labelNewOrganizationalUnitID";
            this.labelNewOrganizationalUnitID.Size = new System.Drawing.Size(137, 30);
            this.labelNewOrganizationalUnitID.TabIndex = 78;
            this.labelNewOrganizationalUnitID.Text = "Name";
            this.labelNewOrganizationalUnitID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOrganizationalUnitID
            // 
            this.labelOrganizationalUnitID.AutoSize = true;
            this.labelOrganizationalUnitID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOrganizationalUnitID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrganizationalUnitID.Location = new System.Drawing.Point(71, 87);
            this.labelOrganizationalUnitID.Name = "labelOrganizationalUnitID";
            this.labelOrganizationalUnitID.Size = new System.Drawing.Size(137, 30);
            this.labelOrganizationalUnitID.TabIndex = 83;
            this.labelOrganizationalUnitID.Text = "Description";
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
            // textexName
            // 
            this.textexName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexName.Editable = false;
            this.textexName.Font = new System.Drawing.Font("Calibri Light", 10.2F);
            this.textexName.Location = new System.Drawing.Point(212, 58);
            this.textexName.Margin = new System.Windows.Forms.Padding(1);
            this.textexName.Name = "textexName";
            this.textexName.Size = new System.Drawing.Size(427, 28);
            this.textexName.TabIndex = 2;
            // 
            // UserGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 195);
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add new group";
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
        private System.Windows.Forms.Label labelOrganizationalUnitID;
        private System.Windows.Forms.PictureBox pictureBox2;
        private CustomControls.TextexBox textexName;
        private System.Windows.Forms.Label labelNewLocationID;
        private CustomControls.TextexBox textexDescription;
        private CustomControls.TextexBox textexCode;
    }
}