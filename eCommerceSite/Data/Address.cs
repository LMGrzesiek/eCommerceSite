using System;
using System.Collections.Generic;

namespace eCommerceSite.Data
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? PostalCode { get; set; }
        public int? PhoneNumber { get; set; }
        
        public ApplicationUser User { get; set; }
    }
}
