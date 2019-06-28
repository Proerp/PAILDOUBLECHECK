namespace TotalSmartCoding.Views.Mains
{
    partial class Webapi
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
            this.textexConsumerKey = new CustomControls.TextexBox();
            this.labelConsumerKey = new System.Windows.Forms.Label();
            this.labelApplicationRoleName = new System.Windows.Forms.Label();
            this.labelConsumerSecret = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.textexBaseUri = new CustomControls.TextexBox();
            this.textexConsumerSecret = new CustomControls.TextexBox();
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
            this.panelBottom.Location = new System.Drawing.Point(0, 187);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(642, 42);
            this.panelBottom.TabIndex = 75;
            // 
            // panelBottomRight
            // 
            this.panelBottomRight.Controls.Add(this.toolStrip1);
            this.panelBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomRight.Location = new System.Drawing.Point(143, 0);
            this.panelBottomRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBottomRight.Name = "panelBottomRight";
            this.panelBottomRight.Size = new System.Drawing.Size(499, 42);
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
            this.toolStrip1.Size = new System.Drawing.Size(499, 42);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonExit
            // 
            this.buttonExit.Image = global::TotalSmartCoding.Properties.Resources.LogoutApp;
            this.buttonExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonExit.Size = new System.Drawing.Size(61, 39);
            this.buttonExit.Text = "Exit";
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
            this.layoutTop.Controls.Add(this.textexConsumerKey, 3, 4);
            this.layoutTop.Controls.Add(this.labelConsumerKey, 3, 3);
            this.layoutTop.Controls.Add(this.labelApplicationRoleName, 3, 1);
            this.layoutTop.Controls.Add(this.labelConsumerSecret, 3, 5);
            this.layoutTop.Controls.Add(this.pictureBoxIcon, 1, 1);
            this.layoutTop.Controls.Add(this.textexBaseUri, 3, 2);
            this.layoutTop.Controls.Add(this.textexConsumerSecret, 3, 6);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.layoutTop.RowCount = 8;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutTop.Size = new System.Drawing.Size(642, 187);
            this.layoutTop.TabIndex = 104;
            // 
            // textexConsumerKey
            // 
            this.textexConsumerKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexConsumerKey.Editable = false;
            this.textexConsumerKey.Location = new System.Drawing.Point(112, 83);
            this.textexConsumerKey.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexConsumerKey.Name = "textexConsumerKey";
            this.textexConsumerKey.PasswordChar = '*';
            this.textexConsumerKey.Size = new System.Drawing.Size(509, 23);
            this.textexConsumerKey.TabIndex = 91;
            this.textexConsumerKey.TextChanged += new System.EventHandler(this.textexWebapi_TextChanged);
            // 
            // labelConsumerKey
            // 
            this.labelConsumerKey.AutoSize = true;
            this.labelConsumerKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelConsumerKey.Location = new System.Drawing.Point(108, 67);
            this.labelConsumerKey.Margin = new System.Windows.Forms.Padding(0);
            this.labelConsumerKey.Name = "labelConsumerKey";
            this.labelConsumerKey.Size = new System.Drawing.Size(514, 15);
            this.labelConsumerKey.TabIndex = 90;
            this.labelConsumerKey.Text = "Consumer Key";
            this.labelConsumerKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelApplicationRoleName
            // 
            this.labelApplicationRoleName.AutoSize = true;
            this.labelApplicationRoleName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelApplicationRoleName.Location = new System.Drawing.Point(108, 27);
            this.labelApplicationRoleName.Margin = new System.Windows.Forms.Padding(0);
            this.labelApplicationRoleName.Name = "labelApplicationRoleName";
            this.labelApplicationRoleName.Size = new System.Drawing.Size(514, 15);
            this.labelApplicationRoleName.TabIndex = 78;
            this.labelApplicationRoleName.Text = "Base URI";
            this.labelApplicationRoleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelConsumerSecret
            // 
            this.labelConsumerSecret.AutoSize = true;
            this.labelConsumerSecret.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelConsumerSecret.Location = new System.Drawing.Point(108, 117);
            this.labelConsumerSecret.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.labelConsumerSecret.Name = "labelConsumerSecret";
            this.labelConsumerSecret.Size = new System.Drawing.Size(514, 15);
            this.labelConsumerSecret.TabIndex = 83;
            this.labelConsumerSecret.Text = "Consumer Secret";
            this.labelConsumerSecret.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = global::TotalSmartCoding.Properties.Resources.if_key_48;
            this.pictureBoxIcon.Location = new System.Drawing.Point(35, 27);
            this.pictureBoxIcon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.layoutTop.SetRowSpan(this.pictureBoxIcon, 6);
            this.pictureBoxIcon.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxIcon.TabIndex = 11;
            this.pictureBoxIcon.TabStop = false;
            // 
            // textexBaseUri
            // 
            this.textexBaseUri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexBaseUri.Editable = false;
            this.textexBaseUri.Location = new System.Drawing.Point(112, 43);
            this.textexBaseUri.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexBaseUri.Name = "textexBaseUri";
            this.textexBaseUri.Size = new System.Drawing.Size(509, 23);
            this.textexBaseUri.TabIndex = 88;
            this.textexBaseUri.TextChanged += new System.EventHandler(this.textexWebapi_TextChanged);
            // 
            // textexConsumerSecret
            // 
            this.textexConsumerSecret.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textexConsumerSecret.Editable = false;
            this.textexConsumerSecret.Location = new System.Drawing.Point(112, 133);
            this.textexConsumerSecret.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.textexConsumerSecret.Name = "textexConsumerSecret";
            this.textexConsumerSecret.PasswordChar = '*';
            this.textexConsumerSecret.Size = new System.Drawing.Size(509, 23);
            this.textexConsumerSecret.TabIndex = 89;
            this.textexConsumerSecret.TextChanged += new System.EventHandler(this.textexWebapi_TextChanged);
            // 
            // Webapi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 229);
            this.ControlBox = false;
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.panelBottom);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Webapi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web API Settings";
            this.Load += new System.EventHandler(this.Webapi_Load);
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
        private CustomControls.TextexBox textexBaseUri;
        private CustomControls.TextexBox textexConsumerSecret;
        private System.Windows.Forms.ToolStripButton buttonApplicationRoleRequired;
        private System.Windows.Forms.ToolStripButton buttonApplicationRoleIgnored;
        private CustomControls.TextexBox textexConsumerKey;
        private System.Windows.Forms.Label labelConsumerKey;
    }
}