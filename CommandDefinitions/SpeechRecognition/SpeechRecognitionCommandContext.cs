using System.Collections.Generic;
using System.Speech.Recognition;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.SpeechRecognition
{
    public class SpeechRecognitionCommandContext : RoomieCommandContext
    {
        public SpeechRecognitionCommandContext(RoomieCommandContext context)
            : base(context)
        {
            
        }

        public SpeechResponder SpeechRecognizer
        {
            get
            {
                var key = typeof(SpeechRecognitionCommandContext);

                if (!DataStore.Contains(key))
                {
                    DataStore.Add(key, new SpeechResponder(Engine));
                }

                var value = DataStore[key] as SpeechResponder;

                return value;
            }
        }
    }
}
