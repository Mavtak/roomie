﻿using System;
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

        public TaskModel[] Get(ScriptModel script)
        {
            var result = _tasks
                .Where(x => x.Script.Id == script.Id)
                .ToArray();

            return result;
        }

        public void Add(TaskModel task)
        {
            _tasks.Add(task);
        }

        public void Remove(TaskModel task)
        {
            _tasks.Remove(task);
        }

        public Page<TaskModel> List(UserModel user, ListFilter filter)
        {
            var results = _tasks
                .Where(x => x.Owner.Id == user.Id)
                .Page(filter, x => x.Script.CreationTimestamp)
                ;

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