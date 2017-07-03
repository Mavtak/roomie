using System;
using System.Data;
using System.Linq;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class TaskRepository : ITaskRepository
    {
        private IDbConnection _connection;

        IComputerRepository _computerRepository;
        IScriptRepository _scriptRepository;
        IUserRepository _userRepository;

        public TaskRepository(IDbConnection connection, IComputerRepository computerRepository, IScriptRepository scriptRepository, IUserRepository userRepository)
        {
            _connection = connection;

            _computerRepository = computerRepository;
            _scriptRepository = scriptRepository;
            _userRepository = userRepository;
        }

        public void Add(Task task)
        {
            var model = TaskModel.FromRepositoryType(task);
            var sql = @"
                INSERT
                INTO TaskModels
                (
                  Expiration,
                  Origin,
                  Owner_Id,
                  ReceivedTimestamp,
                  Script_Id,
                  Target_Id
                )
                VALUES
                (
                  @Expiration,
                  @Origin,
                  @Owner_Id,
                  @ReceivedTimestamp,
                  @Script_Id,
                  @Target_Id
                )

                SELECT CAST(IDENT_CURRENT('TaskModels') as int)
            ";
            var parameters = new
            {
                Expiration = model.Expiration,
                Origin = model.Origin,
                Owner_Id = model.Owner_Id,
                ReceivedTimestamp = model.ReceivedTimestamp,
                Script_Id = model.Script_Id,
                Target_Id = model.Target_Id,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            task.SetId(id);
        }

        public Task[] ForComputer(Computer computer, DateTime now)
        {
            var computerModel = ComputerModel.FromRepositoryType(computer);
            var sql = @"
                SELECT *
                FROM TaskModels
                WHERE Target_Id = @Target_Id
                  AND ReceivedTimestamp IS NULL
                  AND Expiration > @Now
            ";
            var parameters = new
            {
                Now = now,
                Target_Id = computerModel.Id,
            };

            var models = _connection.Query<TaskModel>(sql, parameters).ToArray();
            var result = models
                .Select(x => x.ToRepositoryType(_computerRepository, _scriptRepository, _userRepository))
                .ToArray();

            return result;
        }

        public Task[] Get(Script script)
        {
            var scriptModel = ScriptModel.FromRepositoryType(script);
            var sql = @"
                SELECT *
                FROM TaskModels
                WHERE Script_Id = @Script_Id
            ";
            var parameters = new
            {
                Script_Id = scriptModel.Id,
            };

            var models = _connection.Query<TaskModel>(sql, parameters).ToArray();
            var result = models
                .Select(x => x.ToRepositoryType(_computerRepository, _scriptRepository, _userRepository))
                .ToArray();

            return result;
        }

        public Task Get(int id)
        {
            var sql = @"
                SELECT *
                FROM TaskModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = id,
            };

            var model = _connection.QuerySingle<TaskModel>(sql, parameters);
            var result = model.ToRepositoryType(_computerRepository, _scriptRepository, _userRepository);

            return result;
        }

        public Task Get(User user, int id)
        {
            var userModel = UserModel.FromRepositoryType(user);
            var result = Get(id);

            if(result?.Owner?.Id != user.Id)
            {
                return null;
            }

            return result;
        }

        public Page<Task> List(User user, ListFilter filter)
        {
            var userModel = UserModel.FromRepositoryType(user);
            var sql = $@"
                SELECT COUNT(*)
                FROM TaskModels
                WHERE Owner_Id = @Owner_Id

                SELECT *
                FROM TaskModels
                WHERE Owner_Id = @Owner_Id
                ORDER BY Id {SqlUtilities.OrderByDirection(filter.SortDirection)}
                OFFSET @Start ROWS
                FETCH NEXT @Count ROWS ONLY
            ";
            var parameters = new
            {
                Count = filter.Count,
                Owner_Id = userModel.Id,
                Start = filter.Start,
            };

            int total;
            TaskModel[] models;

            using (var multiQuery = _connection.QueryMultiple(sql, parameters))
            {
                total = multiQuery.ReadSingle<int>();
                models = multiQuery.Read<TaskModel>().ToArray();
            }

            var result = new Page<Task>
            {
                Count = filter.Count,
                Sort = filter.SortDirection,
                Items = models
                    .Select(x => x.ToRepositoryType(_computerRepository, _scriptRepository, _userRepository))
                    .ToArray(),
                Start = filter.Start,
                Total = total,
            };

            return result;
        }

        public void Remove(Task task)
        {
            var taskModel = TaskModel.FromRepositoryType(task);
            var sql = @"
                DELETE
                FROM TaskModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = taskModel.Id,
            };

            _connection.Execute(sql, parameters);
        }

        public void Update(Task task)
        {
            var taskModel = TaskModel.FromRepositoryType(task);
            var sql = @"
                UPDATE TaskModels
                SET
                  ReceivedTimestamp = @ReceivedTimestamp
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = taskModel.Id,
                ReceivedTimestamp = taskModel.ReceivedTimestamp,
            };

            _connection.Execute(sql, parameters);
        }
    }
}
