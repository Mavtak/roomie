using System;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class UserSession : IHasDivId
    {
        public DateTime CreationTimeStamp { get; private set; }
        public int Id { get; set; }
        public DateTime LastContactTimeStamp { get; private set; }
        public string Token { get; private set; }
        public virtual EntityFrameworkUserModel User { get; private set; }

        private UserSession()
        {
        }

        public UserSession(DateTime creationTimeStamp, int id, DateTime lastContactTimeStamp, string token, EntityFrameworkUserModel user)
        {
            CreationTimeStamp = creationTimeStamp;
            Id = id;
            LastContactTimeStamp = lastContactTimeStamp;
            Token = token;
            User = user;
        }

        public static UserSession Create(EntityFrameworkUserModel user)
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

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "usersession" + Id;
            }
        }

        #endregion
    }
}
