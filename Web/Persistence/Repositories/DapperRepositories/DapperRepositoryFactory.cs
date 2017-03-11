using System;
using System.Data;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class DapperRepositoryFactory : IRepositoryFactory
    {
        private IDbConnection _connection;
        private Lazy<IRepositoryFactory> _parentFactory;

        private IComputerRepository _computerRepository;
        private IDeviceRepository _deviceRepository;
        private INetworkRepository _networkRepository;
        private ISessionRepository _sessionRepository;
        private IScriptRepository _scriptRepository;
        private IUserRepository _userRepository;

        public DapperRepositoryFactory(IDbConnection connection, Lazy<IRepositoryFactory> parentFactory)
        {
            _connection = connection;
            _parentFactory = parentFactory;
        }

        public IComputerRepository GetComputerRepository()
        {
            if (_computerRepository == null)
            {
                var scriptRepository = GetRepository(x => x.GetScriptRepository());
                var userRepository = GetRepository(x => x.GetUserRepository());
                _computerRepository = new ComputerRepository(_connection, scriptRepository, userRepository);
            }

            return _computerRepository;
        }

        public IDeviceRepository GetDeviceRepository()
        {
            if (_deviceRepository == null)
            {
                var networkRepository = GetRepository(x => x.GetNetworkRepository());
                var scriptRepository = GetRepository(x => x.GetScriptRepository());
                var taskRepository = GetRepository(x => x.GetTaskRepository());
                _deviceRepository = new DeviceRepository(_connection, networkRepository, scriptRepository, taskRepository);
            }

            return _deviceRepository;
        }

        public INetworkGuestRepository GetNetworkGuestRepository()
        {
            return null;
        }

        public INetworkRepository GetNetworkRepository()
        {
            if (_networkRepository == null)
            {
                var computerRepository = GetRepository(x => x.GetComputerRepository());
                var userRepository = GetRepository(x => x.GetUserRepository());
                var networkGuestRepository = GetRepository(x => x.GetNetworkGuestRepository());
                var networkRepository = new NetworkRepository(_connection, computerRepository, userRepository);

                _networkRepository = new GuestEnabledNetworkRepository(networkRepository, networkGuestRepository);
            }

            return _networkRepository;
        }

        public IScriptRepository GetScriptRepository()
        {
            if (_scriptRepository == null)
            {
                _scriptRepository = new ScriptRepository(_connection);
            }

            return _scriptRepository;
        }

        public ISessionRepository GetSessionRepository()
        {
            if (_sessionRepository == null)
            {
                var computerRepository = GetRepository(x => x.GetComputerRepository());
                var userRepository = GetRepository(x => x.GetUserRepository());
                
                _sessionRepository = new SessionRepository(
                    new UserSessionRepository(_connection, userRepository),
                    new WebHookSessionRepository(_connection, computerRepository)
                ); 
            }

            return _sessionRepository;
        }

        public ITaskRepository GetTaskRepository()
        {
            return null;
        }

        public IUserRepository GetUserRepository()
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_connection);
            }

            return _userRepository;
        }

        private T GetRepository<T>(Func<IRepositoryFactory, T> selector)
        {
            var factory = _parentFactory?.Value ?? this;

            return selector(factory);
        }
    }
}
