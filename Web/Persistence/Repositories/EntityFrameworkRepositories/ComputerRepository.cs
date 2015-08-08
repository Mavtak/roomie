using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly DbSet<EntityFrameworkComputerModel> _computers;
        private readonly DbSet<EntityFrameworkScriptModel> _scripts;
        private readonly DbSet<EntityFrameworkUserModel> _users;

        public ComputerRepository(DbSet<EntityFrameworkComputerModel> computers, DbSet<EntityFrameworkScriptModel> scripts, DbSet<EntityFrameworkUserModel> users)
        {
            _computers = computers;
            _scripts = scripts;
            _users = users;
        }

        public Computer Get(int id)
        {
            var model = _computers.Find(id);

            if (model == null)
            {
                return null;
            }

            return model.ToRepositoryType();
        }

        public Computer Get(User user, int id)
        {
            var result = Get(id);

            if (result == null)
            {
                return null;
            }

            if (result.Owner == null)
            {
                throw new Exception("Owner not set");
            }

            if (result.Owner.Id != user.Id)
            {
                result = null;
            }

            return result;
        }

        public Computer Get(string accessKey)
        {
            var model = _computers.FirstOrDefault(x => x.AccessKey == accessKey);

            if (model == null)
            {
                return null;
            }

            return model.ToRepositoryType();
        }

        public Computer Get(User user, string name)
        {
            var model = _computers.Where(x => x.Owner.Id == user.Id)
                                   .Where(x => x.Name == name)
                                   .FirstOrDefault();

            if (model == null)
            {
                return null;
            }

            return model.ToRepositoryType();
        }

        public Computer[] Get(Script script)
        {
            var result = _computers
                .Where(x => x.LastScript.Id == script.Id)
                .ToArray()
                .Select(x => x.ToRepositoryType())
                .ToArray();

            return result;
        }

        public Computer[] Get(User user)
        {
            var result = _computers
                .Where(x => x.Owner.Id == user.Id)
                .ToArray()
                .Select(x => x.ToRepositoryType())
                .ToArray();

            return result;
        }

        public void Add(Computer computer)
        {
            var model = EntityFrameworkComputerModel.FromRepositoryType(computer, _scripts, _users);

            _computers.Add(model);
        }

        public void Update(Computer computer)
        {
            var model = _computers.Find(computer.Id);

            model.AccessKey = computer.AccessKey;
            model.Address = computer.Address;
            model.EncryptionKey = computer.EncryptionKey;
            model.LastPing = computer.LastPing;
            model.LastScript = (computer.LastScript == null) ? null : _scripts.Find(computer.LastScript.Id);
            model.Name = computer.Name;
            model.Owner = (computer.Owner == null) ? null : _users.Find(computer.Owner.Id);
        }

        public void Remove(Computer computer)
        {
            var model = _computers.Find(computer.Id);

            _computers.Remove(model);
        }
    }
}
