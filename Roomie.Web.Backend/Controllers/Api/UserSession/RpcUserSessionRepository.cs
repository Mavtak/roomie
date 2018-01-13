using Roomie.Common;
using Roomie.Common.Api.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Backend.Controllers.Api.UserSession
{
    public class RpcUserSessionRepository
    {
        private IRepositoryFactory _repositoryFactory;
        private ISessionRepository _sessionRepository;
        private Persistence.Models.User _user;

        public RpcUserSessionRepository(IRepositoryFactory repositoryFactory, Persistence.Models.User user)
        {
            _repositoryFactory = repositoryFactory;
            _user = user;

            _sessionRepository = _repositoryFactory.GetSessionRepository();
        }

        public Response<Persistence.Models.UserSession> Read(string token)
        {
            var cache = new InMemoryRepositoryModelCache();
            var session = _sessionRepository.GetUserSession(token, cache);
            var result = GetSerializableVersion(session);

            return Response.Create(result);
        }

        public Response<Page<Persistence.Models.UserSession>> List(int count = 50, string sortDirection = null, int start = 0)
        {
            var filter = new ListFilter
            {
                Count = count,
                SortDirection = sortDirection == null ? SortDirection.Descending : EnumParser.Parse<SortDirection>(sortDirection),
                Start = start,
            };

            var cache = new InMemoryRepositoryModelCache();
            var sessions = _sessionRepository.ListUserSessions(_user, filter, cache);

            var result = sessions.Transform(GetSerializableVersion);

            return Response.Create(result);
        }

        private static Persistence.Models.UserSession GetSerializableVersion(Persistence.Models.UserSession model)
        {
            Persistence.Models.User user = null;

            if (model.User != null)
            {
                user = new Persistence.Models.User(
                    alias: model.User.Alias,
                    email: null,
                    id: model.User.Id,
                    registeredTimestamp: null,
                    secret: null,
                    token: null
                );
            }

            var result = new Persistence.Models.UserSession(
                creationTimeStamp: model.CreationTimeStamp,
                id: model.Id,
                lastContactTimeStamp: model.LastContactTimeStamp,
                token: null,
                user: user
            );

            return result;
        }
    }
}