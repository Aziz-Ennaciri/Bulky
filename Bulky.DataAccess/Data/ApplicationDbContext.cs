using BulkyModels;
using BulkyModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyDataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData( 
                new Category { id=1,Name="Action",DisplayOrder=1 },
                new Category { id=2,Name="shi",DisplayOrder=2 },
                new Category { id=3,Name="faa",DisplayOrder=3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    categoryId = 1,
                    imageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    categoryId = 1,
                    imageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    categoryId = 1,
                    imageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    categoryId = 2,
                    imageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    categoryId = 2,
                    imageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    categoryId = 3,
                    imageUrl = ""
                }
            );

            modelBuilder.Entity<Company>().HasData(
            new Company
                {
                    Id = 1,
                    Name = "Tech Corp",
                    StreetAddress = "123 Tech Avenue",
                    City = "San Francisco",
                    State = "CA",
                    PostalCode = "94103",
                    PhoneNumber = "555-1234"
                },
            new Company
                {
                    Id = 2,
                    Name = "Innovate LLC",
                    StreetAddress = "456 Innovation Drive",
                    City = "Austin",
                    State = "TX",
                    PostalCode = "73301",
                    PhoneNumber = "555-5678"
                },
            new Company
                {
                    Id = 3,
                    Name = "Bright Futures Inc.",
                    StreetAddress = "789 Future Blvd",
                    City = "Seattle",
                    State = "WA",
                    PostalCode = "98101",
                    PhoneNumber = "555-7890"
                },
            new Company
                {
                    Id = 4,
                    Name = "Pioneer Tech",
                    StreetAddress = "101 Pioneer Way",
                    City = "Denver",
                    State = "CO",
                    PostalCode = "80202",
                    PhoneNumber = "555-1010"
                },
            new Company
                {
                    Id = 5,
                    Name = "Eco Innovators",
                    StreetAddress = "202 Green Lane",
                    City = "Portland",
                    State = "OR",
                    PostalCode = "97209",
                    PhoneNumber = "555-2020"
                },
            new Company
                {
                    Id = 6,
                    Name = "NextGen Solutions",
                    StreetAddress = "303 Progress Rd",
                    City = "Chicago",
                    State = "IL",
                    PostalCode = "60601",
                    PhoneNumber = "555-3030"
                }
        );

        }
    }
}
