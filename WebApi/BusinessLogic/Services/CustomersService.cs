using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Interfaces;
using DataAccess.Common;
using DataAccess.Interfaces;
using DataAccess.POCO;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersContext _context;
        private readonly ICustomerRepository _customerRepo;
        private readonly IAddressesRepository _addrRepo;

        public CustomersService(ICustomersContext context)
        {
            _context = context;
            _customerRepo = new CustomerRepository(context);
            _addrRepo = new AddressRepository(context);
            
        }

        public OperationResult<IEnumerable<Customers>>  GetAllCustomers()
        {
            var result = new OperationResult<IEnumerable<Customers>>();
            result.Data = _customerRepo.GetAll();
            result.Status = eOperationStatus.Success;
            return result;
        }

        public OperationResult<bool> AddCustomer(Customers customer)
        {
            var result = new OperationResult<bool>();
            try
            {
                _customerRepo.Add(customer);
                var saveResult = _context.SaveChanges();
                result.Data = true;
                result.Status = eOperationStatus.Success;
            }
            catch (Exception ex)
            {
                result.Status = eOperationStatus.GeneralError;
                result.ExceptionMessage = ex.Message;
                result.Data = false;
            }

            return result;
        }

        public OperationResult<bool> UpdateCustomer(Customers customer)
        {
            var result = new OperationResult<bool>();
            try
            {
                var toUpdate = _customerRepo.Get(customer.CustomerId);
                toUpdate.Addresses = customer.Addresses;
                toUpdate.City = customer.City;
                toUpdate.Country = customer.Country;
                toUpdate.Name = customer.Name;
                toUpdate.Street = customer.Street;
                toUpdate.Zip = customer.Zip;
                var saveResult = _context.SaveChanges();
                if (saveResult > 0)
                {
                    result.Data = true;
                    result.Status = eOperationStatus.Success;
                }
                else
                {
                    result.Data = false;
                    result.Status = eOperationStatus.GeneralError;
                    result.ExceptionMessage = "Could not update Customer";
                }
            }
            catch (Exception ex)
            {
                result.Status = eOperationStatus.GeneralError;
                result.ExceptionMessage = ex.Message;
                result.Data = false;
            }

            return result;
        }
    }
}
