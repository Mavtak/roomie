using System;

namespace Roomie.Web.Persistence.Models
{
    public class Task
    {
        public DateTime? Expiration { get; private set; }
        public int Id { get; private set; }
        public string Origin { get; private set; }
        public virtual User Owner { get; private set; }
        public DateTime? ReceivedTimestamp { get; private set; }
        public virtual Script Script { get; private set; }
        public virtual Computer Target { get; private set; }

        private Task()
        {
        }

        public Task(DateTime? expiration, int id, string origin, User owner, DateTime? receivedTimestamp, Script script, Computer target)
        {
            Expiration = expiration;
            Id = id;
            Origin = origin;
            Owner = owner;
            ReceivedTimestamp = receivedTimestamp;
            Script = script;
            Target = target;
        }

        public static Task Create(User owner, string origin, Computer target, Script script)
        {
            var result = new Task
            {
                Expiration = DateTime.UtcNow.AddSeconds(30),
                Origin = origin,
                Owner = owner,
                Script = script,
                Target = target
            };

            return result;
        }

        public bool Received
        {
            get
            {
                return ReceivedTimestamp.HasValue;
            }
        }

        public bool Waiting
        {
            get
            {
                return !Received
                    && ((Expiration >= DateTime.UtcNow)
                        || !Expiration.HasValue);
            }
        }

        public bool Expired
        {
            get
            {
                return !Received && (Expiration < DateTime.UtcNow);
            }
        }

        public void MarkAsReceived()
        {
            ReceivedTimestamp = DateTime.UtcNow;
        }

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "task" + Id;
            }
        }

        #endregion
    }
}
