using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly DbSet<ComputerModel> _computers;

        public ComputerRepository(DbSet<ComputerModel> computers)
        {
            _computers = computers;
        }

        public ComputerModel Get(int id)
        {
            var result = _computers.Find(id);

            return result;
        }

        public ComputerModel Get(UserModel user, int id)
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

        public ComputerModel Get(string accessKey)
        {
            var result = _computers.FirstOrDefault(x => x.AccessKey == accessKey);

            return result;
        }

        public ComputerModel Get(UserModel user, string name)
        {
            var result = _computers.Where(x => x.Owner.Id == user.Id)
                                   .Where(x => x.Name == name)
                                   .FirstOrDefault();

            return result;
        }

        public ComputerModel[] Get(UserModel user)
        {
            var result = _computers.Where(x => x.Owner.Id == user.Id).ToArray();

            return result;
        }

        public void Add(ComputerModel computer)
        {
            _computers.Add(computer);
        }

        public void Remove(ComputerModel computer)
        {
            _computers.Remove(computer);
        }
    }
}
