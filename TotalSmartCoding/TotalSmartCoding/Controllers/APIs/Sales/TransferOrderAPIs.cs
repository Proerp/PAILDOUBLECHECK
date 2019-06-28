﻿using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Sales;

namespace TotalSmartCoding.Controllers.APIs.Sales
{
    public class TransferOrderAPIs
    {
        private readonly ITransferOrderAPIRepository transferOrderAPIRepository;

        public TransferOrderAPIs(ITransferOrderAPIRepository transferOrderAPIRepository)
        {
            this.transferOrderAPIRepository = transferOrderAPIRepository;
        }


        public ICollection<TransferOrderIndex> GetTransferOrderIndexes()
        {
            return this.transferOrderAPIRepository.GetEntityIndexes<TransferOrderIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }
    }
}
