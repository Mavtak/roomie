using System.IO;

namespace Roomie.Desktop.Engine.StreamStorage
{
    public interface IStreamStore
    {
        Stream OpenRead(string name);
        Stream OpenWrite(string name);
        void Rename(string oldName, string newName);
    }
}
