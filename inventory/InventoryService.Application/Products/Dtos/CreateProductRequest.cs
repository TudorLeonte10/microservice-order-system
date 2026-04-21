using System.ComponentModel.DataAnnotations;

namespace InventoryService.Application.Products.Dtos
{
    public class CreateProductRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Stock quantity must be at least 1.")]
        public int StockQuantity { get; set; }
    }
}
