using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IDeviceRepository
    {
        Device Get(int id, IRepositoryModelCache cache = null);
        Device Get(User user, int id, IRepositoryModelCache cache = null);
        Device[] Get(Network network, IRepositoryModelCache cache = null);
        void Add(Device device);
        void Remove(Device device);
        void Update(Device device);
        void Update(int id, IDeviceState state);
    }
}
