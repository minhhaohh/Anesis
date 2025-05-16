using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;

namespace Anesis.ApiService.Services.IServices
{
    public interface IChangeLogService
    {
        Task<List<ChangeLogDto>> GetChangeLogsAsync<T>(int entityId,
            string[] excludeFields = null, CancellationToken cancellationToken = default);

        Task<bool> AddChangeLogAsync<T>(
            int entityId, string actionName, string userComment,
            bool adminOnly = false, string notes = null,
            CancellationToken cancellationToken = default);

        Task<bool> AddChangeLogAsync<T>(
           int entityId, string actionName, string userComment,
           string changedFields, T afterChange, T beforeChange,
           bool adminOnly = false, string notes = null,
           CancellationToken cancellationToken = default);

        Task<int> DeleteChangeLogsAsync<T>(int entityId, CancellationToken cancellationToken = default);

        Task<int> DeleteManyChangeLogsAsync<T>(List<int> entityIds, CancellationToken cancellationToken = default);
    }
}
