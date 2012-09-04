using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.TwitterCommands.Commands.Twitter
{
    [Parameter("Text", "String")]
    [Description("This command sends a tweet.")]
    public class Tweet : TwitterCommand
    {
        protected override void Execute_TwitterDefinition(TwitterCommandContext context)
        {
            var scope = context.Scope;
            var user = context.User;

            string text = scope.GetValue("Text");
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
