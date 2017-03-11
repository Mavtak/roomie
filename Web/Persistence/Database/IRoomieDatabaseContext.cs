using System;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieDatabaseContext : IDisposable
    {
        IComputerRepository Computers { get; }
        IDeviceRepository Devices { get; }
        INetworkGuestRepository NetworkGuests { get; }
        INetworkRepository Networks { get; }
        IScriptRepository Scripts { get; }
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }
        ISessionRepository Sessions { get; }
    }
}