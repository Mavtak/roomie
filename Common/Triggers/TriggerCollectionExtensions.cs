using System.Linq;

namespace Roomie.Common.Triggers
{
    public static class TriggerCollectionExtensions
    {
        public static void CheckAndAct(this ITriggerCollection triggers)
        {
            var bundles = triggers.Check();
            var actions = bundles.SelectMany(x => x.Actions);

            foreach (var action in actions)
            {
                action.Action();
            }
        }
    }
}
