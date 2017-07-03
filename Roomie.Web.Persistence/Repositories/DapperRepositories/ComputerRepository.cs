using System;
using System.Data;
using System.Linq;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class ComputerRepository : IComputerRepository
    {
        private IDbConnection _connection;

        private IScriptRepository _scriptRepository;
        private IUserRepository _userRepository;

        public ComputerRepository(IDbConnection connection, IScriptRepository scriptRepository, IUserRepository userRepository)
        {
            _connection = connection;

            _scriptRepository = scriptRepository;
            _userRepository = userRepository;
        }

        public void Add(Computer computer)
        {
            var model = ComputerModel.FromRepositoryType(computer);
            var sql = @"
                INSERT INTO ComputerModels
                (
                  AccessKey,
                  Address,
                  EncryptionKey,
                  LastPing,
                  LastScript_Id,
                  Name,
                  Owner_Id
                )
                VALUES
                (
                  @AccessKey,
                  @Address,
                  @EncryptionKey,
                  @LastPing,
                  @LastScript_Id,
                  @Name,
                  @Owner_Id
                )

                SELECT CAST(IDENT_CURRENT('ComputerModels') as int)
            ";
            var parameters = new
            {
                AccessKey = model.AccessKey,
                Address = model.Address,
                EncryptionKey = model.EncryptionKey,
                LastPing = model.LastPing,
                LastScript_Id = model.LastScript_Id,
                Name = model.Name,
                Owner_Id = model.Owner_Id,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            computer.SetId(id);
        }

        public Computer Get(string accessKey)
        {
            var sql = @"
                SELECT *
                FROM ComputerModels
                WHERE AccessKey = @AccessKey
            ";
            var parameters = new
            {
                AccessKey = accessKey,
            };

            var model = _connection.QuerySingle<ComputerModel>(sql, parameters);
            var result = model.ToRepositoryType(_scriptRepository, _userRepository);

            return result;
        }

        public Computer[] Get(Script script)
        {
            var sql = @"
                SELECT *
                FROM ComputerModels
                WHERE LastScript_Id = @LastScript_Id
            ";
            var parameters = new
            {
                LastScript_Id = script.Id,
            };

            var models = _connection.Query<ComputerModel>(sql, parameters);
            var result = models
                .Select(x => x.ToRepositoryType(_scriptRepository, _userRepository))
                .ToArray();

            return result;
        }

        public Computer[] Get(User user)
        {
            var sql = @"
                SELECT *
                FROM ComputerModels
                WHERE Owner_Id = @Owner_Id
            ";
            var parameters = new
            {
                Owner_Id = user.Id,
            };

            var models = _connection.Query<ComputerModel>(sql, parameters);
            var result = models
                .Select(x => x.ToRepositoryType(_scriptRepository, _userRepository))
                .ToArray();

            return result;
        }

        public Computer Get(int id)
        {
            var sql = @"
                SELECT *
                FROM ComputerModels
                WHERE Id = @id
            ";
            var parameters = new
            {
                Id = id,
            };

            var model = _connection.QuerySingle<ComputerModel>(sql, parameters);
            var result = model.ToRepositoryType(_scriptRepository, _userRepository);

            return result;
        }

        public Computer Get(User user, string name)
        {
            var sql = @"
                SELECT *
                FROM ComputerModels
                WHERE Owner_Id = @Owner_Id
                  AND Name = @Name
            ";
            var parameters = new
            {
                Owner_Id = user.Id,
                Name = name,
            };

            var model = _connection.QuerySingle<ComputerModel>(sql, parameters);
            var result = model.ToRepositoryType(_scriptRepository, _userRepository);

            return result;
        }

        public Computer Get(User user, int id)
        {
            var sql = @"
                SELECT *
                FROM ComputerModels
                WHERE Owner_Id = @Owner_Id
                  AND Id = @Id
            ";
            var parameters = new
            {
                Owner_Id = user.Id,
                Id = id,
            };

            var model = _connection.QuerySingle<ComputerModel>(sql, parameters);
            var result = model.ToRepositoryType(_scriptRepository, _userRepository);

            return result;
        }

        public void Remove(Computer computer)
        {
            var sql = @"
                DELETE
                FROM ComputerModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = computer.Id,
            };

            _connection.Execute(sql, parameters);
        }

        public void Update(Computer computer)
        {
            var model = ComputerModel.FromRepositoryType(computer);
            var sql = @"
                UPDATE ComputerModels
                SET
                  AccessKey = @AccessKey,
                  Address = @Address,
                  EncryptionKey = @EncryptionKey,
                  LastPing = @LastPing,
                  LastScript_Id = @LastScript_Id,
                  Name = @Name,
                  Owner_Id = @Owner_Id
                WHERE Id = @Id
            ";
            var parameters = new
            {
                AccessKey = model.AccessKey,
                Address = model.Address,
                EncryptionKey = model.EncryptionKey,
                Id = model.Id,
                LastPing = model.LastPing,
                LastScript_Id = model.LastScript_Id,
                Name = model.Name,
                Owner_Id = model.Owner_Id,
            };

            _connection.Execute(sql, parameters);
        }
    }
}
