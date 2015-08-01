using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<EntityFrameworkUserModel> _users;

        public UserRepository(DbSet<EntityFrameworkUserModel> users)
        {
            _users = users;
        }

        public User Get(int id)
        {
            var model = _users.Find(id);

            if (model == null)
            {
                return null;
            }

            return model.ToRepositoryType();
        }

        public User Get(string token)
        {
            var model = _users.FirstOrDefault(x => x.Token == token);

            if (model == null)
            {
                return null;
            }

            return model.ToRepositoryType();
        }

        public void Add(User user)
        {
            var model = EntityFrameworkUserModel.FromRepositoryType(user);

            _users.Add(model);
        }
    }
}
