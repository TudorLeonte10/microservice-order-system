
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Dtos;
using OrderService.Application.Orders.Services;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IConfirmOrderService _confirmService;
        private readonly IOrderPaymentService _orderPaymentService;
        private readonly IUpdateOrderStatus _updateOrderStatus;

        public OrdersController(IOrderService orderService, IConfirmOrderService confirmService, IOrderPaymentService orderPaymentService, IUpdateOrderStatus updateOrderStatus)
        {
            _orderService = orderService;
            _confirmService = confirmService;
            _orderPaymentService = orderPaymentService;
            _updateOrderStatus = updateOrderStatus;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderById(id);
            
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var orderId = await _orderService.CreateOrder(request);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { id = orderId});
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmOrder([FromRoute] Guid id)
        {
            var order = await _confirmService.ConfirmOrder(id);
            return Ok(order);
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PayForOrder([FromRoute] Guid id)
        {
            var order = await _orderPaymentService.PayForOrder(id);
            return Ok(order);
        }

        [HttpPost("{id}/mark-as-paid")]
        public async Task<IActionResult> MarkAsPaid([FromRoute] Guid id)
        {
            await _updateOrderStatus.UpdateStatus(id);
            return Ok();
        }
    }
}
