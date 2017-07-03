using System.Data;
using System.Data.SqlClient;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Add(User user)
        {
            var model = UserModel.FromRepositoryType(user);
            var sql = @"
                INSERT INTO UserModels
                (
                  Alias,
                  Email,
                  RegisteredTimestamp,
                  Secret,
                  Token
                )
                VALUES
                (
                  @Alias,
                  @Email,
                  @RegisteredTimestamp,
                  @Secret,
                  @Token
                )

                SELECT CAST(IDENT_CURRENT('UserModels') as int)
            ";
            var parameters = new
            {
                Alias = model.Alias,
                Email = model.Email,
                RegisteredTimestamp = model.RegisteredTimestamp,
                Secret = model.Secret,
                Token = model.Token,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            user.SetId(id);
        }

        public User Get(string token)
        {
            var sql = @"
                SELECT *
                FROM UserModels
                WHERE Token = @Token
            ";
            var parameters = new
            {
                Token = token
            };

            var model = _connection.QuerySingle<UserModel>(sql, parameters);
            var result = model.ToRepositoryType();

            return result;
        }

        public User Get(int id)
        {
            var sql = @"
                SELECT *
                FROM UserModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = id
            };

            var model = _connection.QuerySingle<UserModel>(sql, parameters);
            var result = model.ToRepositoryType();

            return result;
        }

        public void Update(User user)
        {
            var model = UserModel.FromRepositoryType(user);
            var sql = @"
                UPDATE UserModels
                SET
                  Alias = @Alias,
                  Email = @Email,
                  Secret = @Secret,
                  Token = @Token
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Alias = model.Alias,
                Email = model.Email,
                Id = model.Id,
                Secret = model.Secret,
                Token = model.Token,
            };

            _connection.Execute(sql, parameters);
        }
    }
}
