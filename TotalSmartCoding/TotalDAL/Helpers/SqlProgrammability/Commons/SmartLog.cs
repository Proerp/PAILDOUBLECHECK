using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class SmartLog
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public SmartLog(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.AddDataLogs();
            this.AddEventLogs();

            this.DataLogJournals();
            this.EventLogJournals();
        }

        private void AddDataLogs()
        {
            string queryString = " @LocationID Int, @EntryID int, @EntryDetailID int, @EntryDate DateTime, @ModuleName nvarchar(100), @UserName  nvarchar(100), @IPAddress nvarchar(100), @ActionType nvarchar(100), @EntityName nvarchar(100), @PropertyName nvarchar(100), @PropertyValue nvarchar(500)" + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "               INSERT              DataLogs      (LocationID, EntryID, EntryDetailID, EntryDate, ModuleName, UserName, IPAddress, ActionType, EntityName, PropertyName, PropertyValue) " + "\r\n";
            queryString = queryString + "               VALUES              (@LocationID, @EntryID, @EntryDetailID, @EntryDate, @ModuleName, @UserName, @IPAddress, @ActionType, @EntityName, @PropertyName, @PropertyValue) " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("AddDataLogs", queryString);
        }

        private void AddEventLogs()
        {
            string queryString = " @LocationID Int, @EntryDate DateTime, @UserName nvarchar(100), @IPAddress nvarchar(100), @ModuleName nvarchar(100), @ActionType nvarchar(100), @EntryID int, @Remarks nvarchar(200) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "               DECLARE             @EventLogID Int " + "\r\n";

            queryString = queryString + "               INSERT              EventLogs     (LocationID, EntryDate, UserName, IPAddress, ModuleName, ActionType, EntryID, Remarks) " + "\r\n";
            queryString = queryString + "               VALUES             (@LocationID, @EntryDate, @UserName, @IPAddress, @ModuleName, @ActionType, @EntryID, @Remarks) " + "\r\n";

            queryString = queryString + "               SELECT              @EventLogID = SCOPE_IDENTITY(); " + "\r\n";
            queryString = queryString + "               UPDATE              LastEventLogs SET EventLogID = @EventLogID WHERE UserName = @UserName; " + "\r\n";
            queryString = queryString + "               IF @@ROWCOUNT < 1   INSERT LastEventLogs (EventLogID, UserName) VALUES (@EventLogID, @UserName) " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("AddEventLogs", queryString);
        }


        private void DataLogJournals()
        {
            string queryString = " @FromDate DateTime, @ToDate DateTime, @UserName nvarchar(80) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       DECLARE     @LocalFromDate DateTime, @LocalToDate DateTime, @LocalUserName nvarchar(80) " + "\r\n";
            queryString = queryString + "       SET         @LocalFromDate = @FromDate      SET @LocalToDate = @ToDate      SET @LocalUserName = @UserName " + "\r\n";

            queryString = queryString + "       IF  (@LocalUserName <> '#') " + "\r\n";
            queryString = queryString + "           " + this.DataLogJournalSQL(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.DataLogJournalSQL(false) + "\r\n";

            queryString = queryString + "   END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("DataLogJournals", queryString);
        }

        private string DataLogJournalSQL(bool isUserName)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       SELECT      DataLogs.DataLogID, DataLogs.LocationID, Locations.Code AS LocationCode, DataLogs.EntryID, DataLogs.EntryDetailID, DataLogs.EntryDate, DataLogs.ModuleName, DataLogs.UserName, DataLogs.IPAddress, DataLogs.ActionType, DataLogs.EntityName, DataLogs.PropertyName, DataLogs.PropertyValue " + "\r\n";
            queryString = queryString + "       FROM        DataLogs INNER JOIN Locations ON DataLogs.EntryDate >= @LocalFromDate AND DataLogs.EntryDate <= @LocalToDate " + (isUserName ? "AND DataLogs.UserName = @LocalUserName" : "") + " AND DataLogs.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "       ORDER BY    DataLogs.DataLogID " + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private void EventLogJournals()
        {
            string queryString = " @FromDate DateTime, @ToDate DateTime, @UserName nvarchar(80), @LastEventLogs bit " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       DECLARE     @LocalFromDate DateTime, @LocalToDate DateTime, @LocalUserName nvarchar(80), @LocalLastEventLogs bit " + "\r\n";
            queryString = queryString + "       SET         @LocalFromDate = @FromDate      SET @LocalToDate = @ToDate      SET @LocalUserName = @UserName      SET @LocalLastEventLogs = @LastEventLogs " + "\r\n";
            
            queryString = queryString + "       IF (@LastEventLogs <> 0) " + "\r\n";
            queryString = queryString + "           " + this.EventLogJournalSQL(true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.EventLogJournalSQL(false) + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("EventLogJournals", queryString);
        }

        private string EventLogJournalSQL(bool isLastEventLogs)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       IF  (@LocalUserName <> '#') " + "\r\n";
            queryString = queryString + "           " + this.EventLogJournalSQL(isLastEventLogs, true) + "\r\n";
            queryString = queryString + "       ELSE " + "\r\n";
            queryString = queryString + "           " + this.EventLogJournalSQL(isLastEventLogs, false) + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

        private string EventLogJournalSQL(bool isLastEventLogs, bool isUserName)
        {
            string queryString = "";
            queryString = queryString + "   BEGIN " + "\r\n";
            queryString = queryString + "       SELECT      EventLogs.EventLogID, EventLogs.LocationID, Locations.Code AS LocationCode, EventLogs.EntryDate, EventLogs.UserName, EventLogs.IPAddress, EventLogs.ModuleName, EventLogs.ActionType, EventLogs.EntryID, EventLogs.Remarks " + "\r\n";
            queryString = queryString + "       FROM        EventLogs " + "\r\n";
            queryString = queryString + "                   INNER JOIN Locations ON " + (isLastEventLogs ? "EventLogs.EventLogID IN (SELECT EventLogID FROM LastEventLogs)" : ("EventLogs.EntryDate >= @LocalFromDate AND EventLogs.EntryDate <= @LocalToDate" + (isUserName ? " AND EventLogs.UserName = @LocalUserName" : ""))) + " AND EventLogs.LocationID = Locations.LocationID " + "\r\n";
            queryString = queryString + "       ORDER BY    EventLogs.EventLogID " + "\r\n";
            queryString = queryString + "   END " + "\r\n";

            return queryString;
        }

    }
}

