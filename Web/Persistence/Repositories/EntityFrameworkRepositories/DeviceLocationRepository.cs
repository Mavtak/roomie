using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class DeviceLocationRepository : IDeviceLocationRepository
    {
        private readonly DbSet<DeviceLocationModel> _locations;

        public DeviceLocationRepository(DbSet<DeviceLocationModel> locations)
        {
            _locations = locations;
        }

        public DeviceLocationModel Get(int id)
        {
            var result = _locations.Find(id);

            return result;
        }

        public DeviceLocationModel Get(UserModel user, string path)
        {
            var matches = _locations.Where(x => x.Owner.Id == user.Id)
                                    .Where(x => x.Name == path);
            var result = matches.FirstOrDefault();

            return result;
        }

        public void Add(DeviceLocationModel location)
        {
            _locations.Add(location);
        }

        public void Remove(DeviceLocationModel location)
        {
            _locations.Remove(location);
        }
    }
}
