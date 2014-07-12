using System;
using System.Linq;

namespace Roomie.Web.Persistence.Repositories
{
    public class Page<T>
    {
        public T[] Items { get; internal set; }
        public int Total { get; internal set; }
        public SortDirection Sort { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }

        public Page<TOther> Transform<TOther>(Func<T, TOther> transform)
        {
            var newItems = Items.Select(transform).ToArray();

            var result = new Page<TOther>
            {
                Items = newItems,
                Total = Total,
                Sort = Sort,
                PageNumber = PageNumber,
                PageSize = PageSize
            };

            return result;
        }
    }
}
