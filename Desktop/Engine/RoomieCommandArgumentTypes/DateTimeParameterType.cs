namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class DateTimeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "DateTime";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return TimeUtils.IsDateTime(value);
        }
    }
}
