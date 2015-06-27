using System;
using Roomie.Common.TextUtilities;

namespace Roomie.Common.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TableBuilderDemo();

            Console.ReadKey();
        }

        static void TableBuilderDemo()
        {
            var table = new TextTable(new int[] { 10, 15, 20 });

            Console.WriteLine(table.TopLine);
            Console.WriteLine(table.ContentLine(new string[] { "column 1", "column 2", "column 3" }));
            Console.WriteLine(table.DividerLine);
            Console.WriteLine(table.ContentLine(new string[] { "Item 1a", "Item 2b", "Item 1c" }));
            Console.WriteLine(table.ContentLine(new string[] { "Item 2", "Item 2b", "Item 2c" }));
            Console.WriteLine(table.ContentLine(new string[] { "Item 3a", "Item 3b", "Item 3c" }));
            Console.WriteLine(table.BottomLine);
        }
    }
}
