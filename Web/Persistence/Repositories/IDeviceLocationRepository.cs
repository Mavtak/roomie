
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories
{
    public interface IDeviceLocationRepository
    {
        EntityFrameworkDeviceLocationModel Get(int id);
        EntityFrameworkDeviceLocationModel Get(User user, string path);
        void Add(EntityFrameworkDeviceLocationModel location);
        void Remove(EntityFrameworkDeviceLocationModel location);
    }
}
