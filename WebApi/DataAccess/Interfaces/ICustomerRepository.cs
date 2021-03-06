﻿using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.POCO;

namespace DataAccess.Interfaces
{
    public interface ICustomerRepository : IRepository<Customers>
    {
            IEnumerable<Customers> GetAll();
    }
}
