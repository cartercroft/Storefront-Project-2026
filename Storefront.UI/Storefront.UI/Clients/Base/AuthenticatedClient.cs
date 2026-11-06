
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Storefront.UI.Classes;

namespace Storefront.UI.Clients
{
    public class AuthenticatedClient : ClientBase
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        public AuthenticatedClient(HttpClient client, ProtectedSessionStorage sessionStorage) : base(client)
        {
            _sessionStorage = sessionStorage;
        }
        private async Task<Dictionary<string, string>?> AddAuthToken(Dictionary<string, string>? headers)
        {
            if (headers == null)
                headers = new Dictionary<string, string>();
            if (!headers.ContainsKey("Authorization"))
            {
                var storageEntry = await _sessionStorage.GetAsync<string>("AuthToken");
                if (storageEntry.Success && storageEntry.Value != null)
                {
                    headers.Add("Authorization", $"Bearer {storageEntry.Value}");
                }
            }
            return headers;
        }
        public override async Task<Response<TResponse>> GetAsync<TResponse>(string endpoint, Dictionary<string, object>? pathParameters = null, Dictionary<string, string>? headers = null)
        {
            return await base.GetAsync<TResponse>(endpoint, pathParameters, await AddAuthToken(headers));
        }
        public override async Task<Response> PostAsync(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            return await base.PostAsync(endpoint, bodyContent, await AddAuthToken(headers));
        }
        public override async Task<Response<TResponse>> PostAsync<TResponse>(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            return await base.PostAsync<TResponse>(endpoint, bodyContent, await AddAuthToken(headers));
        }
    }
}
