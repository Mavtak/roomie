using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IDeviceRepository
    {
        DeviceModel Get(int id);
        DeviceModel Get(UserModel user, int id);
        DeviceModel[] Get(NetworkModel network);
        void Add(DeviceModel device);
        void Remove(DeviceModel device);
    }
}
