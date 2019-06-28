using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface IFillingLineRepository : IGenericWithDetailRepository<FillingLine, FillingLineDetail>
    {
    }

    public interface IFillingLineAPIRepository : IGenericAPIRepository
    {
        IList<FillingLineSetting> GetFillingLineSettings(int fillingLineID, int deviceID);
        IList<FillingLineBase> GetFillingLineBases();
    }
}
