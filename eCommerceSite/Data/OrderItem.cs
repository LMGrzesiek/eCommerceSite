using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string CardName { get; set; }
        public string CardDescription { get; set; }
        [Column(TypeName = "Money")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "Money")]
        public decimal LineTotal { get; set; }

    }
}
