using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Timetables;
using Anesis.ApiService.Extensions;
using Anesis.ApiService.Services.IServices;
using Anesis.ApiService.Validators.Timetables;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class TimetablesController
    {
        private readonly ILogger<TimetablesController> _logger;
        private readonly ITimetableService _timetableService;

        public TimetablesController(
            ILogger<TimetablesController> logger,
            ITimetableService timetableService)
        {
            _logger = logger;
            _timetableService = timetableService;
        }

        [HttpGet("StaffSchedules")]
        public async Task<Result> SearchStaffSchedules([FromQuery] StaffScheduleFilterDto filter)
        {
            var scheduleEvents = await _timetableService.SearchStaffSchedulesAsync(filter);

            return Result.Ok(scheduleEvents);
        }

        [HttpGet("StaffSchedules/{id}")]
        public async Task<Result> GetStaffScheduleById([FromRoute] int id)
        {
            var scheduleEvent = await _timetableService.GetStaffScheduleByIdAsync(id);

            return scheduleEvent == null
                ? Result.Error($"Could not find schedule event with id #{id}.")
                : Result.Ok(scheduleEvent);
        }

        [HttpGet("StaffSchedules/ChangeLogs/{id}")]
        public async Task<Result> GetStaffScheduleChangeLogs([FromRoute] int id)
        {
            var changeLogs = await _timetableService.GetStaffScheduleChangeLogsAsync(id);

            return Result.Ok(changeLogs);
        }

        [HttpPost("StaffSchedules")]
        public async Task<Result> CreateSchedules([FromBody] StaffScheduleEditDto model)
        {
            // Make sure the event id = 0
            model.Id = 0;

            var validator = new StaffScheduleEditDtoValidator(_timetableService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _timetableService.CreateStaffSchedulesAsync(model))
            {
                return Result.Error("Something went wrong when creating staff schedules. Please try again.");
            }

            return Result.Ok($"Created staff schedules successful.");
        }

        [HttpPut("StaffSchedules/{id}")]
        public async Task<Result> UpdateSchedules([FromRoute] int id, [FromBody] StaffScheduleEditDto model)
        {
            // Make sure the event id is from the route
            model.Id = id;

            var validator = new StaffScheduleEditDtoValidator(_timetableService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _timetableService.ScheduleEventsForStaffAsync(model))
            {
                return Result.Error("Something went wrong when updating staff schedules. Please try again.");
            }

            return Result.Ok($"Updated staff schedules successful.");
        }

        [HttpDelete("StaffSchedules/{id}")]
        public async Task<Result> DeleteStaffSchedules([FromRoute] int id, [FromQuery] StaffScheduleDeleteDto model)
        {
            // Make sure the event id is from the route
            model.Id = id;

            var validator = new StaffScheduleDeleteDtoValidator(_timetableService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _timetableService.DeleteStaffSchedulesAsync(model))
            {
                return Result.Error("Something went wrong when deleting staff schedules. Please try again.");
            }

            return Result.Ok($"Deleted staff schedules successful.");
        }
    }
}
