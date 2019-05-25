using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.POCO;

namespace DataAccess.Interfaces
{
    public interface  IAddressesRepository : IRepository<Addresses>
    {
        IEnumerable<Addresses> GetByCustomer(string id);
    }
}
