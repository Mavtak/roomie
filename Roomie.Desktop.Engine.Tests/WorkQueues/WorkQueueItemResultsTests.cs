using System;
using NUnit.Framework;
using Roomie.Desktop.Engine.WorkQueues;

namespace Roomie.Desktop.Engine.Tests.WorkQueues
{
    public class WorkQueueItemResultsTests
    {
        [TestCase(true, (uint)0, false)]
        [TestCase(true, (uint)1, false)]
        [TestCase(false, (uint)0, false)]
        [TestCase(false, (uint)1, true)]
        public void ShouldRetryReturnsTheCorrectValue(bool success, uint tries, bool expected)
        {
            var result = new WorkQueueItemResult(success, tries);

            Assert.That(result.ShouldRetry, Is.EqualTo(expected));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ItSavesSuccess(bool success)
        {
            var result = new WorkQueueItemResult(success, 0);

            Assert.That(result.Success, Is.EqualTo(success));
        }

        [TestCase((uint)0)]
        [TestCase((uint)1)]
        [TestCase((uint)2)]
        public void ItSavesTries(uint tries)
        {
            var result = new WorkQueueItemResult(true, tries);

            Assert.That(result.Tries, Is.EqualTo(tries));
        }
        [Test]
        public void ItSavesTheException()
        {
            var exception = new Exception();

            var result = new WorkQueueItemResult(false, 0, exception);

            Assert.That(result.Exception, Is.EqualTo(exception));
        }
    }
}
