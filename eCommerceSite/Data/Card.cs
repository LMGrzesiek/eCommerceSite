using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public class Card
    {
        public Card()
        {
            this.Carts = new HashSet<Cart>();
        }

        public int CardID { get; set; } //Because I called this column "ID", and typed it "int', EF will assume it is a PK and it will make it auto-numuerable
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public ICollection<Cart> Carts { get; set; }
    }
}