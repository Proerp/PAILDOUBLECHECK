using System;
using System.Windows.Forms;

using Ninject;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Generals;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;


namespace TotalSmartCoding.Views.Mains
{
    public partial class UserOUs : Form
    {
        private bool addOU;

        private IOrganizationalUnitRepository organizationalUnitRepository { get; set; }
        private OrganizationalUnitAPIs organizationalUnitAPIs { get; set; }

        public int? NewLocationID { get; set; }
        public int? OrganizationalUnitID { get; set; }

        private Binding bindingNewLocationID;
        private Binding bindingOrganizationalUnitID;

        public UserOUs(OrganizationalUnitAPIs organizationalUnitAPIs, bool addOU)
        {
            InitializeComponent();

            try
            {
                this.organizationalUnitRepository = CommonNinject.Kernel.Get<IOrganizationalUnitRepository>();

                LocationAPIs locationAPIs = new LocationAPIs(CommonNinject.Kernel.Get<ILocationAPIRepository>());
                this.combexNewLocationID.DataSource = locationAPIs.GetLocationBases();
                this.combexNewLocationID.DisplayMember = CommonExpressions.PropertyName<LocationBase>(p => p.Name);
                this.combexNewLocationID.ValueMember = CommonExpressions.PropertyName<LocationBase>(p => p.LocationID);
                this.bindingNewLocationID = this.combexNewLocationID.DataBindings.Add("SelectedValue", this, "NewLocationID", true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingNewLocationID.BindingComplete += binding_BindingComplete;

                this.organizationalUnitAPIs = organizationalUnitAPIs;
                this.combexOrganizationalUnitID.DataSource = this.organizationalUnitAPIs.GetOrganizationalUnitIndexes();
                this.combexOrganizationalUnitID.DisplayMember = CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.LocationOrganizationalUnitName);
                this.combexOrganizationalUnitID.ValueMember = CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.OrganizationalUnitID);
                this.bindingOrganizationalUnitID = this.combexOrganizationalUnitID.DataBindings.Add("SelectedValue", this, CommonExpressions.PropertyName<OrganizationalUnitIndex>(p => p.OrganizationalUnitID), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingOrganizationalUnitID.BindingComplete += binding_BindingComplete;
                this.textexNewOrganizationalUnitID.TextChanged += textexNewOrganizationalUnitID_TextChanged;

                this.addOU = addOU;
                this.labelOrganizationalUnitID.Visible = !this.addOU; this.combexOrganizationalUnitID.Visible = !this.addOU;
                this.labelNewOrganizationalUnitID.Visible = this.addOU; this.textexNewOrganizationalUnitID.Visible = this.addOU;
                this.labelNewLocationID.Visible = this.addOU; this.combexNewLocationID.Visible = this.addOU;
                this.Text = this.addOU ? "Add new organizational unit" : "Remove OU";
                this.buttonOK.Text = this.addOU ? "Add" : "Remove";
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void textexNewOrganizationalUnitID_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.addOU && this.NewLocationID != null && this.textexNewOrganizationalUnitID.Text.Trim().Length > 0;
        }

        private void binding_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (this.addOU && sender.Equals(this.bindingNewLocationID))
                textexNewOrganizationalUnitID_TextChanged(this.textexNewOrganizationalUnitID, new EventArgs());

            if (!this.addOU && sender.Equals(this.bindingOrganizationalUnitID))
                this.buttonOK.Enabled = this.OrganizationalUnitID != null && this.organizationalUnitRepository.GetEditable((int)this.OrganizationalUnitID); ;
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    OrganizationalUnitIndex organizationalUnitIndex = null;
                    this.textexNewOrganizationalUnitID.Text = this.textexNewOrganizationalUnitID.Text.Trim();

                    if (!this.addOU && this.combexOrganizationalUnitID.SelectedIndex >= 0) organizationalUnitIndex = this.combexOrganizationalUnitID.SelectedItem as OrganizationalUnitIndex;

                    if ((this.addOU && this.NewLocationID != null && this.textexNewOrganizationalUnitID.Text.Length > 0) || organizationalUnitIndex != null)
                    {
                        if (CustomMsgBox.Show(this, "Are you sure you want to " + (this.addOU ? "add" : "remove") + " this organizational unit?" + "\r\n" + "\r\n" + (this.addOU ? this.textexNewOrganizationalUnitID.Text + "\r\nAt:  " + this.combexNewLocationID.Text : organizationalUnitIndex.OrganizationalUnitName + "\r\nAt:  " + organizationalUnitIndex.LocationName), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                        {
                            if (this.addOU) this.organizationalUnitAPIs.OrganizationalUnitAdd(this.NewLocationID, this.textexNewOrganizationalUnitID.Text, this.textexNewOrganizationalUnitID.Text);
                            if (!this.addOU) this.organizationalUnitAPIs.OrganizationalUnitRemove(organizationalUnitIndex.OrganizationalUnitID, organizationalUnitIndex.OrganizationalUnitName);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }

                if (sender.Equals(this.buttonESC))
                    this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

    }
}
