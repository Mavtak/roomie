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
        public IScriptRepository Scripts { get; set; }
        public ITaskRepository Tasks { get; set; }
        public IUserRepository Users { get; set; }
        public ISessionRepository Sessions { get; set; }

        public static string ConnectionString { private get; set; }

        private readonly EntityFrameworkRoomieDatabaseBackend _database;

        public EntityFrameworkRoomieDatabaseBackend Backend
        {
            get
            {
                return _database;
            }
        }

        public RoomieDatabaseContext()
        {
            _database = new EntityFrameworkRoomieDatabaseBackend(ConnectionString ?? "RoomieDatabaseContext");

            Tasks = new TaskRepository(_database.Tasks, _database.Computers, _database.Scripts, _database.Users);

            Scripts = new ScriptRepository(_database.Scripts, () => SaveChanges());

            Computers = new ComputerRepository(_database.Computers, _database.Scripts, _database.Users);

            NetworkGuests = new NetworkGuestRepository(_database.NetworkGuests, _database.Users);

            var entityframeworkNetworkRepository = new NetworkRepository(_database.Networks);
            Networks = new GuestEnabledNetworkRepository(entityframeworkNetworkRepository, NetworkGuests);

            var entityFrameworkDeviceRepository = new DeviceRepository(_database.Devices, Scripts, Tasks);
            Devices = new GuestEnabledDeviceRepository(entityFrameworkDeviceRepository, NetworkGuests);

            DeviceLocations = new DeviceLocationRepository(_database.DeviceLocations);

            Users = new UserRepository(_database.Users);

            Sessions = new SessionRepository(_database.UserSessions, _database.WebHookSessions, _database.Computers, _database.Users);
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