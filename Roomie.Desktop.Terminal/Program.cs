using System;
using System.Xml;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;

namespace Roomie.Desktop.Terminal
{
    class Program
    {
        static void setOutput(RoomieEngine engine)
        {
            var streamWriter = new RoomieEventTextStream(engine, Console.Out, new TimeSpan(hours: 0, minutes: 0, seconds: 30));
        }
        static void Main(string[] args)
        {
            var engine = new RoomieEngine();
            setOutput(engine);
            //engine.TerminalMessageSent += new TerminalOutputEventHandler(controller_TerminalMessageSent);
            engine.Start();

            var line = "";
            var input = "";
            var xmlInput = new XmlDocument();
            //TODO: improve this.  Make the while loop directly depend on the engine's internal state.
            while (!input.Contains("Core.ShutDown"))
            {
                //TODO: pull reading from a text stream into Roomie.Desktop.Engine.RoomieEngine
                line = Console.ReadLine().Trim();
                if (!String.IsNullOrEmpty(line))
                {
                    if (input.Length > 0)
                    {
                        input += "\n";
                    }
                    input += line.TrimEnd('_');
                    //TODO: read multiple lines
                    if (!line.EndsWith("_"))
                    {
                        try
                        {
                            var script = ScriptCommandList.FromText(input);
                            engine.Threads.AddCommands(script);
                            Console.WriteLine("Command accepted.");
                            input = "";
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Error. " + exception.Message);
                        }
                    }

                }
                else
                {
                    if (input != "")
                    {
                        input = "";
                        Console.WriteLine("Input cleared.");
                    }
                    
                }
            }
            
        }
    }
}
