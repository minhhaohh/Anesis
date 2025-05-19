using Anesis.Web.Data.Models;
using Anesis.Web.Data.Models.Common;

namespace Anesis.Web.Data.Services
{
    public interface IProposalService
    {
        Task<ResponseModel<List<ProposalViewModel>>> SearchProposalsAsync(
            ProposalFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<ProposalViewModel>> GetProposalByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<ChangeLogViewModel>>> GetProposalChangeLogsAsync(
           int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateProposalAsync(
            ProposalEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateProposalAsync(
            ProposalEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> ToggleProposalFlagAsync(
            FlagToggleModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> ReviewProposalAsync(
            ProposalReviewModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> ScheduleSurgeryAsync(
            ProposalScheduleSurgeryModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> SetProposalStatusAsync(
            ProposalSetStatusModel model, CancellationToken cancellationToken = default);
    }
}
