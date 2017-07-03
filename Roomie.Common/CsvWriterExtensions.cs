using System.Collections.Generic;
using System.Text;

namespace Roomie.Common
{
    public static class CsvWriterExtensions
    {
        public static void Write<T>(this CsvWriter<T> writer, string path, IEnumerable<T> items, bool includeHeaders)
        {
            using (var file = new System.IO.StreamWriter(path, true, Encoding.UTF8))
            {
                writer.Write(file, items, includeHeaders);
            }
        }
    }
}
