using System;
using System.Linq;
using System.Linq.Expressions;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public static class Helpers
    {
        public static IQueryable<T> Page<T, TKey>(this IQueryable<T> items, ListFilter filter, Expression<Func<T, TKey>> keySelector)
        {
            if (filter == null)
            {
                filter = new ListFilter();
            }

            var page = filter.Page;
            var count = filter.Count;

            IOrderedQueryable<T> wholeList;

            switch (filter.SortDirection)
            {
                case SortDirection.Ascending:
                    wholeList = items.OrderBy(keySelector);
                    break;

                case SortDirection.Descending:
                    wholeList = items.OrderByDescending(keySelector);
                    break;

                default:
                    throw new Exception("Unknown SortDirection " + filter.SortDirection + ".");
            }

            var pagedList = wholeList.Skip((page - 1)*count);
            pagedList = pagedList.Take(count);

            return pagedList;
        }
    }
}
