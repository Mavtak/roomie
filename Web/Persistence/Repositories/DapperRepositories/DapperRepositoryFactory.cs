﻿using System.Data;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class DapperRepositoryFactory : IRepositoryFactory
    {
        private IDbConnection _connection;

        private IScriptRepository _scriptRepository;
        private IUserRepository _userRepository;

        public DapperRepositoryFactory(IDbConnection connection)
        {
            _connection = connection;
        }

        public IComputerRepository GetComputerRepository()
        {
            return null;
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
            return null;
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
    }
}
