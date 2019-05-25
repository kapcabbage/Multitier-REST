using System;
using System.Collections.Generic;

namespace DataAccess.POCO
{
    public partial class Customers
    {
        public Customers()
        {
            Addresses = new HashSet<Addresses>();
        }

        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
    }
}