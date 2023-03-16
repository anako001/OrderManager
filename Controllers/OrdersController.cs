using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManager.Data;
using OrderManager.Models;
using System.Linq;

namespace OrderManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public OrdersController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpPost]
        [Route("Create")]
        public  async Task<IActionResult> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (order == null) return NotFound();
            
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch]
        [Route("Update")]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
            if (existingOrder == null) return NotFound();

            existingOrder.CustomerName = order.CustomerName;
            existingOrder.Type = order.Type;
            existingOrder.CreatedDate = order.CreatedDate;
            existingOrder.CreatedByUsername = order.CreatedByUsername;

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
