using OrderNow.Services.EmailAPI.Message;
using OrderNow.Services.EmailAPI.Models.DTO;

namespace OrderNow.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDTO cartDto);
        Task RegisterUserEmailAndLog(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}
