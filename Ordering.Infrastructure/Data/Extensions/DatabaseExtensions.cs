using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        } 

        private static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            await SeedCustomerAsync(dbContext);
            await SeedProductsAsync(dbContext);
            await SeedOrdersAndItemsAsyn(dbContext);
        }

        private static async Task SeedCustomerAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Customers.AnyAsync())
            {
                await dbContext.Customers.AddRangeAsync(InitialData.Customers);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedProductsAsync(ApplicationDbContext dbContext)
        {
            if(!await dbContext.Products.AnyAsync())
            {
                await dbContext.Products.AddRangeAsync(InitialData.Products);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedOrdersAndItemsAsyn(ApplicationDbContext dbContext)
        {
            if(!await dbContext.Orders.AnyAsync())
            {
                await dbContext.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await dbContext.SaveChangesAsync() ;
            }
        }
    }
}
