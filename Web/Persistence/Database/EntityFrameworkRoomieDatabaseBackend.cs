using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public sealed class EntityFrameworkRoomieDatabaseBackend : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserSessionModel> UserSessions { get; set; }
        public DbSet<ComputerModel> Computers { get; set; }
        public DbSet<NetworkGuestModel> NetworkGuests { get; set; }
        public DbSet<NetworkModel> Networks { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ScriptModel> Scripts { get; set; }
        public DbSet<SavedScriptModel> SavedScripts { get; set; }
        public DbSet<WebHookSessionModel> WebHookSessions { get; set; }
        public DbSet<DeviceLocationModel> DeviceLocations { get; set; }

        public EntityFrameworkRoomieDatabaseBackend(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DeviceTypeMapping());

            modelBuilder.Entity<DeviceModel>().Ignore(x => x.CurrentAction);
        }

        public void Reset()
        {
            DatabaseUtilities.Reset(this);
        }
    }
}
