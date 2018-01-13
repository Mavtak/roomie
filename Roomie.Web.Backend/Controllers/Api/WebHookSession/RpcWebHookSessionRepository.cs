using Roomie.Common.Api.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Backend.Controllers.Api.WebHookSession
{
    public class RpcWebHookSessionRepository
    {
        private IRepositoryFactory _repositoryFactory;
        private ISessionRepository _sessionRepository;
        private Persistence.Models.User _user;

        public RpcWebHookSessionRepository(IRepositoryFactory repositoryFactory, Persistence.Models.User user)
        {
            _repositoryFactory = repositoryFactory;
            _user = user;

            _sessionRepository = _repositoryFactory.GetSessionRepository();
        }

        public Response<string> Create(string accessKey)
        {
            var computerRepository = _repositoryFactory.GetComputerRepository();
            var computer = computerRepository.Get(accessKey);

            var session = Persistence.Models.WebHookSession.Create(computer);

            _sessionRepository.Add(session);

            return Response.Create(session.Token);
        }
    }
}