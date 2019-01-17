using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceSite.Data
{
    public partial class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
        public int ID { get; set; }
        public string TrackingNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }
        public DateTime PlacementDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        [Column(TypeName = "Money")]
        public decimal SubTotal { get; set; }
        [Column(TypeName = "Money")]
        public decimal Tax { get; set; }
        [Column(TypeName = "Money")]
        public decimal Shipping { get; set; }
        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        public string ShippingStreet1 { get; set; }
        public string ShippingStreet2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingRecipient { get; set; }
        public string ShippingInstructions { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
