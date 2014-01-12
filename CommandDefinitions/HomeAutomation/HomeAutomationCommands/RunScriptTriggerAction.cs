using System.Collections.Generic;
using System.Linq;
using Roomie.Common.ScriptingLanguage;
using Roomie.Common.Triggers;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class RunScriptTriggerAction : ITriggerAction
    {
        private ThreadPool _threadPool;
        private IEnumerable<ScriptCommandList> _scripts;

        public RunScriptTriggerAction(ThreadPool threadPool, params ScriptCommandList[] scripts)
        {
            _threadPool = threadPool;
            _scripts = scripts.ToArray();
        }

        public void Action()
        {
            foreach (var script in _scripts)
            {
                _threadPool.AddCommands(script);
            }
        }
    }
}
