using Anesis.ApiService.Domain.DTOs.Common;
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

        Task<bool> UpdateAsync(int id, ProposalEditDto model, 
            CancellationToken cancellationToken = default);

        Task<bool> ReviewAsync(int id, ProposalReviewDto model, 
            CancellationToken cancellationToken = default);

        Task<bool> SetStatusAsync(int id, PotentialProcedureStatus status, 
            string reason = null, CancellationToken cancellationToken = default);

        Task<bool> ScheduleSurgeryAsync(int id, ProposalScheduleDto model, 
            CancellationToken cancellationToken = default);
    }
}
