namespace Anesis.ApiService.Domain.DTOs.SiteSettings
{
    public class GeneralSettings : ISettings
    {
        /// <summary>
        /// This setting value determine max days that we should keep the recent
        /// log activities when performing the action Delete Activities will
        /// delete all the activities that were created prior to MAX AGE days ago.
        /// </summary>
        public int ActivityMaxAge { get; set; } = 60;

        ///// <summary>
        ///// List of companies that use our software. This value is used
        ///// for the field Company in Software screen. It is a list of
        ///// strings separated by commas or semi-colon.
        ///// </summary>
        //public List<string> CompaniesUseSoftware { get; set; } = new List<string>();

        /// <summary>
        /// Indicates the start time of office working hours. This will be used to limit the time period for customer calendar events.
        /// </summary>
        public string BusinessStartTime { get; set; } = "06:00";

        /// <summary>
        /// Indicates the end time of office working hours. This will be used to limit the time period for customer calendar events.
        /// </summary>
        public string BusinessEndTime { get; set; } = "20:00";

        /// <summary>
        /// Indicates whether the office is open on Saturday or not.
        /// </summary>
        public bool SaturdayOpen { get; set; } = false;

        /// <summary>
        /// Indicates whether the office is open on Sunday or not.
        /// </summary>
        public bool SundayOpen { get; set; } = false;

        /// <summary>
        /// Specify the media storage provider should be used to store media files.
        /// List of providers: AzureBlobStorageProvider, DropBoxStorageProvider
        /// </summary>
        public string MediaStorage { get; set; } = "AzureBlobStorageProvider";
    }
}
