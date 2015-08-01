using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbSet<EntityFrameworkTaskModel> _tasks;

        public TaskRepository(DbSet<EntityFrameworkTaskModel> tasks)
        {
            _tasks = tasks;
        }

        public EntityFrameworkTaskModel Get(int id)
        {
            var result = _tasks.Find(id);

            return result;
        }

        public EntityFrameworkTaskModel Get(EntityFrameworkUserModel user, int id)
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

        public EntityFrameworkTaskModel[] Get(EntityFrameworkScriptModel script)
        {
            var result = _tasks
                .Where(x => x.Script.Id == script.Id)
                .ToArray();

            return result;
        }

        public void Add(EntityFrameworkTaskModel task)
        {
            _tasks.Add(task);
        }

        public void Remove(EntityFrameworkTaskModel task)
        {
            _tasks.Remove(task);
        }

        public Page<EntityFrameworkTaskModel> List(EntityFrameworkUserModel user, ListFilter filter)
        {
            var results = _tasks
                .Where(x => x.Owner.Id == user.Id)
                .Page(filter, x => x.Script.CreationTimestamp)
                ;

            return results;
        }

        public EntityFrameworkTaskModel[] ForComputer(EntityFrameworkComputerModel computer, DateTime now)
        {
            var results = (from t in _tasks
                          where t.Target.Id == computer.Id
                                && t.ReceivedTimestamp == null
                                && t.Expiration.Value > now
                          select t)
                              .ToArray();

            return results;
        }
    }
}
