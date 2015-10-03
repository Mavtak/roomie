using System;
using System.Collections.Generic;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    public sealed class DeviceSort : IComparer<Device>
    {
        int IComparer<Device>.Compare(Device x, Device y)
        {
            int result = x.Location.CompareByParts(y.Location);
            if (result != 0)
            {
                return result;
            }

            result = string.Compare(x.Name ?? String.Empty, y.Name ?? String.Empty, StringComparison.InvariantCultureIgnoreCase);

            if (result != 0)
            {
                return result;
            }

            result = string.Compare(x.Address ?? String.Empty, y.Address ?? string.Empty, StringComparison.CurrentCultureIgnoreCase);

            return result;
        }
    }
}