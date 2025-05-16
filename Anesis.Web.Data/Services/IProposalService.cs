using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IProposalService
    {
        Task<ResponseModel<List<ProposalViewModel>>> SearchProposalsAsync(
            ProposalFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<ProposalViewModel>> GetProposalByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateProposalAsync(
            ProposalEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateProposalAsync(
            ProposalEditModel model, CancellationToken cancellationToken = default);
    }
}
