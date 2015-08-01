using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly DbSet<EntityFrameworkComputerModel> _computers;

        public ComputerRepository(DbSet<EntityFrameworkComputerModel> computers)
        {
            _computers = computers;
        }

        public EntityFrameworkComputerModel Get(int id)
        {
            var result = _computers.Find(id);

            return result;
        }

        public EntityFrameworkComputerModel Get(EntityFrameworkUserModel user, int id)
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

        public EntityFrameworkComputerModel Get(string accessKey)
        {
            var result = _computers.FirstOrDefault(x => x.AccessKey == accessKey);

            return result;
        }

        public EntityFrameworkComputerModel Get(EntityFrameworkUserModel user, string name)
        {
            var result = _computers.Where(x => x.Owner.Id == user.Id)
                                   .Where(x => x.Name == name)
                                   .FirstOrDefault();

            return result;
        }

        public EntityFrameworkComputerModel[] Get(EntityFrameworkScriptModel script)
        {
            var result = _computers
                .Where(x => x.LastScript.Id == script.Id)
                .ToArray();

            return result;
        }

        public EntityFrameworkComputerModel[] Get(EntityFrameworkUserModel user)
        {
            var result = _computers.Where(x => x.Owner.Id == user.Id).ToArray();

            return result;
        }

        public void Add(EntityFrameworkComputerModel computer)
        {
            _computers.Add(computer);
        }

        public void Remove(EntityFrameworkComputerModel computer)
        {
            _computers.Remove(computer);
        }
    }
}
