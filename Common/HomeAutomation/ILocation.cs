
using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    public interface ILocation
    {
        IEnumerable<string> GetParts();
        void Update(IEnumerable<string> parts);

        //TODO: remove this maybe?  An extension method instead?
        bool IsSet { get; }
    }
}
