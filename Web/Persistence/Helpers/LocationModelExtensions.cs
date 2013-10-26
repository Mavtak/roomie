using System;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    public static class LocationModelExtensions
    {
        public static string[] GetParts(this DeviceLocationModel location)
        {
            if (location == null)
            {
                return new string[0];
            }

            if (location.Name == null)
            {
                return new string[0];
            }

            var parts = location.Name.Split('/');

            return parts;
        }

        public static int CompareByParts(this ILocation location1, ILocation location2)
        {
            var location1Parts = (location1 == null) ? (new string[0]) : (location1.GetParts().ToArray());
            var location2Parts = (location2 == null) ? (new string[0]) : (location2.GetParts().ToArray());

            for (var i = 0; i < location1Parts.Length && i < location2Parts.Length; i++)
            {
                var location1Part = location1Parts[i];
                var location2Part = location2Parts[i];

                var result = string.Compare(location1Part, location2Part, StringComparison.InvariantCultureIgnoreCase);
                
                if (result != 0)
                {
                    return result;
                }
            }

            if (location1Parts.Length < location2Parts.Length)
            {
                return -1;
            }
            else if (location1Parts.Length > location2Parts.Length)
            {
                return 1;
            }

            return 0;
        }
    }
}
