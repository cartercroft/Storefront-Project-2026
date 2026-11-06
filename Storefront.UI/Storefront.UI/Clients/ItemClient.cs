using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Storefront.Shared.Search;
using Storefront.UI.Classes;

namespace Storefront.UI.Clients
{
    public class ItemClient : AuthenticatedClient
    {
        public ItemClient(IHttpClientFactory factory, ProtectedSessionStorage sessionStorage) : base(factory.CreateClient(Enumeration.ClientTypes.StorefrontApi.ToString()), sessionStorage){}
        public async Task<Response<List<ItemViewModel>>> Search(ItemSearchParameters searchParameters)
        {
            return await GetAsync<List<ItemViewModel>>("/search", searchParameters.GetQueryString());
        }
    }
}
