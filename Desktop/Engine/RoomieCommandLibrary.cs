using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Roomie.Desktop.Engine.Delegates;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    //TODO: catch loading exceptions
    public sealed class RoomieCommandLibrary
        : IEnumerable<RoomieCommand>, System.Collections.IEnumerable
    {
        public event RoomieCommandLibraryEventDelegate Message;

        private readonly Dictionary<string, RoomieCommand> commands;
        private readonly HashSet<string> groups;

        // Could this be componentized into the CoreCommands extension?
        private readonly List<RoomieDynamicCommand> customCommands;
        
        public RoomieCommandLibrary()
        {
            commands = new Dictionary<string, RoomieCommand>();
            groups = new HashSet<string>();
            customCommands = new List<RoomieDynamicCommand>();
        }

        private void writeMessage(string message)
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
            lock (commands)
            {
                lock (customCommands)
                {
                    commands.Clear();
                    customCommands.Clear();
                    groups.Clear();
                }
            }
        }

        #region add commands
        //TODO: could AddCommand be made internal?

        public void AddCommand(RoomieCommand newCommand)
        {
            var key = newCommand.FullName;

            lock (commands)
            {
                if (commands.ContainsKey(key))
                {
                    commands.Remove(key);
                }

                commands.Add(key, newCommand);

                var dynamic = newCommand as RoomieDynamicCommand;

                if (dynamic != null)
                {
                    if (customCommands.Contains(dynamic))
                    {
                        customCommands.Remove(dynamic);
                    }
                    customCommands.Add(dynamic);
                }

                if (!groups.Contains(newCommand.Group))
                {
                    groups.Add(newCommand.Group);
                }
            }

            if (!newCommand.Finalized)
                writeMessage("Warning: " + newCommand.FullName + " is not finalized.");

            if (newCommand.Name.Equals("StartupTasks") && !newCommand.GetType().IsSubclassOf(typeof(StartupCommand)))
            {
                writeMessage("Command \"" + newCommand.Name + "\" is labeled as a Startup command, but is not implemented correctly.");
            }

            if (newCommand.Name.Equals("ShutdownTasks") && !newCommand.GetType().IsSubclassOf(typeof(ShutdownCommand)))
            {
                writeMessage("Command \"" + newCommand.Name + "\" is labeled as a Shutdown command, but is not implemented correctly.");
            }
                
        }

        public void AddCommands(IEnumerable<RoomieCommand> newCommands)
        {
            foreach (var newCommand in newCommands)
                AddCommand(newCommand);
        }

        public void AddCommandsFromAssembly(System.Reflection.Assembly assembly)
        {
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(RoomieCommand))
                        && type.FullName.Contains(".Commands.")
                        && !type.IsAbstract)
                    {
                        System.Reflection.ConstructorInfo constructorInfo = type.GetConstructor(new Type[0]);
                        try
                        {
                            object command = constructorInfo.Invoke(new object[0]);
                            AddCommand((RoomieCommand)(command));
                        }
                        catch (System.Reflection.TargetInvocationException exception)
                        {
                            writeMessage("Error loading " + type.FullName + ". " + exception.InnerException.Message);
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
                    writeMessage(exception.Message);
                }
            }
        }
        #endregion

        #region lookup
        public int Count
        {
            get
            {
                return commands.Count;
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
                var result = new LinkedList<RoomieCommand>();
                foreach (RoomieCommand command in this)
                {
                    if (command.GetType().IsSubclassOf(typeof(ShutdownCommand)))
                    {
                        result.AddLast(command);
                    }
                }
                return result;
            }

        }

        public bool ContainsCommandGroup(string groupName)
        {
            return groups.Contains(groupName);
        }

        public IEnumerable<string> Groups
        {
            get
            {
                //TODO: can this be done without copying elements?
                return groups.ToList();
            }
        }

        public RoomieCommand GetCommandFromFullName(string fullName)
        {
            return commands[fullName];
        }

        public bool ContainsCommandFullName(string fullName)
        {
            return commands.ContainsKey(fullName);
        }

        public RoomieCommand GetCommandFromType(Type type)
        {
            foreach (var command in this.commands.Values)
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
            return commands.Values.GetEnumerator();
        }
        System.Collections.Generic.IEnumerator<RoomieCommand> IEnumerable<RoomieCommand>.GetEnumerator()
        {
            return commands.Values.GetEnumerator();
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
