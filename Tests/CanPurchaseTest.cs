using Microsoft.EntityFrameworkCore;
using ProvaPub.Infra.Db;
using ProvaPub.Models.Entities;
using ProvaPub.Services;

namespace Tests;

public class CanPurchaseTest
{
    private DbContextOptions<TestDbContext> _ctx;

    public CanPurchaseTest()
    {
        _ctx = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task CanPurchase_ValidCustomerWithPurchase_ReturnsTrue()
    {
        using (var context = new TestDbContext(_ctx))
        {
            var customer = new Customer { Id = 1, Name = "Teste" };
            context.Customers.Add(customer);
            context.Orders.Add(new Order { CustomerId = customer.Id, OrderDate = DateTime.UtcNow });
            context.SaveChanges();
        }

        using (var context = new TestDbContext(_ctx))
        {
            var customerService = new CustomerService(context);

            bool verify = true;

            try
            {
                var result = await customerService.CanPurchase(1, 50);
            } catch
            {
                verify = false;
            }

            Assert.True(verify);
        }
    }

    [Fact]
    public async Task CanPurchase_ValidCustomerWithoutPurchase_ReturnsTrue()
    {
        using (var context = new TestDbContext(_ctx))
        {
            context.Customers.Add(new Customer { Id = 2, Name = "Teste2" });
            context.SaveChanges();
        }

        using (var context = new TestDbContext(_ctx))
        {
            var customerService = new CustomerService(context);

            var result = await customerService.CanPurchase(2, 50);

            Assert.True(result);
        }
    }

    [Fact]
    public async Task CanPurchase_InvalidCustomerId_ReturnsFalse()
    {
        using (var context = new TestDbContext(_ctx))
        {
            context.Customers.Add(new Customer { Id = 3, Name = "Teste3" });
            context.SaveChanges();
        }

        using (var context = new TestDbContext(_ctx))
        {
            var customerService = new CustomerService(context);

            bool verify = true;

            try
            {
                var result = await customerService.CanPurchase(8, 50);
            } catch
            {
                verify = false;
            }

            Assert.False(verify);
        }
    }

    [Fact]
    public async Task CanPurchase_InvalidPurchaseValue_ReturnsFalse()
    {
        using (var context = new TestDbContext(_ctx))
        {
            context.Customers.Add(new Customer { Id = 4, Name = "Teste4" });
            context.SaveChanges();
        }

        using (var context = new TestDbContext(_ctx))
        {
            var customerService = new CustomerService(context);

            bool verify = true;

            try
            {
                var result = await customerService.CanPurchase(4, -50);
            } catch
            {
                verify = false;
            }

            Assert.False(verify);
        }
    }

}
