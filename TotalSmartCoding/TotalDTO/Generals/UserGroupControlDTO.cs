using TotalModel.Helpers;

namespace TotalDTO.Generals
{
    public class UserGroupControlDTO : AccessControlDTO
    {
        public int UserGroupControlID { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public int ModuleDetailID { get; set; }
        public string ModuleDetailName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }

    public class UserGroupReportDTO : NotifyPropertyChangeObject
    {
        public int UserGroupReportID { get; set; }
        public int ReportID { get; set; }
        public string ReportGroupName { get; set; }
        public string ReportName { get; set; }

        private bool enabled;
        public bool Enabled
        {
            get { return this.enabled; }
            set { ApplyPropertyChange<UserGroupReportDTO, bool>(ref this.enabled, o => o.Enabled, value); }
        }
    }
    
}
