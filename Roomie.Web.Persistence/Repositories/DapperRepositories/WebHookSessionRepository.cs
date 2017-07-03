using System.Data;
using System.Linq;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class WebHookSessionRepository
    {
        private IDbConnection _connection;

        private IComputerRepository _computerRepository;

        public WebHookSessionRepository(IDbConnection connection, IComputerRepository computerRepository)
        {
            _connection = connection;

            _computerRepository = computerRepository;
        }

        public void Add(WebHookSession session)
        {
            var model = WebHookSessionModel.FromRepositoryType(session);
            var sql = @"
                INSERT INTO WebHookSessionModels
                (
                  Computer_Id,
                  LastPing,
                  Token
                )
                VALUES
                (
                  @Computer_Id,
                  @LastPing,
                  @Token
                )

                SELECT CAST(IDENT_CURRENT('WebHookSessionModels') as int)
            ";
            var parameters = new
            {
                model.Computer_Id,
                model.LastPing,
                model.Token,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            session.SetId(id);
        }

        public WebHookSession Get(string token)
        {
            var sql = @"
                SELECT *
                FROM WebHookSessionModels
                WHERE Token = @Token
            ";
            var parameters = new
            {
                Token = token,
            };

            var model = _connection.QuerySingle<WebHookSessionModel>(sql, parameters);
            var result = model.ToRepositoryType(_computerRepository);

            return result;
        }

        public Page<WebHookSession> List(User user, ListFilter filter)
        {
            var model = UserModel.FromRepositoryType(user);
            var sql = $@"
                SELECT COUNT(*)
                FROM WebHookSessionModels
                JOIN ComputerModels
                  ON ComputerModels.Id = WebHookSessionModels.Computer_Id
                WHERE ComputerModels.Owner_Id = @User_Id

                SELECT *
                FROM WebHookSessionModels
                JOIN ComputerModels
                  ON ComputerModels.Id = WebHookSessionModels.Computer_Id
                WHERE ComputerModels.Owner_Id = @User_Id
                ORDER BY CreationTimestamp {SqlUtilities.OrderByDirection(filter.SortDirection)}
                OFFSET @Start ROWS
                FETCH NEXT @Count ROWS ONLY
            ";
            var parameters = new
            {
                Count = filter.Count,
                Start = filter.Start,
                User_Id = user.Id,
            };

            int total;
            WebHookSessionModel[] models;

            using (var multiQuery = _connection.QueryMultiple(sql, parameters))
            {
                total = multiQuery.ReadSingle<int>();
                models = multiQuery.Read<WebHookSessionModel>().ToArray();
            }

            var result = new Page<WebHookSession>
            {
                Count = filter.Count,
                Sort = filter.SortDirection,
                Items = models
                    .Select(x => x.ToRepositoryType(_computerRepository))
                    .ToArray(),
                Start = filter.Start,
                Total = total,
            };

            return result;
        }

    }
}
