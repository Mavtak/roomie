using System.Collections.Generic;

using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.TwitterCommands
{
    public class TwitterCommandContext : RoomieCommandContext
    {
        public Twitterizer.Framework.Twitter User { get; set; }

        public TwitterCommandContext(RoomieCommandContext context)
            : base(context)
        { }

        public Dictionary<string, Twitterizer.Framework.Twitter> TwitterUsers
        {
            get
            {
                // Access the dictionary of twitter users from the central data store
                var key = typeof(InternalLibraryVersion);
                var value = DataStore.GetAdd<Dictionary<string, Twitterizer.Framework.Twitter>>(key);
                return value;
            }
        }
    }
}
