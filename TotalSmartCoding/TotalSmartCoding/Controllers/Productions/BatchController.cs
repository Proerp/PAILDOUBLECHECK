using TotalModel.Models;
using TotalDTO.Productions;

using TotalBase;
using TotalCore.Services.Productions;
using TotalSmartCoding.ViewModels.Productions;

namespace TotalSmartCoding.Controllers.Productions
{
    public class BatchController : GenericSimpleController<Batch, BatchDTO, BatchPrimitiveDTO, BatchViewModel>
    {
        private BatchViewModel batchViewModel;
        public BatchController(IBatchService batchService, BatchViewModel batchViewModel)
            : base(batchService, batchViewModel)
        {
            this.batchViewModel = batchViewModel;
        }
        
        public override void Create()
        {
            base.Create();
            this.batchViewModel.EntryMonthID = CommonExpressions.GetEntryMonthID(this.batchViewModel.EntryDate);
        }
    }
}

