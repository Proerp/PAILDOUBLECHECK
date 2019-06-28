using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class FillingLineRepository : GenericWithDetailRepository<FillingLine, FillingLineDetail>, IFillingLineRepository
    {
        public FillingLineRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "FillingLineEditable", null, "FillingLineDeletable")
        {
        }
    }






    public class FillingLineAPIRepository : GenericAPIRepository, IFillingLineAPIRepository
    {
        public FillingLineAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetFillingLineIndexes")
        {
        }

        public IList<FillingLineSetting> GetFillingLineSettings(int fillingLineID, int deviceID)
        {
            return this.TotalSmartCodingEntities.GetFillingLineSettings(fillingLineID, deviceID).ToList();
        }

        public IList<FillingLineBase> GetFillingLineBases()
        {
            return this.TotalSmartCodingEntities.GetFillingLineBases().ToList();
        }
    }
}
