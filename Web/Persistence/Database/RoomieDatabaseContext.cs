using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public sealed class RoomieDatabaseContext : DbContext, IRoomieDatabaseContext
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

        public static string ConnectionString { private get; set; }

        public RoomieDatabaseContext()
            : base(ConnectionString??"RoomieDatabaseContext")
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

    public class DeviceTypeMapping : ComplexTypeConfiguration<Roomie.Common.HomeAutomation.DeviceType>
    {
        public DeviceTypeMapping()
        {
            this.Property(p => p.Name);
        }
    }
}