
namespace Roomie.Common.HomeAutomation.Keypads.Buttons
{
    public interface IKeypadButtonState
    {
        string Id { get; }
        bool? Pressed { get; }
    }
}
