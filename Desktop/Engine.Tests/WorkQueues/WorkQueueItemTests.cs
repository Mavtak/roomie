using System;
using System.IO;
using NUnit.Framework;
using Roomie.Desktop.Engine.WorkQueues;

namespace Roomie.Desktop.Engine.Tests.WorkQueues
{
    public class WorkQueueItemTests
    {
        [Test]
        public void DoWorkDoesTheWork()
        {
            var actionCompleted = false;

            var work = new WorkQueueItem(() => actionCompleted = true, 1);

            var result = work.DoWork();

            Assert.That(actionCompleted, Is.True);
            Assert.That(result.Success, Is.True);
            Assert.That(result.ShouldRetry, Is.False);
        }

        [Test]
        public void DoWorkCatchesExceptions()
        {
            var exception = new Exception();

            var work = new WorkQueueItem(() =>
                {
                    throw exception;
                }, 1);

            var result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.False);
            Assert.That(result.Exception, Is.EqualTo(exception));
        }

        [Test]
        public void DoWorkWillRetryForRetryExceptions()
        {
            var work = new WorkQueueItem(() =>
            {
                throw new ArgumentException();
            }, 2, new[] { typeof(ArgumentException) });

            var result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.True);
        }

        [Test]
        public void DoWorkWillNotRetryForRetryExceptions()
        {
            var work = new WorkQueueItem(() =>
            {
                throw new FileLoadException();
            }, 2, new[] { typeof(ArgumentException) });

            var result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.False);
        }

        [Test]
        public void DoWorkWillRetryForChildrenOfRetryExceptions()
        {
            var work = new WorkQueueItem(() =>
            {
                throw new ArgumentNullException();
            }, 2, new[] { typeof(ArgumentException) });

            var result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.True);
        }

        [Test]
        public void TriesAreDecremented()
        {
            var work = new WorkQueueItem(() =>
            {
                throw new ArgumentException();
            }, 3, new[] { typeof(ArgumentException) });

            var result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.True);
            Assert.That(result.Tries, Is.EqualTo(2));

            result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.True);
            Assert.That(result.Tries, Is.EqualTo(1));

            result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.False);
            Assert.That(result.Tries, Is.EqualTo(0));
        }

        [Test]
        public void ResultIsSetToTheLastResult()
        {
            var called = false;

            var work = new WorkQueueItem(() =>
                {
                    throw new ArgumentException();
                }, 2, new[] {typeof (ArgumentException)});

            var result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.True);

            result = work.DoWork();

            Assert.That(result.Success, Is.False);
            Assert.That(result.ShouldRetry, Is.False);
            Assert.That(work.Result, Is.EqualTo(result));
        }

        [Test]
        public void ResultCanBeCalledMultipleTimes()
        {
            var work = new WorkQueueItem(() => { }, 1);

            work.DoWork();

            var result1 = work.Result;
            var result2 = work.Result;

            Assert.That(result1, Is.EqualTo(result2));
        }
    }
}
