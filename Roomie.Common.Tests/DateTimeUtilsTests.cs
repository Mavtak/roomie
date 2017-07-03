using NUnit.Framework;

namespace Roomie.Common.Tests
{
    //TODO: add more unit tests
    [TestFixture]
    public class DateTimeUtilsTests
    {
        public static string[] ValidInputs = new [] {"8am", "Monday at 12pm", "weekday at 5pm"};

        [TestCaseSource("ValidInputs")]
        public void ItAcceptsWellFormedInput(string input)
        {
            var result = TimeUtils.IsDateTime(input);

            Assert.That(result, Is.True);
        }

        [TestCaseSource("ValidInputs")]
        public void ItDoesNotThrowAnExceptionWhenParsingValidInputs(string input)
        {
            TimeUtils.ParseDateTime(input);
        }
    }
}
