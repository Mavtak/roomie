namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class DateTimeParameterType : IRoomieCommandArgumentType
    {
        public string Name { get; private set; }

        public DateTimeParameterType()
        {
            Name = "TimeSpan";
        }

        public bool Validate(string value)
        {
            return TimeUtils.IsDateTime(value);
        }
    }
}
