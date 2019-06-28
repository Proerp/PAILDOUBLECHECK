using System;
using System.Windows.Forms;
using System.Linq;

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
    public partial class LockedDates : Form
    {
        private ILocationAPIRepository locationAPIRepository;
        private LocationAPIs locationAPIs;

        public LockedDates()
        {
            InitializeComponent();

            this.locationAPIRepository = CommonNinject.Kernel.Get<ILocationAPIRepository>();
            this.locationAPIs = new LocationAPIs(this.locationAPIRepository);
            this.loadLocationIndexes();
        }

        private void loadLocationIndexes()
        {
            try
            {
                this.fastUserGroups.SetObjects(this.locationAPIs.GetLocationIndexes());
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonESC))
                    this.DialogResult = DialogResult.Cancel;
                else
                {
                    if (this.fastUserGroups.SelectedObject != null)
                    {
                        int? accessLevel = this.locationAPIRepository.TotalSmartCodingEntities.GetAccessLevel(ContextAttributes.User.UserID, (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.MonthEnd, 0).Single();
                        if (accessLevel != (int)TotalBase.Enums.GlobalEnums.AccessLevel.Editable) throw new System.ArgumentException("Lỗi phân quyền", "Không có quyền truy cập dữ liệu");

                        LocationIndex locationIndex = (LocationIndex)this.fastUserGroups.SelectedObject;
                        if (locationIndex != null)
                        {
                            DateTime newLockedDate = sender.Equals(this.buttonForward) ? locationIndex.LockedDate.AddDays(1).AddMonths(1).AddDays(-1) : locationIndex.LockedDate.AddDays(-locationIndex.LockedDate.Day);
                            if (CustomMsgBox.Show(this, "The current closing date for " + locationIndex.Name + " is:" + "\r\n" + "\r\n" + locationIndex.LockedDate + "\r\n" + "\r\n" + "Are you sure you want to change " + (sender.Equals(this.buttonForward) ? "forward" : "backward") + " to:" + "\r\n" + "\r\n" + newLockedDate, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                            {
                                this.locationAPIs.UpdateLockedDate(locationIndex.LocationID, locationIndex.Name, newLockedDate);
                                this.loadLocationIndexes();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


    }
}
