using System.Web.Mvc;
using Roomie.Web.Models;

namespace Roomie.Web.ViewModels
{
    public class DeviceViewModel
    {
        public WidgetData WidgetData { get; set; }
        public DeviceModel Device { get; set; }
        public bool DisplayEditor { get; set; }
        public bool DisplayButtons { get; set; }

        public DeviceViewModel(DeviceModel device, UrlHelper urlHelper)
        {
            Device = device;

            var location = (Device.Location == null) ? (null) : (Device.Location.Name);
            var status = (Device.IsAvailable ? "connected" : "disconnected");
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