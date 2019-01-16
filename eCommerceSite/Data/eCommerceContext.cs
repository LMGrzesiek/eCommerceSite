using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCommerceSite.data
{
    public partial class eCommerceContext : DbContext
    {
        public eCommerceContext()
        {
        }

        public eCommerceContext(DbContextOptions<eCommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> User { get; set; }

        // Unable to generate entity type for table 'dbo.cartProduct'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.cartCustomer'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.addressCustomer'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.orderProduct'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.orderCustomer'. Please see the warning messages.



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.AddressId)
                    .HasColumnName("addressID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("addressLine1")
                    .HasMaxLength(100);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("addressLine2")
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

                entity.Property(e => e.PostalCode).HasColumnName("postalCode");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.CartId)
                    .HasColumnName("cartID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CookieId).HasColumnName("cookieID");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("dateAdded")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("addressID");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("orders");

                entity.Property(e => e.OrderId)
                    .HasColumnName("orderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Details)
                    .HasColumnName("details")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductDescription)
                    .HasColumnName("productDescription")
                    .HasMaxLength(1000);

                entity.Property(e => e.ProductImage)
                    .HasColumnName("productImage")
                    .HasMaxLength(100);

                entity.Property(e => e.ProductName)
                    .HasColumnName("productName")
                    .HasMaxLength(100);

                entity.Property(e => e.ProductPrice)
                    .HasColumnName("productPrice")
                    .HasColumnType("money");

                entity.Property(e => e.ProductQuantity).HasColumnName("productQuantity");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100);
            });
        }
    }
}
