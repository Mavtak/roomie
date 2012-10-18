using System.Collections.Generic;


namespace Roomie.Desktop.Engine.Exceptions
{
    public class MissingArgumentsException : VariableException
    {

        List<string> missingVariables;

        public MissingArgumentsException(List<string> missingVariables)
            : base("missing variables: " + Common.NiceList(missingVariables))
        {
            this.missingVariables = missingVariables;
        }

        public MissingArgumentsException(string argument)
            :this(new List<string>(new string[]{argument}))
            {
            }
    }
}
