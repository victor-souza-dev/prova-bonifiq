using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models.DTOs;
using ProvaPub.Models.Entities;

namespace ProvaPub.Infra.Db;


public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NSerial>().ToTable("NSerials");

        modelBuilder.Entity<Customer>().HasData(getCustomerSeed());
        modelBuilder.Entity<Product>().HasData(getProductSeed());
        modelBuilder.Entity<NSerial>().HasKey(e => e.IdNSerial);
    }

    private Customer[] getCustomerSeed()
    {
        List<Customer> result = new();
        for (int i = 0; i < 20; i++)
        {
            result.Add(new Customer()
            {
                Id = i + 1,
                Name = new Faker().Person.FullName,
            });
        }
        return result.ToArray();
    }
    private Product[] getProductSeed()
    {
        List<Product> result = new();
        for (int i = 0; i < 20; i++)
        {
            result.Add(new Product()
            {
                Id = i + 1,
                Name = new Faker().Commerce.ProductName()
            });
        }
        return result.ToArray();
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<NSerial> NSerials { get; set; }
}
