using System.Data;
using System.Linq;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class UserSessionRepository
    {
        private IDbConnection _connection;

        private IUserRepository _userRepository;

        public UserSessionRepository(IDbConnection connection, IUserRepository userRepository)
        {
            _connection = connection;

            _userRepository = userRepository;
        }

        public void Add(UserSession session)
        {
            var model = UserSessionModel.FromRepositoryType(session);
            var sql = @"
                INSERT INTO UserSessionModels
                (
                  CreationTimeStamp,
                  LastContactTimeStamp,
                  Token,
                  User_Id
                )
                VALUES
                (
                  @CreationTimeStamp,
                  @LastContactTimeStamp,
                  @Token,
                  @User_Id
                )

                SELECT CAST(IDENT_CURRENT('UserSessionModels') as int)
            ";
            var parameters = new
            {
                CreationTimeStamp = model.CreationTimeStamp,
                LastContactTimeStamp = model.LastContactTimeStamp,
                Token = model.Token,
                User_Id = model.User_Id,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            session.SetId(id);
        }

        public UserSession Get(string token, IRepositoryModelCache cache = null)
        {
            var sql = @"
                SELECT *
                FROM UserSessionModels
                WHERE Token = @Token
            ";
            var parameters = new
            {
                Token = token,
            };

            var model = _connection.QuerySingle<UserSessionModel>(sql, parameters);
            var result = model.ToRepositoryType(cache, _userRepository);

            return result;
        }

        public Page<UserSession> List(User user, ListFilter filter, IRepositoryModelCache cache = null)
        {
            var model = UserModel.FromRepositoryType(user);
            var sql = $@"
                SELECT COUNT(*)
                FROM UserSessionModels
                WHERE User_Id = @User_Id

                SELECT *
                FROM UserSessionModels
                WHERE User_Id = @User_Id
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
            UserSessionModel[] models;

            using (var multiQuery = _connection.QueryMultiple(sql, parameters))
            {
                total = multiQuery.ReadSingle<int>();
                models = multiQuery.Read<UserSessionModel>().ToArray();
            }

            var result = new Page<UserSession>
            {
                Count = filter.Count,
                Sort = filter.SortDirection,
                Items = models
                    .Select(x => x.ToRepositoryType(cache, _userRepository))
                    .ToArray(),
                Start = filter.Start,
                Total = total,
            };

            return result;
        }

        public void Update(UserSession session)
        {
            var model = UserSessionModel.FromRepositoryType(session);
            var sql = @"
                UPDATE UserSessionModels
                SET
                  LastContactTimeStamp = @LastContactTimeStamp
            ";
            var parameters = new
            {
                LastContactTimeStamp = model.LastContactTimeStamp
            };

            _connection.Execute(sql, parameters);
        }
    }
}
