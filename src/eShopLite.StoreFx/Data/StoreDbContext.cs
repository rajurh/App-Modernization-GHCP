using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using eShopLite.StoreFx.Models;

namespace eShopLite.StoreFx.Data
{
    /// <summary>
    /// Database context for the eShopLite store.
    /// </summary>
    public class StoreDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Products table.
        /// </summary>
        public DbSet<Product> Products { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Stores table.
        /// </summary>
        public DbSet<StoreInfo> Stores { get; set; } = null!;

        /// <summary>
        /// Configures the database schema using Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database schema.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Description)
                    .HasMaxLength(1000);
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500);
            });

            // Configure StoreInfo entity
            modelBuilder.Entity<StoreInfo>(entity =>
            {
                entity.ToTable("StoreInfo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);
                entity.Property(e => e.Hours)
                    .HasMaxLength(100);
            });
        }
    }

    /// <summary>
    /// Provides methods for seeding the database with initial data.
    /// </summary>
    public static class StoreDbContextSeed
    {
        /// <summary>
        /// Seeds the database with initial products and stores data.
        /// </summary>
        /// <param name="context">The database context.</param>
        public static void Seed(StoreDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if data already exists
            if (context.Products.Any())
            {
                return; // DB has been seeded
            }

            var products = new List<Product>
            {
                new() { Id = 1, Name = "Solar Powered Flashlight", Description = "A fantastic product for outdoor enthusiasts", Price = 19.99m, ImageUrl = "product1.png" },
                new() { Id = 2, Name = "Hiking Poles", Description = "Ideal for camping and hiking trips", Price = 24.99m, ImageUrl = "product2.png" },
                new() { Id = 3, Name = "Outdoor Rain Jacket", Description = "This product will keep you warm and dry in all weathers", Price = 49.99m, ImageUrl = "product3.png" },
                new() { Id = 4, Name = "Survival Kit", Description = "A must-have for any outdoor adventurer", Price = 99.99m, ImageUrl = "product4.png" },
                new() { Id = 5, Name = "Outdoor Backpack", Description = "This backpack is perfect for carrying all your outdoor essentials", Price = 39.99m, ImageUrl = "product5.png" },
                new() { Id = 6, Name = "Camping Cookware", Description = "This cookware set is ideal for cooking outdoors", Price = 29.99m, ImageUrl = "product6.png" },
                new() { Id = 7, Name = "Camping Stove", Description = "This stove is perfect for cooking outdoors", Price = 49.99m, ImageUrl = "product7.png" },
                new() { Id = 8, Name = "Camping Lantern", Description = "This lantern is perfect for lighting up your campsite", Price = 19.99m, ImageUrl = "product8.png" },
                new() { Id = 9, Name = "Camping Tent", Description = "This tent is perfect for camping trips", Price = 99.99m, ImageUrl = "product9.png" },
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var stores = new List<StoreInfo>
            {
                new() { Id = 1, Name = "Outdoor Store", City = "Seattle", State = "WA", Hours = "9am - 5pm" },
                new() { Id = 2, Name = "Camping Supplies", City = "Portland", State = "OR", Hours = "10am - 6pm" },
                new() { Id = 3, Name = "Hiking Gear", City = "San Francisco", State = "CA", Hours = "11am - 7pm" },
                new() { Id = 4, Name = "Fishing Equipment", City = "Los Angeles", State = "CA", Hours = "8am - 4pm" },
                new() { Id = 5, Name = "Climbing Gear", City = "Denver", State = "CO", Hours = "9am - 5pm" },
                new() { Id = 6, Name = "Cycling Supplies", City = "Austin", State = "TX", Hours = "10am - 6pm" },
                new() { Id = 7, Name = "Winter Sports Gear", City = "Salt Lake City", State = "UT", Hours = "11am - 7pm" },
                new() { Id = 8, Name = "Water Sports Equipment", City = "Miami", State = "FL", Hours = "8am - 4pm" },
                new() { Id = 9, Name = "Outdoor Clothing", City = "New York", State = "NY", Hours = "9am - 5pm" }
            };

            context.Stores.AddRange(stores);
            context.SaveChanges();
        }
    }
}