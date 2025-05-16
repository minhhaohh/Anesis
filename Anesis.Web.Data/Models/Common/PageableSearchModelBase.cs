using Anesis.Web.Data.Constants;

namespace Anesis.Web.Data.DTO
{
    public class PageableSearchModelBase
    {
        public int StartIndex { get; set; } = 0;

        public int CountNumber { get; set; } = PageSize.Default();

        public string Sidx { get; set; }

        public string Sord { get; set; }
    }
}
