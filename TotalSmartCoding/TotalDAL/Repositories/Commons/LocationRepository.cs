using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;
using System;


namespace TotalDAL.Repositories.Commons
{
    //public class LocationRepository : GenericRepository<Location>, ILocationRepository
    //{
    //    public LocationRepository(TotalSmartCodingEntities totalSmartCodingEntities)
    //        : base(totalSmartCodingEntities, "LocationEditable")
    //    {
    //    }
    //}





    public class LocationAPIRepository : GenericAPIRepository, ILocationAPIRepository
    {
        public LocationAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetLocationIndexes")
        {
        }

        public IList<LocationBase> GetLocationBases(bool withNullRow)
        {
            IList<LocationBase> locationBases = this.TotalSmartCodingEntities.GetLocationBases().OrderBy(o => o.Name).ToList();
            if (withNullRow) locationBases.Add(new LocationBase() { LocationID = 0 });
            return locationBases;
        }

        public int UpdateLockedDate(int userID, int locationID, DateTime lockedDate)
        {
            return this.TotalSmartCodingEntities.UpdateLockedDate(userID, locationID, lockedDate);
        }
    }
}
