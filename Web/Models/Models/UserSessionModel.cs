using System;
using Roomie.Web.Models.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Roomie.Web.Models
{
    public class UserSessionModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public UserModel User { get; set; }
        public DateTime CreationTimeStamp { get; set; }
        public DateTime LastContactTimeStamp { get; set; }
        public string Token { get; set; }
        public string Data { get; set; }
        //public ICollection<StringStringPair> Data { get; set; }
        //TODO: add "Expires" and "IsExpired" properties

        public UserSessionModel()
        {
            CreationTimeStamp = DateTime.UtcNow;
            LastContactTimeStamp = DateTime.UtcNow;
            Token = Guid.NewGuid().ToString();
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
