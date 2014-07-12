using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ITaskRepository
    {
        TaskModel Get(int id);
        TaskModel Get(UserModel user, int id);
        TaskModel[] Get(ScriptModel script);
        void Add(TaskModel task);
        void Remove(TaskModel task);
        Page<TaskModel> List(UserModel user, ListFilter filter);
        TaskModel[] ForComputer(ComputerModel computer, DateTime now);
    }
}
