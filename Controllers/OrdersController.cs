using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManager.Core;
using OrderManager.Data;
using OrderManager.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace OrderManager.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _unitOfWork.Orders.All());
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetOrder([FromQuery] string id)
        {
            return Ok(await _unitOfWork.Orders.GetById(Guid.Parse(id)));
        }

        [HttpPost]
        [Route("create")]
        public  async Task<IActionResult> CreateOrder(Order order)
        {
            _unitOfWork.Orders.Add(order);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteOrders([FromQuery] string ids)
        {
            if (string.IsNullOrEmpty(ids)) return BadRequest();

            var idList = ids.Split(',').Select(id => Guid.Parse(id)).ToArray();
            var orders = await _unitOfWork.Orders.GetAllByIds(idList);

            if (orders == null || orders.Count == 0) return NotFound();

            _unitOfWork.Orders.DeleteRange(orders);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var existingOrder = await _unitOfWork.Orders.GetById(order.Id);
            if (existingOrder == null) return NotFound();
            await _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();
            var upDatedOrder = await _unitOfWork.Orders.GetById(order.Id);
            return Ok(upDatedOrder);
        }

        [HttpGet]
        [Route("OrderType")]
        public async Task<IActionResult> GetOrderByType([FromQuery] string type)
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
