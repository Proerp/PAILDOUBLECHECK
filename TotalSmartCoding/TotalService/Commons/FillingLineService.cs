using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class FillingLineService : GenericWithDetailService<FillingLine, FillingLineDetail, FillingLineDTO, FillingLinePrimitiveDTO, FillingLineDetailDTO>, IFillingLineService
    {
        public FillingLineService(IFillingLineRepository fillingLineRepository)
            : base(fillingLineRepository)
        {
        }
    }
}