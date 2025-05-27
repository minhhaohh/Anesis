using Anesis.ApiService.Domain.DTOs.Timetables;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using FluentValidation;

namespace Anesis.ApiService.Validators.Timetables
{
    public class StaffScheduleEditDtoValidator : AbstractValidator<StaffScheduleEditDto>
    {
        private readonly ITimetableService _timetableService;

        public StaffScheduleEditDtoValidator(ITimetableService timetableService)
        {
            _timetableService = timetableService;

            RuleFor(x => x.LocationId)
                .NotNull()
                .WithMessage("The Location field is required.");

            RuleFor(x => x.EmployeeIds)
                .Must(AtLeastOneEmployee)
                .WithMessage("Please select at least 1 employee.")
                .MustAsync(NoConflictEvents)
                .WithMessage("{ValidationMessages}");

            RuleFor(x => x.FromDate)
                .NotNull()
                .WithMessage("The From Date field is required.")
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage($"The From Date must be greater than or equal to current date {DateTime.Now.ToString("MM/dd/yyyy")}.");

            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate)
                .WithMessage("The To Date must be greater than or equal to the From Date.");

            RuleFor(x => x.DaysOfWeek)
                .Must(AtLeastOneDay)
                .WithMessage("Please select at least 1 day of week.")
                .Must(HasDayInDateRange)
                .WithMessage("No {DaysOfWeek} found between {FromDate} and {ToDate}.");

            RuleFor(x => x.StartTime)
                .NotNull()
                .WithMessage("The Start Time field is required.")
                .Must(BetweenBusinessHours)
                .WithMessage("The Start Time must be between 06:00 and 20:00.");

            RuleFor(x => x.EndTime)
                .NotNull()
                .WithMessage("The End Time field is required.")
                .Must(BetweenBusinessHours)
                .WithMessage("The End Time must be between 06:00 and 20:00.")
                .GreaterThan(x => x.StartTime)
                .WithMessage("The End Time must be greater than the Start Time.");


            When(x => x.IsSingleEvent(), () =>
            {
                RuleFor(x => x.Id)
                    .MustAsync(EventExistedAsync)
                    .WithMessage(x => $"Could not find schedule event with id #{x.Id}.")
                    .MustAsync(EventInFutureAsync)
                    .WithMessage(x => $"Could not change the past schedule.");
            });
        }

        private bool AtLeastOneEmployee(List<int> employeeIds)
        {
            return employeeIds.Count > 0;
        }

        private bool BetweenBusinessHours(TimeSpan? time)
        {
            return time == null || (time >= TimeSpan.FromHours(6) && time <= TimeSpan.FromHours(20));
        }

        private bool AtLeastOneDay(StaffScheduleEditDto model, List<int> days)
        {
            return model.FromDate == null
                || model.FromDate.Value.Date < DateTime.Now.Date
                || model.ToDate == null
                || days.Count > 0;
        }

        private async Task<bool> NoConflictEvents(
            StaffScheduleEditDto model, List<int> employeeIds,
            ValidationContext<StaffScheduleEditDto> context, CancellationToken cancellationToken)
        {
            if (model.LocationId == null
                || employeeIds.Count == 0
                || model.FromDate == null
                || model.FromDate.Value.Date < DateTime.Now.Date
                || model.SaveOption != StaffScheduleSaveOption.ShowErrors)
            {
                return true;
            }    

            var messages = await _timetableService.CheckStaffConflictEvents(model, cancellationToken);

            context.MessageFormatter.AppendArgument("ValidationMessages", messages.StrJoin("<br/>"));

            return messages.Count == 0;
        }

        private bool HasDayInDateRange(
            StaffScheduleEditDto model, List<int> daysOfWeek,
            ValidationContext<StaffScheduleEditDto> context)
        {
            if (daysOfWeek.Count == 0
                || model.FromDate == null
                || model.FromDate.Value < DateTime.Now.Date
                || model.ToDate == null
                || model.ToDate.Value.Date <= model.FromDate.Value.Date)
            {
                return true;
            }    

            context.MessageFormatter.AppendArgument("DaysOfWeek", daysOfWeek.Select(x => $"{(DayOfWeek)x}").StrJoin(","));
            context.MessageFormatter.AppendArgument("FromDate", model.FromDate.Value.ToString("MM/dd/yyyy"));
            context.MessageFormatter.AppendArgument("ToDate", model.ToDate.Value.ToString("MM/dd/yyyy"));

            for (var checkDate = model.FromDate.Value.Date; checkDate <= model.ToDate.Value.Date; checkDate = checkDate.AddDays(1))
            {
                if (daysOfWeek.Contains((int)checkDate.DayOfWeek))
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> EventExistedAsync(int? id, CancellationToken cancellationToken)
        {
            return await _timetableService.GetStaffScheduleByIdAsync(id.Value, cancellationToken) != null;
        }

        private async Task<bool> EventInFutureAsync(int? id, CancellationToken token)
        {
            var evt = await _timetableService.GetStaffScheduleByIdAsync(id.Value);

            return evt == null || evt.StartTime >= DateTime.Now.Date;
        }
    }
}
