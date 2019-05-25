using System.Collections.Generic;
using DataAccess.Common;
using DataAccess.POCO;

namespace BusinessLogic.Interfaces
{
    public interface ICustomersService
    {
        OperationResult<IEnumerable<Customers>> GetAllCustomers();
        OperationResult<bool> AddCustomer(Customers customer);
        OperationResult<bool> UpdateCustomer(Customers customer);
    }
}
