using System.Text;

namespace Roomie.Common.ScriptingLanguage
{
    public static class ScriptCommandExtensions
    {
        internal static string Format(this IScriptCommand command)
        {
            var result = new StringBuilder();

            result.Append(command.FullName);

            foreach (var parameter in command.Parameters)
            {
                result.Append(" ");
                result.Append(parameter.Name);
                result.Append("=\"");
                result.Append(parameter.Value);
                result.Append("\"");
            }

            return result.ToString();
        }
    }
}
