using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Helpers.Secrets;

namespace Roomie.Web.Persistence.Models
{
    [Table("UserModels")]
    public class EntityFrameworkUserModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public string Token{ get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }
        public DateTime? RegisteredTimestamp { get; set; }

        public virtual ICollection<EntityFrameworkComputerModel> Computers { get; set; }
        public virtual ICollection<EntityFrameworkNetworkModel> HomeAutomationNetworks { get; set; }
        public virtual ICollection<EntityFrameworkTaskModel> Tasks { get; set; }
        public virtual ICollection<EntityFrameworkSavedScriptModel> SavedScripts { get; set; }
        public virtual ICollection<DeviceLocationModel> DeviceLocations { get; set; }

        public ISecret ParseSecret()
        {
            var result = SecretExtensions.Parse(Secret);

            return result;
        }
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

            return this.Equals((EntityFrameworkUserModel)obj);
        }

        public bool Equals(EntityFrameworkUserModel that)
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

        public static bool Equals(EntityFrameworkUserModel user1, EntityFrameworkUserModel user2)
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