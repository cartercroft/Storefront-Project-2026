namespace Storefront.API.Data.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
    }
}
