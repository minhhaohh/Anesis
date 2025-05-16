namespace Anesis.Web.Data.Models
{
    public class GeneralSettingsModel
    {
        public int ActivityMaxAge { get; set; }

        //public List<string> CompaniesUseSoftware { get; set; }

        public string BusinessStartTime { get; set; }
        
        public string BusinessEndTime { get; set; }

        public bool SaturdayOpen { get; set; }

        public bool SundayOpen { get; set; }

        public string MediaStorage { get; set; }
    }

    public class StaffScheduleSettingsModel
    {
        public string TimeOffResourceLabel { get; set; }

        public string HolidayResourceLabel { get; set; }

        public int SlotWidth { get; set; }

        public string SlotDuration { get; set; }

        public string BelRedEndTime { get; set; }

        public string ScheduleChangeInformEmails { get; set; }
    }
}
