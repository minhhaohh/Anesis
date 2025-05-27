using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.Timetables;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocationService _locationService;
        private readonly IChangeLogService _changeLogService;
        private readonly IRepository<Timetable> _timetableRepo;
        private readonly IRepository<TimeClock> _timeClockRepo;
        private readonly IRepository<Employee> _employeeRepo;

        public TimetableService(IMapper mapper,
            IUnitOfWork unitOfWork,
            ILocationService locationService,
            IChangeLogService changeLogService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _timetableRepo = unitOfWork.GetRepository<Timetable>();
            _timeClockRepo = unitOfWork.GetRepository<TimeClock>();
            _employeeRepo = unitOfWork.GetRepository<Employee>();
            _locationService = locationService;
            _changeLogService = changeLogService;
        }

        public async Task<List<CalendarEventDto>> SearchStaffSchedulesAsync(
            StaffScheduleFilterDto filter, CancellationToken cancellationToken = default)
        {
            var events = new List<CalendarEventDto>();
            var doctorRoles = new[] { "PA", "PA-C", "Doctor", "ARNP" };

            if (filter.ResourceType == ScheduleResourceType.Employee)
            {
                // Calendar events view by employee
                var employeeScheduleEvents = await SearchEmployeeSchedules(filter.FromDate, filter.ToDate, null, filter.ResourceIds, filter.HiddenDaysOfWeek)
                    .Select(x => new CalendarEventDto()
                    {
                        Id = x.Id,
                        ResourceId = x.EmployeeId.Value,
                        Title = x.Location.ShortName,
                        StartTime = x.CalendarDate.Add(x.StartTime),
                        EndTime = x.CalendarDate.Add(x.EndTime),
                        ExtendedProps = new
                        {
                            EventType = x.Employee.JobRoleId > 0 && x.Employee.JobRole.IsManager ? StaffScheduleEventType.Manager
                                    : x.Employee.JobRoleId > 0 && doctorRoles.Contains(x.Employee.JobRole.Name) ? StaffScheduleEventType.Doctor
                                    : StaffScheduleEventType.Employee,
                            JobRole = x.Employee.JobRoleId > 0 ? x.Employee.JobRole.Name : "",
                            Notes = x.Notes,
                        }
                    })
                    .ToListAsync(cancellationToken);

                events.AddRange(employeeScheduleEvents);

                // Time off events
                var timeOffEvents = await SearchTimeOffDays(filter.FromDate, filter.ToDate, filter.ResourceIds)
                    .Select(x => new CalendarEventDto()
                    {
                        Id = x.Id,
                        ResourceId = x.EmployeeId,
                        Title = $"{x.ClockType.ToString()} {x.TotalHours} hours",
                        StartTime = x.ClockDate.Add(TimeSpan.FromMinutes(((x.StartTime ?? 800) / 100 * 60) + ((x.StartTime ?? 800) % 100))),
                        EndTime = x.ClockDate.Add(TimeSpan.FromMinutes(((x.StartTime ?? 800) / 100 * 60)
                            + ((x.StartTime ?? 800) % 100) + ((double)x.TotalHours * 60))),
                        ExtendedProps = new
                        {
                            EventType = StaffScheduleEventType.TimeOff,
                            JobRole = x.Employee.JobRoleId > 0 ? x.Employee.JobRole.Name : "",
                            Notes = x.Notes,
                        }
                    })
                    .ToListAsync(cancellationToken);

                events.AddRange(timeOffEvents);

                // ResourceId = -1 => Holiday
                if (filter.ResourceIds.Contains(-1))
                {
                    var holidayEvents = await SearchHolidays(filter.FromDate, filter.ToDate)
                        .Select(x => new CalendarEventDto()
                        {
                            Id = x.Id,
                            ResourceId = -1,
                            Title = x.Tooltip,
                            StartTime = x.CalendarDate.Add(x.StartTime),
                            EndTime = x.CalendarDate.Add(x.EndTime),
                            Editable = false,
                            StartEditable = false,
                            DurationEditable = false,
                            ExtendedProps = new
                            {
                                EventType = StaffScheduleEventType.Holiday,
                                Notes = x.Notes,
                            }
                        })
                        .ToListAsync(cancellationToken);

                    events.AddRange(holidayEvents);
                }
            }
            else if (filter.ResourceType == ScheduleResourceType.Location)
            {

                // Calendar events view by location
                var employeeScheduleEvents = await SearchEmployeeSchedules(filter.FromDate, filter.ToDate, filter.ResourceIds, null, filter.HiddenDaysOfWeek)
                    .Select(x => new CalendarEventDto()
                    {
                        Id = x.Id,
                        ResourceId = x.LocationId,
                        Title = $"{x.Employee.FirstName} {x.Employee.LastName}",
                        StartTime = x.CalendarDate.Add(x.StartTime),
                        EndTime = x.CalendarDate.Add(x.EndTime),
                        DisplayOrder = x.Employee.JobRoleId > 0 && x.Employee.JobRole.IsManager ? 1
                            : x.Employee.JobRoleId > 0 && doctorRoles.Contains(x.Employee.JobRole.Name) ? 2
                            : 3,
                        ExtendedProps = new
                        {
                            EventType = x.Employee.JobRoleId > 0 && x.Employee.JobRole.IsManager ? StaffScheduleEventType.Manager
                                    : x.Employee.JobRoleId > 0 && doctorRoles.Contains(x.Employee.JobRole.Name) ? StaffScheduleEventType.Doctor
                                    : StaffScheduleEventType.Employee,
                            EmployeeId = x.EmployeeId,
                            JobRole = x.Employee.JobRoleId > 0 ? x.Employee.JobRole.Name : "",
                            Notes = x.Notes,
                        }
                    })
                    .ToListAsync(cancellationToken);

                events.AddRange(employeeScheduleEvents);

                // Clinic closed events
                var clinicClosedEvents = await SearchClinicClosedDays(filter.FromDate, filter.ToDate, filter.ResourceIds)
                    .Select(x => new CalendarEventDto()
                    {
                        Id = x.Id,
                        ResourceId = x.LocationId,
                        Title = "Clinic Closed",
                        StartTime = x.CalendarDate.Add(x.StartTime),
                        EndTime = x.CalendarDate.Add(x.EndTime),
                        Editable = false,
                        StartEditable = false,
                        DurationEditable = false,
                        ExtendedProps = new
                        {
                            EventType = StaffScheduleEventType.ClinicClosed,
                            Notes = x.Notes,
                        }
                    })
                    .ToListAsync(cancellationToken);

                events.AddRange(clinicClosedEvents);

                // Holiday events
                var holidays = await SearchHolidays(filter.FromDate, filter.ToDate).ToListAsync(cancellationToken);
                var holidayEvents = CopyHolidayEventsForAllClinics(holidays, filter.ResourceIds);

                events.AddRange(holidayEvents);

                // ResourceId = 0 => Time Off
                if (filter.ResourceIds.Contains(0))
                {
                    // Time off events
                    var timeOffEvents = await SearchTimeOffDays(filter.FromDate, filter.ToDate)
                        .Select(x => new CalendarEventDto()
                        {
                            Id = x.Id,
                            ResourceId = 0,
                            Title = $"{x.Employee.FirstName} {x.Employee.LastName}",
                            StartTime = x.ClockDate.Add(TimeSpan.FromMinutes(((x.StartTime ?? 800) / 100 * 60) + ((x.StartTime ?? 800) % 100))),
                            EndTime = x.ClockDate.Add(TimeSpan.FromMinutes(((x.StartTime ?? 800) / 100 * 60)
                                + ((x.StartTime ?? 800) % 100) + ((double)x.TotalHours * 60))),
                            Editable = false,
                            StartEditable = false,
                            DurationEditable = false,
                            ExtendedProps = new
                            {
                                EventType = StaffScheduleEventType.TimeOff,
                                JobRole = x.Employee.JobRoleId > 0 ? x.Employee.JobRole.Name : "",
                                TimeOffType = $"{x.ClockType.ToString()} {x.TotalHours} hours",
                                Notes = x.Notes,
                            }
                        })
                        .ToListAsync(cancellationToken);

                    events.AddRange(timeOffEvents);
                }
            }

            return events.OrderBy(x => x.Start).ThenBy(x => x.DisplayOrder).ToList();
        }

        public async Task<CalendarEventDto> GetStaffScheduleByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var scheduleEvent = await FindByTypeByIdAsync(ScheduleEventType.StaffSchedule, id, cancellationToken);

            if (scheduleEvent == null)
            {
                return null;
            }

            return new CalendarEventDto()
            {
                Id = scheduleEvent.Id,
                ResourceId = scheduleEvent.LocationId,
                StartTime = scheduleEvent.CalendarDate.Add(scheduleEvent.StartTime),
                EndTime = scheduleEvent.CalendarDate.Add(scheduleEvent.EndTime),
                ExtendedProps = new
                {
                    EmployeeId = scheduleEvent.EmployeeId,
                }
            };
        }

        public async Task<List<ChangeLogDto>> GetStaffScheduleChangeLogsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _changeLogService.GetChangeLogsAsync<Timetable>(id, null, cancellationToken);
        }

        public async Task<bool> ScheduleEventsForStaffAsync(
            StaffScheduleEditDto model, CancellationToken cancellationToken = default)
        {
            if (model.IsSingleEvent())
            {
                return await UpdateStaffSchedulesAsync(model, cancellationToken);
            }
            else
            {
                return await CreateStaffSchedulesAsync(model, cancellationToken);
            }
        }

        public async Task<bool> UpdateStaffSchedulesAsync(
            StaffScheduleEditDto model, CancellationToken cancellationToken = default)
        {
            var timetable = await FindByTypeByIdAsync(ScheduleEventType.StaffSchedule, model.Id.Value, cancellationToken);

            if (timetable == null)
            {
                return false;
            }

            var modifyFields = model.GetModifiedFields(timetable);

            model.ApplyToChanges(timetable);
            _timetableRepo.Update(timetable);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<Timetable>(model.Id.Value, "Update", model.Notes,
                modifyFields, timetable, null, false, $"Updated schedule event #{model.Id}.", cancellationToken);

            return true;
        }

        public async Task<bool> CreateStaffSchedulesAsync(
            StaffScheduleEditDto command, CancellationToken cancellationToken = default)
        {
            var timeOffTypes = TimeClockType.GetTimeOffHourTypes();
            var newEvents = command.ToTimetables();

            var existingEvents = await SearchEmployeeSchedules(command.FromDate.Value, command.ToDate, null, command.EmployeeIds)
                .ToListAsync(cancellationToken);

            var clinicClosedEvents = await SearchClinicClosedDays(command.FromDate.Value, command.ToDate, new List<int>() { command.LocationId.Value })
                .ToListAsync(cancellationToken);

            var holidayEvents = await SearchHolidays(command.FromDate.Value, command.ToDate)
                .ToListAsync(cancellationToken);

            var timeOffEvents = await SearchTimeOffDays(command.FromDate.Value, command.ToDate, command.EmployeeIds)
                .ToListAsync(cancellationToken);

            // Remove clinic closed days
            if (clinicClosedEvents.Count > 0)
            {
                newEvents = newEvents.Where(x => !IsConflictEvent(clinicClosedEvents, x.CalendarDate, x.StartTime, x.EndTime)).ToList();
            }

            // Remove holidays
            if (holidayEvents.Count > 0)
            {
                newEvents = newEvents.Where(x => !holidayEvents.Any(h => h.CalendarDate == x.CalendarDate)).ToList();
            }

            // Remove time off  days
            if (timeOffEvents.Count > 0)
            {
                newEvents = newEvents.Where(x => !IsTimeOffDay(timeOffEvents, x.CalendarDate, x.StartTime, x.EndTime)).ToList();
            }

            switch (command.SaveOption)
            {
                case StaffScheduleSaveOption.OverwriteExisting:
                    // Delete conflict events in db to create new events
                    await DeleteConflictEventsAsync(newEvents, existingEvents, cancellationToken);
                    _timetableRepo.InsertRange(newEvents);
                    break;
                case StaffScheduleSaveOption.IgnoreConflicts:
                    // Remove conflict events for new data
                    newEvents = newEvents
                        .Where(x => !IsConflictEvent(existingEvents.Where(e => e.EmployeeId == x.EmployeeId).ToList(), x.CalendarDate, x.StartTime, x.EndTime))
                        .ToList();
                    _timetableRepo.InsertRange(newEvents);
                    break;
                case StaffScheduleSaveOption.ShowErrors:
                    _timetableRepo.InsertRange(newEvents);
                    break;
                default:
                    return false;
            }

            // Save changes
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
                return false;

            var employees = await _employeeRepo.All(true)
                .Where(x => command.EmployeeIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
            var location = await _locationService.GetByIdAsync(command.LocationId.Value, cancellationToken);

            // Create change log for event
            foreach (var newEvent in newEvents)
            {
                var employee = employees.FirstOrDefault(x => x.Id == newEvent.EmployeeId);

                var msg = $"Created new schedule event #{newEvent.Id.ToString()} for {employee.FirstName} {employee.LastName} at {location.ShortName}"
                    + $"from {newEvent.StartTime.ToString("hh\\:mm")} to {newEvent.EndTime.ToString("hh\\:mm")} on {newEvent.CalendarDate.ToString("MM/dd/yyyy")}.";

                await _changeLogService.AddChangeLogAsync<Timetable>(newEvent.Id, "Create", newEvent.Notes,
                    "*", newEvent, null, false, msg, cancellationToken);
            }

            return true;
        }

        public async Task<bool> DeleteStaffSchedulesAsync(
           StaffScheduleDeleteDto model, CancellationToken cancellationToken = default)
        {
            if (model.IsDeleteById())
            {
                return await DeleteStaffScheduleByIdAsync(model.Id.Value, cancellationToken);
            }
            else
            {
                return await DeleteManyStaffSchedulesAsync(model, cancellationToken);
            }
        }

        public async Task<bool> DeleteStaffScheduleByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var timetable = await _timetableRepo.DeleteAsync(cancellationToken, id);

            // Save changes
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            // Delete all change logs of deleted events
            await _changeLogService.DeleteChangeLogsAsync<Timetable>(id, cancellationToken);

            return true;
        }

        public async Task<bool> DeleteManyStaffSchedulesAsync(
            StaffScheduleDeleteDto model, CancellationToken cancellationToken = default)
        {
            var eventsToDelete = await _timetableRepo.All()
                .Where(x => x.EventType == ScheduleEventType.StaffSchedule && x.LocationId == model.LocationId
                    && x.EmployeeId > 0 && model.EmployeeIds.Contains(x.EmployeeId.Value))
                .WhereIf(x => x.CalendarDate == model.FromDate, model.ToDate == null)
                .WhereIf(x => x.CalendarDate >= model.FromDate && x.CalendarDate <= model.ToDate, model.ToDate != null)
                .ToListAsync(cancellationToken);

            // If the To Date has value => just delete selected days of week
            if (model.ToDate != null)
                eventsToDelete = eventsToDelete.Where(x => model.DaysOfWeek.Contains((int)x.CalendarDate.DayOfWeek)).ToList();

            _timetableRepo.DeleteRange(eventsToDelete);

            // Save changes
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            // Delete all change logs of deleted events
            var eventIds = eventsToDelete.Select(x => x.Id).ToList();
            await _changeLogService.DeleteManyChangeLogsAsync<Timetable>(eventIds, cancellationToken);

            return true;
        }

        public async Task<List<string>> CheckStaffConflictEvents(StaffScheduleEditDto model, CancellationToken cancellationToken = default)
        {
            var messages = new List<string>();
            var timeOffTypes = TimeClockType.GetTimeOffHourTypes();

            var existingEvents = await SearchEmployeeSchedules(model.FromDate.Value, model.ToDate, null, model.EmployeeIds)
                .WhereIf(x => x.Id != model.Id, model.IsSingleEvent()) // Remove current event if in update mode (resize, drop)
                .Include(x => x.Employee) // Get employee info
                .ToListAsync(cancellationToken);

            var clinicClosedEvents = await SearchClinicClosedDays(model.FromDate.Value, model.ToDate, new List<int>() { model.LocationId.Value })
                .Include(x => x.Location)
                .ToListAsync();

            var holidayEvents = await SearchHolidays(model.FromDate.Value, model.ToDate)
                .ToListAsync(cancellationToken);

            var timeOffEvents = await SearchTimeOffDays(model.FromDate.Value, model.ToDate, model.EmployeeIds)
                .Include(x => x.Employee) // Get employee info
                .ToListAsync(cancellationToken);

            if (existingEvents.Count == 0 && holidayEvents.Count == 0 && timeOffEvents.Count == 0 && clinicClosedEvents.Count == 0)
                return messages;

            for (var checkDate = model.FromDate.Value; checkDate <= (model.ToDate ?? model.FromDate.Value); checkDate = checkDate.AddDays(1))
            {
                if (model.ToDate != null && !model.DaysOfWeek.Contains((int)checkDate.DayOfWeek))
                {
                    continue;
                }

                // Check holiday
                var holiday = holidayEvents.FirstOrDefault(x => x.CalendarDate == checkDate);
                if (holiday != null)
                {
                    messages.Add($"{checkDate.ToString("MM/dd/yyyy")} is the {holiday.Tooltip}.");
                    {
                        continue;
                    }
                }

                foreach (var employeeId in model.EmployeeIds)
                {
                    var comparingEvents = existingEvents.Where(x => x.EmployeeId == employeeId).ToList();
                    var empTimeOffEvents = timeOffEvents.Where(x => x.EmployeeId == employeeId).ToList();

                    if (comparingEvents.Count == 0 && empTimeOffEvents.Count == 0 && clinicClosedEvents.Count == 0)
                    {
                        continue;
                    }

                    // Check employee is already scheduled or has time off
                    var msg = CheckConflictEvent(comparingEvents, clinicClosedEvents, empTimeOffEvents, checkDate, model.StartTime.Value, model.EndTime.Value);
                    if (msg.HasValue())
                    {
                        messages.Add(msg);
                    }
                }

            }

            return messages;
        }

        // SUPPORT METHODS
        private async Task<Timetable> FindByTypeByIdAsync(ScheduleEventType eventType, int id, CancellationToken cancellationToken = default)
        {
            return await _timetableRepo.All()
                .FirstOrDefaultAsync(x => x.Id == id && x.EventType == eventType, cancellationToken);
        }

        private IQueryable<TimeClock> SearchTimeOffDays(
            DateTime fromDate, DateTime? toDate, List<int> employeeIds = null)
        {
            var timeOffTypes = TimeClockType.GetTimeOffHourTypes();

            return _timeClockRepo.All(true)
                .Where(x => timeOffTypes.Contains(x.ClockType))
                .WhereIf(x => employeeIds.Contains(x.EmployeeId), employeeIds != null && employeeIds.Count > 0)
                .WhereIf(x => x.ClockDate == fromDate, toDate == null)
                .WhereIf(x => x.ClockDate >= fromDate && x.ClockDate <= toDate, toDate != null);
        }

        private IQueryable<Timetable> SearchHolidays(DateTime fromDate, DateTime? toDate)
        {
            return _timetableRepo.All(true)
                .Where(x => x.EventType == ScheduleEventType.Holiday)
                .WhereIf(x => x.CalendarDate == fromDate, toDate == null)
                .WhereIf(x => x.CalendarDate >= fromDate && x.CalendarDate <= toDate, toDate != null);
        }

        private IQueryable<Timetable> SearchClinicClosedDays(
            DateTime fromDate, DateTime? toDate, List<int> locationIds = null)
        {
            return _timetableRepo.All(true)
                .Where(x => x.EventType == ScheduleEventType.ClinicClosed)
                .WhereIf(x => locationIds.Contains(x.LocationId), locationIds != null && locationIds.Count > 0)
                .WhereIf(x => x.CalendarDate == fromDate, toDate == null)
                .WhereIf(x => x.CalendarDate >= fromDate && x.CalendarDate <= toDate, toDate != null);
        }

        private IQueryable<Timetable> SearchEmployeeSchedules(
            DateTime fromDate, DateTime? toDate, List<int> locationIds = null,
            List<int> employeeIds = null, List<int> hiddenDaysOfWeek = null)
        {
            var firstSunday = fromDate.AddDays(-(int)fromDate.DayOfWeek);

            return _timetableRepo.All()
                .Where(x => x.EventType == ScheduleEventType.StaffSchedule)
                .WhereIf(x => locationIds.Contains(x.LocationId), locationIds != null && locationIds.Count > 0)
                .WhereIf(x => x.EmployeeId > 0 && employeeIds.Contains(x.EmployeeId.Value), employeeIds != null && employeeIds.Count > 0)
                .WhereIf(x => !hiddenDaysOfWeek.Contains(EF.Functions.DateDiffDay(firstSunday, x.CalendarDate) % 7),
                    hiddenDaysOfWeek != null && hiddenDaysOfWeek.Count > 0) // Remove hidden days in calendar
                .WhereIf(x => x.CalendarDate == fromDate, toDate == null)
                .WhereIf(x => x.CalendarDate >= fromDate && x.CalendarDate <= toDate, toDate != null);
        }

        private List<CalendarEventDto> CopyHolidayEventsForAllClinics(List<Timetable> holidays, List<int> locationIds)
        {
            var events = new List<CalendarEventDto>();

            if (holidays.Count == 0 || locationIds.Count == 0)
            {
                return events;
            }

            foreach (var locationId in locationIds)
            {
                if (locationId <= 0)
                {
                    continue;
                }

                var holidayEvents = holidays
                    .Select(x => new CalendarEventDto()
                    {
                        Id = x.Id,
                        ResourceId = locationId,
                        Title = x.Tooltip,
                        StartTime = x.CalendarDate.Add(x.StartTime),
                        EndTime = x.CalendarDate.Add(x.EndTime),
                        Editable = false,
                        StartEditable = false,
                        DurationEditable = false,
                        ExtendedProps = new
                        {
                            EventType = StaffScheduleEventType.Holiday,
                            Notes = x.Notes,
                        }
                    });

                events.AddRange(holidayEvents);
            }

            return events;
        }

        private string CheckConflictEvent(
            List<Timetable> comparingEvents, List<Timetable> clinicClosedEvents, List<TimeClock> timeOffEvents,
            DateTime checkDate, TimeSpan checkStartTime, TimeSpan checkEndTime)
        {
            if (IsConflictEvent(clinicClosedEvents, checkDate, checkStartTime, checkEndTime))
            {
                var locationName = clinicClosedEvents.FirstOrDefault()?.Location?.ShortName;
                return $"{locationName} clinic closed on {checkDate.ToString("MM/dd/yyyy")}.";
            }

            if (IsTimeOffDay(timeOffEvents, checkDate, checkStartTime, checkEndTime))
            {
                var employeeName = $"{timeOffEvents.FirstOrDefault()?.Employee?.FirstName} {timeOffEvents.FirstOrDefault()?.Employee?.LastName}";
                return $"{employeeName} has time off on {checkDate.ToString("MM/dd/yyyy")}.";
            }

            if (IsConflictEvent(comparingEvents, checkDate, checkStartTime, checkEndTime))
            {
                var employeeName = $"{comparingEvents.FirstOrDefault()?.Employee?.FirstName} {comparingEvents.FirstOrDefault()?.Employee?.LastName}";
                return $"{employeeName} is already scheduled on {checkDate.ToString("MM/dd/yyyy")}.";
            }

            return "";
        }

        private bool IsTimeOffDay(
            List<TimeClock> timeOffEvents, DateTime checkDate,
            TimeSpan checkStartTime, TimeSpan checkEndTime)
        {
            return timeOffEvents
                .Any(x => x.ClockDate == checkDate
                    && checkStartTime < TimeSpan.FromMinutes(((x.StartTime ?? 800) / 100 * 60) + ((x.StartTime ?? 800) % 100) + ((double)x.TotalHours * 60))
                    && checkEndTime > TimeSpan.FromMinutes(((x.StartTime ?? 800) / 100 * 60) + ((x.StartTime ?? 800) % 100)));
        }

        private bool IsConflictEvent(
            List<Timetable> comparingEvents, DateTime checkDate,
            TimeSpan checkStartTime, TimeSpan checkEndTime)
        {
            return comparingEvents
                .Any(x => x.CalendarDate == checkDate && checkStartTime < x.EndTime && checkEndTime > x.StartTime);
        }

        private async Task<int> DeleteConflictEventsAsync(
            List<Timetable> newEvents, List<Timetable> existingEvents,
            CancellationToken cancellationToken = default)
        {
            if (newEvents.Count == 0 || existingEvents.Count == 0)
            {
                return 0;
            }

            var eventsToDelete = existingEvents.Where(x =>
                    IsConflictEvent(newEvents.Where(x => x.EmployeeId == x.EmployeeId).ToList(), x.CalendarDate, x.StartTime, x.EndTime))
                .ToList();

            if (eventsToDelete.Count == 0)
            {
                return 0;
            }

            var eventIds = eventsToDelete.Select(x => x.Id).ToList();
            await _changeLogService.DeleteManyChangeLogsAsync<Timetable>(eventIds, cancellationToken);

            // Delete all change logs of deleted events
            _timetableRepo.DeleteRange(eventsToDelete);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
