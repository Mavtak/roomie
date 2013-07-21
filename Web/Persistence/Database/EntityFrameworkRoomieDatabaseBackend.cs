using Roomie.Web.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Roomie.Web.Persistence.Database
{
    public sealed class EntityFrameworkRoomieDatabaseBackend : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserSessionModel> UserSessions { get; set; }
        public DbSet<ComputerModel> Computers { get; set; }
        public DbSet<NetworkModel> Networks { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ScriptModel> Scripts { get; set; }
        public DbSet<SavedScriptModel> SavedScripts { get; set; }
        public DbSet<WebHookSessionModel> WebHookSessions { get; set; }
        public DbSet<DeviceLocationModel> DeviceLocations { get; set; }
        //public DbSet<StringStringPair> StringStringDictionary { get; set; }
        //public DbSet<HomeModel> Homes { get; set; }

        public EntityFrameworkRoomieDatabaseBackend(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Roomie.Common.HomeAutomation.DeviceType>(new DeviceTypeMapping());
        }

        public void Reset()
        {
            DatabaseUtilities.Reset(this);
        }
    }
}
