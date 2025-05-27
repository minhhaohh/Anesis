using Anesis.Web.Data.Models;
using Anesis.Web.Data.Models.Common;

namespace Anesis.Web.Data.Services
{
    public interface ITimetableService
    {
        Task<ResponseModel<List<CalendarEventViewModel>>> SearchStaffSchedulesAsync(
            StaffScheduleFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<CalendarEventViewModel>> GetStaffScheduleByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<ChangeLogViewModel>>> GetStaffScheduleChangeLogsAsync(
           int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateStaffSchedulesAsync(
            StaffScheduleEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateStaffSchedulesAsync(
            StaffScheduleEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> DeleteStaffSchedulesAsync(
            StaffScheduleDeleteModel model, CancellationToken cancellationToken = default);
    }
}
