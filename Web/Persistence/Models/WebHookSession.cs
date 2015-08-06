using System;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class WebHookSession : IHasDivId
    {
        public virtual Computer Computer { get; private set; }
        public int Id { get; private set; }
        public DateTime? LastPing { get; private set; }
        public string Token { get; private set; }

        private WebHookSession()
        {
        }

        public WebHookSession(Computer computer, int id, DateTime? lastPing, string token)
        {
            Computer = computer;
            Id = id;
            LastPing = lastPing;
            Token = token;
        }

        public static WebHookSession Create(Computer computer)
        {
            var result = new WebHookSession
            {
                Computer = computer,
                LastPing = DateTime.UtcNow,
                Token = Guid.NewGuid().ToString()
            };

            return result;
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

        public bool IsConnected
        {
            get
            {
                TimeSpan? temp = TimeSinceLastPing;
                if (TimeSinceLastPing == null)
                    return false;

                return temp.Value.TotalSeconds < 10;
            }
        }

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "webhooksession" + Id;
            }
        }

        #endregion
    }
}
