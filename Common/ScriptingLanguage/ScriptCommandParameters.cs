using System.Collections.Generic;
using System.Text;

namespace Roomie.Common.ScriptingLanguage
{
    //TODO: encapsulate
    public class ScriptCommandParameters : HashSet<ScriptCommandParameter>
    {
        public bool ContainsParameterName(string name)
        {
            foreach (var parameter in this)
            {
                if (parameter.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public ScriptCommandParameter this[string name]
        {
            get
            {
                foreach (var paremeter in this)
                {
                    if (paremeter.Name == name)
                    {
                        return paremeter;
                    }
                }

                return null;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("{");
            foreach (var parameter in this)
            {
                result.Append(parameter.ToString());
                result.Append(", ");
                //TODO: detect last element
            }
            result.Append("}");

            return result.ToString();
        }
    }
}
