using Newtonsoft.Json;
using OrderNow.WebApp.Models;
using OrderNow.WebApp.Service.IService;
using System.Net;
using System.Text;
using static OrderNow.WebApp.Utility.Helpers;

namespace OrderNow.WebApp.Service
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public HttpClientService(IHttpClientFactory clientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = clientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDTO> SendAsync(RequestDTO request, bool withBearer = true)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("OrderNowAPI");
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Headers.Add("Accept", "application/json");
            //token
            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                requestMessage.Headers.Add("Authorization", $"Bearer {token}");
            }

            requestMessage.RequestUri = new Uri(request.ApiUrl);
            if (request.Data != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
            }
            switch (request.ApiType)
            {
                case ApiType.POST: requestMessage.Method = HttpMethod.Post; break;
                case ApiType.PUT: requestMessage.Method = HttpMethod.Put; break;
                case ApiType.DELETE: requestMessage.Method = HttpMethod.Delete; break;
                default: requestMessage.Method = HttpMethod.Get; break;
            }

            HttpResponseMessage apiResponse = await httpClient.SendAsync(requestMessage);

            try
            {
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound: return new() { IsSuccess = false, Message = "Not found" };
                    case HttpStatusCode.Forbidden: return new() { IsSuccess = false, Message = "Access denied" };
                    case HttpStatusCode.Unauthorized: return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError: return new() { IsSuccess = false, Message = "Internal server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseDTO { IsSuccess = false, Message = ex.ToString() };
                return response;
            }
        }
    }
}
