using System;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class ByteParameterType : IRoomieCommandArgumentType
    {
        public string Name { get; private set; }

        public ByteParameterType()
        {
            Name = "Byte";
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
