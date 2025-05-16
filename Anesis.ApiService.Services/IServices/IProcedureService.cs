using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Procedures;

namespace Anesis.ApiService.Services.IServices
{
    public interface IProcedureService
    {
        Task<IPagedList<ProcedureDto>> SearchAsync(
            ProcedureFilterDto filter, CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetProceduresAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);
    }
}
