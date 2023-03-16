using OrderManager.Models;

namespace OrderManager.Core
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetByOrderType(string type);
        Task<Order> GetById(Guid id);
    }
}
