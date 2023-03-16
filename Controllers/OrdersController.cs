using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using System.Linq;

namespace OrderManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private static List<Order> _orders = new List<Order>()
        {
            new Order()
            {
                Id = Guid.NewGuid(),
                Type = OrderType.Standard.ToString(),
                CustomerName = "Dave",
                CreatedDate = DateTime.Now,
                CreatedByUsername = "Davie504"
            },

            new Order()
            {
                Id = Guid.NewGuid(),
                Type = OrderType.SaleOrder.ToString(),
                CustomerName = "George",
                CreatedDate = DateTime.Now,
                CreatedByUsername = "Georgie504"
            },

            new Order()
            {
                Id = Guid.NewGuid(),
                Type = OrderType.PurchaseOrder.ToString(),
                CustomerName = "Cathy",
                CreatedDate = DateTime.Now,
                CreatedByUsername = "Catwoman"
            },
        };

        [HttpGet]
        [Route("Get")]
        public IActionResult GetOrders()
        {
            return Ok(_orders);
        }

        [HttpPost]
        [Route("Create")]
        public  IActionResult CreateOrder(Order order)
        {
            _orders.Add(order);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteOrder(Guid id)
        {
            var order = _orders.FirstOrDefault(x => x.Id.Equals(id));
            if (order == null) return NotFound();
            
            _orders.Remove(order);
            return NoContent();
        }

        [HttpPatch]
        [Route("Update")]
        public IActionResult UpdateOrder(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(x => x.Id == order.Id);
            if (existingOrder == null) return NotFound();

            existingOrder.CustomerName = order.CustomerName;
            existingOrder.Type = order.Type;
            existingOrder.CreatedDate = order.CreatedDate;
            existingOrder.CreatedByUsername = order.CreatedByUsername;

            return NoContent();
        }

    }
}
