using System.Collections.Generic;
using DataAccess.Common;
using DataAccess.POCO;

namespace BusinessLogic.Interfaces
{
    public interface ICustomersService
    {
        OperationResult<IEnumerable<Customers>> GetAllCustomers();
        OperationResult<Customers> GetCustomer(string id);
        OperationResult<IEnumerable<Addresses>> GetCustomerAddresses(string id);
        OperationResult<bool> AddAddress(string id, Addresses address);
        OperationResult<bool> DeleteAddress(string id, string addressId);
        OperationResult<bool> UpdateAddress(string id, Addresses address);
        OperationResult<Addresses> GetAddress(string id, string addresId);
        OperationResult<bool> AddCustomer(Customers customer);
        OperationResult<bool> UpdateCustomer(Customers customer);
        OperationResult<bool> DeleteCustomer(string id);
    }
}
