using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
    }

    public interface ICustomerAPIRepository : IGenericAPIRepository
    {
        IList<CustomerBase> GetCustomerBases(bool isCustomer, bool isReceiver);
        IList<CustomerTree> GetCustomerTrees();
    }
}

