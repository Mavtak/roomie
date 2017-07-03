
using Roomie.Desktop.Engine.Commands;

namespace Roomie.Desktop.Engine.Tests.Commands
{
    [Description("A command")]
    [Group("TestCommands")]
    [StringParameter("Text")]
    [IntegerParameter("Number")]
    [StringParameter("TextWithDefault", "derp")]
    class AttributeTestCommand
    {
    }
}
