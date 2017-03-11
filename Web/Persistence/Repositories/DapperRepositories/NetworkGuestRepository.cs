using System.Data;
using System.Linq;
using Dapper;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class NetworkGuestRepository : INetworkGuestRepository
    {
        IDbConnection _connection;

        INetworkRepository _networkRepository;
        IUserRepository _userRepository;

        public NetworkGuestRepository(IDbConnection connection, INetworkRepository networkRepository, IUserRepository userRepository)
        {
            _connection = connection;

            _networkRepository = networkRepository;
            _userRepository = userRepository;
        }

        public void Add(Network network, User user)
        {
            var networkModel = NetworkModel.FromRepositoryType(network);
            var userModel = UserModel.FromRepositoryType(user);
            var sql = @"
                INSERT
                INTO NetworkGuestModels
                (
                  Network_Id,
                  User_Id
                )
                VALUES
                (
                  @Network_Id,
                  @User_Id
                )
            ";
            var parameters = new
            {
                Network_Id = networkModel.Id,
                User_Id = userModel.Id,
            };

            _connection.Execute(sql, parameters);
        }

        public bool Check(Network network, User user)
        {
            var networkModel = NetworkModel.FromRepositoryType(network);
            var userModel = UserModel.FromRepositoryType(user);
            var sql = @"
                SELECT COUNT(*)
                FROM NetworkGuestModels
                WHERE Network_Id = @Network_Id
                  AND User_Id = @User_Id
            ";
            var parameters = new
            {
                Network_Id = networkModel.Id,
                User_Id = userModel.Id,
            };

            var count = _connection.QuerySingle<int>(sql, parameters);

            return count > 0;
        }

        public User[] Get(Network network)
        {
            var networkModel = NetworkModel.FromRepositoryType(network);
            var sql = @"
                SELECT User_Id
                FROM NetworkGuestModels
                WHERE Network_Id = @Network_Id
            ";
            var parameters = new
            {
                Network_Id = networkModel.Id,
            };

            var userIds = _connection.Query<int>(sql, parameters).ToArray();
            var result = userIds
                .Select(_userRepository.Get)
                .ToArray();

            return result;
        }

        public Network[] Get(User user)
        {
            var userModel = UserModel.FromRepositoryType(user);
            var sql = @"
                SELECT Network_Id
                FROM NetworkGuestModels
                WHERE User_Id = @User_Id
            ";
            var parameters = new
            {
                User_Id = userModel.Id,
            };

            var networkIds = _connection.Query<int>(sql, parameters).ToArray();
            var result = networkIds
                .Select(_networkRepository.Get)
                .ToArray();

            return result;
        }

        public void Remove(Network network, User user)
        {
            var networkModel = NetworkModel.FromRepositoryType(network);
            var userModel = UserModel.FromRepositoryType(user);
            var sql = @"
                DELETE
                FROM NetworkGuestModels
                WHERE Network_Id = @Network_Id
                  AND User_Id = @User_Id
            ";
            var parameters = new
            {
                Network_Id = networkModel.Id,
                User_Id = userModel.Id,
            };

            _connection.Execute(sql, parameters);
        }
    }
}
