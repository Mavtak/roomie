using System;
using NUnit.Framework;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Tests.Commands.TestCommands;

namespace Roomie.Desktop.Engine.Tests.Commands
{
    public class NamespaceBasedCommandSpecificationTests
    {
        [Test]
        public void NameWorks()
        {
            var specification = new NamespaceBasedCommandSpecification(typeof(NamespaceTestCommand));

            Assert.That(specification.Name, Is.EqualTo("NamespaceTestCommand"));
        }

        [Test]
        public void GroupWorks()
        {
            var specification = new NamespaceBasedCommandSpecification(typeof(NamespaceTestCommand));

            Assert.That(specification.Group, Is.EqualTo("TestCommands"));
        }

        [Test]
        public void DescriptionWorks()
        {
            var specification = new NamespaceBasedCommandSpecification(typeof(NamespaceTestCommand));

            Assert.That(specification.Description, Is.Null);
        }

        [Test]
        public void SourceWorks()
        {
            var specification = new NamespaceBasedCommandSpecification(typeof(NamespaceTestCommand));

            Assert.That(specification.Source, Is.StringEnding("Roomie.Desktop.Engine.Tests.DLL"));
            Assert.That(specification.Source, Is.StringContaining(":"));
        }

        [Test]
        public void ExtensionNameWorks()
        {
            var specification = new NamespaceBasedCommandSpecification(typeof(NamespaceTestCommand));

            Assert.That(specification.ExtensionName, Is.EqualTo("Roomie.Desktop.Engine.Tests"));
        }

        [Test]
        public void ExtensionVersionWorks()
        {
            var specification = new NamespaceBasedCommandSpecification(typeof(NamespaceTestCommand));

            Assert.That(specification.ExtensionVersion, Is.EqualTo(new Version(1, 0, 0, 0)));
        }

        [Test]
        public void ArgumentsWorks()
        {
            var specification = new NamespaceBasedCommandSpecification(typeof(NamespaceTestCommand));

            Assert.That(specification.Arguments, Is.Null);
        }
    }
}
