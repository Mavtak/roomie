using System;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieDatabaseContext : IDisposable
    {
        IRoomieEntitySet<UserSessionModel> UserSessions { get; set; }
        IRoomieEntitySet<ComputerModel> Computers { get; set; }
        IRoomieEntitySet<ScriptModel> Scripts { get; set; }
        IRoomieEntitySet<SavedScriptModel> SavedScripts { get; set; }
        IRoomieEntitySet<WebHookSessionModel> WebHookSessions { get; set; }
        IRoomieEntitySet<DeviceLocationModel> DeviceLocations { get; set; }
        int SaveChanges();
        void Reset();

        IDeviceRepository Devices { get; set; }
        INetworkRepository Networks { get; set; }
        ITaskRepository Tasks { get; set; }
        IUserRepository Users { get; set; }
        //public DbSet<StringStringPair> StringStringDictionary { get; set; }
        //public DbSet<HomeModel> Homes { get; set; }
    }
}