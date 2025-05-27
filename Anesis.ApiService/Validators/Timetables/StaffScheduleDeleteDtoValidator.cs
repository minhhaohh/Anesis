using Anesis.ApiService.Domain.DTOs.Timetables;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Extensions;
using FluentValidation;

namespace Anesis.ApiService.Validators.Timetables
{
    public class StaffScheduleDeleteDtoValidator : AbstractValidator<StaffScheduleDeleteDto>
    {
        private readonly ITimetableService _timetableService;

        public StaffScheduleDeleteDtoValidator(ITimetableService timetableService)
        {
            _timetableService = timetableService;

            When(x => x.IsDeleteById(), () =>
            {
                RuleFor(x => x.Id)
                    .MustAsync(EventExistedAsync)
                    .WithMessage(x => $"Could not find schedule event with id #{x.Id}.")
                    .MustAsync(EventInFutureAsync)
                    .WithMessage(x => $"Could not change the past schedule.");
            });

            When(x => !x.IsDeleteById(), () =>
            {

                RuleFor(x => x.LocationId)
                    .GreaterThan(0)
                    .WithMessage("Please select the Location field.");

                RuleFor(x => x.EmployeeIds)
                    .Must(AtLeastOneEmployee)
                    .WithMessage("Please select at least 1 employee.");

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
            });
        }

        private bool AtLeastOneEmployee(List<int> employeeIds)
        {
            return employeeIds.Count > 0;
        }

        private bool AtLeastOneDay(StaffScheduleDeleteDto model, List<int> daysOfWeek)
        {
            return model.ToDate == null || daysOfWeek.Count > 0;
        }

        private bool HasDayInDateRange(
            StaffScheduleDeleteDto model, List<int> daysOfWeek,
            ValidationContext<StaffScheduleDeleteDto> context)
        {
            if (daysOfWeek.Count == 0
                || model.FromDate == null
                || model.FromDate.Value.Date < DateTime.Now.Date
                || model.ToDate == null
                || model.ToDate.Value.Date < model.FromDate.Value.Date)
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
