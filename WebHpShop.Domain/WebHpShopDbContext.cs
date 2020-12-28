using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebHpShop.Domain.Entities;

namespace WebStore.Domain
{
    public class WebHpShopDbContext : DbContext
    {
        public WebHpShopDbContext(DbContextOptions<WebHpShopDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
            .HasKey(p => new { p.OrderId, p.ProductId });
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
