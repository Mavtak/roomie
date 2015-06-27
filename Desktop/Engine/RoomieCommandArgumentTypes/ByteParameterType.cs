﻿using System;
using System.Text;
using Roomie.Desktop.Engine.Parameters;

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

        public bool Validate(IParameter parameter)
        {
            try
            {
                Convert.ToByte(parameter.Value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a number from 0 to 255");
            }

            return builder.ToString();
        }
    }
}
