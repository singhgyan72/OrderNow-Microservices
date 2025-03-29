using OrderNow.Services.AuthAPI.Models.DTO;

namespace OrderNow.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO registrationRequestDto);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
