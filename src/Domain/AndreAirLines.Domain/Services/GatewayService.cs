using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services.Base;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Services
{
    public class GatewayService : BaseService
    {
        private readonly HttpClient _httpClient;

        public GatewayService(HttpClient httpClient, INotifier notifier) : base(notifier)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44393");
        }

        public async Task<T> GetFromJsonAsync<T>(string path)
        {
            try
            {
                using var response = await _httpClient.GetAsync(path);

                return ErrorsResponse(response) ? await DeserializeObjectResponse<T>(response) : default(T);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Notification("API Unavailable try again later");
                return default(T);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            return await _httpClient.GetAsync(path);
        }

        protected bool ErrorsResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    Notification("Internal error");
                    return false;
                //throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage)
        {
            return await responseMessage.Content.ReadFromJsonAsync<T>();
        }
    }
}
