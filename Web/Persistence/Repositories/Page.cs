
namespace Roomie.Web.Persistence.Repositories
{
    public class Page<T>
    {
        public T[] Items { get; internal set; }
        public int Total { get; internal set; }
        public SortDirection Sort { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
    }
}
