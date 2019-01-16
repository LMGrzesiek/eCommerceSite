using System;
using System.Collections.Generic;

namespace eCommerceSite.data
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AddressId { get; set; }
    }
}
