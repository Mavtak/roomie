using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Desktop.Engine.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class NotFinishedAttribute : Attribute
    { }
}
