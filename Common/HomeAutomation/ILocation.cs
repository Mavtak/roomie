
using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    public interface ILocation
    {
        IEnumerable<string> GetParts();
        void Update(IEnumerable<string> parts);
        bool IsSet { get; }
    }
}
