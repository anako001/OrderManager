using Microsoft.EntityFrameworkCore;
using OrderManager.Data;
using OrderManager.Models;

namespace OrderManager.Core.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApiDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Order> GetById(Guid id)
        {
            try
            {
                return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Order>> GetAllByIds(Guid[] ids)
        {
            var orders = await _context.Orders.Where(o => ids.Contains(o.Id)).ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetByOrderType(string type)
        {
            try
            {
                var list = await _context.Orders.AsNoTracking().Where(
                    x => x.Type.ToLower() == type.ToLower()
                    ).ToListAsync();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteRange(IEnumerable<Order> orders)
        {
            _context.Orders.RemoveRange(orders);
        }
    }
}
