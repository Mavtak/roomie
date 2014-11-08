
namespace Roomie.Web.Persistence.Repositories
{
    public class CleaningResult
    {
        public ListFilter NextFilter { get; set; }
        public int Deleted { get; set; }
        public int Skipped { get; set; }
        public bool Done { get; set; }

        public CleaningResult(ListFilter filter, int totalItemsCount, int deletedItemsCount)
        {
            Deleted = deletedItemsCount;
            Skipped = totalItemsCount - Deleted;
            NextFilter = new ListFilter
            {
                SortDirection = filter.SortDirection,
                Start = filter.Start + Skipped
            };

            if (Deleted == 0 && Skipped == 0)
            {
                Done = true;
            }
        }
    }
}
