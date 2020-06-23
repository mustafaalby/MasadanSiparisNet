using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectRestaurant.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRestaurant.Data.Context
{
    public class RestaurantDbContext:IdentityDbContext<Restaurant>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options): base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Restaurant>().
                HasOne(x => x.RestaurantAddress).
                WithOne(y => y.Restaurant).
                HasForeignKey<RestaurantAddress>(z => z.RestaurantId);
            builder.Entity<Restaurant>()
                .HasMany(x => x.Table).
                WithOne(y => y.Restaurant).
                HasForeignKey(z => z.RestaurantId);
            builder.Entity<Table>().
                HasMany(x => x.Sessions).
                WithOne(y => y.Table).
                HasForeignKey(z => z.TableId);
            builder.Entity<Session>().
                HasMany(x => x.Order).
                WithOne(y => y.Session).
                HasForeignKey(z => z.SessionId);
            builder.Entity<Menu>().
                HasOne(x => x.ProductType).
                WithOne(y => y.Menu).
                HasForeignKey<Menu>(z => z.ProductTypeId);
            
            base.OnModelCreating(builder);
        }

        public DbSet<Table> Table { get; set; }
        public  DbSet<Menu> Menu { get; set; }
        public  DbSet<Session> Session { get; set; }
        
        public DbSet<RestaurantAddress> RestaurantAddress { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
    }
}
