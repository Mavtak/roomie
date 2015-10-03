using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public static class ExtensionsToIRoomieDatabaseContext
    {
        public static EntityFrameworkDeviceLocationModel GetDeviceLocation(this IRoomieDatabaseContext database, User user, string locationName)
        {
            //TODO: fix

            return new EntityFrameworkDeviceLocationModel
            {
                Owner = database.Backend.Users.Find(user.Id),
                Name = locationName
            };

            var result = database.DeviceLocations.Get(user, locationName);

            if (result == null)
            {
                result = new EntityFrameworkDeviceLocationModel
                    {
                        Owner = database.Backend.Users.Find(user.Id),
                        Name = locationName
                    };

                database.DeviceLocations.Add(result);

            }

            //the DB changes will have to be saved

            return result;
        }
    }
}
