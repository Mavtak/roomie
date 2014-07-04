using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ITaskRepository
    {
        TaskModel Get(int id);
        TaskModel Get(UserModel user, int id);
        void Add(TaskModel task);
        void Remove(TaskModel task);
        TaskModel[] List(UserModel user, ListFilter filter);
        TaskModel[] ForComputer(ComputerModel computer, DateTime now);
    }
}
