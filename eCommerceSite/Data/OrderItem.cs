using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public Card Card { get; set; }
        public int CardID { get; set; }
    }
}
