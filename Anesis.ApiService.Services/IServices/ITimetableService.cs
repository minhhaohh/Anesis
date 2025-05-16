using Anesis.ApiService.Domain.DTOs.Timetables;

namespace Anesis.ApiService.Services.IServices
{
    public interface ITimetableService
    {
        Task<List<CalendarEventDto>> SearchStaffSchedulesAsync(
            StaffScheduleFilterDto filter, CancellationToken cancellationToken = default);

        Task<CalendarEventDto> GetStaffScheduleEventByIdAsync(int id, CancellationToken cancellationToken = default);

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
