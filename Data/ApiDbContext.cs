using Microsoft.EntityFrameworkCore;
using OrderManager.Models;

namespace OrderManager.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    }
}
