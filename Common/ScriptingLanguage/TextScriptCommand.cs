using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Roomie.Common.ScriptingLanguage.Exceptions;
using System.IO;

namespace Roomie.Common.ScriptingLanguage
{
    public class TextScriptCommand : IScriptCommand
    {
        public string FullName { get; private set; }
        public ScriptCommandParameters Parameters { get; private set; }
        public ScriptCommandList InnerCommands { get; private set; }
        public string OriginalText { get; private set; }


        private const string ParameterNamePattern = @"(?<Name>[a-zA-Z0-9]+)";
        private const string QuotedStringPattern = @"""(?<Value>[^""]*)""";
        private const string ParameterPattern = "(?<Parameter>" + ParameterNamePattern + "=" + QuotedStringPattern + ")";
        private const string MaybeASpace = "[ ]?";
        private const string ParametersPattern = "(?<Parameters>(" + ParameterPattern + MaybeASpace + ")*)";
        private const string CommandNamePattern = "(?<FullName>[a-zA-z0-9.]+)";
        private const string LinePattern = "^" + CommandNamePattern + MaybeASpace + ParametersPattern +"$";
        private static Regex _lineRegex;
        private static Regex _parametersRegex;
        private static Regex _parameterRegex;

        public TextScriptCommand(string line)
        {
            if (_lineRegex == null)
            {
                _lineRegex = new Regex(LinePattern);
                _parametersRegex = new Regex(ParametersPattern);
                _parameterRegex = new Regex(ParameterPattern);
            }

            Parameters = new ScriptCommandParameters();
            InnerCommands = new ScriptCommandList();
            OriginalText = line;

            var matches = _lineRegex.Match(line);

            if (!matches.Groups["FullName"].Success)
            {
                throw new RoomieScriptSyntaxErrorException("No command name specified");
            }

            FullName = matches.Groups["FullName"].Value;

            //TODO: make this less awful
            var nextPart = matches.Groups["Parameters"];
            if (nextPart.Success && nextPart.Length > 0)
            {
                foreach (var capture in _parametersRegex.Match(nextPart.Value).Captures)
                {
                    var parameterMatch = _parameterRegex.Match(capture.ToString());

                    string name = parameterMatch.Groups["Name"].Value;
                    string value = parameterMatch.Groups["Value"].Value;

                    var parameter = new ScriptCommandParameter(name, value);
                    Parameters.Add(parameter);
                }
            }
        }

        public static IEnumerable<IScriptCommand> FromLines(string text)
        {
            var reader = new StringReader(text);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var result = new TextScriptCommand(line);
                yield return result;
            }
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
