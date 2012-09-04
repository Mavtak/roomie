﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roomie.Web.Models;

namespace Roomie.Web.Website.Helpers
{
    public class DeviceSort : IComparer<DeviceModel>
    {
        int IComparer<DeviceModel>.Compare(DeviceModel x, DeviceModel y)
        {
            if (x.Location != null && !String.IsNullOrWhiteSpace(x.Location.Name)
                && y.Location != null && !String.IsNullOrWhiteSpace(y.Location.Name))
            {
                if (x.Location.Name != y.Location.Name)
                {
                    return x.Location.Name.CompareTo(y.Location.Name);
                }
            }
            else if (x.Location != null && !String.IsNullOrWhiteSpace(x.Location.Name))
            {
                return -1;
            }
            else if (y.Location != null && !String.IsNullOrWhiteSpace(y.Location.Name))
            {
                return 1;
            }

            return (x.Name ?? String.Empty).CompareTo(y.Name ?? String.Empty);
        }
    }
}