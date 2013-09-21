//using Roomie.Common.HomeAutomation;

//namespace Roomie.CommandDefinitions.HomeAutomationCommands
//{
//    //TODO: replace this godawful class
//    public class DeviceCollection : BaseDeviceCollection
//    {

//        public DeviceCollection(Network network)
//            : base(network)
//        { }

//        public override void Add(IDevice device)
//        {
//            lock (this)
//            {
//                base.Add(device);

//                DeviceAdded((Device)device);
//            }
//        }

//        //optionally override to include more computations on add
//        protected virtual void DeviceAdded(Device device)
//        { }

//        public void Remove(Device device)
//        {
//            lock (this)
//            {
//                devices.Remove(device);

//                DeviceRemoved(device);
//            }
//        }

//        //optionally override to include more computations on remove
//        protected virtual void DeviceRemoved(Device device)
//        { }

//    }
//}
