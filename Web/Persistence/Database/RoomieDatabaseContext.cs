using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Persistence.Database
{
    public sealed class RoomieDatabaseContext : IRoomieDatabaseContext
    {
        public IUserRepository Users { get; set; }
        public IRoomieEntitySet<UserSessionModel> UserSessions { get; set; }
        public IRoomieEntitySet<ComputerModel> Computers { get; set; }
        public INetworkGuestRepository NetworkGuests { get; set; }
        public INetworkRepository Networks { get; set; }
        public IDeviceRepository Devices { get; set; }
        public ITaskRepository Tasks { get; set; }
        public IRoomieEntitySet<ScriptModel> Scripts { get; set; }
        public IRoomieEntitySet<SavedScriptModel> SavedScripts { get; set; }
        public IRoomieEntitySet<WebHookSessionModel> WebHookSessions { get; set; }
        public IRoomieEntitySet<DeviceLocationModel> DeviceLocations { get; set; }
        //public IRoomieEntitySet<StringStringPair> StringStringDictionary { get; set; }
        //public IRoomieEntitySet<HomeModel> Homes { get; set; }

        public static string ConnectionString { private get; set; }

        private readonly EntityFrameworkRoomieDatabaseBackend database;

        public RoomieDatabaseContext()
        {
            this.database = new EntityFrameworkRoomieDatabaseBackend(ConnectionString ?? "RoomieDatabaseContext");

            UserSessions = new DbContextAdapter<UserSessionModel>(database.UserSessions);
            Computers = new DbContextAdapter<ComputerModel>(database.Computers);
            Scripts = new DbContextAdapter<ScriptModel>(database.Scripts);
            SavedScripts = new DbContextAdapter<SavedScriptModel>(database.SavedScripts);
            WebHookSessions = new DbContextAdapter<WebHookSessionModel>(database.WebHookSessions);
            DeviceLocations = new DbContextAdapter<DeviceLocationModel>(database.DeviceLocations);

            NetworkGuests = new EntityFrameworkNetworkGuestRepository(database.NetworkGuests);

            var entityframeworkNetworkRepository = new EntityFrameworkNetworkRepository(database.Networks);
            Networks = new GuestEnabledNetworkRepository(entityframeworkNetworkRepository, NetworkGuests);

            var entityFrameworkDeviceRepository = new EntityFrameworkDeviceRepository(database.Devices, Networks);
            Devices = new GuestEnabledDeviceRepository(entityFrameworkDeviceRepository, NetworkGuests);

            Tasks = new EntityFrameworkTaskRepository(database.Tasks);

            Users = new EntityFrameworkUserRepository(database.Users);
        }

        public void Reset()
        {
            DatabaseUtilities.Reset(database);
        }

        public int SaveChanges()
        {
            return database.SaveChanges();
        }

        public void Dispose()
        {
            database.Dispose();
        }

        private sealed class DbContextAdapter<TEntityType> : IRoomieEntitySet<TEntityType>
            where TEntityType : class
        {
            private DbSet<TEntityType> set;
            private IQueryable<TEntityType> queriable;

            public DbContextAdapter(DbSet<TEntityType> set)
            {
                this.set = set;
                queriable = (IQueryable<TEntityType>)set;
            }

            #region IRoomieEntitySet implementation
            void IRoomieEntitySet<TEntityType>.Add(TEntityType entity)
            {
                set.Add(entity);
            }

            void IRoomieEntitySet<TEntityType>.Remove(TEntityType entity)
            {
                set.Remove(entity);
            }

            TEntityType IRoomieEntitySet<TEntityType>.Find(object id)
            {
                return set.Find(id);
            }
            #endregion

            #region IQueriable implementation
            Type IQueryable.ElementType
            {
                get
                {
                    return queriable.ElementType;
                }
            }

            System.Linq.Expressions.Expression IQueryable.Expression
            {
                get
                {
                    return queriable.Expression;
                }
            }

            IQueryProvider IQueryable.Provider
            {
                get
                {
                    return queriable.Provider;
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return queriable.GetEnumerator();
            }

            IEnumerator<TEntityType> IEnumerable<TEntityType>.GetEnumerator()
            {
                return queriable.GetEnumerator();
            }
            #endregion
        }
    }
}