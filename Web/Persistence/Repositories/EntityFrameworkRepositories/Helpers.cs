using System.Linq;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public static class Helpers
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> items, int? page, int? count)
        {
            var result = items.Skip((page ?? 1 - 1)*count ?? 0)
                              .Take(count ?? 0);

            return result;
        }
    }
}
