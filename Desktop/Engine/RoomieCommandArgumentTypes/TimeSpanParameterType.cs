namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class TimeSpanParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "TimeSpan";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return TimeUtils.IsTimeSpan(value);
        }
    }
}
