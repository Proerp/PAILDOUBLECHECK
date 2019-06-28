using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

using TotalDTO.Productions;

namespace TotalCore.Services.Productions
{
    public interface IPackService : IGenericService<Pack, PackDTO, PackPrimitiveDTO>
    {
        IList<Pack> GetPacks(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? cartonID);

        bool UpdateEntryStatus(string packIDs, GlobalVariables.BarcodeStatus barcodeStatus);

        bool UpdateQueueID(string packIDs, int queueID);
    }
}
