namespace Roomie.Common.Api.Models
{
    public class Response<TData>
        where TData : class
    {
        public TData Data { get; set; }
        public Error Error { get; set; }
    }

    public class Response : Response<object>
    {
        public static Response<TData> Create<TData>(TData data)
          where TData : class
        {
            return new Response<TData>
            {
                Data = data,
            };
        }

        public static Response<TData> CreateError<TData>(string message, params string[] types)
          where TData : class
        {
            return new Response<TData>
            {
                Error = new Error
                {
                    Message = message,
                    Types = types,
                }
            };
        }

        public static Response CreateError(string message, params string[] types)
        {
            return new Response
            {
                Error = new Error
                {
                    Message = message,
                    Types = types,
                }
            };
        }
    }
}
