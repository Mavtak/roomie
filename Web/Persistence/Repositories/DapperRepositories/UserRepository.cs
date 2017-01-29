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
                    @alias,
                    @email,
                    @registeredTimestamp,
                    @secret,
                    @token
                )

                SELECT CAST(IDENT_CURRENT('UserModels') as int)
            ";
            var parameters = new
            {
                alias = model.Alias,
                email = model.Email,
                registeredTimestamp = model.RegisteredTimestamp,
                secret = model.Secret,
                token = model.Token,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            user.SetId(id);
        }

        public User Get(string token)
        {
            var sql = @"
                SELECT *
                FROM UserModels
                WHERE Token = @token
            ";
            var parameters = new
            {
                token = token
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
                WHERE Id = @id
            ";
            var parameters = new
            {
                id = id
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
                    Alias = @alias,
                    Email = @email,
                    Secret = @secret,
                    Token = @token
                WHERE Id = @id
            ";
            var parameters = new
            {
                alias = model.Alias,
                email = model.Email,
                id = model.Id,
                secret = model.Secret,
                token = model.Token,
            };

            _connection.Execute(sql, parameters);
        }
    }
}
