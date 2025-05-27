using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.Timetables;

namespace Anesis.ApiService.Services.IServices
{
    public interface ITimetableService
    {
        Task<List<CalendarEventDto>> SearchStaffSchedulesAsync(
            StaffScheduleFilterDto filter, CancellationToken cancellationToken = default);

        Task<CalendarEventDto> GetStaffScheduleByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<List<ChangeLogDto>> GetStaffScheduleChangeLogsAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ScheduleEventsForStaffAsync(
            StaffScheduleEditDto model, CancellationToken cancellationToken = default);

        Task<bool> UpdateStaffSchedulesAsync(
            StaffScheduleEditDto model, CancellationToken cancellationToken = default);

        Task<bool> CreateStaffSchedulesAsync(
            StaffScheduleEditDto command, CancellationToken cancellationToken = default);

        Task<bool> DeleteStaffSchedulesAsync(
           StaffScheduleDeleteDto model, CancellationToken cancellationToken = default);

        Task<bool> DeleteStaffScheduleByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> DeleteManyStaffSchedulesAsync(
            StaffScheduleDeleteDto model, CancellationToken cancellationToken = default);

        Task<List<string>> CheckStaffConflictEvents(
            StaffScheduleEditDto model, CancellationToken cancellationToken = default);
    }
}
