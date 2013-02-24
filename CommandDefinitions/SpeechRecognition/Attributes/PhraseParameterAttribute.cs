using Roomie.Desktop.Engine.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roomie.CommandDefinitions.SpeechRecognitionCommands.Attributes
{
    public class PhraseParameterAttribute : StringParameterAttribute
    {
        public const string Key = "Phrase";

        public PhraseParameterAttribute()
            : base(Key)
        {
        }

        public PhraseParameterAttribute(string @default)
            : base(Key, @default)
        {
        }
    }
}
