using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories;

namespace Roomie.Web.Persistence.Database
{
    public sealed class RoomieDatabaseContext : IRoomieDatabaseContext
    {
        public IComputerRepository Computers { get; set; }
        public IDeviceRepository Devices { get; set; }
        public INetworkGuestRepository NetworkGuests { get; set; }
        public INetworkRepository Networks { get; set; }
        public IScriptRepository Scripts { get; set; }
        public ITaskRepository Tasks { get; set; }
        public IUserRepository Users { get; set; }
        public ISessionRepository Sessions { get; set; }

        private readonly EntityFrameworkRoomieDatabaseBackend _database;

        private IRepositoryFactory _repositoryFactory;

        public EntityFrameworkRoomieDatabaseBackend Backend
        {
            get
            {
                return _database;
            }
        }

        public RoomieDatabaseContext()
            : this(new EntityFrameworkRoomieDatabaseBackend())
        {
        }

        public RoomieDatabaseContext(EntityFrameworkRoomieDatabaseBackend database)
        {
            _database = database;

            _repositoryFactory = new EntityFrameworkRepositoryFactory(database);

            Tasks = _repositoryFactory.GetTaskRepository();
            Scripts = _repositoryFactory.GetScriptRepository();
            Computers = _repositoryFactory.GetComputerRepository();
            NetworkGuests = _repositoryFactory.GetNetworkGuestRepository();
            Networks = _repositoryFactory.GetNetworkRepository();
            Devices = _repositoryFactory.GetDeviceRepository();
            Users = _repositoryFactory.GetUserRepository();
            Sessions = _repositoryFactory.GetSessionRepository();
        }

        public void Reset()
        {
            DatabaseUtilities.Reset(_database);
        }

        private void SaveChanges()
        {
            _database.SaveChanges();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}