using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface ITaskRepository
    {
        EntityFrameworkTaskModel Get(int id);
        EntityFrameworkTaskModel Get(User user, int id);
        EntityFrameworkTaskModel[] Get(EntityFrameworkScriptModel script);
        void Add(EntityFrameworkTaskModel task);
        void Remove(EntityFrameworkTaskModel task);
        Page<EntityFrameworkTaskModel> List(User user, ListFilter filter);
        EntityFrameworkTaskModel[] ForComputer(EntityFrameworkComputerModel computer, DateTime now);
    }
}
