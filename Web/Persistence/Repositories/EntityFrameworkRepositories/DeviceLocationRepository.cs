using System.Data.Entity;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class DeviceLocationRepository : IDeviceLocationRepository
    {
        private readonly DbSet<EntityFrameworkDeviceLocationModel> _locations;

        public DeviceLocationRepository(DbSet<EntityFrameworkDeviceLocationModel> locations)
        {
            _locations = locations;
        }

        public EntityFrameworkDeviceLocationModel Get(int id)
        {
            var result = _locations.Find(id);

            return result;
        }

        public EntityFrameworkDeviceLocationModel Get(User user, string path)
        {
            var matches = _locations.Where(x => x.Owner.Id == user.Id)
                                    .Where(x => x.Name == path);
            var result = matches.FirstOrDefault();

            return result;
        }

        public void Add(EntityFrameworkDeviceLocationModel location)
        {
            _locations.Add(location);
        }

        public void Remove(EntityFrameworkDeviceLocationModel location)
        {
            _locations.Remove(location);
        }
    }
}
