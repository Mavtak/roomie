﻿using System;
using System.Collections.Generic;
using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
    class MistypedArgumentException : RoomieRuntimeException
    {
        //TODO
        List<string> mistypedVariables;

        public MistypedArgumentException(List<string> mistypedVariables)
            : base("mistyped variables: " + Common.NiceList(mistypedVariables))
        {
            this.mistypedVariables = mistypedVariables;
        }
    }
}
