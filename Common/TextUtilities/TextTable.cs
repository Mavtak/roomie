using System;
using System.Linq;
using System.Text;

namespace Roomie.Common.TextUtilities
{
    public class TextTable
    {
        private readonly int [] columnWidths;
        private readonly int totalWidth;
        private readonly int columnCount;

        private readonly string contentLineTemplate;

        public readonly string TopLine;
        public readonly string DividerLine;
        public readonly string BottomLine;

        public TextTable(int[] columnWidths)
        {
            this.columnWidths = columnWidths;
            this.columnCount = columnWidths.Length;
            this.totalWidth = columnWidths.Sum() + (columnCount * 3) + 1;

            var topLineBuilder = new StringBuilder();
            var dividerLineBuilder = new StringBuilder();
            var bottomLineBuilder = new StringBuilder();

            topLineBuilder.Append('┌');
            bottomLineBuilder.Append('└');
            dividerLineBuilder.Append('├');

            for (int x = 0; x < columnWidths.Length; x++)
            {
                if (x != 0)
                {
                    topLineBuilder.Append('┬');
                    bottomLineBuilder.Append('┴');
                    dividerLineBuilder.Append('┼');
                }

                var temp = new string('─', columnWidths[x] + 2);
                topLineBuilder.Append(temp);
                dividerLineBuilder.Append(temp);
                bottomLineBuilder.Append(temp);
            }

            topLineBuilder.Append('┐');
            dividerLineBuilder.Append('┤');
            bottomLineBuilder.Append('┘');

            TopLine = topLineBuilder.ToString();
            DividerLine = dividerLineBuilder.ToString();
            BottomLine = bottomLineBuilder.ToString();

            var contentLineTemplateBuilder = new StringBuilder();

            for (int x = 0; x < columnWidths.Length; x++)
            {
                contentLineTemplateBuilder.Append("{0}");

                contentLineTemplateBuilder.Append(" {");
                contentLineTemplateBuilder.Append(x + 1);
                contentLineTemplateBuilder.Append(",");
                contentLineTemplateBuilder.Append("-");
                contentLineTemplateBuilder.Append(columnWidths[x]);
                contentLineTemplateBuilder.Append("} ");
            }

            contentLineTemplateBuilder.Append("{0}");


            contentLineTemplate = contentLineTemplateBuilder.ToString();
        }

        public string PrintLine()
        {
            return DividerLine;
        }

        public string ContentLine(params object[] values)
        {
            var allContent = new string[values.Length + 3];

            allContent[0] = "│";

            for (int i = 0; i < values.Length; i++)
            {
                allContent[i + 1] = values[i].ToString();
            }

            return String.Format(contentLineTemplate, allContent);
        }

        public string StartOfTable(params object[] values)
        {
            var builder = new StringBuilder();
            builder.AppendLine(TopLine);
            builder.AppendLine(ContentLine(values));
            builder.Append(DividerLine);

            return builder.ToString();
        }

        public string EndOfTable()
        {
            return BottomLine;
        }
    }
}
