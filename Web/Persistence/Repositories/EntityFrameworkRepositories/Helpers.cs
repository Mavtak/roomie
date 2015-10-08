using System;
using System.Linq;
using System.Linq.Expressions;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public static class Helpers
    {
        public static Page<T> Page<T, TKey>(this IQueryable<T> items, ListFilter filter, Expression<Func<T, TKey>> keySelector)
        {
            if (filter == null)
            {
                filter = new ListFilter();
            }

            var offset = filter.Start;
            var count = filter.Count;
            var total = items.Count();

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

            var itemsOnPage = wholeList
                .Skip(offset)
                .Take(count)
                .ToArray();

            var result = new Page<T>
            {
                Items = itemsOnPage,
                Start = filter.Start,
                Count = filter.Count,
                Sort = filter.SortDirection,
                Total = total
            };

            return result;
        }
    }
}
