using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class Territory
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Territory(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetTerritoryIndexes();

            this.TerritoryEditable();
            this.TerritoryDeletable();
            this.TerritorySaveRelative();
            this.TerritoryPostSaveValidate();

            this.GetTerritoryBases();
        }


        private void GetTerritoryIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TerritoryID, Name, N'Chevron Vietnam' AS GlobalName, Remarks " + "\r\n";
            queryString = queryString + "       FROM        Territories " + "\r\n";
            queryString = queryString + "       WHERE      (SELECT TOP 1 OrganizationalUnitID FROM AccessControls WHERE UserID = @UserID AND NMVNTaskID = " + (int)TotalBase.Enums.GlobalEnums.NmvnTaskID.Territories + " AND AccessControls.AccessLevel > 0) > 0 " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTerritoryIndexes", queryString);
        }


        private void TerritorySaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("TerritorySaveRelative", queryString);
        }

        private void TerritoryPostSaveValidate()
        {
            string[] queryArray = new string[1];

            string queryString = "              UPDATE      EntireTerritories " + "\r\n";
            queryString = queryString + "       SET         EntireTerritories.Name = Territories.Name, EntireTerritories.EntireName = Territories.Name, EntireTerritories.Name1 = Territories.Name " + "\r\n";
            queryString = queryString + "       FROM        EntireTerritories INNER JOIN Territories ON EntireTerritories.TerritoryID = @EntityID AND EntireTerritories.TerritoryID = Territories.TerritoryID " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               INSERT INTO EntireTerritories (TerritoryID, Name, EntireName, TerritoryID1, Name1, TerritoryID2, Name2, TerritoryID3, Name3) " + "\r\n";
            queryString = queryString + "               SELECT      TerritoryID, Name, Name AS EntireName, TerritoryID AS TerritoryID1, Name AS Name1, 0 AS TerritoryID2, N'' AS Name2, 0 AS TerritoryID3, N'' AS Name3 FROM Territories WHERE TerritoryID = @EntityID " + "\r\n";

            queryString = queryString + "               IF @@ROWCOUNT <> 1 " + "\r\n";
            queryString = queryString + "                   DELETE      EntireTerritories WHERE TerritoryID = @EntityID " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            queryArray[0] = " SELECT TOP 1 @FoundEntity = TerritoryID FROM Territories WHERE TerritoryID <> TerritoryID "; //ALWAYS RETURN NOTHING

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TerritoryPostSaveValidate", queryArray, queryString);
        }

        private void TerritoryEditable()
        {
            string[] queryArray = new string[0];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TerritoryEditable", queryArray);
        }

        private void TerritoryDeletable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = TerritoryID FROM Customers WHERE TerritoryID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TerritoryDeletable", queryArray);
        }

        private void GetTerritoryBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TerritoryID, Name, EntireName " + "\r\n";
            queryString = queryString + "       FROM        EntireTerritories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTerritoryBases", queryString);
        }

    }
}
