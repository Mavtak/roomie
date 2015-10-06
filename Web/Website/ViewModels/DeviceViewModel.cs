using System.Web.Mvc;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.ViewModels
{
    public class DeviceViewModel
    {
        public WidgetData WidgetData { get; set; }
        public Device Device { get; set; }
        public bool DisplayEditor { get; set; }
        public bool DisplayButtons { get; set; }

        public DeviceViewModel(Device device, UrlHelper urlHelper)
        {
            Device = device;

            var location = (Device.Location == null) ? (null) : (Device.Location.Format());
            var status = WidgetData.ConnectedOrDisconnected(Device.IsAvailable);
            var target = urlHelper.Action(
                actionName: "Details",
                controllerName: "Device",
                routeValues: new
                {
                    id = Device.Id,
                    name = (Device.Name != null) ? (Device.Name.Replace(' ', '_')) : (null)
                }
            );

            WidgetData = new WidgetData
            {
                DebugText = Device.ToString(),
                DivId = Device.DivId,
                Location = location,
                Name = Device.Name,
                Status = status,
                Target = target,
                Type = "device"
            };

            DisplayEditor = false;
            DisplayButtons = true;
        }
    }
}