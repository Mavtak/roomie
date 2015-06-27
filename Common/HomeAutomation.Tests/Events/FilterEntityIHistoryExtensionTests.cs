using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.Common.HomeAutomation.Tests.Events
{
    public class FilterEntityIHistoryExtensionTests
    {
        private Mock<IHistory<IEvent>> _historyMock;
        private IHistory<IEvent> _history;
        private Dictionary<IDevice, int> _devices;
        private List<IEvent> _events;

        [SetUp]
        public void SetUp()
        {
            _devices = new Dictionary<IDevice, int>();

            for (var i = 0; i < 5; i++)
            {
                var device = NewDevice("Device " + i);

                _devices.Add(device, 0);
            }

            _events = Utilities.GenerateEvents<IEvent>(e =>
            {
                var device = Utilities.Random.RandomElement(_devices.Keys);
                e.SetupGet(x => x.Entity).Returns(device);
                _devices[device] = _devices[device] + 1;
            }).ToList();

            _historyMock = new Mock<IHistory<IEvent>>();
            _historyMock.Setup(x => x.GetEnumerator()).Returns(() => _events.GetEnumerator());
            _history = _historyMock.Object;
        }

        [Test]
        public void ForDeviceWorksWhenThereShouldBeResults()
        {
            var device = Utilities.Random.RandomElement(_devices.Keys);
            var results = _history.FilterEntity(device).ToList();

            foreach (var result in results)
            {
                Assert.That(result.Entity, Is.EqualTo(device));
            }

           Assert.That(results.Count(), Is.EqualTo(_devices[device]));
        }

        [Test]
        public void ForDeviceWorksWhenThereShouldBeNoResults()
        {
            var device = NewDevice("Not in history");
            var results = _history.FilterEntity(device);

            CollectionAssert.IsEmpty(results);
        }

        [Test]
        public void FilteringByANullDeviceIsOkay()
        {
            var results = _history.FilterEntity(null);

            CollectionAssert.IsEmpty(results);
        }

        [Test]
        public void NullDevicesAreSearchableToo()
        {
            var eventWithNullDeviceMock = new Mock<IEvent>();
            eventWithNullDeviceMock.SetupGet(x => x.Entity).Returns((IDevice)null);
           _events.Add(eventWithNullDeviceMock.Object);

            var results = _history.FilterEntity(null);

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private IDevice NewDevice(string name)
        {
            var device = new Mock<IDevice>();
            device.SetupGet(x => x.Name).Returns(name);

            return device.Object;
        }
    }
}
