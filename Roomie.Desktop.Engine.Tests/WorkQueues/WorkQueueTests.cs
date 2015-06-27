using System;
using NUnit.Framework;
using Roomie.Desktop.Engine.WorkQueues;

namespace Roomie.Desktop.Engine.Tests.WorkQueues
{
    public class WorkQueueTests
    {
        [Test]
        public void ItWaitsToDoWork()
        {
            var complete = false;
            var queue = new WorkQueue();

            queue.Add(new WorkQueueItem(() => complete = true, 1));

            Assert.That(complete, Is.False);
        }

        [Test]
        public void ItKnowsWhenItDoesNotHaveWork()
        {
            var queue = new WorkQueue();

            Assert.That(queue.HasWork, Is.False);
        }

        [Test]
        public void ItKnowsWhenItHasWork()
        {
            var complete = false;
            var queue = new WorkQueue();

            queue.Add(new WorkQueueItem(() => complete = true, 1));

            Assert.That(queue.HasWork, Is.True);
        }

        [Test]
        public void ItDoesWork()
        {
            var complete = false;
            var queue = new WorkQueue();

            queue.Add(new WorkQueueItem(() => complete = true, 1));

            queue.DoUnitOfWork();

            Assert.That(complete, Is.True);
        }

        [Test]
        public void ItDoesWorkInOrder()
        {
            var complete1 = false;
            var complete2 = false;
            var complete3 = false;
            var queue = new WorkQueue();

            queue.Add(new WorkQueueItem(() => complete1 = true, 1));
            queue.Add(new WorkQueueItem(() => complete2 = true, 1));
            queue.Add(new WorkQueueItem(() => complete3 = true, 1));

            queue.DoUnitOfWork();
            Assert.That(complete1, Is.True);
            Assert.That(complete2, Is.False);
            Assert.That(complete3, Is.False);

            queue.DoUnitOfWork();
            Assert.That(complete1, Is.True);
            Assert.That(complete2, Is.True);
            Assert.That(complete3, Is.False);

            queue.DoUnitOfWork();
            Assert.That(complete1, Is.True);
            Assert.That(complete2, Is.True);
            Assert.That(complete3, Is.True);
        }

        [Test]
        public void ItRetriesWork()
        {
            var complete = false;
            var queue = new WorkQueue();

            queue.Add(new WorkQueueItem(() =>
            {
                complete = true;
                throw new Exception();
            }, 2, new[] { typeof(Exception) }));

            queue.DoUnitOfWork();

            Assert.That(complete, Is.True);

            complete = false;
            queue.DoUnitOfWork();

            Assert.That(complete, Is.True);
        }

        [Test]
        public void ItGivesUpOnWork()
        {
            var queue = new WorkQueue();

            queue.Add(new WorkQueueItem(() =>
            {
                throw new Exception();
            }, 2));

            queue.DoUnitOfWork();

            Assert.That(queue.HasWork, Is.False);
        }
    }
}
