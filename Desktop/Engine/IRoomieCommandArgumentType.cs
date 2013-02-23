namespace Roomie.Desktop.Engine
{
    public interface IRoomieCommandArgumentType
    {
        string Name { get; }
        bool Validate(string value);
    }
}
