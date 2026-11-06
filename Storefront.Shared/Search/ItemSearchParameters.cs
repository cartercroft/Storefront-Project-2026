namespace Storefront.Shared.Search
{
    public class ItemSearchParameters : SearchParameters
    {
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
