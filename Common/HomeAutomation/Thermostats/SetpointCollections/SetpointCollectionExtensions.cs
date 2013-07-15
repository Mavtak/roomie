
using System.Linq;
using System.Text;
namespace Roomie.Common.HomeAutomation.Thermostats.SetpointCollections
{
    public static class SetpointCollectionExtensions
    {
        public static ReadOnlySetPointCollection Copy(this ISetpointCollectionState source)
        {
            return ReadOnlySetPointCollection.CopyFrom(source);
        }

        public static string Describe(this ISetpointCollectionState state)
        {
            var result = new StringBuilder();
            
            if (state == null)
            {
                return result.ToString();
            }

            var setpointTypes = state.AvailableSetpoints.ToList();

            if (setpointTypes.Count == 0)
            {
                return result.ToString();
            }

            foreach (var setpointType in setpointTypes)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append("[");
                result.Append(setpointType);

                var temperature = state[setpointType];

                result.Append(":");
                
                if (temperature == null)
                {
                    result.Append("?");
                }
                else
                {
                    result.Append(temperature);
                }

                result.Append("]");
            }

            return result.ToString();
        }
    }
}
