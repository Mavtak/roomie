using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public static class ExtensionsToIRoomieDatabaseContext
    {
        public static DeviceLocationModel GetDeviceLocation(this IRoomieDatabaseContext database, User user, string locationName)
        {
            //TODO: fix

            return new DeviceLocationModel
            {
                Owner = database.Backend.Users.Find(user.Id),
                Name = locationName
            };

            var result = database.DeviceLocations.Get(user, locationName);

            if (result == null)
            {
                result = new DeviceLocationModel
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
