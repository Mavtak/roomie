﻿using System;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Database
{
    public interface IRoomieDatabaseContext : IDisposable
    {
        IRoomieEntitySet<UserModel> Users { get; set; }
        IRoomieEntitySet<UserSessionModel> UserSessions { get; set; }
        IRoomieEntitySet<ComputerModel> Computers { get; set; }
        IRoomieEntitySet<NetworkModel> Networks { get; set; }
        IRoomieEntitySet<DeviceModel> Devices { get; set; }
        IRoomieEntitySet<TaskModel> Tasks { get; set; }
        IRoomieEntitySet<ScriptModel> Scripts { get; set; }
        IRoomieEntitySet<SavedScriptModel> SavedScripts { get; set; }
        IRoomieEntitySet<WebHookSessionModel> WebHookSessions { get; set; }
        IRoomieEntitySet<DeviceLocationModel> DeviceLocations { get; set; }
        int SaveChanges();
        void Reset();
        //public DbSet<StringStringPair> StringStringDictionary { get; set; }
        //public DbSet<HomeModel> Homes { get; set; }
    }
}