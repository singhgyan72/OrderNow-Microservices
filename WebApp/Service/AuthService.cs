using OrderNow.WebApp.Models;
using OrderNow.WebApp.Service.IService;
using OrderNow.WebApp.Utility;

namespace OrderNow.WebApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientService _httpClientService;

        public AuthService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ResponseDTO> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.POST,
                ApiUrl = Helpers.AuthAPIBase + "/authAPI/assignRole",
                Data = registrationRequestDTO
            });
        }

        public async Task<ResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.POST,
                ApiUrl = Helpers.AuthAPIBase + "/authAPI/login",
                Data = loginRequestDTO
            }, withBearer: false);
        }

        public async Task<ResponseDTO> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _httpClientService.SendAsync(new RequestDTO
            {
                ApiType = Utility.Helpers.ApiType.POST,
                ApiUrl = Helpers.AuthAPIBase + "/authAPI/register",
                Data = registrationRequestDTO
            }, withBearer: false);
        }
    }
}
