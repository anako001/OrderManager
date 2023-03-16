using OrderManager.Core;
using OrderManager.Core.Repositories;

namespace OrderManager.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiDbContext _context;

        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(ApiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var _logger = loggerFactory.CreateLogger("logs");

            Orders = new OrderRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
