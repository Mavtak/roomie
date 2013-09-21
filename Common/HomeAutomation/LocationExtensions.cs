using System;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation
{
    public static class LocationExtensions
    {
        private static int Distance(this IEnumerable<string> sequence, string search)
        {
            var result = 0;

            foreach (var item in sequence)
            {
                result++;

                if (item == search)
                {
                    return result;
                }
            }

            return int.MaxValue;
        }

        public static int CalculateCloseness(this IEnumerable<string> sequenceA, IEnumerable<string> sequenceB, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            var score = 0;

            var a = new Queue<string>(sequenceA);
            var b = new Queue<string>(sequenceB);

            while (a.Any() && b.Any())
            {
                if (string.Equals(a.Peek(), b.Peek(), comparisonType))
                {
                    a.Dequeue();
                    b.Dequeue();
                }
                else
                {
                    var aDistance = a.Distance(b.Peek());
                    var bDistance = b.Distance(a.Peek());

                    if (aDistance == int.MaxValue && bDistance == int.MaxValue)
                    {
                        break;
                    }

                    if (aDistance < bDistance)
                    {
                        for (var i = 1; i < aDistance; i++)
                        {
                            a.Dequeue();
                            score++;
                        }
                    }
                    else
                    {
                        for (var i = 1; i < bDistance; i++)
                        {
                            b.Dequeue();
                            score++;
                        }
                    }
                }
            }

            score += a.Count + b.Count;

            return score;
        }

        public static int CalculateCloseness(this ILocation locationA, ILocation locationB, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            var sequenceA = locationA.GetParts();
            var sequenceB = locationB.GetParts();

            return sequenceA.CalculateCloseness(sequenceB, comparisonType);
        }

        public static ILocation Copy(this ILocation location)
        {
            var result = new Location();
            result.Update(location);

            return result;
        }

        public static bool Equals(this ILocation a, ILocation b)
        {
            var result = a.GetParts().SequenceEqual(b.GetParts());

            return result;
        }

        public static string Format(this ILocation location)
        {
            var result = string.Join("/", location.GetParts());

            return result;
        }

        public static string LastPart(this ILocation location)
        {
            var result = location.GetParts().LastOrDefault() ?? string.Empty;

            return result;
        }

        public static void Update(this ILocation destination, ILocation source)
        {
            destination.Update(source.GetParts());
        }

        public static void Update(this ILocation destination, string format)
        {
            destination.Update((format??string.Empty).Split('/'));
        }
    }
}
