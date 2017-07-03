using System.Collections.Generic;

namespace Roomie.Common.ScriptingLanguage
{
    public class RoomieScript : IEnumerable<IScriptCommand>
    {
        public string Source { get; private set; }
        public ScriptCommandList Commands { get; private set; }

        private RoomieScript()
        {
        }

        public static RoomieScript FromFile(string path)
        {
            var result = new RoomieScript
            {
                Source = path,
                Commands = ScriptCommandList.FromFile(path)
            };

            return result;
        }

        //TODO: avoid casting
        IEnumerator<IScriptCommand> IEnumerable<IScriptCommand>.GetEnumerator()
        {
            return ((IEnumerable<IScriptCommand>)Commands).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IScriptCommand>)Commands).GetEnumerator();
        }
    }
}
