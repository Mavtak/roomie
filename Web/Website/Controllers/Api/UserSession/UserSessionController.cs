using System.Web.Http;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.UserSession
{
    [ApiRestrictedAccess]
    public class UserSessionController : BaseController
    {
        private ISessionRepository _sessionRepository;

        public UserSessionController()
        {
            _sessionRepository = RepositoryFactory.GetSessionRepository();
        }

        public Persistence.Models.UserSession Get(string token)
        {
            var result = _sessionRepository.GetUserSession(token);

            return result;
        }

        public Page<object> Get([FromUri] ListFilter filter)
        {
            var sessions = _sessionRepository.ListUserSessions(User, filter);

            var result = sessions.Transform(Transform);

            return result;
        }

        private static object Transform(Persistence.Models.UserSession model)
        {
            var result = new
                {
                    Id = model.Id,
                    CreationTimeStamp = model.CreationTimeStamp,
                    User = model.User.Id,
                    LastContactTimeStamp = model.LastContactTimeStamp,
                };

            return result;
        }
    }
}
