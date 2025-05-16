using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IAuthService
    {
        Task<ResponseModel<LoginResponseModel>> LoginAsync(UserLoginModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<LoginResponseModel>> GetLoggedUserInfoAsync(CancellationToken cancellationToken = default);

        Task<ResponseModel<bool>> LogoutAsync(CancellationToken cancellationToken = default);
    }
}
