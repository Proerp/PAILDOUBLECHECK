using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

using TotalDTO.Productions;

namespace TotalCore.Services.Productions
{
    public interface ICartonService : IGenericService<Carton, CartonDTO, CartonPrimitiveDTO>
    {
        IList<Carton> GetCartons(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? palletID);
        IList<CartonAttribute> GetCartonAttributes(GlobalVariables.FillingLine fillingLineID, string submitStatusIDs, int? palletID);

        bool UpdateEntryStatus(string cartonIDs, GlobalVariables.BarcodeStatus barcodeStatus);
        bool UpdateSubmitStatus(string cartonIDs, GlobalVariables.SubmitStatus submitStatus, string remarks);

        bool CartonChecked(int? batchID, string label);
    }
}
