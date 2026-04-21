namespace InventoryService.Application.Products.Dtos
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}
