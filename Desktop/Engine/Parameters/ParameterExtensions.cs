using System.Text;

namespace Roomie.Desktop.Engine.Parameters
{
    public static class ParameterExtensions
    {
        public static string Format(this IParameter parameter)
        {
            var result = new StringBuilder();

            result.Append(parameter.Name);
            result.Append("=\"");
            result.Append(parameter.Value);
            result.Append("\"");

            return result.ToString();
        }
    }
}
