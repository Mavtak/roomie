using System;
using System.IO;
using Newtonsoft.Json;

namespace Q42HueCommands
{
    public class AppDataRepository
    {
        private const string Filename = "Q42HueAppSettings.json";

        public IAppData Load()
        {
            try
            {
                return LoadFromFile();
            }
            catch(FileNotFoundException)
            {
                var result = CreateNew();
                Save(result);

                return result;
            }
        }

        public void Save(IAppData appData)
        {
            var serializer = new JsonSerializer();

            using (var streamWriter = new StreamWriter(Filename))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonTextWriter, appData);
            }
        }

        private static AppData LoadFromFile()
        {
            var serializer = new JsonSerializer();

            using (var streamReader = new StreamReader(Filename))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return serializer.Deserialize<AppData>(jsonTextReader);
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
