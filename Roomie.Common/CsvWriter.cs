using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Roomie.Common
{
    public class CsvWriter<T>
    {
        private const string Quote = "\"";
        private const string EscapedQuote = "\"\"";
        private const string NewLine = "\r\n";
        private readonly string _separator;
        private readonly KeyValuePairConverter<T> _converter;

        public CsvWriter(KeyValuePairConverter<T> converter)
        {
            _converter = converter;
            _separator = ",";
        }

        public void Write(TextWriter stream, IEnumerable<T> items, bool includeHeaders)
        {
            if (includeHeaders)
            {
                var headers = _converter.Keys;

                Write(stream, headers);
            }

            foreach (var item in items)
            {
                var pairs = _converter.Convert(item);

                var values = pairs.Select(x => x.Value);

                Write(stream, values);
            }
        }

        private void Write(TextWriter stream, IEnumerable<string> values)
        {
            var firstItem = true;

            foreach (var value in values)
            {
                if (!firstItem)
                {
                    stream.Write(_separator);
                }

                firstItem = false;

                Write(stream, value);
            }

            stream.Write(NewLine);
        }

        private void Write(TextWriter stream, string value)
        {
            var needsQuotes = value.Contains(_separator) || value.Contains(Quote);

            if (needsQuotes)
            {
                stream.Write(Quote);
            }

            stream.Write(value.Replace(Quote, EscapedQuote));

            if (needsQuotes)
            {
                stream.Write(Quote);
            }
        }
    }
}
