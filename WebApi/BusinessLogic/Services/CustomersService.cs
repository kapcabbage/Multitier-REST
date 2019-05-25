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
                if (!CountryValidator.Validate(customer.Country))
                {
                    result.Data = false;
                    result.Status = eOperationStatus.GeneralError;
                    result.ExceptionMessage = "Country code does not exists";
                   
                }
                else if (_customerRepo.Get(customer.CustomerId) != null)
                {
                    result.Data = false;
                    result.Status = eOperationStatus.GeneralError;
                    result.ExceptionMessage = $"Customer: {customer.CustomerId} already exists";
                }
                else
                {
                    _customerRepo.Add(customer);
                    var saveResult = _context.SaveChanges();
                    result.Data = true;
                    result.Status = eOperationStatus.Success;
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

        public OperationResult<bool> UpdateCustomer(Customers customer)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (!CountryValidator.Validate(customer.Country))
                {
                    result.Data = false;
                    result.Status = eOperationStatus.GeneralError;
                    result.ExceptionMessage = "Country code does not exists";
                }
                else
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
               
            }
            catch (Exception ex)
            {
                result.Status = eOperationStatus.GeneralError;
                result.ExceptionMessage = ex.Message;
                result.Data = false;
            }

            return result;
        }

        public OperationResult<Customers> GetCustomer(string id)
        {
            var result = new OperationResult<Customers>();
            var customer = _customerRepo.Get(id);
            if (customer != null)
            {
                result.Data = customer;
                result.Status = eOperationStatus.Success;

            }
            else
            {
                result.Data = null;
                result.Status = eOperationStatus.NotFound;
                
            }

            return result;
        }

        public OperationResult<bool> DeleteCustomer(string id)
        {
            var result = new OperationResult<bool>();
            try
            {
                var customer = _customerRepo.Get(id);
                if (customer != null)
                {
                    _customerRepo.Delete(customer);
                    foreach (var address in customer.Addresses)
                    {
                        _addrRepo.Delete(address);
                    }
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
                    }
                }
                else
                {
                    result.Data = false;
                    result.Status = eOperationStatus.NotFound;

                }

            }
            catch (Exception ex)
            {
                result.Data = false;
                result.Status = eOperationStatus.GeneralError;
                result.ExceptionMessage = ex.Message;
            }
           
            return result;
        }

        public OperationResult<IEnumerable<Addresses>> GetCustomerAddresses(string id)
        {
            var result = new OperationResult<IEnumerable<Addresses>>();
            var customer = _customerRepo.Get(id);
            if (customer == null)
            {
                result.Status = eOperationStatus.NotFound;
            }
            else
            {
                result.Data = _addrRepo.GetByCustomer(id);
                result.Status = eOperationStatus.Success;
            }

            return result;
        }

        public OperationResult<Addresses> GetAddress(string id, string addressId)
        {
            var result = new OperationResult<Addresses>();
            var customer = _customerRepo.Get(id);
            if (customer != null)
            {
                var address = _addrRepo.Get(addressId);
                if (address != null && address.CustomerId == id)
                {
                    
                    result.Data = address;
                    result.Status = eOperationStatus.Success;
                }
                else
                {
                    result.Status = eOperationStatus.NotFound;
                    result.ExceptionMessage = "Address not found";
                }

            }
            else
            {
                result.Status = eOperationStatus.NotFound;
                result.ExceptionMessage = "Customer not found";

            }
            return result;
        }

        public OperationResult<bool> AddAddress(string id, Addresses address)
        {
            var result = new OperationResult<bool>();
            try
            {
                var customer = _customerRepo.Get(id);
                if (customer != null)
                {
                    if (!CountryValidator.Validate(address.Country))
                    {
                        result.Data = false;
                        result.Status = eOperationStatus.GeneralError;
                        result.ExceptionMessage = "Country code does not exists";
                    }
                    else if(_addrRepo.Get(address.AddressId) != null)
                    {
                        result.Data = false;
                        result.Status = eOperationStatus.GeneralError;
                        result.ExceptionMessage = $"Address:{address.AddressId} already exists";
                    }
                    else
                    {
                        address.CustomerId = id;
                        _addrRepo.Add(address);

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
                        }
                    }

                }
                else
                {
                    result.Data = false;
                    result.Status = eOperationStatus.NotFound;
                    result.ExceptionMessage = "Customer not found";

                }
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ExceptionMessage = ex.Message;
                result.Status = eOperationStatus.GeneralError;
            }
            return result;
        }
        public OperationResult<bool> UpdateAddress(string id, Addresses address)
        {
            var result = new OperationResult<bool>();
            try
            {
                var customer = _customerRepo.Get(id);
                if (customer != null)
                {
                    if (!CountryValidator.Validate(address.Country))
                    {
                        result.Data = false;
                        result.Status = eOperationStatus.GeneralError;
                        result.ExceptionMessage = "Country code does not exists";
                    }
                    else
                    {
                        var addressToUpdate = _addrRepo.Get(address.AddressId);
                        if (addressToUpdate != null && addressToUpdate.CustomerId == id)
                        {
                            addressToUpdate.AddressType = address.AddressType;
                            addressToUpdate.City = address.City;
                            addressToUpdate.Country = address.Country;
                            addressToUpdate.Name = address.Name;
                            addressToUpdate.Street = address.Street;
                            addressToUpdate.Zip = address.Zip;
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
                            }
                        }
                        else
                        {
                            result.Data = false;
                            result.Status = eOperationStatus.NotFound;
                            result.ExceptionMessage = "Address not found";
                        }

                    }

                }
                else
                {
                    result.Data = false;
                    result.Status = eOperationStatus.NotFound;
                    result.ExceptionMessage = "Customer not found";

                }
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ExceptionMessage = ex.Message;
                result.Status = eOperationStatus.GeneralError;
            }
            return result;
        }
        public OperationResult<bool> DeleteAddress(string id, string addressId)
        {
            var result = new OperationResult<bool>();
            try
            {
                var customer = _customerRepo.Get(id);
                if (customer != null)
                {
                    var address = _addrRepo.Get(addressId);
                    if (address != null && address.CustomerId == id)
                    {
                        _addrRepo.Delete(address);
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
                        }
                    }
                    else
                    {
                        result.Data = false;
                        result.Status = eOperationStatus.NotFound;
                        result.ExceptionMessage = "Address not found";
                    }

                }
                else
                {
                    result.Data = false;
                    result.Status = eOperationStatus.NotFound;
                    result.ExceptionMessage = "Customer not found";

                }
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.ExceptionMessage = ex.Message;
                result.Status = eOperationStatus.GeneralError;
            }
           
            return result;
        }
    }
}
