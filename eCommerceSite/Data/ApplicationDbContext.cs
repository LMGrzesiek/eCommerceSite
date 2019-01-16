using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eCommerceSite.data;

namespace eCommerceSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Example 2: This will create a new table called CardTypes.  Any "Dbnet" in tyhe ApplicationDbContext will become a new table.  I define the schema
        //for that table in a class project
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<eCommerceSite.data.Cart> Cart { get; set; }

    }

    //Example 1: This will change the builid-in AspNetUser table definition
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }

    }

    public class CardType
    {
        public int ID { get; set; } //Because I called this column "ID", and typed it "int', EF will assume it is a PK and it will make it auto-numuerable
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }


    }

    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string AnonymousIdentifier { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public int ID { get; set; }
        public Cart Cart { get; set; }
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public CardType CardType { get; set; }
        public int CardTypeID { get; set; }
    }


}
