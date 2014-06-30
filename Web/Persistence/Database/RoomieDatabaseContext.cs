using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories;

namespace Roomie.Web.Persistence.Database
{
    public sealed class RoomieDatabaseContext : IRoomieDatabaseContext
    {
        public IComputerRepository Computers { get; set; }
        public IDeviceRepository Devices { get; set; }
        public IDeviceLocationRepository DeviceLocations { get; set; }
        public INetworkGuestRepository NetworkGuests { get; set; }
        public INetworkRepository Networks { get; set; }
        public ISavedScriptRepository SavedScripts { get; set; }
        public IScriptRepository Scripts { get; set; }
        public ITaskRepository Tasks { get; set; }
        public IUserRepository Users { get; set; }
        public ISessionRepository Sessions { get; set; }

        public static string ConnectionString { private get; set; }

        private readonly EntityFrameworkRoomieDatabaseBackend _database;

        public RoomieDatabaseContext()
        {
            _database = new EntityFrameworkRoomieDatabaseBackend(ConnectionString ?? "RoomieDatabaseContext");

            Computers = new ComputerRepository(_database.Computers);

            NetworkGuests = new NetworkGuestRepository(_database.NetworkGuests);

            var entityframeworkNetworkRepository = new NetworkRepository(_database.Networks);
            Networks = new GuestEnabledNetworkRepository(entityframeworkNetworkRepository, NetworkGuests);

            var entityFrameworkDeviceRepository = new DeviceRepository(_database.Devices);
            Devices = new GuestEnabledDeviceRepository(entityFrameworkDeviceRepository, NetworkGuests);

            DeviceLocations = new DeviceLocationRepository(_database.DeviceLocations);

            SavedScripts = new SavedScriptRepository(_database.SavedScripts);

            Scripts = new ScriptRepository(_database.Scripts);

            Tasks = new TaskRepository(_database.Tasks);

            Users = new UserRepository(_database.Users);

            Sessions = new SessionRepository(_database.UserSessions, _database.WebHookSessions);
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