using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class CommodityCategory
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public CommodityCategory(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetCommodityCategoryIndexes();

            this.CommodityCategoryEditable();
            this.CommodityCategoryDeletable();
            this.CommodityCategorySaveRelative();

            this.GetCommodityCategoryBases();
        }


        private void GetCommodityCategoryIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CommodityCategoryID, Name, N'Chevron Vietnam' AS GlobalName, Remarks " + "\r\n";
            queryString = queryString + "       FROM        CommodityCategories " + "\r\n";
            queryString = queryString + "       WHERE      (SELECT TOP 1 OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.CommodityCategories + " AND AccessControls.AccessLevel > 0) > 0 " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityCategoryIndexes", queryString);
        }


        private void CommodityCategorySaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
          
            this.totalSmartCodingEntities.CreateStoredProcedure("CommodityCategorySaveRelative", queryString);
        }


        private void CommodityCategoryEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CommodityCategoryEditable", queryArray);
        }

        private void CommodityCategoryDeletable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = CommodityCategoryID FROM Commodities WHERE CommodityCategoryID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("CommodityCategoryDeletable", queryArray);
        }

        private void GetCommodityCategoryBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      CommodityCategoryID, Name " + "\r\n";
            queryString = queryString + "       FROM        CommodityCategories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetCommodityCategoryBases", queryString);
        }

    }
}
