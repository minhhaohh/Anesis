using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<GeneralSettingsModel>> GetGeneralSettingsAsync()
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<GeneralSettingsModel>>($"{API_Site_Settings}/GeneralSettings");
        }

        public async Task<ResponseModel<StaffScheduleSettingsModel>> GetStaffScheduleSettingsAsync()
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<StaffScheduleSettingsModel>>($"{API_Site_Settings}/StaffScheduleSettings");
        }
    }
}
