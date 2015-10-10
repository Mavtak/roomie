using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Helpers.Secrets;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Action _save;
        private readonly DbSet<UserModel> _users;

        public UserRepository(Action save, DbSet<UserModel> users)
        {
            _save = save;
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
            var model = UserModel.FromRepositoryType(user);

            _users.Add(model);

            _save();

            user.SetId(model.Id);
        }

        public void Update(User user)
        {
            var model = _users.Find(user.Id);

            model.Alias = user.Alias;
            model.Email= user.Email;
            model.Secret = (user.Secret == null) ? null : user.Secret.Format();

            _save();
        }
    }
}
