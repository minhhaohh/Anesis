namespace Anesis.ApiService.Domain.DTOs.Accounts
{
    public class LoginResponseDto
    {
        public string UserName { get; set; }

        public DateTime ExpirationUtcDate { get; set; }
    }
}
