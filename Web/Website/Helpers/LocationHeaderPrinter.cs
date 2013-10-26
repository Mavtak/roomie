using System.Linq;
using System.Web;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Website.Helpers
{
    public class LocationHeaderPrinter
    {
        private ILocation _lastLocation;
        private ILocation _thisLocation;

        public void SetCurrentLocation(ILocation location)
        {
            _lastLocation = _thisLocation;
            _thisLocation = location;
        }

        public HtmlString PrintHeader()
        {
            if (_lastLocation.CompareByParts(_thisLocation) != 0)
            {
                var lastLocationParts = (_lastLocation == null) ? new string[0] : _lastLocation.GetParts().ToArray();
                var locationParts = _thisLocation.GetParts().ToArray();

                if (locationParts.Length > 0)
                {
                    for (var i = 0; i < locationParts.Length; i++)
                    {
                        if (i >= lastLocationParts.Length || lastLocationParts[i] != locationParts[i])
                        {
                            var headerNumber = i + 2;
                            return new HtmlString("<h" + headerNumber + ">" + locationParts[i] + "</h" + headerNumber + ">");
                        }

                    }
                }
            }

            return new HtmlString(string.Empty);
        }
    }
}