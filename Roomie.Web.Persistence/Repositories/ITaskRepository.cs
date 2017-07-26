using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ITaskRepository
    {
        Task Get(int id, IRepositoryModelCache cache = null);
        Task Get(User user, int id, IRepositoryModelCache cache = null);
        Task[] Get(Script script, IRepositoryModelCache cache = null);
        void Add(Task task);
        void Update(Task task);
        void Remove(Task task);
        Page<Task> List(User user, ListFilter filter, IRepositoryModelCache cache = null);
        Task[] ForComputer(Computer computer, DateTime now, IRepositoryModelCache cache = null);
    }
}
