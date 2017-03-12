using System;
using System.Data;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Persistence.Repositories.DapperRepositories;
using WebCommunicator;

namespace Roomie.Web.WebHook
{
    internal class WebHookContext : TransmissionContext, IDisposable
    {
        private IDbConnection _databaseConnection;
        private IRepositoryFactory _repositoryFactory;

        public Computer Computer { get; set; }
        public WebHookSession Session { get; set; }
        public User User
        {
            get
            {
                return Computer.Owner;
            }
        }
        public IRepositoryFactory RepositoryFactory
        {
            get
            {
                if (_repositoryFactory == null)
                {
                    _databaseConnection = DatabaseConnectionFactory.Connect();
                    _repositoryFactory = new CompositeImplementationRepositoryFactory(
                        new DapperRepositoryFactory(
                            _databaseConnection,
                            new Lazy<IRepositoryFactory>(() => _repositoryFactory)
                        )
                    );
                }

                return _repositoryFactory;
            }
        }

        public void Dispose()
        {
            if (_databaseConnection != null)
            {
                _databaseConnection.Dispose();
            }
        }
    }
}
