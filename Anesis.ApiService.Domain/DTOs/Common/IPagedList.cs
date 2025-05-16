namespace Anesis.ApiService.Domain.DTOs.Common
{
    public interface IPagedList<T>
    {
        public List<T> Data { get; set; }

        public int TotalCount { get; set; }

        public int TotalPage { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
