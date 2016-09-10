using System.IO;

namespace Roomie.Desktop.Engine.StreamStorage
{
    public class BasicStreamStore : IStreamStore
    {
        private const string StorageFolder = "saved data";

        public void Delete(string name)
        {
            var path = Path.Combine(StorageFolder, name);

            File.Delete(path);
        }

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

        public void Rename(string oldName, string newName)
        {
            var oldPath = Path.Combine(StorageFolder, oldName);
            var newPath = Path.Combine(StorageFolder, newName);

            try
            {
                File.Move(oldPath, newPath);
            }
            catch (IOException exception)
            {
                if (exception is FileNotFoundException)
                {
                    throw;
                }

                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                    File.Move(oldPath, newPath);

                    return;
                }

                throw;
            }
        }
    }
}
