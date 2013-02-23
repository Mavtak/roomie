using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class RetriesParameterAttribute : ParameterAttribute
    {
        public const string Key = "Retries";

        public RetriesParameterAttribute()
            : base(Key, "Integer", "3")
        {
        }
    }
}
