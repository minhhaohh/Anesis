namespace Anesis.Web.Data.Constants
{
    public static class SurgeryCaseStatusColors
    {
        public const string Pending = "rz-color-warning";
        public const string Completed = "rz-color-success";
        public const string Cancelled = "rz-color-danger";

        public static Dictionary<string, string> GetAll()
        {
            return new Dictionary<string, string>() 
            {
                [nameof(Pending)] = Pending,
                [nameof(Completed)] = Completed,
                [nameof(Cancelled)] = Cancelled,
            };
        }

        public static string GetColorClass(string status)
        {
            return GetAll().GetValueOrDefault(status, "");
        }
    }
}
