using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ICustomerCategoryRepository : IGenericRepository<CustomerCategory>
    {

    }

    public interface ICustomerCategoryAPIRepository : IGenericAPIRepository
    {
        IList<CustomerCategoryBase> GetCustomerCategoryBases();
    }
}
