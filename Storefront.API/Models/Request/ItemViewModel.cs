namespace Storefront.API.Models
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
