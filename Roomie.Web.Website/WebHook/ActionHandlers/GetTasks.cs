using System;
using System.Xml.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.WebHook.ActionHandlers
{
    internal class GetTasks : ActionHandler
    {
        public GetTasks()
        { }

        public override void ProcessMessage(WebHookContext context)
        {
            var response = context.Response;
            var computer = context.Computer;

            var computerRepository = context.RepositoryFactory.GetComputerRepository();
            var taskRepository = context.RepositoryFactory.GetTaskRepository();

            var getForComputer = new Controllers.Api.Task.Actions.GetForComputer(computerRepository, taskRepository);
            Task[] tasks;
            try
            {
                tasks = getForComputer.Run(computer, TimeSpan.FromSeconds(90), TimeSpan.FromSeconds(1 / 4));
            }
            catch (Exception exception)
            {
                response.ErrorMessage = "Exception while getting tasks: " + exception;
                return;
            }


            if (tasks == null || tasks.Length == 0)
            {
                response.Values.Add("Response", "no tasks.");
            }
            else
            {
                response.Values.Add("Response", "added " + tasks.Length + " task(s)");

                foreach (var task in tasks)
                {
                    try
                    {
                        response.Payload.Add(
                            new XElement("Script",
                                new XAttribute("Text", task.Script.Text)
                            ));
                    }
                    catch
                    { }
                }
            }
        }
    }
}