using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Roomie.Common.HomeAutomation.Events;
using Moq;

namespace Roomie.Common.HomeAutomation.Tests.Events
{
    public class EventHistoryTests
    {
        private History<IEvent> _history;

        [SetUp]
        public void SetUp()
        {
            _history = new History<IEvent>();
        }

        [Test]
        public void TheyStayInOrder()
        {
            var events = Utilities.GenerateEvents<IEvent>().ToList();

            events.ForEach(e => _history.Add(e));

            CollectionAssert.AreEqual(events, _history);
        }
    }
}
