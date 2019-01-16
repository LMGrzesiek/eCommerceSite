using System;
using System.Collections.Generic;

namespace eCommerceSite.Data
{
    public partial class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
        public int OrderId { get; set; }
        public string Details { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
