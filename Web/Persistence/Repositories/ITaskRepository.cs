using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ITaskRepository
    {
        TaskModel Get(int id);
        void Add(TaskModel task);
        TaskModel[] List(UserModel user, int page, int count);
        TaskModel[] ForComputer(ComputerModel computer, DateTime now);
    }
}
