using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;



using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Productions;
using TotalBase;

namespace TotalSmartCoding.Controllers.APIs.Productions
{
    public class BatchAPIs
    {
        private readonly IBatchAPIRepository batchAPIRepository;

        public BatchAPIs(IBatchAPIRepository batchAPIRepository)
        {
            this.batchAPIRepository = batchAPIRepository;
        }


        public ICollection<BatchIndex> GetBatchIndexes(GlobalEnums.ActiveOption activeOption)
        {
            this.batchAPIRepository.RepositoryBag["ActiveOption"] = (int)activeOption;
            ICollection<BatchIndex> goodsReceiptIndexes = this.batchAPIRepository.GetEntityIndexes<BatchIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);

            return goodsReceiptIndexes;
        }

        public BatchIndex GetActiveBatchIndex()
        {
            BatchIndex goodsReceiptIndexes = this.GetBatchIndexes(GlobalEnums.ActiveOption.Active).Where(w => w.IsDefault).FirstOrDefault();

            return goodsReceiptIndexes;
        }

        public List<BatchMaxNo> GetBatchMaxNo(int? fillingLineID, int? commodityID, string code)
        {
            return this.batchAPIRepository.GetBatchMaxNo(fillingLineID, commodityID, code);
        }

        public List<BatchMaxNo> GetBatchMaxNo(int? fillingLineID, int? commodityID, int? entryMonthID)
        {
            return this.batchAPIRepository.GetBatchMaxNo(fillingLineID, commodityID, entryMonthID);
        }

        public IList<BatchAvailable> GetBatchAvailables(int? locationID, int? deliveryAdviceID, int? transferOrderID, int? commodityID, bool withNullRow)
        {
            return this.batchAPIRepository.GetBatchAvailables(locationID, deliveryAdviceID, transferOrderID, commodityID, withNullRow);
        }

        public List<BatchSumup> GetBatchSumups(DateTime fromDate, DateTime toDate, string entryStatusIDs)
        {
            return this.batchAPIRepository.GetBatchSumups(fromDate, toDate, entryStatusIDs);
        }
    }
}
