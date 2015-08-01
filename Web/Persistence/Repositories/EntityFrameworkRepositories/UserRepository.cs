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

        public EntityFrameworkUserModel Get(int id)
        {
            var result = _users.Find(id);

            return result;
        }

        public EntityFrameworkUserModel Get(string token)
        {
            var result = _users.FirstOrDefault(x => x.Token == token);

            return result;
        }

        public void Add(EntityFrameworkUserModel user)
        {
            _users.Add(user);
        }
    }
}
