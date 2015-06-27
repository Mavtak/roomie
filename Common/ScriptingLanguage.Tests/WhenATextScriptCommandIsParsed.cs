using NUnit.Framework;

namespace Roomie.Common.ScriptingLanguage.Tests
{
    [TestFixture]
    public class WhenATextScriptCommandIsParsed
    {
        public TestDataFormat[] TestData = new []
        {
            new TestDataFormat
                {
                    Text = "Command.Derp",
                    CommandName = "Command.Derp"
                },

            new TestDataFormat
                {
                    Text = "Command.Derp Parameter=\"Value\"",
                    CommandName = "Command.Derp",
                    ParameterData = new[]
                        {
                            new TestDataFormat.ParameterDataFormat
                                {
                                    Name = "Parameter",
                                    Value = "Value"
                                }
                        }
                },

            new TestDataFormat
                {
                    Text = "Command.Derp Parameter=\"Value\" Parameter2=\"Value2\"",
                    CommandName = "Command.Derp",
                    ParameterData = new[]
                        {
                            new TestDataFormat.ParameterDataFormat
                                {
                                    Name = "Parameter",
                                    Value = "Value"
                                },
                            new TestDataFormat.ParameterDataFormat
                                {
                                    Name = "Parameter2",
                                    Value = "Value2"
                                }
                        }
                },

            new TestDataFormat
                {
                    Text = "Command.Derp Parameter=\"Value\"Parameter2=\"Value2\"",
                    CommandName = "Command.Derp",
                    ParameterData = new[]
                        {
                            new TestDataFormat.ParameterDataFormat
                                {
                                    Name = "Parameter",
                                    Value = "Value"
                                },
                            new TestDataFormat.ParameterDataFormat
                                {
                                    Name = "Parameter2",
                                    Value = "Value2"
                                }
                        }
                }
        };

        [TestCaseSource("TestData")]
        public void ItIsParsedCorrectly(TestDataFormat testData)
        {
            var command = new TextScriptCommand(testData.Text);

            Assert.That(command.FullName, Is.EqualTo(testData.CommandName));


            if (testData.ParameterData == null)
            {
                testData.ParameterData = new TestDataFormat.ParameterDataFormat[]{};
            }

            Assert.That(command.Parameters.Count, Is.EqualTo(testData.ParameterData.Length));

            foreach (var parameter in testData.ParameterData)
            {
                Assert.That(command.Parameters.ContainsParameterName(parameter.Name));
                Assert.That(command.Parameters[parameter.Name].Value, Is.EqualTo(parameter.Value));
            }
        }

        public class TestDataFormat
        {
            public string Text { get; set; }
            public string CommandName { get; set; }
            public ParameterDataFormat[] ParameterData { get; set; }

            public class ParameterDataFormat
            {
                public string Name { get; set; }
                public string Value { get; set; }
            }
        }
    }
}
