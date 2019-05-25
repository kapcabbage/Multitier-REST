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
            throw new NotImplementedException();
        }

        public void Update(Addresses entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
