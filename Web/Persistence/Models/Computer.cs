using System;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class Computer : IHasDivId
    {
        public string AccessKey {get; private set; }
        public string Address {get; private set; }
        public string EncryptionKey {get; private set; }
        public int Id {get; private set; }
        public DateTime? LastPing {get; private set; }
        public Script LastScript {get; private set; }
        public string Name {get; private set; }
        public User Owner {get; private set; }

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

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                    return null;
                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        public Computer()
        {
        }

        public Computer(string accessKey, string address, string encryptionKey, int id, DateTime? lastPing, Script lastScript, string name, User owner)
        {
            AccessKey = accessKey;
            Address = address;
            EncryptionKey = encryptionKey;
            Id = id;
            LastPing = lastPing;
            LastScript = lastScript;
            Name = name;
            Owner = owner;
        }

        public static Computer Create(string name, User owner, DateTime? lastPing = null)
        {
            var result = new Computer
            {
                Name = name,
                LastPing = lastPing,
                Owner = owner
            };

            return result;
        }

        public void UpdateLastScript(Script lastScript)
        {
            LastScript = lastScript;
        }
        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        private static string GenerateKey()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
        }

        public void RenewWebhookKeys()
        {
            AccessKey = GenerateKey();
            EncryptionKey = GenerateKey();
        }

        public void DisableWebhook()
        {
            AccessKey = null;
            EncryptionKey = null;
        }

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
