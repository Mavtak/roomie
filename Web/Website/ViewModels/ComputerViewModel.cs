using System.Web.Mvc;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.ViewModels
{
    public class ComputerViewModel
    {
        public WidgetData WidgetData { get; set; }
        public ComputerModel Computer { get; set; }
        public bool DisplayWebhookSettings { get; set; }

        public ComputerViewModel(ComputerModel computer, UrlHelper urlHelper)
        {
            Computer = computer;

            var status = WidgetData.ConnectedOrDisconnected(Computer.IsConnected);
            var target = urlHelper.Action(
                actionName: "Details",
                controllerName: "Computer",
                routeValues: new
                {
                    id = Computer.Id,
                    name = (Computer.Name != null) ? (Computer.Name.Replace(' ', '_')) : (null)
                }
            );
            WidgetData = new WidgetData
            {
                DebugText = Computer.ToString(),
                DivId = Computer.DivId,
                Name = Computer.Name,
                Status = status,
                Target = target,
                Type = "computer"
            };

            DisplayWebhookSettings = false;
        }
    }
}