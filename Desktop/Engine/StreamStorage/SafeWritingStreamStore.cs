using System;
using System.IO;

namespace Roomie.Desktop.Engine.StreamStorage
{
    public class SafeWritingStreamStore : IStreamStore
    {
        IStreamStore _streamStore;

        public SafeWritingStreamStore(IStreamStore streamStore)
        {
            _streamStore = streamStore;
        }

        public void Delete(string name)
        {
            _streamStore.Delete(name);
        }

        public Stream OpenRead(string name)
        {
            return _streamStore.OpenRead(name);
        }

        public Stream OpenWrite(string name)
        {
            var tempName = string.Join(".", new[] {
                name,
                Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4),
                "tmp",
            });
            Action rename = () => _streamStore.Rename(tempName, name);
            var stream = _streamStore.OpenWrite(tempName);
            var disposeNotifyingStream = new DisposeNotifyingStream(stream, rename);

            return disposeNotifyingStream;
        }

        public void Rename(string oldName, string newName)
        {
            _streamStore.Rename(oldName, newName);
        }
    }
}
