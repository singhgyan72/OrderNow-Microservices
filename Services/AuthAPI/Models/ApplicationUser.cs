using Microsoft.AspNetCore.Identity;

namespace OrderNow.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
