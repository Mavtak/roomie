using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roomie.Web.Persistence.Models;

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
            var user = new UserModel
            {
                Token = token,
                RegisteredTimestamp = DateTime.UtcNow,
                IsAdmin = token.Equals("openid:http://davidmcgrath.com/")
            };

            database.Users.Add(user);

            database.CreateHome(user);

            return user;
        }

        public static HomeModel CreateHome(this IRoomieDatabaseContext database, UserModel owner)
        {
            var home = new HomeModel
            {
                CreationTimestamp = DateTime.UtcNow,
                Name = "Home",
                Owners = new List<UserModel>(new UserModel[] { owner })
            };

            //database.Homes.Add(home);

            return home;
        }

        //public static IEnumerable<HomeModel> GetHomes(this IRoomieDatabaseContext database, UserModel user)
        //{
        //    var result = from home in database.Homes
        //                 where home.Owners.Contains(user)
        //                    || home.Guests.Contains(user)
        //                 select home;

        //    return result;
        //}
    }
}
