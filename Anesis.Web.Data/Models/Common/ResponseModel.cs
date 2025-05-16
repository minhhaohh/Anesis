namespace Anesis.Web.Data.Models
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }

        public List<string> Messages { get; set; }

        public T Data { get; set; }

        public int TotalCount { get; set; }

        public ResponseModel () 
        {
            Success = true;
            Messages = new List<string>();
            TotalCount = 0;
        }
    }
}
