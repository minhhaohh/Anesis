namespace Anesis.ApiService.Services.IServices
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(string accountId, int permUniqueId, CancellationToken cancellationToken = default);

        Task<List<int>> GetPermissionsForUserAsync(string accountId, CancellationToken cancellationToken = default);
    }
}
