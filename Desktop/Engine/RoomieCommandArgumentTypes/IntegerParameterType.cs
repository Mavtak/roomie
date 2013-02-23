using System;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class IntegerParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Integer";

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
