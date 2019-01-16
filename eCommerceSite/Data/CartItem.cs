using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public Cart Cart { get; set; }
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public Card Card { get; set; }
        public int CardID { get; set; }
    }
}
