using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Productions
{
    public class Pack
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Pack(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.PackEditable();


            this.GetPacks();

            this.PackUpdateQueueID();
            this.PackUpdateEntryStatus();

            this.SearchPacks();
        }

        private void PackEditable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = PackID FROM Packs WHERE PackID = @EntityID AND NOT CartonID IS NULL";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("PackEditable", queryArray);
        }





        private void GetPacks()
        {
            string sqlSelect = "       SELECT * FROM Packs WHERE FillingLineID = @FillingLineID AND EntryStatusID IN (SELECT Id FROM dbo.SplitToIntList (@EntryStatusIDs)) " + "\r\n";

            string queryString = " @FillingLineID int, @EntryStatusIDs varchar(3999), @CartonID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "   IF (@CartonID IS NULL) " + "\r\n";
            queryString = queryString + "       " + sqlSelect + "\r\n";
            queryString = queryString + "   ELSE " + "\r\n";
            queryString = queryString + "       " + sqlSelect + " AND CartonID = @CartonID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetPacks", queryString);
        }

        private void PackUpdateQueueID()
        {
            string queryString = " @PackIDs varchar(3999), @QueueID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Packs" + "\r\n";
            queryString = queryString + "       SET         QueueID = @QueueID " + "\r\n";
            queryString = queryString + "       WHERE       PackID IN (SELECT Id FROM dbo.SplitToIntList (@PackIDs)) " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> ((SELECT (LEN(@PackIDs) - LEN(REPLACE(@PackIDs, ',', '')))) + 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'System Error: Some pack does not exist!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PackUpdateQueueID", queryString);
        }

        private void PackUpdateEntryStatus()
        {
            string queryString = " @PackIDs varchar(3999), @EntryStatusID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Packs" + "\r\n";
            queryString = queryString + "       SET         EntryStatusID = @EntryStatusID " + "\r\n";
            queryString = queryString + "       WHERE       PackID IN (SELECT Id FROM dbo.SplitToIntList (@PackIDs)) " + "\r\n";

            queryString = queryString + "       IF @@ROWCOUNT <> ((SELECT (LEN(@PackIDs) - LEN(REPLACE(@PackIDs, ',', '')))) + 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";
            queryString = queryString + "               DECLARE     @msg NVARCHAR(300) = N'System Error: Some pack does not exist!' ; " + "\r\n";
            queryString = queryString + "               THROW       61001,  @msg, 1; " + "\r\n";
            queryString = queryString + "           END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("PackUpdateEntryStatus", queryString);
        }


        private void SearchPacks()
        {
            string queryString;

            queryString = " @Barcode varchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";
            queryString = queryString + "       SELECT TOP (200) * FROM Packs WHERE Code LIKE '%' + @Barcode+ '%' ORDER BY EntryDate DESC " + "\r\n";
            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SearchPacks", queryString);
        }
    }
}
