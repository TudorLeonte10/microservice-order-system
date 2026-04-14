
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

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
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
            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, null);
        }
    }
}
