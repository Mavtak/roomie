using System.Linq;
using NUnit.Framework;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.Desktop.Engine.Tests.Commands
{
    public class AttributeBasedCommandSpecificationTests
    {
        [Test]
        public void NameWorks()
        {
            var specification = new AttributeBasedCommandSpecification(typeof(AttributeTestCommand));

            Assert.That(specification.Name, Is.Null);
        }

        [Test]
        public void GroupWorks()
        {
            var specification = new AttributeBasedCommandSpecification(typeof(AttributeTestCommand));

            Assert.That(specification.Group, Is.EqualTo("TestCommands"));
        }

        [Test]
        public void DescriptionWorks()
        {
            var specification = new AttributeBasedCommandSpecification(typeof(AttributeTestCommand));

            Assert.That(specification.Description, Is.EqualTo("A command"));
        }

        [Test]
        public void SourceWorks()
        {
            var specification = new AttributeBasedCommandSpecification(typeof(AttributeTestCommand));

            Assert.That(specification.Source, Is.Null);
        }

        [Test]
        public void ExtensionNameWorks()
        {
            var specification = new AttributeBasedCommandSpecification(typeof(AttributeTestCommand));

            Assert.That(specification.ExtensionName, Is.Null);
        }

        [Test]
        public void ExtensionVersionWorks()
        {
            var specification = new AttributeBasedCommandSpecification(typeof(AttributeTestCommand));

            Assert.That(specification.ExtensionVersion, Is.Null);
        }

        [Test]
        public void ArgumentsWorks()
        {
            var specification = new AttributeBasedCommandSpecification(typeof(AttributeTestCommand));

            var actual = specification.Arguments.ToArray();
            actual = actual.OrderBy(x => x.Name).ToArray();

            Assert.That(actual.Length, Is.EqualTo(3));

            Assert.That(actual[0].Name, Is.EqualTo("Number"));
            Assert.That(actual[0].Type, Is.TypeOf<IntegerParameterType>());
            Assert.That(actual[0].HasDefault, Is.EqualTo(false));
            Assert.That(actual[0].DefaultValue, Is.EqualTo(null));

            Assert.That(actual[1].Name, Is.EqualTo("Text"));
            Assert.That(actual[1].Type, Is.TypeOf<StringParameterType>());
            Assert.That(actual[1].HasDefault, Is.EqualTo(false));
            Assert.That(actual[1].DefaultValue, Is.EqualTo(null));

            Assert.That(actual[2].Name, Is.EqualTo("TextWithDefault"));
            Assert.That(actual[2].Type, Is.TypeOf<StringParameterType>());
            Assert.That(actual[2].HasDefault, Is.EqualTo(true));
            Assert.That(actual[2].DefaultValue, Is.EqualTo("derp"));
        }
    }
}
