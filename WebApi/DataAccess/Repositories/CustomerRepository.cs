using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interfaces;
using DataAccess.POCO;

namespace DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly ICustomersContext _context;
        public CustomerRepository(ICustomersContext context)
        {
            _context = context;
        }

        #region Implementation of ICustomerRepository

        public IEnumerable<Customers> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customers Get(string id)
        {
            return _context.Customers.FirstOrDefault(x => x.CustomerId == id);
        }

        public void Add(Customers entity)
        {
            _context.Customers.Add(entity);
        }

        public void Delete(Customers entity)
        {
            _context.Customers.Remove(entity);
        }

        #endregion
    }
}
