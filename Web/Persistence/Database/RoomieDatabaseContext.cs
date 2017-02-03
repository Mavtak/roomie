using System;
using System.Data.Common;
using System.Data.SqlClient;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Persistence.Repositories.DapperRepositories;
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

        private SqlConnection _databaseConnection;
        private EntityFrameworkRoomieDatabaseBackend _entityFrameworkRoomieDatabaseBackend;
        private IRepositoryFactory _repositoryFactory;

        public EntityFrameworkRoomieDatabaseBackend Backend
        {
            get
            {
                return _entityFrameworkRoomieDatabaseBackend;
            }
        }

        public RoomieDatabaseContext()
        {
            _databaseConnection = DatabaseConnectionFactory.Connect();
            _entityFrameworkRoomieDatabaseBackend = new EntityFrameworkRoomieDatabaseBackend(_databaseConnection);
            _repositoryFactory = new CompositeImplementationRepositoryFactory(
                new DapperRepositoryFactory(_databaseConnection),
                new EntityFrameworkRepositoryFactory(
                    _entityFrameworkRoomieDatabaseBackend,
                    new Lazy<IRepositoryFactory>(() => _repositoryFactory)
                )
            );

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
            DatabaseUtilities.Reset(_entityFrameworkRoomieDatabaseBackend);
        }

        private void SaveChanges()
        {
            _entityFrameworkRoomieDatabaseBackend.SaveChanges();
        }

        public void Dispose()
        {
            _databaseConnection.Dispose();
            _entityFrameworkRoomieDatabaseBackend.Dispose();
        }
    }
}