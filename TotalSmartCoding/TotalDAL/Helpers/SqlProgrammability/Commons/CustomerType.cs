using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class CustomerType
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public CustomerType(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCustomerTypeIndexes();

            this.CustomerTypeEditable();
            this.CustomerTypeDeletable();
            this.CustomerTypeSaveRelative();

            this.GetCustomerTypeBases();
        }


        private void GetCustomerTypeIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerTypeID, Name, N'Chevron Vietnam' AS GlobalName, Remarks " + "\r\n";
            queryString = queryString + "       FROM        CustomerTypes " + "\r\n";
            queryString = queryString + "       WHERE      (SELECT TOP 1 OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.CustomerTypes + " AND AccessControls.AccessLevel > 0) > 0 " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerTypeIndexes", queryString);
        }


        private void CustomerTypeSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("CustomerTypeSaveRelative", queryString);
        }


        private void CustomerTypeEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CustomerTypeEditable", queryArray);
        }

        private void CustomerTypeDeletable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = CustomerTypeID FROM Customers WHERE CustomerTypeID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CustomerTypeDeletable", queryArray);
        }

        private void GetCustomerTypeBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CustomerTypeID, Name " + "\r\n";
            queryString = queryString + "       FROM        CustomerTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCustomerTypeBases", queryString);
        }

    }
}
