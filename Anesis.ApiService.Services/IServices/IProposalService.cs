using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Services.IServices
{
    public interface IProposalService
    {
        Task<IPagedList<ProposalDto>> SearchAsync(ProposalFilterDto filter, 
            CancellationToken cancellationToken = default);

        Task<ProposalDto> GetByIdAsync(int id, 
            CancellationToken cancellationToken = default);

        Task<bool> CreateAsync(ProposalEditDto model, 
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(ProposalEditDto model, 
            CancellationToken cancellationToken = default);

        Task<bool> ToggleFlagAsync(
            FlagToggleDto model, CancellationToken cancellationToken = default);

        Task<bool> ReviewAsync(ProposalReviewDto model, 
            CancellationToken cancellationToken = default);

        Task<bool> ScheduleSurgeryAsync(ProposalScheduleSurgeryDto model, 
            CancellationToken cancellationToken = default);

        Task<bool> SetStatusAsync(ProposalSetStatusDto model, CancellationToken cancellationToken = default);

        Task<List<ChangeLogDto>> GetChangeLogsAsync(int id, CancellationToken cancellationToken = default);
    }
}
