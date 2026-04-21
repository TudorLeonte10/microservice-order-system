using InventoryService.Application.Products.Dtos;
using InventoryService.Application.Products.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase 
    {
        private readonly IReserveProductService _reserveProductService;
        private readonly ICreateProductService _createProductService;
        private readonly IGetProductByIdService _getProductByIdService;

        public ProductController(IReserveProductService reserveProductService, ICreateProductService createProductService, IGetProductByIdService getProductByIdService)
        {
            _reserveProductService = reserveProductService;
            _createProductService = createProductService;
            _getProductByIdService = getProductByIdService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = await _createProductService.CreateProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var product = await _getProductByIdService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost("{id}/reserve")]
        public async Task<IActionResult> ReserveProduct([FromRoute] Guid id, [FromBody] ReserveProductRequest request)
        {
            await _reserveProductService.ReserveProduct(id, request.Quantity);
            return Ok();
        }
    }
}
