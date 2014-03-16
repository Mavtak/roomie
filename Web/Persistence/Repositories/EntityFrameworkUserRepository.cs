using System.Linq;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkUserRepository : IUserRepository
    {
        private readonly IRoomieEntitySet<UserModel> _users;

        public EntityFrameworkUserRepository(IRoomieEntitySet<UserModel> users)
        {
            _users = users;
        }

        public UserModel Get(int id)
        {
            var result = _users.Find(id);

            return result;
        }

        public UserModel Get(string token)
        {
            var result = _users.FirstOrDefault(x => x.Token == token);

            return result;
        }

        public void Add(UserModel user)
        {
            _users.Add(user);
        }
    }
}
