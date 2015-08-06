using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ITaskRepository
    {
        Task Get(int id);
        Task Get(User user, int id);
        Task[] Get(EntityFrameworkScriptModel script);
        void Add(Task task);
        void Update(Task task);
        void Remove(Task task);
        Page<Task> List(User user, ListFilter filter);
        Task[] ForComputer(Computer computer, DateTime now);
    }
}
