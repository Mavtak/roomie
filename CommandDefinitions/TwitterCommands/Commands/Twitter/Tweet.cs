using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.TwitterCommands.Commands.Twitter
{
    [StringParameter("Text")]
    [Description("This command sends a tweet.")]
    public class Tweet : TwitterCommand
    {
        protected override void Execute_TwitterDefinition(TwitterCommandContext context)
        {
            var user = context.User;

            string text = context.ReadParameter("Text").Value;
            try
            {
                user.Status.Update(text);
            }
            catch (Twitterizer.Framework.TwitterizerException e)
            {
                throw new RoomieRuntimeException(e.Message);
            }
        }
    }
}
