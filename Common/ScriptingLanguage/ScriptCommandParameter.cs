using System.Text;

namespace Roomie.Common.ScriptingLanguage
{
    public class ScriptCommandParameter
    {
        public ScriptCommandParameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("[");
            result.Append(Name);
            result.Append(", ");
            result.Append(Value);
            result.Append("]");

            return result.ToString();
        }
    }
}
