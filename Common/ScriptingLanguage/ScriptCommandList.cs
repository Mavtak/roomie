using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Roomie.Common.ScriptingLanguage
{
    //TODO: is it bad practice to lock in 'this'?
    //TODO: eliminate use of System.Xml
    public class ScriptCommandList : IEnumerable<ScriptCommand>
    {
        private LinkedList<ScriptCommand> commands;
        public string OriginalText { get; private set; }

        public ScriptCommandList()
        {
            this.commands = new LinkedList<ScriptCommand>();
        }

        public ScriptCommandList(XmlNodeList nodes, string originalText)
            : this()
        {
            this.OriginalText = originalText;
            Add(nodes);
        }

        public static ScriptCommandList FromFile(string path)
        {
            var result = new ScriptCommandList();

            //TODO: improve
            var nodes = Common.LoadXml(path);
            result.OriginalText = nodes.OuterXml;
            result.Add(nodes);

            return result;
        }
        public static ScriptCommandList FromText(string text)
        {
            var result = new ScriptCommandList();

            result.Add(text);
            result.OriginalText = text;

            return result;
        }

        #region add
        public void Add(ScriptCommand command)
        {
            lock (this)
            {
                this.commands.AddLast(command);
            }
        }
        public void Add(XmlNode node)
        {
            Add(new ScriptCommand(node));
        }
        public void Add(IEnumerable<ScriptCommand> commands)
        {
            lock (this)
            {
                foreach (var command in commands)
                {
                    Add(command);
                }
            }
        }
        public void Add(IEnumerable<XmlNode> nodes)
        {
            lock (this)
            {
                foreach (var node in nodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        Add(node);
                    }
                }
            }
        }
        public void Add(XmlNodeList nodes)
        {
            lock (this)
            {
                foreach (XmlNode node in nodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        Add(node);
                    }
                }
            }
        }
        public void Add(string text)
        {
            var nodes = Common.GetXml(text);
            Add(nodes);
        }
        #endregion

        #region AddBeginning
        public void AddBeginning(ScriptCommand command)
        {
            lock (this)
            {
                this.commands.AddFirst(command); 
            }
        }
        public void AddBeginning(IEnumerable<ScriptCommand> commands)
        {
            lock (this)
            {
                foreach (var command in commands.Reverse())
                {
                    AddBeginning(command);
                }
            }
        }
        public void AddBeginning(IEnumerable<XmlNode> nodes)
        {
            var commands = new LinkedList<ScriptCommand>();

            foreach (var node in nodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    commands.AddLast(new ScriptCommand(node));
                }
            }

            AddBeginning(commands);
        }
        public void AddBeginning(string text)
        {
            var nodes = Common.GetXml(text);
            AddBeginning(nodes);
        }
        #endregion

        public int Count
        {
            get
            {
                lock (this)
                {
                    return this.commands.Count;
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
            this.commands.Clear();
        }

        public ScriptCommand PopFirst()
        {
            var result = this.commands.First.Value;
            this.commands.RemoveFirst();
            return result;
        }

        public ScriptCommand Select(string name)
        {
            //this is used for commands that require inner command lists to be specified. (see If and DefineCommand)
            foreach (var command in this)
            {
                if (command.FullName.Equals(name))
                {
                    return command;
                }
            }

            return null;
        }

        #region IEnumerable Interface
        IEnumerator<ScriptCommand> IEnumerable<ScriptCommand>.GetEnumerator()
        {
            return commands.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return commands.GetEnumerator();
        }
        #endregion
    }
}
