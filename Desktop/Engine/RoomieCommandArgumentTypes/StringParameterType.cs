
namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class StringParameterType : IRoomieCommandArgumentType
    {
        public string Name { get; private set; }

        public StringParameterType()
        {
            Name = "String";
        }

        public bool Validate(string value)
        {
            return true;
        }
    }
}
