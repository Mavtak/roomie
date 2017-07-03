using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Roomie.Common.Tests
{
    public class CsvWriterTests
    {
        [TestCaseSource("WorksData")]
        public void Works(Item[] items, bool includeHeaders, string expected)
        {
            var converter = new ItemConverter();
            var writer = new CsvWriter<Item>(converter);
            var memoryStream = new MemoryStream();
            using (var stream = new StreamWriter(memoryStream))
            {

                writer.Write(stream, items, includeHeaders);
            }

            var actual = Encoding.Default.GetString(memoryStream.ToArray());

            Assert.That(actual, Is.EqualTo(expected));
        }

        public IEnumerable<TestCaseData> WorksData
        {
            get
            {
                const string newLine = "\r\n";

                yield return new TestCaseData(
                    new Item[0],
                    false,
                    ""
                    ).SetName("no items, no header");

                yield return new TestCaseData(
                    new[]
                        {
                            new Item
                                {
                                    Value1 = "a",
                                    Value2 = true
                                }
                        },
                    false,
                    "a,True" + newLine
                    ).SetName("one item, no header");

                yield return new TestCaseData(
                    new[]
                        {
                            new Item
                                {
                                    Value1 = "a",
                                    Value2 = true
                                }
                        },
                    true,
                    "Value 1,Value 2" + newLine + "a,True" + newLine
                    ).SetName("one item, with header");

                yield return new TestCaseData(
                    new Item[0],
                    true,
                    "Value 1,Value 2" + newLine
                    ).SetName("no items, with header");

                yield return new TestCaseData(
                    new[]
                        {
                            new Item
                                {
                                    Value1 = "a,",
                                    Value2 = true
                                }
                        },
                    false,
                    "\"a,\",True" + newLine
                    ).SetName("escaping the seprator with quotes");

                yield return new TestCaseData(
                    new[]
                        {
                            new Item
                                {
                                    Value1 = "a\"",
                                    Value2 = true
                                }
                        },
                    false,
                    "\"a\"\"\",True" + newLine
                    ).SetName("escaping the quotes with more quotes");

                yield return new TestCaseData(
                    new[]
                        {
                            new Item
                                {
                                    Value1 = "a",
                                    Value2 = true
                                },
                            new Item
                                {
                                    Value1 = "b",
                                    Value2 = false
                                }
                        },
                    false,
                    "a,True" + newLine + "b,False" + newLine
                    ).SetName("two items, no header");
            }
        }

        public class Item
        {
            public string Value1;
            public bool Value2;
        }

        public class ItemConverter : KeyValuePairConverter<Item>
        {
            public ItemConverter()
                : base(Conversions())
            {
                
            }

            private static KeyValuePair<string, Func<Item, string>>[] Conversions()
            {
                var result =  new []
                    {
                        Conversion("Value 1", x => x.Value1),
                        Conversion("Value 2", x => x.Value2.ToString())
                    };

                return result;
            }
        }
    }
}
