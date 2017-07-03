using System;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class Script
    {
        public DateTime? CreationTimestamp { get; private set; }
        public int Id { get; private set; }
        public DateTime? LastRunTimestamp { get; private set; }
        public bool? Mutable { get; private set; }
        public int? RunCount { get; private set; }
        public string Text { get; private set; }

        private Script()
        {
        }

        public Script(DateTime? creationTimestamp, int id, DateTime? lastRunTimestamp, bool? mutable, int? runCount, string text)
        {
            CreationTimestamp = creationTimestamp;
            Id = id;
            LastRunTimestamp = lastRunTimestamp;
            Mutable = mutable;
            RunCount = runCount;
            Text = text;
        }

        public static Script Create(bool mutalbe, string text)
        {
            var result = new Script
            {
                CreationTimestamp = DateTime.UtcNow,
                Mutable = mutalbe,
                RunCount = 0,
                Text = text
            };

            return result;
        }

        public static Script Create(int id)
        {
            var result = new Script
            {
                Id = id
            };

            return result;
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
