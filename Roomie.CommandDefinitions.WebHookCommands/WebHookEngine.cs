using System;
using System.Collections.Generic;
using Roomie.Common.Api.Client;
using Roomie.Common.Api.Models;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.WebHookCommands
{
    public class WebHookEngine
    {
        private RoomieEngine roomieController;
        private ThreadPool threadPool;
        private System.Threading.Thread parallelThread;
        private string computerName;
        bool running;

        private IRoomieApiClient apiClient;

        public WebHookEngine(RoomieEngine roomieController, string computerName, string apiBaseUrl, string accessKey, string encryptionKey)
        {
            this.roomieController = roomieController;
            this.computerName = computerName;
            string threadPoolName = "Web Hook (to " + apiBaseUrl + ")";

            this.threadPool = roomieController.CreateThreadPool(threadPoolName);

            this.running = false;

            apiClient = new RoomieApiClient(apiBaseUrl, accessKey);
        }

        public void RunAsync()
        {
            lock (this)
            {
                if (parallelThread == null)
                {
                    parallelThread = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
                }

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

            while (true)
            {
                try
                {
                    var response = Send<Task[]>("task", new Request
                    {
                        Action = "GetForComputer",
                        Parameters = new Dictionary<string, object>
                        {
                            { "computerName", computerName }
                        },
                    });

                    if (response.Data.Length == 0)
                    {
                        print("no tasks");
                    }
                    else
                    {
                        foreach (var task in response.Data)
                        {
                            var commands = ScriptCommandList.FromText(task.Script.Text);
                            AddCommands(commands);
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

        public Response<TData> Send<TData>(string repository, Request request)
            where TData : class
        {
            try
            {
                var response = apiClient.Send<TData>(repository, request).Result;

                if (response == null)
                {
                    throw new RoomieRuntimeException("null response from API");
                }

                if (response.Error != null)
                {
                    throw new RoomieRuntimeException($"error from API: {response.Error.Message}.  error tags: {string.Join(",", response.Error.Types)}");
                }

                return response;
            }
            catch (Exception exception)
            {
                //TODO:
                if (exception.Message.Equals("Thread was being aborted.", StringComparison.InvariantCultureIgnoreCase))
                    throw;

                throw new RoomieRuntimeException("REALLY unexpected error transmitting: " + exception.ToString(), exception);
            }
        }

        private void print(string text)
        {
            threadPool.Print(text);
        }

        #region Add Commands
        internal void AddCommand(IScriptCommand command)
        {
            threadPool.AddCommands(command);
        }
        internal void AddCommands(IEnumerable<IScriptCommand> commands)
        {
            threadPool.AddCommands(commands);
        }
        #endregion


        public void Stop()
        {
            running = false;
            threadPool.ShutDown();
            parallelThread.Abort();
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
