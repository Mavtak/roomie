using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public static class ExtensionsToIRoomieDatabaseContext
    {
        public static DeviceLocationModel GetDeviceLocation(this IRoomieDatabaseContext database, UserModel user, string locationName)
        {
            //TODO: fix

            return new DeviceLocationModel
            {
                Owner = user,
                Name = locationName
            };
            //TODO: search in User.DeviceLocations instead?
            var search = from l in database.DeviceLocations
                         where l.Name == locationName && l.Owner.Id == user.Id
                         select l;

            if (search.Count() > 0)
            {
                return search.First();
            }

            var newLocation = new DeviceLocationModel
            {
                Owner = user,
                Name = locationName
            };

            database.DeviceLocations.Add(newLocation);

            //the DB changes will have to be saved

            return newLocation;
        }

        public static UserModel CreateUser(this IRoomieDatabaseContext database, string token)
        {
            var user = database.Users.Add(token);

            return user;
        }
    }
}
