using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "CustomerEditable", null, "CustomerDeletable")
        {
        }
    }








    public class CustomerAPIRepository : GenericAPIRepository, ICustomerAPIRepository
    {
        public CustomerAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetCustomerIndexes")
        {
        }

        protected override ObjectParameter[] GetEntityIndexParameters(int userID, System.DateTime fromDate, System.DateTime toDate)
        {
            ObjectParameter[] baseParameters = base.GetEntityIndexParameters(userID, fromDate, toDate);

            return new ObjectParameter[] { baseParameters[0], baseParameters[1], baseParameters[2], new ObjectParameter("IsCustomers", (bool)(this.RepositoryBag["IsCustomers"] != null ? this.RepositoryBag["IsCustomers"] : 0)) };
        }

        public IList<CustomerBase> GetCustomerBases(bool isCustomer, bool isReceiver)
        {
            return this.TotalSmartCodingEntities.GetCustomerBases(isCustomer, isReceiver).ToList();
        }


        public IList<CustomerTree> GetCustomerTrees()
        {
            return this.TotalSmartCodingEntities.GetCustomerTrees().ToList();
        }

    }

}
