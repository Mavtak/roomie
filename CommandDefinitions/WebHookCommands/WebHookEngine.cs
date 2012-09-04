using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using Roomie.Common.ScriptingLanguage;
using WebCommunicator;
using WebCommunicator.Exceptions;
using Roomie.Common.Exceptions;

namespace Roomie.CommandDefinitions.WebHookCommands
{
    public class WebHookEngine
    {
        private RoomieEngine roomieController;
        private ThreadPool threadPool;
        private System.Threading.Thread parallelThread;
        private string computerName;
        private string communicationUrl;
        private string accessKey;
        private string encryptionKey;
        bool running;

        private Queue<OutputEvent> events;

        private WebCommunicator.CommunicatorClient communicator;

        public WebHookEngine(RoomieEngine roomieController, string computerName, string communicationUrl, string accessKey, string encryptionKey)
        {
            this.roomieController = roomieController;
            this.computerName = computerName;
            this.communicationUrl = communicationUrl;
            this.accessKey = accessKey;
            this.encryptionKey = encryptionKey;

            //TODO: reintroduce multiple webhook engines?
            string threadPoolName = "Web Hook (to " + communicationUrl + ")";
            //int engineCount = Common.GetWebHookEngines(this.roomieController).Count;
            //if (engineCount > 0)
            //    threadPoolName += "(" + engineCount + ")";

            this.threadPool = new ThreadPool(this.roomieController, threadPoolName);

            this.running = false;
            communicator = new WebCommunicator.CommunicatorClient(communicationUrl, accessKey, encryptionKey);


            bool serverFound = false;

            while (!serverFound)
            {
                try
                {
                    print("pinging Webhook server at " + communicationUrl + "...");
                    Message pingResponse = communicator.PingServer();
                    //TODO: should we look at the result?
                    print("Webhook server found!");
                    serverFound = true;

                }
                catch (CommunicationException exception)
                {
                    print("Error contacting server: " + exception.Message);
                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, 10));
                }
            }
            ////initialize connection
            //Dictionary<string, string> sendValues = new Dictionary<string, string>(1);
            //sendValues.Add("action", "start session");
            //sendValues.Add("ComputerName", computerName);
            //SecureHttpCommunication.RecievedPackage package = SendMessage(sendValues, null);
            //if (package.HasErrorMessage)
            //    throw new ScriptException("Error starting webhook session");
            //foreach (SecureHttpCommunication.Message message in package)
            //{
            //    if (message.ContainsParameter("NewSessionToken"))
            //        sessionToken = message.GetValue("NewSessionToken");
            //    break;
            //}
            //if (String.IsNullOrEmpty(sessionToken))
            //    throw new ScriptException("Did not recieve a new SessionID");

            //print("Webhook Session Token: " + sessionToken);

            events = new Queue<OutputEvent>();
            //TODO: make this work!
            //this.roomieController.ScriptMessageSent += new Scope scopeScriptMessageEventHandler(roomieController_ScriptMessageSent);
        }

        public void RunAsync()
        {
            if (parallelThread == null)
                parallelThread = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
            lock (parallelThread)
            {
                parallelThread.Start();
            }
        }

        private void Run()
        {
            running = true;
            print("Webhook reading tasks...");


            foreach (RoomieCommand command in roomieController.CommandLibrary)
            {
                if (command.Name.Equals("WebHookConnectTasks"))
                {
                    AddCommand(command.BlankCommandCall());
                }
            }

            //create an outgoing message to request tasks.  This message object will be reused for every transmission.
            Message outMessage = new Message();
            outMessage.Values.Add("Action", "GetTasks");

            while (true)
            {
                try
                {
                    Message response = SendMessage(outMessage);

                    //TODO: use LINQ?
                    foreach (var payloadNode in response.Payload)
                    {
                        if (payloadNode.Name.LocalName.Equals("Script"))
                        {
                            if (payloadNode.Attribute("Text") == null)
                            {
                                throw new RoomieRuntimeException("'Text' attribute not specified for WebHook script response.");
                            }

                            var commands = ScriptCommandList.FromText(payloadNode.Attribute("Text").Value);
                            AddCommands(commands);
                        }
                        else
                        {
                            throw new RoomieRuntimeException("Received unexpected data while gettings tasks.  Node named \"" + payloadNode.Name.LocalName + "\"");
                        }
                        
                    }
                }
                catch (RoomieRuntimeException exception)
                {
                    print(exception.Message);
                    print("sleeping for 10 seconds because of error...");
                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, 10));
                }
            }
        }

        private void SendMessageAsync(Dictionary<string, string> values)
        {
            //TODO: rewrite this using the data structures in Roomie.Common.ScriptingLanguage
            StringBuilder builder = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(builder, settings);

            writer.WriteStartElement("WebHook.SendMessage");
            foreach (string key in values.Keys)
                writer.WriteAttributeString(key, values[key]);
            writer.WriteEndElement();

            writer.Close();

            var commands = ScriptCommandList.FromText(builder.ToString());
            AddCommands(commands);
        }
        
        public WebCommunicator.Message SendMessage(WebCommunicator.Message outMessage)
        {
            try
            {
                WebCommunicator.Message response = communicator.SendMessage(outMessage, true);

                if (response == null)
                    throw new RoomieRuntimeException("NULL response!");

                if (!String.IsNullOrEmpty(response.ErrorMessage))
                    throw new RoomieRuntimeException("Server returned error: " + response.ErrorMessage);

                if (response.Values.ContainsKey("Response"))
                    print("Server response text: " + response.Values["Response"]);
                else
                    print("No server response text.");

                return response;
            }
            catch (CommunicationException exception)
            {
                throw new RoomieRuntimeException("WebHook Error: " + exception.Message, exception);
            }
            catch (RoomieRuntimeException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                //TODO:
                if (exception.Message.Equals("Thread was being aborted.", StringComparison.InvariantCultureIgnoreCase))
                    throw exception;

                throw new RoomieRuntimeException("REALLY unexpected error transmitting: " + exception.ToString(), exception);
            }
            
        }

        private void print(string text)
        {
            threadPool.Print(text);
        }

        #region Add Commands
        internal void AddCommand(ScriptCommand command)
        {
            threadPool.AddCommand(command);
        }
        internal void AddCommands(IEnumerable<ScriptCommand> commands)
        {
            threadPool.AddCommands(commands);
        }
        #endregion


        public void Stop()
        {
            parallelThread.Abort();
            threadPool.ShutDown();
            running = false;
        }

        public bool Running
        {
            get
            {
                return running;
            }
        }

        public string ComputerName
        {
            get
            {
                return computerName;
            }
        }
    }
}
