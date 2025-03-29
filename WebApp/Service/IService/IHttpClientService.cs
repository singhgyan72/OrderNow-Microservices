using OrderNow.WebApp.Models;

namespace OrderNow.WebApp.Service.IService
{
    public interface IHttpClientService
    {
        Task<ResponseDTO> SendAsync(RequestDTO request, bool withBearer = true);
    }
}
