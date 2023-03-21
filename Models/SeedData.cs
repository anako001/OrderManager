using Microsoft.EntityFrameworkCore;
using OrderManager.Data;

namespace OrderManager.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApiDbContext>>()))
            {
                if (context.Orders.Any())
                {
                    return;   // DB has been seeded
                }
                context.Orders.AddRange(
                    new Order
                    {
                        Type = OrderType.SaleOrder.ToString(),
                        CustomerName = "Jones",
                        CreatedByUsername = "Zach",
                        CreatedDate = DateTime.Now,                    
                    },
                    new Order
                    {
                        Type = OrderType.PurchaseOrder.ToString(),
                        CustomerName = "Jane",
                        CreatedByUsername = "Codi",
                        CreatedDate = DateTime.Now,
                    },
                    new Order
                    {
                        Type = OrderType.TransferOrder.ToString(),
                        CustomerName = "James",
                        CreatedByUsername = "Brian",
                        CreatedDate = DateTime.Now,
                    },
                    new Order
                    {
                        Type = OrderType.Standard.ToString(),
                        CustomerName = "John",
                        CreatedByUsername = "Catie",
                        CreatedDate = DateTime.Now,
                    }
                );
                context.SaveChanges();
            }
        }
    }

}
