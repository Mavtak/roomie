using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.Web.Persistence.Models
{
    class SetpointCollectionModel : ISetpointCollection
    {
        public ITemperature this[SetpointType setpoint]
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public IEnumerable<SetpointType> AvailableSetpoints { get; private set; }

        public void SetSetpoint(SetpointType setpointType, ITemperature temperature)
        {
            throw new System.NotImplementedException();
        }
    }
}
