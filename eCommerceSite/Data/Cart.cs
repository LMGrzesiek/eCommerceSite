using eCommerceSite.Data;
using System;
using System.Collections.Generic;

namespace eCommerceSite.Data
{

    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }
        public int CartID { get; set; }
        public ApplicationUser User { get; set; }
        public string AnonymousIdentifier { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}