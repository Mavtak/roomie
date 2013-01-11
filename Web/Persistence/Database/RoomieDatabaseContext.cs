using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public sealed class RoomieDatabaseContext : IRoomieDatabaseContext
    {
        public IRoomieEntitySet<UserModel> Users { get; set; }
        public IRoomieEntitySet<UserSessionModel> UserSessions { get; set; }
        public IRoomieEntitySet<ComputerModel> Computers { get; set; }
        public IRoomieEntitySet<NetworkModel> Networks { get; set; }
        public IRoomieEntitySet<DeviceModel> Devices { get; set; }
        public IRoomieEntitySet<TaskModel> Tasks { get; set; }
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

            Users = new DbContextAdapter<UserModel>(database.Users);
            UserSessions = new DbContextAdapter<UserSessionModel>(database.UserSessions);
            Computers = new DbContextAdapter<ComputerModel>(database.Computers);
            Devices = new DbContextAdapter<DeviceModel>(database.Devices);
            Tasks = new DbContextAdapter<TaskModel>(database.Tasks);
            Scripts = new DbContextAdapter<ScriptModel>(database.Scripts);
            SavedScripts = new DbContextAdapter<SavedScriptModel>(database.SavedScripts);
            WebHookSessions = new DbContextAdapter<WebHookSessionModel>(database.WebHookSessions);
            DeviceLocations = new DbContextAdapter<DeviceLocationModel>(database.DeviceLocations);
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