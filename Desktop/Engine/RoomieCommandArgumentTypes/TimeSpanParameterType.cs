namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class TimeSpanParameterType : IRoomieCommandArgumentType
    {
        public string Name { get; private set; }

        public TimeSpanParameterType()
        {
            Name = "TimeSpan";
        }

        public bool Validate(string value)
        {
            return TimeUtils.IsTimeSpan(value);
        }
    }
}
