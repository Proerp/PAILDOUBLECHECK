using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Productions;
using System.Data.Entity.Core.Objects;
using TotalBase;

namespace TotalDAL.Repositories.Productions
{
    public class BatchRepository : GenericRepository<Batch>, IBatchRepository
    {
        public BatchRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "BatchEditable")
        {
        }

        public void CommonUpdate(int batchID, string nextPackNo, string nextCartonNo, string nextPalletNo)
        {
            this.TotalSmartCodingEntities.BatchCommonUpdate(batchID, nextPackNo, nextCartonNo, nextPalletNo);
        }

        public void ExtraUpdate(int batchID, string sentPackNo, string sentCartonNo, string sentPalletNo)
        {
            this.TotalSmartCodingEntities.BatchExtraUpdate(batchID, sentPackNo, sentCartonNo, sentPalletNo);
        }

        public void ExtendedUpdate(int batchID, string batchPackNo, string batchCartonNo, string batchPalletNo)
        {
            this.TotalSmartCodingEntities.BatchExtendedUpdate(batchID, batchPackNo, batchCartonNo, batchPalletNo);
        }
    }








    public class BatchAPIRepository : GenericAPIRepository, IBatchAPIRepository
    {
        public BatchAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetBatchIndexes")
        {
        }

        protected override ObjectParameter[] GetEntityIndexParameters(int userID, DateTime fromDate, DateTime toDate)
        {
            ObjectParameter[] baseParameters = base.GetEntityIndexParameters(userID, fromDate, toDate);

            return new ObjectParameter[] { baseParameters[0], baseParameters[1], baseParameters[2], new ObjectParameter("FillingLineID", (int)GlobalVariables.FillingLineID), new ObjectParameter("ActiveOption", (int)(this.RepositoryBag["ActiveOption"] != null ? this.RepositoryBag["ActiveOption"] : GlobalEnums.ActiveOption.Both)) };
        }

        public List<BatchMaxNo> GetBatchMaxNo(int? fillingLineID, int? commodityID, string code)
        {
            return base.TotalSmartCodingEntities.GetBatchMaxNoByCode(fillingLineID, commodityID, code).ToList();
        }

        public List<BatchMaxNo> GetBatchMaxNo(int? fillingLineID, int? commodityID, int? entryMonthID)
        {
            return base.TotalSmartCodingEntities.GetBatchMaxNoByEntryMonthID(fillingLineID, commodityID, entryMonthID).ToList();
        }

        public List<BatchAvailable> GetBatchAvailables(int? locationID, int? deliveryAdviceID, int? transferOrderID, int? commodityID, bool withNullRow)
        {
            List<BatchAvailable> batchAvailables = base.TotalSmartCodingEntities.GetBatchAvailables(locationID, deliveryAdviceID, transferOrderID, commodityID).ToList();
            if (withNullRow) batchAvailables.Add(new BatchAvailable() { QuantityAvailable = 0, LineVolumeAvailable = 0 });
            return batchAvailables;
        }

        public List<BatchSumup> GetBatchSumups(DateTime fromDate, DateTime toDate, string entryStatusIDs)
        {
            return base.TotalSmartCodingEntities.GetBatchSumups(fromDate, toDate, entryStatusIDs).ToList();
        }

        public List<Carton> GetCartonCheckedOuts(int batchID, bool doubleChecked)
        {
            return this.TotalSmartCodingEntities.GetCartonCheckedOuts(batchID, doubleChecked).ToList();
        }
    }


}
