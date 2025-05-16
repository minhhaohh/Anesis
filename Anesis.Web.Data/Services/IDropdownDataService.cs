using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IDropdownDataService
    {
        Task<ResponseModel<Dictionary<int, string>>> GetDropdownLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<Dictionary<int, string>>> GetDropdownAscLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<Dictionary<int, string>>> GetDropdownEmployeesAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<Dictionary<int, string>>> GetDropdownProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<Dictionary<int, string>>> GetDropdownDoctorsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<Dictionary<int, string>>> GetDropdownMidLevelProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<Dictionary<int, string>>> GetDropdownProceduresAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<Dictionary<int, string>>> GetDropdownInsurancesAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<string>>> GetDropdownInvoiceOwnersAsync(
            CancellationToken cancellationToken = default);
    }
}
