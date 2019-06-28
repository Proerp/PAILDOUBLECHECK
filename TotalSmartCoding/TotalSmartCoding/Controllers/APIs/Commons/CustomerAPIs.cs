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
    public class CustomerAPIs
    {
        private readonly ICustomerAPIRepository customerAPIRepository;

        public CustomerAPIs(ICustomerAPIRepository customerAPIRepository)
        {
            this.customerAPIRepository = customerAPIRepository;
        }


        public ICollection<CustomerIndex> GetCustomerIndexes(bool isCustomers)
        {
            this.customerAPIRepository.RepositoryBag["IsCustomers"] = isCustomers;
            return this.customerAPIRepository.GetEntityIndexes<CustomerIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<CustomerBase> GetCustomerBases() { return this.GetCustomerBases(true, true); }
        public IList<CustomerBase> GetCustomerBases(bool isCustomer, bool isReceiver)
        {
            return this.customerAPIRepository.GetCustomerBases(isCustomer, isReceiver);
        }

        public IList<CustomerTree> GetCustomerTrees()
        {
            return this.customerAPIRepository.GetCustomerTrees();
        }
    }
}
