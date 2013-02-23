using System;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class BooleanParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Boolean";

        public string Name
        {
            get
            {
                return Key;
            }
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
