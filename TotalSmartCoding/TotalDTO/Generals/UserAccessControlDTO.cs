using TotalModel.Helpers;

namespace TotalDTO.Generals
{
    public class UserAccessControlDTO : AccessControlDTO
    {
        public int AccessControlID { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public int OrganizationalUnitID { get; set; }
        public string OrganizationalUnitName { get; set; }
    }
}
