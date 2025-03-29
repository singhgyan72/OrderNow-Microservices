using static OrderNow.WebApp.Utility.Helpers;

namespace OrderNow.WebApp.Models
{
    public class RequestDTO
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string ApiUrl { get; set; }

        public object Data { get; set; }

        public string AccessToken { get; set; }

        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
