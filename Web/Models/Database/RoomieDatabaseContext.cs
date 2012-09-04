using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

using Roomie.Web.Models;

namespace Roomie.Web.Helpers
{
    public class RoomieDatabaseContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserSessionModel> UserSessions { get; set; }
        public DbSet<ComputerModel> Computers { get; set; }
        public DbSet<NetworkModel> Networks { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ScriptModel> Scripts { get; set; }
        public DbSet<SavedScriptModel> SavedScripts { get; set; }
        public DbSet<WebHookSessionModel> WebHookSessions { get; set; }
        public DbSet<DeviceLocationModel> DeviceLocations { get; set; }
        //public DbSet<StringStringPair> StringStringDictionary { get; set; }
        //public DbSet<HomeModel> Homes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Roomie.Common.HomeAutomation.DeviceType>(new DeviceTypeMapping());
        }

        public DeviceLocationModel GetDeviceLocation(UserModel user, string locationName)
        {
            //TODO: fix

            return new DeviceLocationModel
            {
                Owner = user,
                Name = locationName
            };
            //TODO: search in User.DeviceLocations instead?
            var search = from l in DeviceLocations
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

            DeviceLocations.Add(newLocation);

            //the DB changes will have to be saved

            return newLocation;
        }

        public UserModel CreateUser(string token)
        {
            var user = new UserModel
            {
                Token = token,
                RegisteredTimestamp = DateTime.UtcNow,
                IsAdmin = token.Equals("openid:http://davidmcgrath.com/")
            };

            Users.Add(user);

            CreateHome(user);

            return user;
        }

        public HomeModel CreateHome(UserModel owner)
        {
            var home = new HomeModel
            {
                CreationTimestamp = DateTime.UtcNow,
                Name = "Home",
                Owners = new List<UserModel>(new UserModel[] { owner })
            };

            //Homes.Add(home);

            return home;
        }

        //public IEnumerable<HomeModel> GetHomes(UserModel user)
        //{
        //    var result = from home in Homes
        //                 where home.Owners.Contains(user)
        //                    || home.Guests.Contains(user)
        //                 select home;

        //    return result;
        //}
    }

    public class DeviceTypeMapping : ComplexTypeConfiguration<Roomie.Common.HomeAutomation.DeviceType>
    {
        public DeviceTypeMapping()
        {
            this.Property(p => p.Name);
        }
    }
}