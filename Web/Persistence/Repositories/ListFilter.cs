
namespace Roomie.Web.Persistence.Repositories
{
    public class ListFilter
    {
        public const int MaxPageSize = 50;
        public const int DefaultPageSize = 25;

        private int? _page;
        private int? _count;

        public int Page
        {
            get
            {
                if (_page < 1 || _page == null)
                {
                    return 1;
                }

                return _page.Value;
            }
            set
            {
                _page = value;
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

    }
}
