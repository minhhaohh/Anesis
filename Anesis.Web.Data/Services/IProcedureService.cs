using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IProcedureService
    {
        Task<ResponseModel<List<ProcedureViewModel>>> SearchProceduresAsync(
            ProcedureFilterModel model, CancellationToken cancellationToken = default);
    }
}
