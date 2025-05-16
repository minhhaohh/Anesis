namespace Anesis.ApiService.Domain.DTOs.Common
{
    public abstract class PageableSearchModelBase
    {
        public int StartIndex { get; set; }

        public int CountNumber { get; set; }

        public string Sidx { get; set; }

        public string Sord { get; set; }
    }
}
