using System;
using System.Data.Common;
using System.Data.SqlClient;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Persistence.Repositories.DapperRepositories;

namespace Roomie.Web.Persistence.Database
{
    public sealed class RoomieDatabaseContext : IRoomieDatabaseContext
    {
        public IComputerRepository Computers => _repositoryFactory.GetComputerRepository();
        public IDeviceRepository Devices => _repositoryFactory.GetDeviceRepository();
        public INetworkGuestRepository NetworkGuests => _repositoryFactory.GetNetworkGuestRepository();
        public INetworkRepository Networks => _repositoryFactory.GetNetworkRepository();
        public IScriptRepository Scripts => _repositoryFactory.GetScriptRepository();
        public ITaskRepository Tasks => _repositoryFactory.GetTaskRepository();
        public IUserRepository Users => _repositoryFactory.GetUserRepository();
        public ISessionRepository Sessions => _repositoryFactory.GetSessionRepository();

        private SqlConnection _databaseConnection;
        private IRepositoryFactory _repositoryFactory;

        public RoomieDatabaseContext()
        {
            _databaseConnection = DatabaseConnectionFactory.Connect();
            _repositoryFactory = new CompositeImplementationRepositoryFactory(
                new DapperRepositoryFactory(
                    _databaseConnection,
                    new Lazy<IRepositoryFactory>(() => _repositoryFactory)
                )
            );
        }

        public void Dispose()
        {
            _databaseConnection.Dispose();
        }
    }
}