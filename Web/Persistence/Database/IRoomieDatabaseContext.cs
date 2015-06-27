using System;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieDatabaseContext : IDisposable
    {
        int SaveChanges();
        void Reset();

        IComputerRepository Computers { get; }
        IDeviceRepository Devices { get; }
        IDeviceLocationRepository DeviceLocations { get; }
        INetworkGuestRepository NetworkGuests { get; }
        INetworkRepository Networks { get; }
        ISavedScriptRepository SavedScripts { get; }
        IScriptRepository Scripts { get; }
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }
        ISessionRepository Sessions { get; }
    }
}