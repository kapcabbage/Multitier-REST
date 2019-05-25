using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interfaces;
using DataAccess.POCO;

namespace DataAccess.Repositories
{
    public class AddressRepository : IAddressesRepository
    {
        protected readonly ICustomersContext _context;
        public AddressRepository(ICustomersContext context)
        {
            _context = context;
        }
        #region Implementation of IRepository<Addresses>

        public Addresses Get(string id)
        {
            return _context.Addresses.FirstOrDefault(x => x.AddressId == id);
        }

        public void Add(Addresses entity)
        {
            _context.Addresses.Add(entity);
        }

        public void Delete(Addresses entity)
        {
            _context.Addresses.Remove(entity);
        }

        public IEnumerable<Addresses> GetByCustomer(string id)
        {
            return _context.Addresses.Where(x => x.CustomerId == id).ToList();
        }

        #endregion
    }
}
