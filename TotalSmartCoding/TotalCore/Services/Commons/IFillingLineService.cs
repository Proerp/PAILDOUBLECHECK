using TotalModel.Models;

using TotalDTO.Commons;

namespace TotalCore.Services.Commons
{
    public interface IFillingLineService : IGenericWithDetailService<FillingLine, FillingLineDetail, FillingLineDTO, FillingLinePrimitiveDTO, FillingLineDetailDTO>
    {
    }
}