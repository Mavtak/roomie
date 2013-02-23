﻿using System;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class IntegerRangeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "IntegerRange";

        private int? _min;
        private int? _max;

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public IntegerRangeParameterType(int? min, int? max)
        {
            _min = min;
            _max = max;
        }

        public bool Validate(string value)
        {
            try
            {
                var number = Convert.ToInt64(value);

                if (number < _min)
                {
                    return false;
                }

                if (number > _max)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
