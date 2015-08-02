using System;
using System.Xml.Linq;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.WebHook.ActionHandlers
{
    internal class GetTasks : ActionHandler
    {
        public GetTasks()
        { }

        public override void ProcessMessage(WebHookContext context)
        {
            var response = context.Response;
            var computer = context.Computer;
            var database = context.Database;

            //TODO: how big can I make this?
            // well, not as big as three minutes
            DateTime endTime = DateTime.Now.AddSeconds(90);

            Task[] tasks = null;

            //tasks = new List<TaskModel>
            //{
            //    new TaskModel
            //    {
            //        Script = new ScriptModel
            //        {
            //            Text = @"<Output.Speak Text=""Hello there!"" />"
            //        }
            //    }
            //};

            while ((tasks == null || tasks.Length == 0) && DateTime.Now <= endTime)
            {
                computer.UpdatePing();
                database.SaveChanges();

                var now = DateTime.UtcNow;
                try
                {
                    tasks = database.Tasks.ForComputer(computer, now);
                    if (tasks.Length == 0)
                    {
                        System.Threading.Thread.Sleep(250);
                    }
                }
                catch (Exception exception)
                {
                    response.ErrorMessage = "Exception while getting tasks: " + exception;
                    return;
                }
            }


            //tasks have been found or the timer has run out


            if (tasks == null || tasks.Length == 0)
            {
                response.Values.Add("Response", "no tasks.");
            }
            else
            {
                //add tasks to the response message and mark the tasks as sent
                foreach (var task in tasks)
                {
                    try
                    {
                        response.Payload.Add(
                            new XElement("Script",
                                new XAttribute("Text", task.Script.Text)
                            ));
                        task.MarkAsReceived();
                        database.Tasks.Update(task);
                    }
                    catch
                    { }
                }

                response.Values.Add("Response", "added " + tasks.Length + " task(s)");
            }

            database.SaveChanges();
        }

    }
}