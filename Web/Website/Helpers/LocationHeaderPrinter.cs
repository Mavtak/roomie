using System.Linq;
using System.Web;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Website.Helpers
{
    public class LocationHeaderPrinter
    {
        private ILocation _lastLocation;
        private IDevice _device;

        public void SetCurrentDevice(IDevice device)
        {
            _lastLocation = (_device == null) ? null : _device.Location;
            _device = device;
        }

        public HtmlString PrintHeader()
        {
            if (_lastLocation.CompareByParts(_device.Location) != 0)
            {
                var lastLocationParts = (_lastLocation == null) ? new string[0] : _lastLocation.GetParts().ToArray();
                var locationParts = _device.Location.GetParts().ToArray();

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