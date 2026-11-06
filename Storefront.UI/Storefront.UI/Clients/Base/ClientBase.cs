using Storefront.UI.Classes;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Storefront.UI.Clients
{
    public class ClientBase
    {
        private readonly HttpClient _client;
        public ClientBase(HttpClient client)
        {
            _client = client;
        }
        protected HttpClient Client => _client;
        public virtual async Task<Response> PostAsync(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            HttpRequestMessage request = GetHttpRequestForPost(bodyContent, endpoint, headers);
            var result = await _client.SendAsync(request);
            Response response = new Response();

            if (!result.IsSuccessStatusCode)
            {
                response.ErrorMessages.Add(await GetRequestErrorMessage(result));
            }

            return response;
        }
        public virtual async Task<Response<TResponse>> PostAsync<TResponse>(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            HttpRequestMessage request = GetHttpRequestForPost(bodyContent, endpoint, headers);
            var result = await _client.SendAsync(request);
            Response<TResponse> response = new Response<TResponse>();

            if (!result.IsSuccessStatusCode)
            {
                response.ErrorMessages.Add(await GetRequestErrorMessage(result));
            }
            else
            {
                response.Result = await GetResponseObject<TResponse>(result);
            }

            return response;
        }
        public virtual async Task<Response<TResponse>> GetAsync<TResponse>(string endpoint, string queryString, Dictionary<string, string>? headers = null)
        {
            HttpRequestMessage getRequest = GetHttpRequestForGet(endpoint, queryString, headers);
            var result = await _client.SendAsync(getRequest);

            Response<TResponse> response = new Response<TResponse>();
            if (!result.IsSuccessStatusCode)
            {
                response.ErrorMessages.Add(await GetRequestErrorMessage(result));
            }
            else
            {
                response.Result = await GetResponseObject<TResponse>(result);
            }
            return response;
        }
        private async Task<TResponse?> GetResponseObject<TResponse>(HttpResponseMessage responseMessage)
        {
            Stream receiveStream = await responseMessage.Content.ReadAsStreamAsync();
            string responseBody = await ReadStreamAsync(receiveStream);
            TResponse? response = JsonSerializer.Deserialize<TResponse>(responseBody);
            return response;
        }
        private async Task<string> ReadStreamAsync(Stream streamToRead)
        {
            using StreamReader readStream = new StreamReader(streamToRead, Encoding.UTF8);
            string streamString = await readStream.ReadToEndAsync();
            readStream.BaseStream.Seek(0, SeekOrigin.Begin);
            return streamString;
        }
        private static HttpContent GetHttpContentBody(object bodyContent)
        {
            string body = JsonSerializer.Serialize(bodyContent);
            HttpContent content = new StringContent(body);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            return content;
        }
        private HttpRequestMessage GetHttpRequestForPost(object bodyContent,
            string endpoint,
            Dictionary<string, string>? headers)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;

            var content = GetHttpContentBody(bodyContent);
            request.Content = content;

            request.RequestUri = new Uri($"{_client.BaseAddress}{endpoint}");

            request = AddHttpRequestMessageHeaders(request, headers);

            return request;
        }
        private HttpRequestMessage GetHttpRequestForGet(string endpoint,
            string queryString,
            Dictionary<string, string>? headers = null)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;

            string uri = $"{_client.BaseAddress}{endpoint}";
            if(!string.IsNullOrEmpty(queryString))
            {
                uri = $"{uri}?{queryString}";
            }
            request.RequestUri = new Uri(uri);

            request = AddHttpRequestMessageHeaders(request, headers);

            return request;
        }
        private async Task<string> GetRequestErrorMessage(HttpResponseMessage httpResponseMessage)
        {
            Stream receiveStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            string responseBody = await ReadStreamAsync(receiveStream);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Error calling {httpResponseMessage.RequestMessage?.RequestUri}.");
            sb.AppendLine($"Error: {httpResponseMessage.ReasonPhrase}");
            if (!string.IsNullOrEmpty(responseBody))
            {
                sb.AppendLine($"Response Body: {responseBody}");
            }
            return sb.ToString();
        }
        private HttpRequestMessage AddHttpRequestMessageHeaders(HttpRequestMessage httpRequestMessage, Dictionary<string, string>? headers = null)
        {
            if (headers != null)
            {
                foreach (var entry in headers)
                {
                    httpRequestMessage.Headers.Add(entry.Key, entry.Value);
                }
            }
            return httpRequestMessage;
        }
    }
}
