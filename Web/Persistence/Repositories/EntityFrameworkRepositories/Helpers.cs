using System.Linq;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public static class Helpers
    {
        public const int MaxPageSize = 50;

        public static IQueryable<T> Page<T>(this IQueryable<T> items, int? page, int? count)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (count < 1)
            {
                count = 1;
            }

            if (count > MaxPageSize)
            {
                count = MaxPageSize;
            }

            var result = items.Skip((page ?? 1 - 1)*count ?? 0)
                              .Take(count ?? 0);

            return result;
        }
    }
}
