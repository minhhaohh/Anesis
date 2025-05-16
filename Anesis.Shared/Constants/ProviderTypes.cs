namespace Anesis.Shared.Constants
{
    public static class ProviderTypes
    {
        public const string ARNP = "ARNP";
        public const string MD = "MD";
        public const string PA = "PA";
        public const string PAC = "PA-C";
        public const string PLLC = "PLLC";

        public static string[] GetAll()
        {
            return [ARNP, MD, PA, PAC];
        }

        public static string[] GetDoctor()
        {
            return [MD];
        }

        public static string[] GetMidLevel()
        {
            return [ARNP, PA, PAC];
        }
    }
}
