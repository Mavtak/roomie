using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IDeviceRepository
    {
        DeviceModel Get(int id);
        DeviceModel[] Get(NetworkModel network);
        DeviceModel[] Get(UserModel user);
        void Add(DeviceModel device);
        void Remove(DeviceModel device);
    }
}
