using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Backend.Helpers;

namespace Roomie.Web.Backend.Controllers.Api.User
{
    public class UserController : BaseController
    {
        private IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = RepositoryFactory.GetUserRepository();
        }

        public void Post(string username, string password)
        {
           _userRepository.Add(username, password);
        }
    }
}