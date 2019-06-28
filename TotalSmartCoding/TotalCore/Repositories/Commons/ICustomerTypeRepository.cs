using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICustomerTypeRepository : IGenericRepository<CustomerType>
    {

    }

    public interface ICustomerTypeAPIRepository : IGenericAPIRepository
    {
        IList<CustomerTypeBase> GetCustomerTypeBases();
    }
}
