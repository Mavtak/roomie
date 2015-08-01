using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IDeviceRepository
    {
        EntityFrameworkDeviceModel Get(int id);
        EntityFrameworkDeviceModel Get(EntityFrameworkUserModel user, int id);
        EntityFrameworkDeviceModel[] Get(EntityFrameworkNetworkModel network);
        void Add(EntityFrameworkDeviceModel device);
        void Remove(EntityFrameworkDeviceModel device);
        void Update(int id, IDeviceState state);
    }
}
