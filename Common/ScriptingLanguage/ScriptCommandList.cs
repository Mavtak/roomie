using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Roomie.Common.ScriptingLanguage
{
    //TODO: is it bad practice to lock in 'this'?
    public class ScriptCommandList : IEnumerable<IScriptCommand>
    {
        private readonly LinkedList<IScriptCommand> _commands;
        public string OriginalText { get; private set; }

        public ScriptCommandList()
        {
            _commands = new LinkedList<IScriptCommand>();
        }

        public static ScriptCommandList FromFile(string path)
        {
            try
            {
                var text = File.ReadAllText(path);
                var result = FromText(text);

                return result;
            }
            catch (Exception exception)
            {
                throw new UnexpectedException(exception);
            }
        }

        public static ScriptCommandList FromText(string text)
        {
            var result = new ScriptCommandList();

            result.Add(text);
            result.OriginalText = text;

            return result;
        }

        #region add

        public void Add(IScriptCommand command)
        {
            lock (this)
            {
                _commands.AddLast(command);
            }
        }

        public void Add(IEnumerable<IScriptCommand> commands)
        {
            lock (this)
            {
                foreach (var command in commands)
                {
                    Add(command);
                }
            }
        }

        public void Add(string text)
        {
            try
            {
                var nodes = Common.GetXml(text);
                var newCommands = XmlScriptCommand.FromNodes(nodes);
                Add(newCommands);
            }
            catch (RoomieScriptSyntaxErrorException)
            {
                //TODO: This is not the best way to detect the script format >.<

                var newCommands = TextScriptCommand.FromLines(text);
                Add(newCommands);
            }
            
        }

        #endregion

        #region AddBeginning

        public void AddBeginning(IScriptCommand command)
        {
            lock (this)
            {
                _commands.AddFirst(command); 
            }
        }

        public void AddBeginning(IEnumerable<IScriptCommand> commands)
        {
            lock (this)
            {
                foreach (var command in commands.Reverse())
                {
                    AddBeginning(command);
                }
            }
        }

        #endregion

        public int Count
        {
            get
            {
                lock (this)
                {
                    return _commands.Count;
                }
            }
        }

        public bool HasCommands
        {
            get
            {
                return Count > 0;
            }
        }

        public void Clear()
        {
            _commands.Clear();
        }

        public IScriptCommand PopFirst()
        {
            lock (this)
            {
                var result = _commands.First.Value;
                _commands.RemoveFirst();
                return result;
            }
        }

        public IScriptCommand Select(string name)
        {
            //this is used for commands that require inner command lists to be specified. (see If and DefineCommand)
            return this.FirstOrDefault(command => command.FullName.Equals(name));
        }

        #region IEnumerable Interface

        IEnumerator<IScriptCommand> IEnumerable<IScriptCommand>.GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        #endregion
    }
}
