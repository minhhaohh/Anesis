using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IPatientService
    {
        Task<ResponseModel<PatientViewModel>> GetPatientByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<PatientViewModel>> GetPatientByChartNoAsync(
            string chartNo, CancellationToken cancellationToken = default);
    }
}
