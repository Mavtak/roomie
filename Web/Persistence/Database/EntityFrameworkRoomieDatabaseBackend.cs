using System.Data.Entity;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models;

namespace Roomie.Web.Persistence.Database
{
    public sealed class EntityFrameworkRoomieDatabaseBackend : DbContext
    {
        public DbSet<EntityFrameworkUserModel> Users { get; set; }
        public DbSet<EntityFrameworkUserSessionModel> UserSessions { get; set; }
        public DbSet<EntityFrameworkComputerModel> Computers { get; set; }
        public DbSet<EntityFrameworkNetworkGuestModel> NetworkGuests { get; set; }
        public DbSet<EntityFrameworkNetworkModel> Networks { get; set; }
        public DbSet<EntityFrameworkDeviceModel> Devices { get; set; }
        public DbSet<EntityFrameworkTaskModel> Tasks { get; set; }
        public DbSet<EntityFrameworkScriptModel> Scripts { get; set; }
        public DbSet<EntityFrameworkWebHookSessionModel> WebHookSessions { get; set; }

        public EntityFrameworkRoomieDatabaseBackend(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DeviceTypeMapping());

            modelBuilder.Entity<EntityFrameworkDeviceModel>().Ignore(x => x.CurrentAction);
        }

        public void Reset()
        {
            DatabaseUtilities.Reset(this);
        }
    }
}
