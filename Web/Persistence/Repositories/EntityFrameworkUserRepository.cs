using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public class EntityFrameworkUserRepository : IUserRepository
    {
        private readonly DbSet<UserModel> _users;

        public EntityFrameworkUserRepository(DbSet<UserModel> users)
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
