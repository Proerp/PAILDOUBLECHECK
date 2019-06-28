using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class AccessControl
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public AccessControl(TotalSmartCodingEntities totalSmartCodingEntities)
        {//A user's access level determines what tasks he or she can perform in the database
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetAccessLevel();
            this.GetApprovalPermitted();
            this.GetUnApprovalPermitted();
            this.GetVoidablePermitted();
            this.GetUnVoidablePermitted();

            this.GetShowDiscount();
            //this.GetShowDiscountByCustomer();
            this.GetLockedDate();
            this.UpdateLockedDate();

            this.GetOnDataLogs();
            this.GetOnEventLogs();
            this.UpdateOnDataLogs();
            this.UpdateOnEventLogs();

            this.GetUserOrganizationalUnit();

            this.GetApplicationRoles();
            this.UpdateApplicationRole();

            this.GetWebapis();
            this.UpdateWebapi();

            this.GetLegalNotice();
            this.UpdateLegalNotice();

            this.GetVersionID();
            this.GetStoredID();
        }

        /// <summary>
        /// Get the permission for a specific UserID on a specific NMVNTaskID ON A SPECIFIC OrganizationalUnitID
        /// Exspecially: WHEN OrganizationalUnitID = 0: Get the top level permission for a specific UserID on a specific NMVNTaskID
        /// </summary>    
        private void GetAccessLevel()
        {
            //VIEC GetAccessLevel: CO 2 TRUONG HOP
            //    '//A. Maintenance List (DANH MUC THAM KHAO): OrganizationalUnitID = 0 => CO QUYEN HAY KHONG MA THOI, BOI VI MAU TIN TRONG DANH SACH THAM KHAO LA CUA CHUNG => DO DO KHONG CAN DEN OrganizationalUnitID
            //    '//B. Transaction Data (CAC MAU TIN GIAO DICH - CAC MAU TIN CO CHU SO HUU), KHI DO CO 2 TINH HUONG:
            //        '//B.1 TINH HUONG 1: MAU TIN DA SAVE: (TUC LA DA CO CHU SO HUU - CO OrganizationalUnitID): NGUOI DANG TRUY CAP CO QUYEN EDIT TREN MAU TIN CO CHU SO HUU HAY KHONG?
            //        '//B2. TINH HUONG 2:
            //                '//B.2: NGUOI DUNG CO QUYEN EDITABLE TREN NMVNTaskID, NHUNG KHONG BIET CO QUYEN TREN MOT DON VI CU THE OrganizationalUnitID HAY KHONG?
            //                '//B.2: THEM VAO DO, TAI THOI DIEM EDIT, CHUA XAC DINH MAU TIN THUOC VE AI, KHI DO OrganizationalUnitID = 0,
            //                '//B.2: DO DO CUNG CHI XAC DINH DUA TREN QUYEN EDITABLE CUA NMVNTaskID CUA UserID HIEN HANH MA THOI
            //                '//B.2: KHI SAVE (HOAC HAY HON LA KHI CHON CHU SO HUU - VI DU: CHON UserID TRONG QUOTATION) TA MOI XAC DINH DUOC OrganizationalUnitID
            //                '//B.2: DEN LUC NAY VIEC XAC DINH QUYEN EDITABLE LA CU THE ROI, DO DO NEU KHONG CO QUYEN EDITABLE CHO MOT DON VI CU THE => THI SE KHONG CHO SAVE

            string queryString = " @UserID Int, @NMVNTaskID Int, @OrganizationalUnitID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      MAX(AccessLevel) AS AccessLevel FROM AccessControls " + "\r\n";
            queryString = queryString + "       WHERE       UserID = @UserID AND NMVNTaskID = @NMVNTaskID AND InActive = 0 AND (@OrganizationalUnitID <= 0 OR (@OrganizationalUnitID > 0 AND OrganizationalUnitID = @OrganizationalUnitID)) " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetAccessLevel", queryString);
        }

        private void GetApprovalPermitted()
        {
            string queryString = " @UserID Int, @NMVNTaskID Int, @OrganizationalUnitID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      CAST(MAX(CAST(ApprovalPermitted AS Int)) AS Bit) AS ApprovalPermitted FROM AccessControls " + "\r\n";
            queryString = queryString + "       WHERE       UserID = @UserID AND NMVNTaskID = @NMVNTaskID AND InActive = 0 AND (@OrganizationalUnitID <= 0 OR (@OrganizationalUnitID > 0 AND OrganizationalUnitID = @OrganizationalUnitID)) " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetApprovalPermitted", queryString);
        }

        private void GetUnApprovalPermitted()
        {
            string queryString = " @UserID Int, @NMVNTaskID Int, @OrganizationalUnitID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      CAST(MAX(CAST(UnApprovalPermitted AS Int)) AS Bit) AS UnApprovalPermitted FROM AccessControls " + "\r\n";
            queryString = queryString + "       WHERE       UserID = @UserID AND NMVNTaskID = @NMVNTaskID AND InActive = 0 AND (@OrganizationalUnitID <= 0 OR (@OrganizationalUnitID > 0 AND OrganizationalUnitID = @OrganizationalUnitID)) " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUnApprovalPermitted", queryString);
        }


        private void GetVoidablePermitted()
        {
            string queryString = " @UserID Int, @NMVNTaskID Int, @OrganizationalUnitID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      CAST(MAX(CAST(VoidablePermitted AS Int)) AS Bit) AS VoidablePermitted FROM AccessControls " + "\r\n";
            queryString = queryString + "       WHERE       UserID = @UserID AND NMVNTaskID = @NMVNTaskID AND InActive = 0 AND (@OrganizationalUnitID <= 0 OR (@OrganizationalUnitID > 0 AND OrganizationalUnitID = @OrganizationalUnitID)) " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetVoidablePermitted", queryString);
        }

        private void GetUnVoidablePermitted()
        {
            string queryString = " @UserID Int, @NMVNTaskID Int, @OrganizationalUnitID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      CAST(MAX(CAST(UnVoidablePermitted AS Int)) AS Bit) AS UnVoidablePermitted FROM AccessControls " + "\r\n";
            queryString = queryString + "       WHERE       UserID = @UserID AND NMVNTaskID = @NMVNTaskID AND InActive = 0 AND (@OrganizationalUnitID <= 0 OR (@OrganizationalUnitID > 0 AND OrganizationalUnitID = @OrganizationalUnitID)) " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUnVoidablePermitted", queryString);
        }


        private void GetShowDiscount()
        {
            string queryString = " @UserID Int, @NMVNTaskID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      CAST(MAX(CAST(ShowDiscount AS Int)) AS Bit) AS ShowDiscount FROM AccessControls " + "\r\n";
            queryString = queryString + "       WHERE       UserID = @UserID AND NMVNTaskID = @NMVNTaskID AND InActive = 0 " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetShowDiscount", queryString);
        }

        private void GetShowDiscountByCustomer()
        {
            string queryString = " @CustomerID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      CAST(MAX(CAST(CustomerCategories.ShowDiscount AS Int)) AS Bit) AS ShowDiscount FROM Customers INNER JOIN CustomerCategories ON Customers.CustomerID = @CustomerID AND Customers.CustomerCategoryID = CustomerCategories.CustomerCategoryID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetShowDiscountByCustomer", queryString);
        }

        private void GetLockedDate()
        {
            string queryString = " @LocationID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      MAX(LockedDate) " + "\r\n";
            queryString = queryString + "       FROM        Locations " + "\r\n";
            queryString = queryString + "       WHERE       LocationID = @LocationID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetLockedDate", queryString);
        }

        private void UpdateLockedDate()
        {
            string queryString = " @UserID Int, @LocationID Int, @LockedDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      Locations " + "\r\n";
            queryString = queryString + "       SET         LockedDate = @LockedDate, UserID = @UserID, EditedDate = GetDate() " + "\r\n";
            queryString = queryString + "       WHERE       LocationID = @LocationID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UpdateLockedDate", queryString);
        }




        private void GetOnDataLogs()
        {
            string queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      MAX(OnDataLogs) " + "\r\n";
            queryString = queryString + "       FROM        Locations " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetOnDataLogs", queryString);
        }

        private void GetOnEventLogs()
        {
            string queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      MAX(OnEventLogs) " + "\r\n";
            queryString = queryString + "       FROM        Locations " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetOnEventLogs", queryString);
        }

        private void UpdateOnDataLogs()
        {
            string queryString = " @OnDataLogs Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      Locations " + "\r\n";
            queryString = queryString + "       SET         OnDataLogs = @OnDataLogs " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UpdateOnDataLogs", queryString);
        }

        private void UpdateOnEventLogs()
        {
            string queryString = " @OnEventLogs Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       UPDATE      Locations " + "\r\n";
            queryString = queryString + "       SET         OnEventLogs = @OnEventLogs " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UpdateOnEventLogs", queryString);
        }



        private void GetUserOrganizationalUnit()
        {
            string queryString = " @UserName nvarchar(256) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT TOP 1 Users.UserID, Users.FirstName, Users.LastName, Users.UserName, Users.SecurityIdentifier, Users.IsDatabaseAdmin, OrganizationalUnitUsers.OrganizationalUnitID FROM Users INNER JOIN OrganizationalUnitUsers ON Users.UserID = OrganizationalUnitUsers.UserID WHERE ({ fn UCASE(Users.SecurityIdentifier) } = { fn UCASE(@UserName) }) AND OrganizationalUnitUsers.InActive = 0 " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetUserOrganizationalUnit", queryString);
        }

        private void GetApplicationRoles()
        {
            string queryString = " @ApplicationRoleID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      Name, Password FROM ApplicationRoles WHERE ApplicationRoleID = @ApplicationRoleID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetApplicationRoles", queryString);
        }

        private void UpdateApplicationRole()
        {
            string queryString = " @ApplicationRoleID Int, @Name nvarchar(100), @Password nvarchar(100) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(ApplicationRoleID) FROM ApplicationRoles WHERE ApplicationRoleID = @ApplicationRoleID) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   INSERT INTO     ApplicationRoles (ApplicationRoleID, Name, Password, EditedDate) VALUES (@ApplicationRoleID, @Name, @Password, GetDate()); " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   UPDATE          ApplicationRoles SET Name = @Name, Password = @Password, EditedDate = GetDate() WHERE ApplicationRoleID = @ApplicationRoleID; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UpdateApplicationRole", queryString);
        }

        private void GetWebapis()
        {
            string queryString = " @WebapiID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT * FROM Webapis WHERE WebapiID = @WebapiID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetWebapis", queryString);
        }

        private void UpdateWebapi()
        {
            string queryString = " @WebapiID Int, @BaseUri nvarchar(100), @ConsumerKey nvarchar(100), @ConsumerSecret nvarchar(100) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "           IF (SELECT COUNT(WebapiID) FROM Webapis WHERE WebapiID = @WebapiID) <= 0 " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   INSERT INTO     Webapis (WebapiID, BaseUri, ConsumerKey, ConsumerSecret, EditedDate) VALUES (@WebapiID, @BaseUri, @ConsumerKey, @ConsumerSecret, GetDate()); " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            queryString = queryString + "           ELSE " + "\r\n";
            queryString = queryString + "               BEGIN " + "\r\n";
            queryString = queryString + "                   UPDATE          Webapis SET BaseUri = @BaseUri, ConsumerKey = @ConsumerKey, ConsumerSecret = @ConsumerSecret, EditedDate = GetDate() WHERE WebapiID = @WebapiID; " + "\r\n";
            queryString = queryString + "               END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UpdateWebapi", queryString);
        }

        private void GetLegalNotice()
        {
            string queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      MAX(LegalNotice) AS LegalNotice FROM Configs " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetLegalNotice", queryString);
        }

        private void UpdateLegalNotice()
        {
            string queryString = " @LegalNotice nvarchar(3999) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Configs SET LegalNotice = @LegalNotice; " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("UpdateLegalNotice", queryString);
        }

        private void GetVersionID()
        {
            string queryString = " @ConfigID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      MAX(VersionID) AS VersionID FROM Configs WHERE ConfigID = @ConfigID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetVersionID", queryString);
        }

        private void GetStoredID()
        {
            string queryString = " @ConfigID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      MAX(StoredID) AS StoredID FROM Configs WHERE ConfigID = @ConfigID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetStoredID", queryString);
        }
    }
}
