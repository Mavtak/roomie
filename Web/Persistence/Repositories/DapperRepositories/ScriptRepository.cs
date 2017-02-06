using System.Data;
using System.Linq;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class ScriptRepository : IScriptRepository
    {

        private IDbConnection _connection;

        public ScriptRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Add(Script script)
        {
            var model = ScriptModel.FromRepositoryType(script);
            var sql = @"
                INSERT INTO ScriptModels
                (
                  CreationTimestamp,
                  LastRunTimestamp,
                  Mutable,
                  RunCount,
                  Text
                )
                VALUES
                (
                  @CreationTimestamp,
                  @LastRunTimestamp,
                  @Mutable,
                  @RunCount,
                  @Text
                )

                SELECT CAST(IDENT_CURRENT('ScriptModels') as int)
            ";
            var parameters = new
            {
                CreationTimestamp = model.CreationTimestamp,
                LastRunTimestamp = model.LastRunTimestamp,
                Mutable = model.Mutable,
                RunCount = model.RunCount,
                Text = model.Text,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            script.SetId(id);
        }

        public Script Get(int id)
        {
            var sql = @"
                SELECT *
                FROM ScriptModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = id,
            };

            var model = _connection.QuerySingle<ScriptModel>(sql, parameters);
            var result = model.ToRepositoryType();

            return result;
        }

        public Page<Script> List(ListFilter filter)
        {
            var sql = $@"
                SELECT COUNT(*)
                FROM ScriptModels

                SELECT *
                FROM ScriptModels
                ORDER BY CreationTimestamp {SqlUtilities.OrderByDirection(filter.SortDirection)}
                OFFSET @Start ROWS
                FETCH NEXT @Count ROWS ONLY
            ";
            var parameters = new
            {
                Count = filter.Count,
                Start = filter.Start,
            };
            
            int total;
            ScriptModel[] models;

            using (var multiQuery = _connection.QueryMultiple(sql, parameters))
            {
                total = multiQuery.ReadSingle<int>();
                models = multiQuery.Read<ScriptModel>().ToArray();
            }

            var result = new Page<Script>
            {
                Count = filter.Count,
                Sort = filter.SortDirection,
                Items = models
                    .Select(x => x.ToRepositoryType())
                    .ToArray(),
                Start = filter.Start,
                Total = total,
            };

            return result;
        }

        public void Remove(Script script)
        {
            var model = ScriptModel.FromRepositoryType(script);
            var sql = @"
                DELETE
                FROM ScriptModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = model.Id,
            };

            _connection.Execute(sql, parameters);
        }
    }
}
