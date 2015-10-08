using System;
using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbSet<TaskModel> _tasks;
        private readonly DbSet<ComputerModel> _computers;
        private readonly DbSet<ScriptModel> _scripts;
        private readonly DbSet<UserModel> _users;

        public TaskRepository(DbSet<TaskModel> tasks, DbSet<ComputerModel> computers, DbSet<ScriptModel> scripts, DbSet<UserModel> users)
        {
            _tasks = tasks;
            _computers = computers;
            _scripts = scripts;
            _users = users;
        }

        public Task Get(int id)
        {
            var result = _tasks.Find(id);

            if (result == null)
            {
                return null;
            }

            return result.ToRepositoryType();
        }

        public Task Get(User user, int id)
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

        public Task[] Get(Script script)
        {
            var result = _tasks
                .Where(x => x.Script.Id == script.Id)
                .Select(x => x.ToRepositoryType())
                .ToArray();

            return result;
        }

        public void Add(Task task)
        {
            var model = TaskModel.FromRepositoryType(task, _computers, _scripts, _users);

            _tasks.Add(model);
        }

        public void Update(Task task)
        {
            var model = _tasks.Find(task.Id);

            model.ReceivedTimestamp = task.ReceivedTimestamp;
        }

        public void Remove(Task task)
        {
            var model = _tasks.Find(task.Id);

            _tasks.Remove(model);
        }

        public Page<Task> List(User user, ListFilter filter)
        {
            var results = _tasks
                .Where(x => x.Owner.Id == user.Id)
                .Page(filter, x => x.Script.CreationTimestamp)
                .Transform(x => x.ToRepositoryType())
                ;

            return results;
        }

        public Task[] ForComputer(Computer computer, DateTime now)
        {
            var results = (from t in _tasks
                          where t.Target.Id == computer.Id
                                && t.ReceivedTimestamp == null
                                && t.Expiration.Value > now
                          select t)
                          .ToArray()
                          .Select(x => x.ToRepositoryType())
                              .ToArray();

            return results;
        }
    }
}
