
namespace Roomie.Web.Persistence.Repositories
{
    public class ListFilter
    {
        public const int MaxPageSize = 50;
        public const int DefaultPageSize = 25;

        private int _page;
        private int _count;

        public ListFilter()
        {
            Page = 1;
            Count = DefaultPageSize;
        }

        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                _page = value;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                if (value > MaxPageSize)
                {
                    value = MaxPageSize;
                }

                _count = value;
            }
        }

    }
}
