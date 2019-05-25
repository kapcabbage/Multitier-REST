using System;
using System.Collections.Generic;

namespace DataAccess.POCO
{
    public partial class Addresses
    {
        public string AddressId { get; set; }
        public string CustomerId { get; set; }
        public string AddressType { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual Customers Customer { get; set; }
    }
}