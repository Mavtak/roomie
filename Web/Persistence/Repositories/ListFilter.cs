
namespace Roomie.Web.Persistence.Repositories
{
    public class ListFilter
    {
        public const int MaxPageSize = 50;
        public const int DefaultPageSize = 25;
        public const SortDirection DefaultSortDirection = SortDirection.Descending;

        private int? _start;
        private int? _count;
        private SortDirection? _sortDirection;

        public int Start
        {
            get
            {
                if (_start < 1 || _start == null)
                {
                    return 0;
                }

                return _start.Value;
            }
            set
            {
                _start = value;
            }
        }

        public int Count
        {
            get
            {
                if (_count < 1)
                {
                    return 1;
                }

                if (_count > MaxPageSize || _count == null)
                {
                    return MaxPageSize;
                }

                return _count.Value;
            }
            set
            {
                _count = value;
            }
        }

        public SortDirection SortDirection
        {
            get
            {
                if (_sortDirection == null)
                {
                    return DefaultSortDirection;
                }

                return _sortDirection.Value;
            }
            set
            {
                _sortDirection = value;
            }
        }
    }
}
