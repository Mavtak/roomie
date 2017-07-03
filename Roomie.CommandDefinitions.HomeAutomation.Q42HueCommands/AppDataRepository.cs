using System;
using System.IO;
using Newtonsoft.Json;
using Roomie.Desktop.Engine.StreamStorage;

namespace Q42HueCommands
{
    public class AppDataRepository
    {
        private const string Filename = "Q42HueAppSettings.json";

        private IStreamStore _streamStore;

        public AppDataRepository(IStreamStore streamStore)
        {
            _streamStore = streamStore;
        }

        public IAppData Load()
        {
            var result = LoadFromFile();

            if (result == null)
            {
                result = CreateNew();
                Save(result);
            }

            return result;

        }

        public void Save(IAppData appData)
        {
            var serializer = new JsonSerializer();

            using (var stream = _streamStore.OpenWrite(Filename))
            using (var streamWriter = new StreamWriter(stream))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonTextWriter, appData);
            }
        }

        private AppData LoadFromFile()
        {
            var serializer = new JsonSerializer();

            using (var stream = _streamStore.OpenRead(Filename))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var streamReader = new StreamReader(stream))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return serializer.Deserialize<AppData>(jsonTextReader);
                }
            }
        }

        private static AppData CreateNew()
        {
            return new AppData
            {
                AppName = "Roomie+Q42.HueApi",
                DeviceName = Guid.NewGuid()
                    .ToString()
                    .Replace("-", "")
                    .Substring(0, 10),
            };
        }
    }
}
