namespace Anesis.Web.Data.Models
{
    public class UserLoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginResponseModel
    {
        public string UserName { get; set; }

        public DateTime ExpirationUtcDate { get; set; }
    }
}
