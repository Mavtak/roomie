using System.Collections.Generic;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    interface INodeDataEntryWithOptions<T> : INodeDataEntry<T>
    {
        IEnumerable<T> GetOptions();
    }
}
