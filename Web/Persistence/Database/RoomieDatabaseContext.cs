﻿using System;
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
        public IComputerRepository Computers { get; set; }
        public INetworkGuestRepository NetworkGuests { get; set; }
        public INetworkRepository Networks { get; set; }
        public IDeviceRepository Devices { get; set; }
        public ITaskRepository Tasks { get; set; }
        public IScriptRepository Scripts { get; set; }
        public ISavedScriptRepository SavedScripts { get; set; }
        public IDeviceLocationRepository DeviceLocations { get; set; }
        public ISessionRepository Sessions { get; set; }
        //public IRoomieEntitySet<StringStringPair> StringStringDictionary { get; set; }
        //public IRoomieEntitySet<HomeModel> Homes { get; set; }

        public static string ConnectionString { private get; set; }

        private readonly EntityFrameworkRoomieDatabaseBackend database;

        public RoomieDatabaseContext()
        {
            this.database = new EntityFrameworkRoomieDatabaseBackend(ConnectionString ?? "RoomieDatabaseContext");

            Computers = new EntityFrameworkComputerRepository(database.Computers);

            NetworkGuests = new EntityFrameworkNetworkGuestRepository(database.NetworkGuests);

            var entityframeworkNetworkRepository = new EntityFrameworkNetworkRepository(database.Networks);
            Networks = new GuestEnabledNetworkRepository(entityframeworkNetworkRepository, NetworkGuests);

            var entityFrameworkDeviceRepository = new EntityFrameworkDeviceRepository(database.Devices, Networks);
            Devices = new GuestEnabledDeviceRepository(entityFrameworkDeviceRepository, NetworkGuests);

            DeviceLocations = new EntityFrameworkDeviceLocationRepository(database.DeviceLocations);

            SavedScripts = new EntityFrameworkSavedScriptRepository(database.SavedScripts);

            Scripts = new EntityFrameworkScriptRepository(database.Scripts);

            Tasks = new EntityFrameworkTaskRepository(database.Tasks);

            Users = new EntityFrameworkUserRepository(database.Users);

            Sessions = new EntityFrameworkSessionRepository(database.UserSessions, database.WebHookSessions);
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