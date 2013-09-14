using System;
using System.Collections.Generic;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    public sealed class DeviceSort : IComparer<DeviceModel>
    {
        int IComparer<DeviceModel>.Compare(DeviceModel x, DeviceModel y)
        {
            int result = x.Location.CompareByParts(y.Location);
            if (result != 0)
            {
                return result;
            }

            result = string.Compare(x.Name ?? String.Empty, y.Name ?? String.Empty, StringComparison.InvariantCultureIgnoreCase);

            return result;
        }
    }
}