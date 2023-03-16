using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManager.Core;
using OrderManager.Data;
using OrderManager.Models;
using System.Linq;

namespace OrderManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _unitOfWork.Orders.All());
        }

        [HttpPost]
        [Route("Create")]
        public  async Task<IActionResult> CreateOrder(Order order)
        {
            _unitOfWork.Orders.Add(order);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _unitOfWork.Orders.GetById(id);
            if (order == null) return NotFound();
            
            await  _unitOfWork.Orders.Delete(order);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPatch]
        [Route("Update")]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var existingOrder = await _unitOfWork.Orders.GetById(order.Id);
            if (existingOrder == null) return NotFound();
            await _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpGet]
        [Route("OrderType")]
        public async Task<IActionResult> GetOrderByType(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException("The vlaue passed cannot be null or empty");

            var isStandardOrder = type.ToLower() == OrderType.Standard.ToString().ToLower();
            var isSaleOrder = type.ToLower() == OrderType.SaleOrder.ToString().ToLower();
            var isPurchaseOrder = type.ToLower() == OrderType.PurchaseOrder.ToString().ToLower();
            var isTransferOrder = type.ToLower() == OrderType.TransferOrder.ToString().ToLower();
            var isReturnOrder = type.ToLower() == OrderType.ReturnOrder.ToString().ToLower();

            var orderTypeFound = isStandardOrder || isSaleOrder || isPurchaseOrder || isTransferOrder || isReturnOrder;

            if (!orderTypeFound) throw new ArgumentException("The Value passed cannot be found");

            var orders = await _unitOfWork.Orders.GetByOrderType(type);
            if (orders == null) return NotFound();
            return Ok(orders);
        }

    }
}
