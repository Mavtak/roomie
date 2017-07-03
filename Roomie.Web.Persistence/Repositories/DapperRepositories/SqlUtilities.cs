using System;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public static class SqlUtilities
    {
        public static string OrderByDirection(SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    return "ASC";

                case SortDirection.Descending:
                    return "DESC";

                default:
                    throw new ArgumentException($"Unrecognized SortDirection \"{sortDirection}\".", nameof(sortDirection));
            }
        }
    }
}
