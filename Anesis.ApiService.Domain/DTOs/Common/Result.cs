namespace Anesis.ApiService.Domain.DTOs.Common
{
    public class Result
    {
        public bool Success => !Messages.Any();

        public List<string> Messages { get; set; }

        public object Data { get; set; }

        public int TotalCount { get; set; }

        public Result()
        {
            Messages = new List<string>();
            TotalCount = 1;
        }

        public Result AddMessages(params string[] messages)
        {
            Messages.AddRange(messages);
            return this;
        }

        public Result AddMessages(IEnumerable<string> messages)
        {
            if (messages != null)
            {
                Messages.AddRange(messages);
            }

            return this;
        }

        public static Result Ok(object data, int total = 1)
        {
            return new Result
            {
                Data = data,
                TotalCount = total
            };
        }

        public static Result Ok<T>(List<T> data)
        {
            return Ok(data, data.Count);
        }

        public static Result Ok<T>(IPagedList<T> result)
        {
            return Ok(result.Data, result.TotalCount);
        }

        public static Result Error(params string[] messages)
        {
            return new Result().AddMessages(messages);
        }

        public static Result Error(IEnumerable<string> messages)
        {
            return new Result().AddMessages(messages);
        }
    }
}
