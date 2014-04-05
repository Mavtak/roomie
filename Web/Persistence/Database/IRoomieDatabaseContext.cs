using System;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieDatabaseContext : IDisposable
    {
        IRoomieEntitySet<ScriptModel> Scripts { get; set; }
        int SaveChanges();
        void Reset();

        IComputerRepository Computers { get; set; }
        IDeviceRepository Devices { get; set; }
        IDeviceLocationRepository DeviceLocations { get; set; }
        INetworkGuestRepository NetworkGuests { get; set; }
        INetworkRepository Networks { get; set; }
        ISavedScriptRepository SavedScripts { get; set; }
        ITaskRepository Tasks { get; set; }
        IUserRepository Users { get; set; }
        ISessionRepository Sessions { get; set; }
    }
}