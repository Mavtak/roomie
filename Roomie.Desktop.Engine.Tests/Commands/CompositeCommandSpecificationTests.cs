using System;
using NUnit.Framework;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.Desktop.Engine.Tests.Commands
{
    public class CompositeCommandSpecificationTests
    {
        [TestCase("a", "b", "a")]
        [TestCase("a", null, "a")]
        [TestCase(null, "b", "b")]
        [TestCase(null, null, null)]
        public void NameWorks(string first, string second, string expected)
        {
            var specification = new CompositeCommandSpecification(
                new ReadOnlyCommandSpecification(name: first),
                new ReadOnlyCommandSpecification(name: second));

            Assert.That(specification.Name, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "a")]
        [TestCase("a", null, "a")]
        [TestCase(null, "b", "b")]
        [TestCase(null, null, null)]
        public void GroupWorks(string first, string second, string expected)
        {
            var specification = new CompositeCommandSpecification(
                new ReadOnlyCommandSpecification(group: first),
                new ReadOnlyCommandSpecification(group: second));

            Assert.That(specification.Group, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "a")]
        [TestCase("a", null, "a")]
        [TestCase(null, "b", "b")]
        [TestCase(null, null, null)]
        public void DescriptionWorks(string first, string second, string expected)
        {
            var specification = new CompositeCommandSpecification(
                new ReadOnlyCommandSpecification(description: first),
                new ReadOnlyCommandSpecification(description: second));

            Assert.That(specification.Description, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "a")]
        [TestCase("a", null, "a")]
        [TestCase(null, "b", "b")]
        [TestCase(null, null, null)]
        public void SourceWorks(string first, string second, string expected)
        {
            var specification = new CompositeCommandSpecification(
                new ReadOnlyCommandSpecification(source: first),
                new ReadOnlyCommandSpecification(source: second));

            Assert.That(specification.Source, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "a")]
        [TestCase("a", null, "a")]
        [TestCase(null, "b", "b")]
        [TestCase(null, null, null)]
        public void ExtensionNameWorks(string first, string second, string expected)
        {
            var specification = new CompositeCommandSpecification(
                new ReadOnlyCommandSpecification(extensionName: first),
                new ReadOnlyCommandSpecification(extensionName: second));

            Assert.That(specification.ExtensionName, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "a")]
        [TestCase("a", null, "a")]
        [TestCase(null, "b", "b")]
        [TestCase(null, null, null)]
        public void ExtensionVersionWorks(string first, string second, string expected)
        {
            var specification = new CompositeCommandSpecification(
                new ReadOnlyCommandSpecification(extensionVersion: GetVersion(first)),
                new ReadOnlyCommandSpecification(extensionVersion: GetVersion(second)));

            Assert.That(specification.ExtensionVersion, Is.EqualTo(GetVersion(expected)));
        }

        private Version GetVersion(string token)
        {
            switch (token)
            {
                case "a":
                    return new Version(1, 0, 0, 0);

                case "b":
                    return new Version(2, 0, 0, 0);

                case null:
                    return null;

                default:
                    throw new ArgumentException();
            }
        }

        [TestCase("a", "b", "a")]
        [TestCase("a", null, "a")]
        [TestCase(null, "b", "b")]
        [TestCase(null, null, null)]
        public void ArgumentsWorks(string first, string second, string expected)
        {
            var specification = new CompositeCommandSpecification(
                new ReadOnlyCommandSpecification(arguments: GetArguments(first)),
                new ReadOnlyCommandSpecification(arguments: GetArguments(second)));

            Assert.That(specification.Arguments, Is.EqualTo(GetArguments(expected)));
        }


        [SetUp]
        public void SetUpArguments()
        {
            _argumentsA = new[] {new RoomieCommandArgument("a", null)};
            _argumentsB = new[] {new RoomieCommandArgument("b", null)};
        }

        private RoomieCommandArgument[] _argumentsA;
        private RoomieCommandArgument[] _argumentsB;
        private RoomieCommandArgument[] GetArguments(string token)
        {
            switch (token)
            {
                case "a":
                    return _argumentsA;

                case "b":
                    return _argumentsB;

                case null:
                    return null;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
