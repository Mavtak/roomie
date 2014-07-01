using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbSet<TaskModel> _tasks;

        public TaskRepository(DbSet<TaskModel> tasks)
        {
            _tasks = tasks;
        }

        public TaskModel Get(int id)
        {
            var result = _tasks.Find(id);

            return result;
        }

        public TaskModel Get(UserModel user, int id)
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

        public void Add(TaskModel task)
        {
            _tasks.Add(task);
        }

        public TaskModel[] List(UserModel user, ListFilter filter)
        {
            var results = (from t in _tasks
                         where t.Owner.Id == user.Id
                         orderby t.Script.CreationTimestamp descending
                         select t).Page(filter)
                         .ToArray();

            return results;
        }

        public TaskModel[] ForComputer(ComputerModel computer, DateTime now)
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
