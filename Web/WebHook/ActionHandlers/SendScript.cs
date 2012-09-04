﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Xml;
using System.Xml.Linq;

using WebCommunicator;
using Roomie.Web.Models;

namespace Roomie.Web.WebHook.ActionHandlers
{
    internal class SendScript : ActionHandler
    {
        public SendScript()
        { }

        public override void ProcessMessage(WebHookContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var database = context.Database;
            var user = context.User;
            var computer = context.Computer;

            if (!request.Values.ContainsKey("TargetComputerName"))
            {
                response.ErrorMessage = "Target Computer Name not set.";
                return;
            }

            if (!request.Values.ContainsKey("ScriptText"))
            {
                response.ErrorMessage = "ScriptText not set";
                return;
            }

            var targetComputer = user.Computers.FirstOrDefault(c => c.Name == request.Values["TargetComputerName"]);

            if(targetComputer == null)
            {
                response.ErrorMessage = "Could not find computer \"" + request.Values["TargetComputerName"] + "\".";
                return;
            }

            var task = new TaskModel
            {
                Owner = user,
                Origin = "WebHook, " + computer.Name,
                Target = targetComputer,
                Script = new ScriptModel
                {
                    Mutable = false,
                    Text = request.Values["ScriptText"]
                }
            };

            database.Tasks.Add(task);
            database.SaveChanges();

            response.Values.Add("Response", "Script queued for delivery to " + targetComputer.Name);

        }
    }
}
