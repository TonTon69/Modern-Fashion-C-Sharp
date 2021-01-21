using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PlayerUI.Models
{
    public partial class QLHHTShop : DbContext
    {
        public QLHHTShop()
            : base("name=QLHHTShop")
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<HauntDetail> HauntDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHaunt> OrderHaunts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }
        public virtual DbSet<productall> productalls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.AccountName)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Administrator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Administrator>()
                .HasMany(e => e.OrderHaunts)
                .WithRequired(e => e.Administrator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.BuyTotal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HauntDetail>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<HauntDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HauntDetail>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HauntDetail>()
                .Property(e => e.SizeCode)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.ToTalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.SizeCode)
                .IsUnicode(false);

            modelBuilder.Entity<OrderHaunt>()
                .Property(e => e.ToTalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OrderHaunt>()
                .HasMany(e => e.HauntDetails)
                .WithRequired(e => e.OrderHaunt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.BuyPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.SellPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.HauntDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductDetail>()
                .Property(e => e.SizeCode)
                .IsUnicode(false);

            modelBuilder.Entity<ProductDetail>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSize>()
                .Property(e => e.SizeCode)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSize>()
                .HasMany(e => e.ProductDetails)
                .WithRequired(e => e.ProductSize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<productall>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<productall>()
                .Property(e => e.BuyPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<productall>()
                .Property(e => e.SellPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<productall>()
                .Property(e => e.SizeCode)
                .IsUnicode(false);
        }
    }
}
