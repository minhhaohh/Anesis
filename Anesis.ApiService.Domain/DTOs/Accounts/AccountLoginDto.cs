﻿namespace Anesis.ApiService.Domain.DTOs.Accounts
{
    public class AccountLoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
