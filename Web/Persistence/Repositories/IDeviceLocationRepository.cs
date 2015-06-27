﻿
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IDeviceLocationRepository
    {
        DeviceLocationModel Get(int id);
        DeviceLocationModel Get(UserModel user, string path);
        void Add(DeviceLocationModel location);
        void Remove(DeviceLocationModel location);
    }
}
