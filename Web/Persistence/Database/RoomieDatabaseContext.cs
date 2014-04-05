using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public sealed class RoomieDatabaseContext : IRoomieDatabaseContext
    {
        public IUserRepository Users { get; set; }
        public IComputerRepository Computers { get; set; }
        public INetworkGuestRepository NetworkGuests { get; set; }
        public INetworkRepository Networks { get; set; }
        public IDeviceRepository Devices { get; set; }
        public ITaskRepository Tasks { get; set; }
        public IScriptRepository Scripts { get; set; }
        public ISavedScriptRepository SavedScripts { get; set; }
        public IDeviceLocationRepository DeviceLocations { get; set; }
        public ISessionRepository Sessions { get; set; }

        public static string ConnectionString { private get; set; }

        private readonly EntityFrameworkRoomieDatabaseBackend _database;

        public RoomieDatabaseContext()
        {
            _database = new EntityFrameworkRoomieDatabaseBackend(ConnectionString ?? "RoomieDatabaseContext");

            Computers = new EntityFrameworkComputerRepository(_database.Computers);

            NetworkGuests = new EntityFrameworkNetworkGuestRepository(_database.NetworkGuests);

            var entityframeworkNetworkRepository = new EntityFrameworkNetworkRepository(_database.Networks);
            Networks = new GuestEnabledNetworkRepository(entityframeworkNetworkRepository, NetworkGuests);

            var entityFrameworkDeviceRepository = new EntityFrameworkDeviceRepository(_database.Devices, Networks);
            Devices = new GuestEnabledDeviceRepository(entityFrameworkDeviceRepository, NetworkGuests);

            DeviceLocations = new EntityFrameworkDeviceLocationRepository(_database.DeviceLocations);

            SavedScripts = new EntityFrameworkSavedScriptRepository(_database.SavedScripts);

            Scripts = new EntityFrameworkScriptRepository(_database.Scripts);

            Tasks = new EntityFrameworkTaskRepository(_database.Tasks);

            Users = new EntityFrameworkUserRepository(_database.Users);

            Sessions = new EntityFrameworkSessionRepository(_database.UserSessions, _database.WebHookSessions);
        }

        public void Reset()
        {
            DatabaseUtilities.Reset(_database);
        }

        public int SaveChanges()
        {
            return _database.SaveChanges();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}