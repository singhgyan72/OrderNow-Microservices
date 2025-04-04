﻿using System.ComponentModel.DataAnnotations;

namespace OrderNow.WebApp.Models
{
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
