using System;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class ByteParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Byte";

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
                Convert.ToByte(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
