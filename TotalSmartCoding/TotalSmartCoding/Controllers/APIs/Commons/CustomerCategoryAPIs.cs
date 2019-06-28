using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;
using TotalCore.Repositories.Commons;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class CustomerCategoryAPIs
    {
        private readonly ICustomerCategoryAPIRepository customerCategoryAPIRepository;

        public CustomerCategoryAPIs(ICustomerCategoryAPIRepository customerCategoryAPIRepository)
        {
            this.customerCategoryAPIRepository = customerCategoryAPIRepository;
        }


        public ICollection<CustomerCategoryIndex> GetCustomerCategoryIndexes()
        {
            return this.customerCategoryAPIRepository.GetEntityIndexes<CustomerCategoryIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<CustomerCategoryBase> GetCustomerCategoryBases()
        {
            return this.customerCategoryAPIRepository.GetCustomerCategoryBases();
        }

    }
}
