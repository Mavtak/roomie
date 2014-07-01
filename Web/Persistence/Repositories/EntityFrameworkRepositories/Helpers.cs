using System.Linq;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public static class Helpers
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> items, ListFilter filter)
        {
            if (filter == null)
            {
                filter = new ListFilter();
            }

            var page = filter.Page;
            var count = filter.Count;

            var result = items.Skip((page - 1)*count)
                              .Take(count);

            return result;
        }
    }
}
