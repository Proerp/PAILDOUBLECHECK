namespace TotalSmartCoding.Views.Generals
{
    partial class WebapiGettsa
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
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelBottomRight = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonExit = new System.Windows.Forms.ToolStripButton();
            this.buttonUpdate = new System.Windows.Forms.ToolStripButton();
            this.panelBottomLeft = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.buttonApplicationRoleIgnored = new System.Windows.Forms.ToolStripButton();
            this.buttonApplicationRoleRequired = new System.Windows.Forms.ToolStripButton();
            this.layoutTop = new System.Windows.Forms.TableLayoutPanel();
            this.textexBatch_serial = new CustomControls.TextexBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textexBatch_number = new CustomControls.TextexBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textexProduct_id = new CustomControls.TextexBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textexProduction_line = new CustomControls.TextexBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textexProduction_date = new CustomControls.TextexBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textexDomino_code = new CustomControls.TextexBox();
            this.labelConsumerKey = new System.Windows.Forms.Label();
            this.labelApplicationRoleName = new System.Windows.Forms.Label();
            this.labelConsumerSecret = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.textexLabel = new CustomControls.TextexBox();
            this.textexValid = new CustomControls.TextexBox();
            this.labelMainStatus = new System.Windows.Forms.Label();
            this.panelBottom.SuspendLayout();
            this.panelBottomRight.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelBottomLeft.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.layoutTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.panelBottomRight);
            this.panelBottom.Controls.Add(this.panelBottomLeft);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 427);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(422, 42);
            this.panelBottom.TabIndex = 75;
            // 
            // panelBottomRight
            // 
            this.panelBottomRight.Controls.Add(this.toolStrip1);
            this.panelBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomRight.Location = new System.Drawing.Point(143, 0);
            this.panelBottomRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottomRight.Name = "panelBottomRight";
            this.panelBottomRight.Size = new System.Drawing.Size(279, 42);
            this.panelBottomRight.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonExit,
            this.buttonUpdate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(279, 42);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonExit
            // 
            this.buttonExit.Image = global::TotalSmartCoding.Properties.Resources.signout_icon_24;
            this.buttonExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonExit.Size = new System.Drawing.Size(64, 39);
            this.buttonExit.Text = "Close";
            this.buttonExit.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Enabled = false;
            this.buttonUpdate.Image = global::TotalSmartCoding.Properties.Resources.LoginApplicationRole;
            this.buttonUpdate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonUpdate.Size = new System.Drawing.Size(81, 39);
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.Visible = false;
            this.buttonUpdate.Click += new System.EventHandler(this.button_Click);
            // 
            // panelBottomLeft
            // 
            this.panelBottomLeft.Controls.Add(this.toolStrip2);
            this.panelBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.panelBottomLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottomLeft.Name = "panelBottomLeft";
            this.panelBottomLeft.Size = new System.Drawing.Size(143, 42);
            this.panelBottomLeft.TabIndex = 1;
            this.panelBottomLeft.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonApplicationRoleIgnored,
            this.buttonApplicationRoleRequired});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(143, 42);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // buttonApplicationRoleIgnored
            // 
            this.buttonApplicationRoleIgnored.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonApplicationRoleIgnored.Image = global::TotalSmartCoding.Properties.Resources.Ignore_x_20;
            this.buttonApplicationRoleIgnored.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonApplicationRoleIgnored.Name = "buttonApplicationRoleIgnored";
            this.buttonApplicationRoleIgnored.Size = new System.Drawing.Size(24, 39);
            this.buttonApplicationRoleIgnored.Text = "Ignore application role";
            this.buttonApplicationRoleIgnored.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonApplicationRoleRequired
            // 
            this.buttonApplicationRoleRequired.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonApplicationRoleRequired.Image = global::TotalSmartCoding.Properties.Resources.Continue_x_20;
            this.buttonApplicationRoleRequired.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonApplicationRoleRequired.Name = "buttonApplicationRoleRequired";
            this.buttonApplicationRoleRequired.Size = new System.Drawing.Size(24, 39);
            this.buttonApplicationRoleRequired.Text = "Use application role";
            this.buttonApplicationRoleRequired.Click += new System.EventHandler(this.button_Click);
            // 
            // layoutTop
            // 
            this.layoutTop.AutoSize = true;
            this.layoutTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTop.BackColor = System.Drawing.SystemColors.Control;
            this.layoutTop.ColumnCount = 5;
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.Controls.Add(this.labelMainStatus, 3, 17);
            this.layoutTop.Controls.Add(this.textexBatch_serial, 3, 12);
            this.layoutTop.Controls.Add(this.label5, 3, 11);
            this.layoutTop.Controls.Add(this.textexBatch_number, 3, 10);
            this.layoutTop.Controls.Add(this.label4, 3, 9);
            this.layoutTop.Controls.Add(this.textexProduct_id, 3, 8);
            this.layoutTop.Controls.Add(this.label3, 3, 7);
            this.layoutTop.Controls.Add(this.textexProduction_line, 3, 6);
            this.layoutTop.Controls.Add(this.label2, 3, 5);
            this.layoutTop.Controls.Add(this.textexProduction_date, 3, 4);
            this.layoutTop.Controls.Add(this.label1, 3, 3);
            this.layoutTop.Controls.Add(this.textexDomino_code, 3, 14);
            this.layoutTop.Controls.Add(this.labelConsumerKey, 3, 13);
            this.layoutTop.Controls.Add(this.labelApplicationRoleName, 3, 1);
            this.layoutTop.Controls.Add(this.labelConsumerSecret, 3, 15);
            this.layoutTop.Controls.Add(this.pictureBoxIcon, 1, 1);
            this.layoutTop.Controls.Add(this.textexLabel, 3, 2);
            this.layoutTop.Controls.Add(this.textexValid, 3, 16);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.layoutTop.RowCount = 18;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
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
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutTop.Size = new System.Drawing.Size(422, 427);
            this.layoutTop.TabIndex = 104;
            // 
            // textexBatch_serial
            // 
            this.textexBatch_serial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexBatch_serial.Editable = false;
            this.textexBatch_serial.Location = new System.Drawing.Point(96, 243);
            this.textexBatch_serial.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexBatch_serial.Name = "textexBatch_serial";
            this.textexBatch_serial.ReadOnly = true;
            this.textexBatch_serial.Size = new System.Drawing.Size(305, 23);
            this.textexBatch_serial.TabIndex = 101;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(92, 227);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(310, 15);
            this.label5.TabIndex = 100;
            this.label5.Text = "Serial Number";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textexBatch_number
            // 
            this.textexBatch_number.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexBatch_number.Editable = false;
            this.textexBatch_number.Location = new System.Drawing.Point(96, 203);
            this.textexBatch_number.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexBatch_number.Name = "textexBatch_number";
            this.textexBatch_number.ReadOnly = true;
            this.textexBatch_number.Size = new System.Drawing.Size(305, 23);
            this.textexBatch_number.TabIndex = 99;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(92, 187);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 15);
            this.label4.TabIndex = 98;
            this.label4.Text = "Batch Number";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textexProduct_id
            // 
            this.textexProduct_id.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexProduct_id.Editable = false;
            this.textexProduct_id.Location = new System.Drawing.Point(96, 163);
            this.textexProduct_id.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexProduct_id.Name = "textexProduct_id";
            this.textexProduct_id.ReadOnly = true;
            this.textexProduct_id.Size = new System.Drawing.Size(305, 23);
            this.textexProduct_id.TabIndex = 97;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(92, 147);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(310, 15);
            this.label3.TabIndex = 96;
            this.label3.Text = "SKU Code";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textexProduction_line
            // 
            this.textexProduction_line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexProduction_line.Editable = false;
            this.textexProduction_line.Location = new System.Drawing.Point(96, 123);
            this.textexProduction_line.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexProduction_line.Name = "textexProduction_line";
            this.textexProduction_line.ReadOnly = true;
            this.textexProduction_line.Size = new System.Drawing.Size(305, 23);
            this.textexProduction_line.TabIndex = 95;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(92, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(310, 15);
            this.label2.TabIndex = 94;
            this.label2.Text = "Production Line";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textexProduction_date
            // 
            this.textexProduction_date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexProduction_date.Editable = false;
            this.textexProduction_date.Location = new System.Drawing.Point(96, 83);
            this.textexProduction_date.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexProduction_date.Name = "textexProduction_date";
            this.textexProduction_date.ReadOnly = true;
            this.textexProduction_date.Size = new System.Drawing.Size(305, 23);
            this.textexProduction_date.TabIndex = 93;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(92, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 15);
            this.label1.TabIndex = 92;
            this.label1.Text = "Production Date";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textexDomino_code
            // 
            this.textexDomino_code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexDomino_code.Editable = false;
            this.textexDomino_code.Location = new System.Drawing.Point(96, 283);
            this.textexDomino_code.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexDomino_code.Name = "textexDomino_code";
            this.textexDomino_code.ReadOnly = true;
            this.textexDomino_code.Size = new System.Drawing.Size(305, 23);
            this.textexDomino_code.TabIndex = 91;
            // 
            // labelConsumerKey
            // 
            this.labelConsumerKey.AutoSize = true;
            this.labelConsumerKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelConsumerKey.Location = new System.Drawing.Point(92, 267);
            this.labelConsumerKey.Margin = new System.Windows.Forms.Padding(0);
            this.labelConsumerKey.Name = "labelConsumerKey";
            this.labelConsumerKey.Size = new System.Drawing.Size(310, 15);
            this.labelConsumerKey.TabIndex = 90;
            this.labelConsumerKey.Text = "Domino Barcode";
            this.labelConsumerKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelApplicationRoleName
            // 
            this.labelApplicationRoleName.AutoSize = true;
            this.labelApplicationRoleName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelApplicationRoleName.Location = new System.Drawing.Point(92, 27);
            this.labelApplicationRoleName.Margin = new System.Windows.Forms.Padding(0);
            this.labelApplicationRoleName.Name = "labelApplicationRoleName";
            this.labelApplicationRoleName.Size = new System.Drawing.Size(310, 15);
            this.labelApplicationRoleName.TabIndex = 78;
            this.labelApplicationRoleName.Text = "Tesa Label";
            this.labelApplicationRoleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelConsumerSecret
            // 
            this.labelConsumerSecret.AutoSize = true;
            this.labelConsumerSecret.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelConsumerSecret.Location = new System.Drawing.Point(92, 317);
            this.labelConsumerSecret.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.labelConsumerSecret.Name = "labelConsumerSecret";
            this.labelConsumerSecret.Size = new System.Drawing.Size(310, 15);
            this.labelConsumerSecret.TabIndex = 83;
            this.labelConsumerSecret.Text = "Label Status";
            this.labelConsumerSecret.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = global::TotalSmartCoding.Properties.Resources.Barcode_green_32;
            this.pictureBoxIcon.Location = new System.Drawing.Point(35, 27);
            this.pictureBoxIcon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.layoutTop.SetRowSpan(this.pictureBoxIcon, 16);
            this.pictureBoxIcon.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxIcon.TabIndex = 11;
            this.pictureBoxIcon.TabStop = false;
            // 
            // textexLabel
            // 
            this.textexLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexLabel.Editable = false;
            this.textexLabel.Location = new System.Drawing.Point(96, 43);
            this.textexLabel.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexLabel.Name = "textexLabel";
            this.textexLabel.ReadOnly = true;
            this.textexLabel.Size = new System.Drawing.Size(305, 23);
            this.textexLabel.TabIndex = 88;
            // 
            // textexValid
            // 
            this.textexValid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexValid.Editable = false;
            this.textexValid.Location = new System.Drawing.Point(96, 333);
            this.textexValid.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexValid.Name = "textexValid";
            this.textexValid.ReadOnly = true;
            this.textexValid.Size = new System.Drawing.Size(305, 23);
            this.textexValid.TabIndex = 89;
            // 
            // labelMainStatus
            // 
            this.labelMainStatus.AutoSize = true;
            this.labelMainStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMainStatus.Location = new System.Drawing.Point(92, 367);
            this.labelMainStatus.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.labelMainStatus.Name = "labelMainStatus";
            this.labelMainStatus.Size = new System.Drawing.Size(310, 58);
            this.labelMainStatus.TabIndex = 102;
            // 
            // WebapiGettsa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 469);
            this.ControlBox = false;
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.panelBottom);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "WebapiGettsa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get Tesa Label";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebapiGettsa_FormClosing);
            this.Shown += new System.EventHandler(this.WebapiGettsa_Shown);
            this.panelBottom.ResumeLayout(false);
            this.panelBottomRight.ResumeLayout(false);
            this.panelBottomRight.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelBottomLeft.ResumeLayout(false);
            this.panelBottomLeft.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.layoutTop.ResumeLayout(false);
            this.layoutTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelBottomRight;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonExit;
        private System.Windows.Forms.ToolStripButton buttonUpdate;
        private System.Windows.Forms.Panel panelBottomLeft;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.TableLayoutPanel layoutTop;
        private System.Windows.Forms.Label labelApplicationRoleName;
        private System.Windows.Forms.Label labelConsumerSecret;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private CustomControls.TextexBox textexLabel;
        private CustomControls.TextexBox textexValid;
        private System.Windows.Forms.ToolStripButton buttonApplicationRoleRequired;
        private System.Windows.Forms.ToolStripButton buttonApplicationRoleIgnored;
        private CustomControls.TextexBox textexDomino_code;
        private System.Windows.Forms.Label labelConsumerKey;
        private CustomControls.TextexBox textexBatch_serial;
        private System.Windows.Forms.Label label5;
        private CustomControls.TextexBox textexBatch_number;
        private System.Windows.Forms.Label label4;
        private CustomControls.TextexBox textexProduct_id;
        private System.Windows.Forms.Label label3;
        private CustomControls.TextexBox textexProduction_line;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextexBox textexProduction_date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMainStatus;
    }
}