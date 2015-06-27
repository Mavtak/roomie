using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine
{
    public interface IRoomieCommandArgumentType
    {
        string Name { get; }
        bool Validate(IParameter parameter);
        string ValidationMessage(string parameterName);
    }
}
