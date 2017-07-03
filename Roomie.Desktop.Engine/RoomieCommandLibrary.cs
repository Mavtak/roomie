using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Delegates;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    //TODO: catch loading exceptions
    public sealed class RoomieCommandLibrary
        : IEnumerable<RoomieCommand>
    {
        public event RoomieCommandLibraryEventDelegate Message;

        private readonly Dictionary<string, RoomieCommand> _commands;
        private readonly HashSet<string> _groups;

        // Could this be componentized into the CoreCommands extension?
        private readonly List<RoomieDynamicCommand> _customCommands;
        
        public RoomieCommandLibrary()
        {
            _commands = new Dictionary<string, RoomieCommand>();
            _groups = new HashSet<string>();
            _customCommands = new List<RoomieDynamicCommand>();
        }

        private void WriteMessage(string message)
        {
            if (Message == null)
            {
                //TODO: what's best?
                //throw new Exception(message);
                return;
            }
            Message(this, new RoomieCommandLibraryEventArgs(message));
        }

        internal void Clear()
        {
            lock (_commands)
            {
                lock (_customCommands)
                {
                    _commands.Clear();
                    _customCommands.Clear();
                    _groups.Clear();
                }
            }
        }

        #region add commands
        //TODO: could AddCommand be made internal?

        public void AddCommand(RoomieCommand newCommand)
        {
            var key = newCommand.FullName;

            lock (_commands)
            {
                if (_commands.ContainsKey(key))
                {
                    _commands.Remove(key);
                }

                _commands.Add(key, newCommand);

                var dynamic = newCommand as RoomieDynamicCommand;

                if (dynamic != null)
                {
                    if (_customCommands.Contains(dynamic))
                    {
                        _customCommands.Remove(dynamic);
                    }
                    _customCommands.Add(dynamic);
                }

                if (!_groups.Contains(newCommand.Group))
                {
                    _groups.Add(newCommand.Group);
                }
            }

            if (!newCommand.Finalized)
                WriteMessage("Warning: " + newCommand.FullName + " is not finalized.");

            if (newCommand.Name.Equals("StartupTasks") && !newCommand.GetType().IsSubclassOf(typeof(StartupCommand)))
            {
                WriteMessage("Command \"" + newCommand.Name + "\" is labeled as a Startup command, but is not implemented correctly.");
            }
        }

        public void AddCommandsFromAssembly(System.Reflection.Assembly assembly)
        {
            try
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(RoomieCommand))
                        && type.FullName.Contains(".Commands.")
                        && !type.IsAbstract)
                    {
                        var constructorInfo = type.GetConstructor(new Type[0]);
                        try
                        {
                            object command = constructorInfo.Invoke(new object[0]);
                            AddCommand((RoomieCommand)(command));
                        }
                        catch (System.Reflection.TargetInvocationException exception)
                        {
                            WriteMessage("Error loading " + type.FullName + ". " + exception.InnerException.Message);
                        }
                    }
                }
            }
            catch (System.Reflection.ReflectionTypeLoadException reflectionException)
            {
                throw new LoadPluginException(assembly, reflectionException);
            }
        }

        public void AddCommandsFromPlugin(string path)
        {
            //TODO: catch exceptions
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(path);

            AddCommandsFromAssembly(assembly);
        }

        public void AddCommandsFromPluginFolder(string directoryPath)
        {
            foreach (string dllPath in System.IO.Directory.GetFiles(directoryPath, "*Commands.dll", System.IO.SearchOption.TopDirectoryOnly))
            {
                try
                {
                    AddCommandsFromPlugin(dllPath);
                }
                catch (LoadPluginException exception)
                {
                    WriteMessage(exception.Message);
                }
            }
        }
        #endregion

        #region lookup
        public int Count
        {
            get
            {
                return _commands.Count;
            }
        }

        internal IEnumerable<RoomieCommand> StartupTasks
        {
            get
            {
                var result = new LinkedList<RoomieCommand>();
                foreach (RoomieCommand command in this)
                {
                    if (command.GetType().IsSubclassOf(typeof(StartupCommand)))
                    {
                        result.AddLast(command);
                    }
                }
                return result;
            }

        }

        internal IEnumerable<RoomieCommand> ShutDownTasks
        {
            get
            {
                var result = this.Where(command => command.Name.Equals("ShutDownTasks"));

                return result;
            }
        }

        public bool ContainsCommandGroup(string groupName)
        {
            return _groups.Contains(groupName);
        }

        public IEnumerable<string> Groups
        {
            get
            {
                //TODO: can this be done without copying elements?
                return _groups.ToList();
            }
        }

        public RoomieCommand GetCommandFromFullName(string fullName)
        {
            return _commands[fullName];
        }

        public bool ContainsCommandFullName(string fullName)
        {
            return _commands.ContainsKey(fullName);
        }

        public RoomieCommand GetCommandFromType(Type type)
        {
            foreach (var command in this._commands.Values)
            {
                if (command.GetType().Equals(type))
                {
                    return command;
                }
            }
            throw new CommandNotFoundException(type.ToString());
        }
        #endregion

        #region enumerator

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _commands.Values.GetEnumerator();
        }
        IEnumerator<RoomieCommand> IEnumerable<RoomieCommand>.GetEnumerator()
        {
            return _commands.Values.GetEnumerator();
        }

        public void WriteToXml(XmlWriter writer, bool includeCustomCommands)
        {
            writer.WriteStartElement("Commands");
            {
                foreach(var command in this)
                {
                    if(includeCustomCommands || !command.IsDynamic)
                        command.WriteToXml(writer);
                }
            }
            writer.WriteEndElement();
        }
        #endregion
    }
}
