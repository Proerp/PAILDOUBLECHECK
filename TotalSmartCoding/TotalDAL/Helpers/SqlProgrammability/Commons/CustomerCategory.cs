using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class CustomerCategory
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public CustomerCategory(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCustomerCategoryIndexes();

            this.CustomerCategoryEditable();
            this.CustomerCategoryDeletable();
            this.CustomerCategorySaveRelative();

            this.GetCustomerCategoryBases();
        }


        private void GetCustomerCategoryIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerCategoryID, Name, N'Chevron Vietnam' AS GlobalName, Remarks " + "\r\n";
            queryString = queryString + "       FROM        CustomerCategories " + "\r\n";
            queryString = queryString + "       WHERE      (SELECT TOP 1 OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.CustomerCategories + " AND AccessControls.AccessLevel > 0) > 0 " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerCategoryIndexes", queryString);
        }


        private void CustomerCategorySaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CustomerCategorySaveRelative", queryString);
        }


        private void CustomerCategoryEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CustomerCategoryEditable", queryArray);
        }

        private void CustomerCategoryDeletable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = CustomerCategoryID FROM Customers WHERE CustomerCategoryID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CustomerCategoryDeletable", queryArray);
        }

        private void GetCustomerCategoryBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerCategoryID, Name " + "\r\n";
            queryString = queryString + "       FROM        CustomerCategories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerCategoryBases", queryString);
        }

    }
}
