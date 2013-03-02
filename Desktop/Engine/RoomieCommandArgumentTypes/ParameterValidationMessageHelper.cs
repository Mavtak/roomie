﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class ParameterValidationMessageHelper : IDisposable
    {
        private StringBuilder _builder;
        private string _parameter;

        public ParameterValidationMessageHelper(StringBuilder builder, string parameter)
        {
            _builder = builder;
            parameter = _parameter;

            if (_parameter != null)
            {
                builder.Append(parameter);
                builder.Append(" must be ");
            }
        }

        public void Dispose()
        {
            if (_parameter != null)
            {
                _builder.Append(".");
            }
        }
    }
}