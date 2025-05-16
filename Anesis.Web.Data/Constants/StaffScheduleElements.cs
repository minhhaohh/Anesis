namespace Anesis.Web.Data.Constants
{
    public static class StaffScheduleElements
    {
        public const string TimelineLocationId = "timeline-location";
        public const string TimelineEmployeeId = "timeline-employee";

        public const string InitLocTimelineInstanceFunc = "initTimelineByLocation";
        public const string InitEmpTimelineInstanceFunc = "initTimelineByEmployee";

        public const string SetDotNetHelperFunc = "setDotNetHepler";
        public const string UpdateTimelineResourcesFunc = "updateTimelineResources";
        public const string UpdateTimelineHiddenDaysFunc = "updateTimelineHiddenDays";
        public const string RefreshTimelineFunc = "refreshTimelineEvents";

        public const int ResourceTimeOffId = 0;
        public const int ResourceHolidayId = -1;

        public static string GetInitTimelineInstanceFunc(string elementId)
        {
            return elementId switch
            {
                TimelineLocationId => InitLocTimelineInstanceFunc,
                TimelineEmployeeId => InitEmpTimelineInstanceFunc,
                _ => string.Empty
            };
        }
    }
}
