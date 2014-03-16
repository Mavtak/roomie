using System;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieDatabaseContext : IDisposable
    {
        IRoomieEntitySet<UserModel> Users { get; set; }
        IRoomieEntitySet<UserSessionModel> UserSessions { get; set; }
        IRoomieEntitySet<ComputerModel> Computers { get; set; }
        IRoomieEntitySet<DeviceModel> Devices { get; set; }
        IRoomieEntitySet<ScriptModel> Scripts { get; set; }
        IRoomieEntitySet<SavedScriptModel> SavedScripts { get; set; }
        IRoomieEntitySet<WebHookSessionModel> WebHookSessions { get; set; }
        IRoomieEntitySet<DeviceLocationModel> DeviceLocations { get; set; }
        int SaveChanges();
        void Reset();

        INetworkRepository Networks { get; set; }
        ITaskRepository Tasks { get; set; }
        //public DbSet<StringStringPair> StringStringDictionary { get; set; }
        //public DbSet<HomeModel> Homes { get; set; }
    }
}