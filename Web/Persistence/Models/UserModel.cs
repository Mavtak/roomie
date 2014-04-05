using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class UserModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public string Token{ get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public DateTime? RegisteredTimestamp { get; set; }

        public virtual ICollection<ComputerModel> Computers { get; set; }
        public virtual ICollection<NetworkModel> HomeAutomationNetworks { get; set; }
        public virtual ICollection<TaskModel> Tasks { get; set; }
        public virtual ICollection<SavedScriptModel> SavedScripts { get; set; }
        public virtual ICollection<DeviceLocationModel> DeviceLocations { get; set; }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("User{Alias='");
            builder.Append(Alias);
            builder.Append("', Token='");
            builder.Append(Token);
            builder.Append("'}");

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((UserModel)obj);
        }

        public bool Equals(UserModel that)
        {
            if (that == null)
            {
                return false;
            }

            if (this.Id != that.Id)
            {
                return false;
            }

            return true;
        }

        public static bool Equals(UserModel user1, UserModel user2)
        {
            if (user1 == null && user2 == null)
            {
                return true;
            }

            if (user1 == null ^ user2 == null)
            {
                return true;
            }

            return user1.Equals(user2);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "user" + Id;
            }
        }

        #endregion
    }
}