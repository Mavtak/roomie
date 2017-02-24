using System;
using System.Data;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class DapperRepositoryFactory : IRepositoryFactory
    {
        private IDbConnection _connection;
        private Lazy<IRepositoryFactory> _parentFactory;

        private IComputerRepository _computerRepository;
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
            return null;
        }

        public INetworkGuestRepository GetNetworkGuestRepository()
        {
            return null;
        }

        public INetworkRepository GetNetworkRepository()
        {
            return null;
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
