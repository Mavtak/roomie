namespace Roomie.Common.Api.Models
{
    public class Response<TData>
        where TData : class
    {
        public TData Data { get; set; }
        public Error Error { get; set; }
    }
}
