using TotalBase;
using TotalModel.Models;

using TotalDTO.Productions;

namespace TotalCore.Services.Productions
{
    public interface IBatchService : IGenericService<Batch, BatchDTO, BatchPrimitiveDTO>
    {
        bool CommonUpdate(int batchID, string nextPackNo, string nextCartonNo, string nextPalletNo);
        bool ExtraUpdate(int batchID, string sentPackNo, string sentCartonNo, string sentPalletNo);
        bool ExtendedUpdate(int batchID, string batchPackNo, string batchCartonNo, string batchPalletNo);
    }
}
