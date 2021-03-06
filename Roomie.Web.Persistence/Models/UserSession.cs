﻿using System;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class UserSession
    {
        public DateTime CreationTimeStamp { get; private set; }
        public int Id { get; set; }
        public DateTime LastContactTimeStamp { get; private set; }
        public string Token { get; private set; }
        public virtual User User { get; private set; }

        private UserSession()
        {
        }

        public UserSession(DateTime creationTimeStamp, int id, DateTime lastContactTimeStamp, string token, User user)
        {
            CreationTimeStamp = creationTimeStamp;
            Id = id;
            LastContactTimeStamp = lastContactTimeStamp;
            Token = token;
            User = user;
        }

        public static UserSession Create(User user)
        {
            var result = new UserSession()
            {
                CreationTimeStamp = DateTime.UtcNow,
                LastContactTimeStamp = DateTime.UtcNow,
                Token = Guid.NewGuid().ToString(),
                User = user
            };

            return result;
        }

        public void UpdateLastContact()
        {
            LastContactTimeStamp = DateTime.UtcNow;
        }

        public void SetId(int id)
        {
            if (Id != 0)
            {
                throw new ArgumentException("Id is already set");
            }

            Id = id;
        }
    }
}
