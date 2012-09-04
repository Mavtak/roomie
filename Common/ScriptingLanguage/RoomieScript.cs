using System.Collections.Generic;

namespace Roomie.Common.ScriptingLanguage
{
    public class RoomieScript : IEnumerable<ScriptCommand>
    {
        public string Source { get; private set; }
        public ScriptCommandList Commands { get; private set; }

        private RoomieScript()
        { }

        public static RoomieScript FromFile(string path)
        {
            var result = new RoomieScript
            {
                Source = path,
                Commands = ScriptCommandList.FromFile(path)
            };

            return result;
        }

        IEnumerator<ScriptCommand> IEnumerable<ScriptCommand>.GetEnumerator()
        {
            return ((IEnumerable<ScriptCommand>)Commands).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ScriptCommand>)Commands).GetEnumerator();
        }
    }
}
