using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public ApplicationUser()
        {
            this.Orders = new HashSet<Order>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }


        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public int? CartId { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
