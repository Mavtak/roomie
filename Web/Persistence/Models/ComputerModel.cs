using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    [Table("ComputerModels")]
    public class ComputerModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public virtual UserModel Owner { get; set; }
        public string Name { get; set; }
        public virtual ScriptModel LastScript { get; set; }

        public string AccessKey { get; set; }
        public string EncryptionKey { get; set; }

        public string Address { get; set; }

        #region LastPing

        public DateTime? LastPing { get; set; }

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                    return null;
                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        public bool IsConnected
        {
            get
            {
                TimeSpan? temp = TimeSinceLastPing;
                if (TimeSinceLastPing == null)
                    return false;

                return temp.Value.TotalSeconds <= 5;
            }
        }

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        #endregion

        private static string generateKey()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
        }

        public void RenewWebhookKeys()
        {
            AccessKey = generateKey();
            EncryptionKey = generateKey();
        }

        public void DisableWebhook()
        {
            AccessKey = null;
            EncryptionKey = null;
        }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as ComputerModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(ComputerModel that)
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

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            builder.Append("Computer{Name='");
            builder.Append(Name ?? "(null)");
            builder.Append("', TimeSinceLastPing='");
            builder.Append(TimeSinceLastPing);
            builder.Append("', Owner='");
            builder.Append((Owner != null) ? Owner.ToString() : "(null)");
            builder.Append("'}");

            return builder.ToString();
        }

        #endregion

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "computer" + Id;
            }
        }

        #endregion
    }
}