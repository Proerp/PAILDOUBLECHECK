namespace TotalSmartCoding.Views.Mains
{
    partial class UserRegister
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
            this.combexUserID = new CustomControls.CombexBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combexOrganizationalUnitID = new CustomControls.CombexBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelSameOU = new System.Windows.Forms.Label();
            this.labelOtherOU = new System.Windows.Forms.Label();
            this.labelSameLocation = new System.Windows.Forms.Label();
            this.labelAccessControl = new System.Windows.Forms.Label();
            this.combexSameOUAccessLevels = new CustomControls.CombexBox();
            this.combexSameLocationAccessLevels = new CustomControls.CombexBox();
            this.combexOtherOUAccessLevels = new CustomControls.CombexBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 415);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(830, 55);
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
            this.buttonESC.Size = new System.Drawing.Size(83, 52);
            this.buttonESC.Text = "Cancel";
            this.buttonESC.Click += new System.EventHandler(this.buttonOKESC_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Image = global::TotalSmartCoding.Properties.Resources.Add_continue;
            this.buttonOK.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonOK.Size = new System.Drawing.Size(118, 52);
            this.buttonOK.Text = "Register";
            this.buttonOK.Click += new System.EventHandler(this.buttonOKESC_Click);
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
            this.layoutTop.Controls.Add(this.label1, 3, 7);
            this.layoutTop.Controls.Add(this.combexUserID, 3, 2);
            this.layoutTop.Controls.Add(this.combexOrganizationalUnitID, 3, 4);
            this.layoutTop.Controls.Add(this.pictureBox2, 1, 1);
            this.layoutTop.Controls.Add(this.labelAccessControl, 3, 6);
            this.layoutTop.Controls.Add(this.combexSameOUAccessLevels, 3, 10);
            this.layoutTop.Controls.Add(this.combexSameLocationAccessLevels, 3, 12);
            this.layoutTop.Controls.Add(this.combexOtherOUAccessLevels, 3, 14);
            this.layoutTop.Controls.Add(this.label5, 3, 1);
            this.layoutTop.Controls.Add(this.label2, 3, 3);
            this.layoutTop.Controls.Add(this.labelSameOU, 3, 9);
            this.layoutTop.Controls.Add(this.labelSameLocation, 3, 11);
            this.layoutTop.Controls.Add(this.labelOtherOU, 3, 13);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.layoutTop.RowCount = 16;
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layoutTop.Size = new System.Drawing.Size(830, 415);
            this.layoutTop.TabIndex = 101;
            // 
            // combexUserID
            // 
            this.combexUserID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexUserID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexUserID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexUserID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combexUserID.Editable = true;
            this.combexUserID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexUserID.FormattingEnabled = true;
            this.combexUserID.Location = new System.Drawing.Point(118, 44);
            this.combexUserID.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.combexUserID.Name = "combexUserID";
            this.combexUserID.ReadOnly = false;
            this.combexUserID.Size = new System.Drawing.Size(691, 29);
            this.combexUserID.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(114, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 21);
            this.label5.TabIndex = 78;
            this.label5.Text = "User";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combexOrganizationalUnitID
            // 
            this.combexOrganizationalUnitID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexOrganizationalUnitID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexOrganizationalUnitID.BackColor = System.Drawing.SystemColors.Window;
            this.combexOrganizationalUnitID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexOrganizationalUnitID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combexOrganizationalUnitID.Editable = true;
            this.combexOrganizationalUnitID.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexOrganizationalUnitID.FormattingEnabled = true;
            this.combexOrganizationalUnitID.Location = new System.Drawing.Point(118, 102);
            this.combexOrganizationalUnitID.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.combexOrganizationalUnitID.Name = "combexOrganizationalUnitID";
            this.combexOrganizationalUnitID.ReadOnly = false;
            this.combexOrganizationalUnitID.Size = new System.Drawing.Size(691, 29);
            this.combexOrganizationalUnitID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(114, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 21);
            this.label2.TabIndex = 83;
            this.label2.Text = "Organizational Unit";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TotalSmartCoding.Properties.Resources.add_user;
            this.pictureBox2.Location = new System.Drawing.Point(38, 25);
            this.pictureBox2.Name = "pictureBox2";
            this.layoutTop.SetRowSpan(this.pictureBox2, 4);
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 75;
            this.pictureBox2.TabStop = false;
            // 
            // labelSameOU
            // 
            this.labelSameOU.AutoSize = true;
            this.labelSameOU.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSameOU.Location = new System.Drawing.Point(114, 226);
            this.labelSameOU.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.labelSameOU.Name = "labelSameOU";
            this.labelSameOU.Size = new System.Drawing.Size(250, 21);
            this.labelSameOU.TabIndex = 84;
            this.labelSameOU.Text = "Access right applies to my own OU";
            this.labelSameOU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOtherOU
            // 
            this.labelOtherOU.AutoSize = true;
            this.labelOtherOU.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOtherOU.Location = new System.Drawing.Point(114, 342);
            this.labelOtherOU.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.labelOtherOU.Name = "labelOtherOU";
            this.labelOtherOU.Size = new System.Drawing.Size(273, 21);
            this.labelOtherOU.TabIndex = 86;
            this.labelOtherOU.Text = "Access right applies to other locations";
            this.labelOtherOU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSameLocation
            // 
            this.labelSameLocation.AutoSize = true;
            this.labelSameLocation.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSameLocation.Location = new System.Drawing.Point(114, 284);
            this.labelSameLocation.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.labelSameLocation.Name = "labelSameLocation";
            this.labelSameLocation.Size = new System.Drawing.Size(283, 21);
            this.labelSameLocation.TabIndex = 87;
            this.labelSameLocation.Text = "Access right applies to my own location";
            this.labelSameLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAccessControl
            // 
            this.labelAccessControl.AutoSize = true;
            this.labelAccessControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAccessControl.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAccessControl.ForeColor = System.Drawing.Color.DarkCyan;
            this.labelAccessControl.Location = new System.Drawing.Point(114, 162);
            this.labelAccessControl.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.labelAccessControl.Name = "labelAccessControl";
            this.labelAccessControl.Size = new System.Drawing.Size(693, 21);
            this.labelAccessControl.TabIndex = 85;
            this.labelAccessControl.Text = "Initialize the access controls:";
            this.labelAccessControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // combexSameOUAccessLevels
            // 
            this.combexSameOUAccessLevels.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexSameOUAccessLevels.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexSameOUAccessLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexSameOUAccessLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combexSameOUAccessLevels.Editable = true;
            this.combexSameOUAccessLevels.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexSameOUAccessLevels.FormattingEnabled = true;
            this.combexSameOUAccessLevels.Location = new System.Drawing.Point(118, 248);
            this.combexSameOUAccessLevels.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.combexSameOUAccessLevels.Name = "combexSameOUAccessLevels";
            this.combexSameOUAccessLevels.ReadOnly = false;
            this.combexSameOUAccessLevels.Size = new System.Drawing.Size(691, 29);
            this.combexSameOUAccessLevels.TabIndex = 3;
            // 
            // combexSameLocationAccessLevels
            // 
            this.combexSameLocationAccessLevels.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexSameLocationAccessLevels.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexSameLocationAccessLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexSameLocationAccessLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combexSameLocationAccessLevels.Editable = true;
            this.combexSameLocationAccessLevels.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexSameLocationAccessLevels.FormattingEnabled = true;
            this.combexSameLocationAccessLevels.Location = new System.Drawing.Point(118, 306);
            this.combexSameLocationAccessLevels.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.combexSameLocationAccessLevels.Name = "combexSameLocationAccessLevels";
            this.combexSameLocationAccessLevels.ReadOnly = false;
            this.combexSameLocationAccessLevels.Size = new System.Drawing.Size(691, 29);
            this.combexSameLocationAccessLevels.TabIndex = 4;
            // 
            // combexOtherOUAccessLevels
            // 
            this.combexOtherOUAccessLevels.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.combexOtherOUAccessLevels.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combexOtherOUAccessLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combexOtherOUAccessLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combexOtherOUAccessLevels.Editable = true;
            this.combexOtherOUAccessLevels.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combexOtherOUAccessLevels.FormattingEnabled = true;
            this.combexOtherOUAccessLevels.Location = new System.Drawing.Point(118, 364);
            this.combexOtherOUAccessLevels.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
            this.combexOtherOUAccessLevels.Name = "combexOtherOUAccessLevels";
            this.combexOtherOUAccessLevels.ReadOnly = false;
            this.combexOtherOUAccessLevels.Size = new System.Drawing.Size(691, 29);
            this.combexOtherOUAccessLevels.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(114, 186);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(693, 21);
            this.label1.TabIndex = 88;
            this.label1.Text = "Notes: All detail rights may be customized later.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 470);
            this.Controls.Add(this.layoutTop);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register user from domain directory for new location";
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
        private CustomControls.CombexBox combexUserID;
        private System.Windows.Forms.Label label5;
        private CustomControls.CombexBox combexOrganizationalUnitID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelSameOU;
        private System.Windows.Forms.Label labelOtherOU;
        private System.Windows.Forms.Label labelSameLocation;
        private System.Windows.Forms.Label labelAccessControl;
        private CustomControls.CombexBox combexSameOUAccessLevels;
        private CustomControls.CombexBox combexSameLocationAccessLevels;
        private CustomControls.CombexBox combexOtherOUAccessLevels;
        private System.Windows.Forms.Label label1;
    }
}