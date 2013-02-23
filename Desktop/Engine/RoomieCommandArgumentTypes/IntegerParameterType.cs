using System;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class IntegerParameterType : IRoomieCommandArgumentType
    {
        public string Name { get; private set; }

        public IntegerParameterType()
        {
            Name = "Integer";
        }

        public bool Validate(string value)
        {
            try
            {
                Convert.ToInt64(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
