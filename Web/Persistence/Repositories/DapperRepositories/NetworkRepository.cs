using System.Data;
using System.Linq;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class NetworkRepository : INetworkRepository
    {
        private IDbConnection _connection;

        private IComputerRepository _computerRepository;
        private IUserRepository _userRepository;

        public NetworkRepository(IDbConnection connection, IComputerRepository computerRepository, IUserRepository userRepository)
        {
            _connection = connection;
            _computerRepository = computerRepository;
            _userRepository = userRepository;
        }

        public void Add(Network network)
        {
            var model = NetworkModel.FromRepositoryType(network);
            var sql = @"
                INSERT INTO NetworkModels
                (
                  Address,
                  AttatchedComputer_Id,
                  LastPing,
                  Name,
                  Owner_Id
                )
                VALUES
                (
                  @Address,
                  @AttatchedComputer_Id,
                  @LastPing,
                  @Name,
                  @Owner_Id
                )
                
                SELECT CAST(IDENT_CURRENT('NetworkModels') as int)
            ";
            var parameters = new
            {
                Address = model.Address,
                AttatchedComputer_Id = model.AttatchedComputer_Id,
                LastPing = model.LastPing,
                Name = model.Name,
                Owner_Id = model.Owner_Id,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            network.SetId(id);
        }

        public Network[] Get(User user)
        {
            var userModel = UserModel.FromRepositoryType(user);
            var sql = @"
                SELECT *
                FROM NetworkModels
                WHERE Owner_Id = @Owner_Id
            ";
            var parameters = new
            {
                Owner_Id = userModel.Id,
            };

            var models = _connection.Query<NetworkModel>(sql, parameters).ToArray();
            var result = models
                .Select(x => x.ToRepositoryType(_computerRepository, _userRepository))
                .ToArray();

            return result;
        }

        public Network Get(int id)
        {
            var sql = @"
                SELECT *
                FROM NetworkModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = id,
            };

            var model = _connection.QuerySingle<NetworkModel>(sql, parameters);
            var result = model.ToRepositoryType(_computerRepository, _userRepository);

            return result;
        }

        public Network Get(User user, string address)
        {
            var userModel = UserModel.FromRepositoryType(user);
            var sql = @"
                SELECT *
                FROM NetworkModels
                WHERE Address = @Address
                  AND Owner_Id = @Owner_Id
            ";
            var parameters = new
            {
                Address = address,
                Owner_Id = userModel.Id,
            };

            var model = _connection.QuerySingle<NetworkModel>(sql, parameters);
            var result = model.ToRepositoryType(_computerRepository, _userRepository);

            return result;
        }

        public Network Get(User user, int id)
        {
            var userModel = UserModel.FromRepositoryType(user);
            var result = Get(id);

            if (result?.Owner?.Id != userModel?.Id)
            {
                return null;
            }

            return result;
        }

        public void Remove(Network network)
        {
            var model = NetworkModel.FromRepositoryType(network);
            var sql = @"
                DELETE
                FROM NetworkModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = model.Id,
            };

            _connection.Execute(sql, parameters);
        }

        public void Update(Network network)
        {
            var model = NetworkModel.FromRepositoryType(network);
            var sql = @"
                UPDATE NetworkModels
                SET
                  Address = @Address,
                  AttatchedComputer_Id = @AttatchedComputer_Id,
                  LastPing = @LastPing,
                  Name = @Name
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Address = model.Address,
                AttatchedComputer_Id = model.AttatchedComputer_Id,
                Id = model.Id,
                LastPing = model.LastPing,
                Name = model.Name,
            };

            _connection.Execute(sql, parameters);
        }
    }
}
