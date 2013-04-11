using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.Common.HomeAutomation.Tests.Events
{
    [TestFixture]
    public class MasterEventHistoryTests
    {
        private MasterHistory _history;
        private Mock<IDeviceHistory> _deviceHistoryMock;
        private Mock<INetworkHistory> _networkHistoryMock;
        private List<IDeviceEvent> _deviceEvents;
        private List<INetworkEvent> _networkEvents;

        [SetUp]
        public void SetUp()
        {
            PopulateIndividualEventLists();

            _deviceHistoryMock = new Mock<IDeviceHistory>();
            _deviceHistoryMock.Setup(x => x.GetEnumerator()).Returns(_deviceEvents.GetEnumerator());

            _networkHistoryMock = new Mock<INetworkHistory>();
            _networkHistoryMock.Setup(x => x.GetEnumerator()).Returns(_networkEvents.GetEnumerator());

            _history = new MasterHistory(_deviceHistoryMock.Object, _networkHistoryMock.Object);
        }

        [Test]
        public void MergedListIsInTheRightOrder()
        {
            var events = _history.ToList();

            for (var i = 1; i < events.Count; i++)
            {
                var previousEvent = events[i - 1];
                var thisEvent = events[i];

                Assert.That(thisEvent.TimeStamp, Is.GreaterThanOrEqualTo(previousEvent.TimeStamp), "The events were not sorted by timestamp.");
            }
        }

        [Test]
        public void MergedListIsTheRightLength()
        {
            //TODO: MergedListHasAllTheRightObjects
            var deviceEventCount = _deviceEvents.Count();
            var networkEventCount = _networkEvents.Count();
            var totalCount = _history.Count();

            Assert.That(totalCount, Is.EqualTo(deviceEventCount + networkEventCount));
        }

        [Test]
        public void AddingADeviceEventWorks()
        {
            var eventMock = new Mock<IDeviceEvent>();
            _history.Add(eventMock.Object);

            _deviceHistoryMock.Verify(x => x.Add(eventMock.Object));
        }

        [Test]
        public void AddingANetworkEventWorks()
        {
            var eventMock = new Mock<INetworkEvent>();
            _history.Add(eventMock.Object);

            _networkHistoryMock.Verify(x => x.Add(eventMock.Object));
        }

        [Test]
        [ExpectedException]
        public void AddingAnUnknownEventFails()
        {
            var eventMock = new Mock<IEvent>();
            _history.Add(eventMock.Object);
        }

        private void PopulateIndividualEventLists()
        {
            _deviceEvents = new List<IDeviceEvent>();
            _networkEvents = new List<INetworkEvent>();
            
            foreach (var timestamp in Utilities.GenerateTimeStamps())
            {
                switch (Utilities.Random.Next(0, 2))
                {
                    case 0:
                        var deviceEventMock = new Mock<IDeviceEvent>();
                        deviceEventMock.SetupGet(e => e.TimeStamp).Returns(timestamp);
                        _deviceEvents.Add(deviceEventMock.Object);
                        break;

                    case 1:
                        var networkEventMock = new Mock<INetworkEvent>();
                        networkEventMock.SetupGet(e => e.TimeStamp).Returns(timestamp);
                        _networkEvents.Add(networkEventMock.Object);
                        break;

                    default:
                        throw new Exception();
                }
            }
        }
    }
}
