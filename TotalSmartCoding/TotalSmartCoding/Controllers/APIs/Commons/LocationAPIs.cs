using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;
using TotalCore.Repositories.Commons;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class LocationAPIs
    {
        private readonly ILocationAPIRepository locationAPIRepository;

        public LocationAPIs(ILocationAPIRepository locationAPIRepository)
        {
            this.locationAPIRepository = locationAPIRepository;
        }


        public ICollection<LocationIndex> GetLocationIndexes()
        {
            return this.locationAPIRepository.GetEntityIndexes<LocationIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }
        
        public IList<LocationBase> GetLocationBases()
        {
            return this.GetLocationBases(false);
        }

        public IList<LocationBase> GetLocationBases(bool withNullRow)
        {
            return this.locationAPIRepository.GetLocationBases(withNullRow);
        }

        public int UpdateLockedDate(int locationID, string locationName, DateTime lockedDate)
        {
            int affectedRows = this.locationAPIRepository.UpdateLockedDate(ContextAttributes.User.UserID, locationID, lockedDate);

            this.AddDataLogs("Update month-end closing", locationID, locationName, lockedDate);

            return affectedRows;
        }

        private void AddDataLogs(string actionType, int locationID, string locationName, DateTime lockedDate)
        {
            if (!this.locationAPIRepository.GetOnDataLogs()) return;// DO NOTHING

            DateTime entryDate = DateTime.Now;

            this.locationAPIRepository.AddDataLogs(locationID, null, entryDate, "Month-Ends", actionType, "Month-End", "LocationID", locationID.ToString());
            this.locationAPIRepository.AddDataLogs(locationID, null, entryDate, "Month-Ends", actionType, "Month-End", "LocationName", locationName);
            this.locationAPIRepository.AddDataLogs(locationID, null, entryDate, "Month-Ends", actionType, "Month-End", "ClosedDate", lockedDate.ToString("dd/MMM/yyyy HH:mm:ss"));
        }
    }
}
