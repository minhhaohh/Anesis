namespace Anesis.Web.Data.Constants
{
    public class PageSize
    {
        public const int Size_5 = 5;
        public const int Size_10 = 10;
        public const int Size_20 = 20;
        public const int Size_50 = 50;
        public const int Size_100 = 100;
        public const int Size_200 = 200;
        public const int Size_500 = 500;
        public const int Size_1000 = 1000;

        public static int Default()
        {
            return Size_20;
        }

        public static List<int> All()
        {
            return [Size_5, Size_10, Size_20, Size_50, Size_100, Size_200, Size_500, Size_1000];
        }
    }
}
