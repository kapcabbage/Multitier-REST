using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.POCO;

namespace DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
            IEnumerable<Customers> GetAll();
            Customers Get(string id);
            void Add(Customers entity);
            void Update(Customers entity);
            void Delete(string id);
    }
}
