﻿using Roomie.Web.Persistence.Models;

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
            var user = context.User;
            var computer = context.Computer;

            var computerRepository = context.RepositoryFactory.GetComputerRepository();
            var scriptRepository = context.RepositoryFactory.GetScriptRepository();
            var taskRepository = context.RepositoryFactory.GetTaskRepository();

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

            var targetComputer = computerRepository.Get(user, request.Values["TargetComputerName"]);

            if(targetComputer == null)
            {
                response.ErrorMessage = "Could not find computer \"" + request.Values["TargetComputerName"] + "\".";
                return;
            }

            var script = Script.Create(false, request.Values["ScriptText"]);
            scriptRepository.Add(script);

            var task = Task.Create(user, "WebHook, " + computer.Name, targetComputer, script);

            taskRepository.Add(task);

            response.Values.Add("Response", "Script queued for delivery to " + targetComputer.Name);

        }
    }
}
