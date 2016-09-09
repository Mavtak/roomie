using System.IO;

namespace Roomie.Desktop.Engine.StreamStorage
{
    public class BasicStreamStore : IStreamStore
    {
        private const string StorageFolder = "saved data";

        public Stream OpenRead(string name)
        {
            var path = Path.Combine(StorageFolder, name);

            try
            {
                return File.Open(path, FileMode.Open);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(StorageFolder);

                try
                {
                    return File.Open(path, FileMode.Open);
                }
                catch(FileNotFoundException)
                {
                    return null;
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public Stream OpenWrite(string name)
        {
            var path = Path.Combine(StorageFolder, name);

            return File.Open(path, FileMode.Create);
        }
    }
}
