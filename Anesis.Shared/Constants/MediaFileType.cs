namespace Anesis.Shared.Constants
{
    public class MediaFileType
    {
        public const string PDF = ".pdf";

        public const string DOC = ".doc";

        public const string DOCX = ".docx";

        public const string XLXS = ".xlxs";

        public const string CSV = ".csv";

        public const string JPEG = ".jpeg";

        public const string PNG = ".png";

        public static string[] GetAll()
        {
            return [PDF, DOC, DOCX, XLXS, CSV, JPEG, PNG];
        }

        public static string[] WordTypes()
        {
            return [DOC , DOCX];
        }

        public static string[] ImageTypes()
        {
            return [JPEG, PNG];
        }
    }
}
