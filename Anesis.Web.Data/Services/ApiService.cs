namespace Anesis.Web.Data.Services
{
    public partial class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        private static string API_Auth = "api/Auth";
        private static string API_Customers = "api/Customers";
        private static string API_Devices = "api/Devices";
        private static string API_Dropdown_Data = "api/DropdownData";
        private static string API_Invoices = "api/Invoices";
        private static string API_Locations = "api/Locations";
        private static string API_Media_Files = "api/MediaFiles";
        private static string API_Menus = "api/Menus";
        private static string API_Patients = "api/Patients";
        private static string API_Procedures = "api/Procedures";
        private static string API_Proposals = "api/Proposals";
        private static string API_Providers = "api/Providers";
        private static string API_Reconciliation = "api/Reconciliation";
        private static string API_Site_Settings = "api/SiteSettings";
        private static string API_Surgery_Cases = "api/SurgeryCases";
        private static string API_Timetables = "api/Timetables";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
