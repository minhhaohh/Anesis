using Anesis.ApiService.Domain.DTOs.Patients;

namespace Anesis.ApiService.Services.IServices
{
    public interface IPatientService
    {
        Task<PatientDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<PatientDto> GetByChartNoAsync(string chartNo, CancellationToken cancellationToken = default);
    }
}
