using System;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class BooleanParameterType : IRoomieCommandArgumentType
    {
        public string Name { get; private set; }

        public BooleanParameterType()
        {
            Name = "Boolean";
        }

        public bool Validate(string value)
        {
            try
            {
                Convert.ToBoolean(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
