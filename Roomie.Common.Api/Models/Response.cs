namespace Roomie.Common.Api.Models
{
    public class Response<TData>
        where TData : class
    {
        public TData Data { get; set; }
        public Error Error { get; set; }
    }

    public class Response
    {
        public static Response<TData> Create<TData>(TData data)
          where TData : class
        {
            return new Response<TData>
            {
                Data = data,
            };
        }
    }
}
