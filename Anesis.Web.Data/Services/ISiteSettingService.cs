using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface ISiteSettingService
    {
        Task<ResponseModel<GeneralSettingsModel>> GetGeneralSettingsAsync();

        Task<ResponseModel<StaffScheduleSettingsModel>> GetStaffScheduleSettingsAsync();
    }
}
