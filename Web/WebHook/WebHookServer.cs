using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using WebCommunicator;
using WebCommunicator.EventArgs;

namespace Roomie.Web.WebHook
{
    internal class WebHookServer
    {
        private AspNetCommunicatorServer<WebHookContext> server;

        private Dictionary<string, ActionHandler> actionHandlers;

        public WebHookServer()
        {
            server = new AspNetCommunicatorServer<WebHookContext>();

            server.GetCredentials += new CommunicatorServer<WebHookContext>.GetCredentialsEventHandler(server_GetCredentials);
            server.ValidateSession += new CommunicatorServer<WebHookContext>.ValidateSessionEventHandler(server_ValidateSession);
            server.CreateNewSession += new CommunicatorServer<WebHookContext>.CreateNewSessionEventHandler(server_CreateNewSession);
            server.ProcessMessage += new CommunicatorServer<WebHookContext>.ProcessMessageEventHandler(server_ProcessMessage);


            actionHandlers = new Dictionary<string, ActionHandler>();

            Assembly assembly = Assembly.GetAssembly(typeof(ActionHandler));
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(ActionHandler)))
                {
                    object handler = type.GetConstructor(new Type[0]).Invoke(new object[0]);
                    addActionHandler((ActionHandler)handler);
                }
            }

        }

        private void addActionHandler(ActionHandler handler)
        {
            actionHandlers.Add(handler.Name, handler);
        }

        public void ProcessRequest(HttpContext httpContext)
        {
            try
            {
                server.ProcessRequest(httpContext);
            }
            catch (Exception exception)
            {
                server.RespondWithScreamingError(httpContext, "Exception caught at the last minute: \r\n" + exception.Message + "\r\n" + exception.StackTrace);
            }
        }

        public void RespondWithScreamingError(HttpContext httpContext, string message)
        {
            server.RespondWithScreamingError(httpContext, message);
        }

        private void server_GetCredentials(object sender, GetCredentialsEventArgs<WebHookContext> eventArgs)
        {
            if (String.IsNullOrEmpty(eventArgs.AccessKey))
            {
                eventArgs.ErrorMessage = "Access Key not set.";
                return;
            }
         
            var context = eventArgs.Context;

            if (context == null)
            {
                throw new Exception("Context not set.");
            }

            context.Database = new RoomieDatabaseContext();

            //TODO: remove hack
            var usersHack = context.Database.Users.ToList();
            var locationsHack = context.Database.DeviceLocations.ToList();

            context.Computer = context.Database.Computers.FirstOrDefault(c => c.AccessKey == eventArgs.AccessKey);
            if (context.Computer == null)
            {
                eventArgs.ErrorMessage = "Computer not found.";
                eventArgs.UserFound = false;
                return;
            }

            if (context.User == null)
            {
                eventArgs.ErrorMessage = "User not found";
                eventArgs.UserFound = false;
                return;
            }

            eventArgs.EncryptionKey = System.Text.Encoding.UTF8.GetBytes(context.Computer.EncryptionKey);
            eventArgs.UserFound = true;
        }

        private void server_ValidateSession(object sender, ValidateSessionEventArgs<WebHookContext> eventArgs)
        {
            var context = eventArgs.Context;

            if (context == null)
            {
                throw new Exception("WebTransmission Context is null.");
            }

            var session = context.Database.WebHookSessions.FirstOrDefault(s => s.Token == eventArgs.SessionKey);
            if (session == null)
            {
                //session does not exist.
                eventArgs.IsValid = false;
                return;
            }

            //TODO: IP address checking?

            if (session.Computer.Equals(context.Computer))
            {
                context.Session = session; //TODO: why does WebCommunicator need to know the SessionRecord?
                eventArgs.IsValid = true;
                return;
            }

            eventArgs.IsValid = false;
        }

        private void server_CreateNewSession(object sender, CreateNewSessionEventArgs<WebHookContext> eventArgs)
        {
            //at this point the user is authenticated.

            var context = eventArgs.Context;

            if (context == null)
            {
                eventArgs.ErrorMessage = "UserRecord is null.";
                eventArgs.SessionCreated = false;
                return;
            }
            if (context.Computer == null)
            {
                eventArgs.ErrorMessage = "Computer is null.";
                eventArgs.SessionCreated = false;
                return;
            }

            var session = new WebHookSessionModel
            {
                Computer = context.Computer,
                LastPing = DateTime.UtcNow,
                Token = Guid.NewGuid().ToString() //TODO: shorten
            };
            context.Database.WebHookSessions.Add(session);
            context.Database.SaveChanges();

            eventArgs.Context.Session = session;
            eventArgs.NewSessionKey = session.Token; //TODO: is this necessary?
            eventArgs.SessionCreated = true;
        }

        private void server_ProcessMessage(object sender, ProcessMessageEventArgs<WebHookContext> eventArgs)
        {
            var context = eventArgs.Context;
            var request = context.Request;
            var response = context.Response;

            if (eventArgs.TransitTime == null)
            {
                throw new WebCommunicator.Exceptions.CommunicationException("Transit Time not set.");
            }
            if (eventArgs.TransitTime.Value.TotalMinutes > 5)
            {
                throw new WebCommunicator.Exceptions.CommunicationException("Transit Time too long. (max time is 5 minutes, transit time for this message was " + eventArgs.TransitTime + ")");
            }
            
            if (!request.Values.ContainsKey("Action"))
            {
                throw new WebCommunicator.Exceptions.CommunicationException("Action not set."); 
            }
            
            string action = request.Values["Action"];

            if (!actionHandlers.ContainsKey(action))
            {
                throw new WebCommunicator.Exceptions.CommunicationException("I don't know how to handle that action. (" + action + ")");
            }


            if (context.Computer == null)
            {
                response.ErrorMessage = "computer is null. (14729)";
                return;
            }

            if (context.User == null)
            {
                response.ErrorMessage = "user is null. (85621)";
                return;
            }

            actionHandlers[action].ProcessMessage(context);

            context.Database.Dispose();
        }
    }
}