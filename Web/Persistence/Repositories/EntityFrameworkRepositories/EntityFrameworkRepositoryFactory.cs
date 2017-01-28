using System;
using Roomie.Web.Persistence.Database;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class EntityFrameworkRepositoryFactory : IRepositoryFactory
    {
        private EntityFrameworkRoomieDatabaseBackend _database;

        private IComputerRepository _computerRepository;
        private IDeviceRepository _deviceRepository;
        private INetworkGuestRepository _networkGuestRepository;
        private INetworkRepository _networkRepository;
        private IScriptRepository _scriptRepository;
        private ITaskRepository _taskRepository;
        private IUserRepository _userRepository;
        private ISessionRepository _sessionRepository;

        public EntityFrameworkRepositoryFactory(EntityFrameworkRoomieDatabaseBackend database)
        {
            _database = database;
        }

        public IComputerRepository GetComputerRepository()
        {
            if (_computerRepository == null)
            {
                _computerRepository = new ComputerRepository(_database.Computers, SaveChanges, _database.Scripts, _database.Users);
            }

            return _computerRepository;
        }

        public IDeviceRepository GetDeviceRepository()
        {
            if (_deviceRepository == null)
            {
                var scriptRepository = GetScriptRepository();
                var taskRepository = GetTaskRepository();
                var entityFrameworkDeviceRepository = new DeviceRepository(_database.Devices, _database.Networks, SaveChanges, scriptRepository, taskRepository);
                _deviceRepository = new GuestEnabledDeviceRepository(entityFrameworkDeviceRepository, _networkGuestRepository);
            }

            return _deviceRepository;
        }

        public INetworkGuestRepository GetNetworkGuestRepository()
        {
            if (_networkGuestRepository == null)
            {
                _networkGuestRepository = new NetworkGuestRepository(_database.NetworkGuests, _database.Networks, SaveChanges, _database.Users);
            }

            return _networkGuestRepository;
        }

        public INetworkRepository GetNetworkRepository()
        {
            if (_networkRepository == null)
            {
                var networkGuestRepository = GetNetworkGuestRepository();
                var entityframeworkNetworkRepository = new NetworkRepository(_database.Networks, _database.Computers, SaveChanges, _database.Users);
                _networkRepository = new GuestEnabledNetworkRepository(entityframeworkNetworkRepository, networkGuestRepository);
            }

            return _networkRepository;
        }

        public IScriptRepository GetScriptRepository()
        {
            if (_scriptRepository == null)
            {
                _scriptRepository = new ScriptRepository(_database.Scripts, SaveChanges);
            }

            return _scriptRepository;
        }

        public ISessionRepository GetSessionRepository()
        {
            if (_sessionRepository == null)
            {
                _sessionRepository = new SessionRepository(_database.UserSessions, _database.WebHookSessions, _database.Computers, SaveChanges, _database.Users);
            }

            return _sessionRepository;
        }

        public ITaskRepository GetTaskRepository()
        {
            if (_taskRepository == null)
            {
                _taskRepository = new TaskRepository(_database.Tasks, _database.Computers, SaveChanges, _database.Scripts, _database.Users);
            }

            return _taskRepository;
        }

        public IUserRepository GetUserRepository()
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(SaveChanges, _database.Users);
            }

            return _userRepository;
        }

        private void SaveChanges()
        {
            _database.SaveChanges();
        }
    }
}
