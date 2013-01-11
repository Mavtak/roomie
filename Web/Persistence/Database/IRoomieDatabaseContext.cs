using System;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieDatabaseContext : IDisposable
    {
        DbSet<UserModel> Users { get; set; }
        DbSet<UserSessionModel> UserSessions { get; set; }
        DbSet<ComputerModel> Computers { get; set; }
        DbSet<NetworkModel> Networks { get; set; }
        DbSet<DeviceModel> Devices { get; set; }
        DbSet<TaskModel> Tasks { get; set; }
        DbSet<ScriptModel> Scripts { get; set; }
        DbSet<SavedScriptModel> SavedScripts { get; set; }
        DbSet<WebHookSessionModel> WebHookSessions { get; set; }
        DbSet<DeviceLocationModel> DeviceLocations { get; set; }
        int SaveChanges();
        void Reset();
        //public DbSet<StringStringPair> StringStringDictionary { get; set; }
        //public DbSet<HomeModel> Homes { get; set; }
    }
}