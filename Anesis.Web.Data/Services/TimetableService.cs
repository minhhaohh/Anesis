using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using Anesis.Web.Data.Models.Common;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<CalendarEventViewModel>>> SearchStaffSchedulesAsync(
            StaffScheduleFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CalendarEventViewModel>>>(
                $"{API_Timetables}/StaffSchedules?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<CalendarEventViewModel>> GetStaffScheduleByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<CalendarEventViewModel>>(
                $"{API_Timetables}/StaffSchedules/{id}", cancellationToken);
        }

        public async Task<ResponseModel<string>> CreateStaffSchedulesAsync(
            StaffScheduleEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"{API_Timetables}/StaffSchedules", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateStaffSchedulesAsync(
            StaffScheduleEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{API_Timetables}/StaffSchedules/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> DeleteStaffSchedulesAsync(
            StaffScheduleDeleteModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync(
                $"{API_Timetables}/StaffSchedules/{model.Id}?{model.ToQueryParams()}", cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }
    }
}
