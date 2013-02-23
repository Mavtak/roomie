
namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class StringParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "String";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return true;
        }
    }
}
