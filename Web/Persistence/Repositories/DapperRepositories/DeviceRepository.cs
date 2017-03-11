using System.Data;
using System.Linq;
using Dapper;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.DapperRepositories.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private IDbConnection _connection;

        INetworkRepository _networkRepository;
        IScriptRepository _scriptRepository;
        ITaskRepository _taskRepository;

        public DeviceRepository(IDbConnection connection, INetworkRepository networkRepository, IScriptRepository scriptRepository, ITaskRepository taskRepository)
        {
            _connection = connection;
            _networkRepository = networkRepository;
            _scriptRepository = scriptRepository;
            _taskRepository = taskRepository;
        }

        public void Add(Device device)
        {
            var model = DeviceModel.FromRepositoryType(device);
            var sql = @"
                INSERT INTO DeviceModels
                (
                  Address,
                  IsConnected,
                  LastPing,
                  Name,
                  Network_Id,
                  Notes,
                  Type_Name
                )
                VALUES
                (
                  @Address,
                  @IsConnected,
                  @LastPing,
                  @Name,
                  @Network_Id,
                  @Notes,
                  @Type_Name
                )
                
                SELECT CAST(IDENT_CURRENT('DeviceModels') as int)
            ";
            var parameters = new
            {
                Address = model.Address,
                IsConnected = model.IsConnected,
                LastPing = model.LastPing,
                Name = model.LastPing,
                Network_Id = model.Network_Id,
                Notes = model.Notes,
                Type_Name = model.Type_Name,
            };

            var id = _connection.QuerySingle<int>(sql, parameters);

            device.SetId(id);
        }

        public Device[] Get(Network network)
        {
            var networkModel = NetworkModel.FromRepositoryType(network);
            var sql = @"
                SELECT *
                FROM DeviceModels
                WHERE Network_Id = @Network_Id
            ";
            var parameters = new
            {
                Network_Id = networkModel.Id,
            };

            var models = _connection.Query<DeviceModel>(sql, parameters).ToArray();
            var result = models
                .Select(x => x.ToRepositoryType(_networkRepository, _scriptRepository, _taskRepository))
                .ToArray();

            return result;
        }

        public Device Get(int id)
        {
            var sql = @"
                SELECT *
                FROM DeviceModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = id,
            };

            var model = _connection.QuerySingle<DeviceModel>(sql, parameters);
            var result = model.ToRepositoryType(_networkRepository, _scriptRepository, _taskRepository);

            return result;
        }

        public Device Get(User user, int id)
        {
            var userModel = UserModel.FromRepositoryType(user);
            var result = Get(id);

            if (result?.Network?.Owner?.Id != userModel?.Id)
            {
                return null;
            }

            return result;
        }

        public void Remove(Device device)
        {
            var model = DeviceModel.FromRepositoryType(device);
            var sql = @"
                DELETE
                FROM DeviceModels
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = model.Id,
            };

            _connection.Execute(sql, parameters);
        }

        public void Update(Device device)
        {
            var model = DeviceModel.FromRepositoryType(device);
            var sql = @"
                UPDATE DeviceModels
                SET
                  Name = @Name,
                  Notes = @Notes,
                  Type_Name = @Type_Name
                WHERE Id = @Id
            ";
            var parameters = new
            {
                Id = model.Id,
                Name = model.Name,
                Notes = model.Notes,
                Type_Name = model.Type_Name,
            };

            _connection.Execute(sql, parameters);
        }

        public void Update(int id, IDeviceState state)
        {
            var device = Get(id);
            device.Update(state, false);

            Update(device);
        }
    }
}
