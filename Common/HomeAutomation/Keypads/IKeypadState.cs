using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.Common.HomeAutomation.Keypads
{
    public interface IKeypadState
    {
        IEnumerable<IKeypadButtonState> Buttons { get; } 
    }
}
