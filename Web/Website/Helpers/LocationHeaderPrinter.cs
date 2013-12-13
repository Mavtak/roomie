using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Website.Helpers
{
    public class LocationHeaderPrinter
    {
        private ILocation _lastLocation;
        private ILocation _thisLocation;
        private int _openDivCount = 0;
        private int _lastHeaderDepth = -1;

        public void SetCurrentLocation(ILocation location)
        {
            _lastLocation = _thisLocation;
            _thisLocation = location;
        }

        public HtmlString PrintHeader()
        {
            var result = new StringBuilder();

            foreach(var tuple in GetHeaders())
            {
                var headerNumber = 2 + tuple.Item2;

                for (var i = 0; i < _lastHeaderDepth - tuple.Item2 + 1; i++)
                {
                    result.Append("</div>");
                    _openDivCount--;
                }

                _lastHeaderDepth = tuple.Item2;

                
                result.Append("<h" + headerNumber + @" class=""collapse-next"">" + tuple.Item1 + "</h" + headerNumber + ">");

                result.Append(@"<div class=""group"">");
                _openDivCount++;
            }

            return new HtmlString(result.ToString());
        }

        public IEnumerable<Tuple<string, int>> GetHeaders()
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
                            yield return new Tuple<string, int>(locationParts[i], i);
                        }

                    }
                }
            }
        }

        public HtmlString PrintDone()
        {
            var builder = new StringBuilder();
            
           while (_openDivCount > 0)
           {
               builder.Append("</div>");
               _openDivCount--;
           }

            return new HtmlString(builder.ToString());
        }
    }
}