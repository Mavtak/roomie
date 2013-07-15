
namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public static class SetpointCollectionExtensions
    {
        public static ReadOnlySetPointCollection Copy(this ISetpointCollectionState source)
        {
            return ReadOnlySetPointCollection.CopyFrom(source);
        }
    }
}
