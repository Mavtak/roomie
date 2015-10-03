using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.ColorSwitch;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Repositories;
using System.Data.Entity;

namespace Roomie.Web.Persistence.Models
{
    [Table("DeviceModels")]
    public class EntityFrameworkDeviceModel
    {
        [Key]
        public int Id { get; set; }

        public bool? IsConnected { get; set; }
        public DeviceType Type { get; set; }

        public string CurrentAction { get; set; }

        public virtual EntityFrameworkNetworkModel Network { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual DeviceLocationModel Location { get; set; }

        public string Notes { get; set; }

        public DateTime? LastPing { get; set; }

        #region conversions

        public static EntityFrameworkDeviceModel FromRepositoryType(Device model, DbSet<EntityFrameworkNetworkModel> networks)
        {
            var result = new EntityFrameworkDeviceModel
            {
                Address = model.Address,
                CurrentAction = model.CurrentAction,
                Id = model.Id,
                IsConnected = model.IsConnected,
                LastPing = model.LastPing,
                Location = model.Location,
                Name = model.Name,
                Network = (model.Network == null) ? null : networks.Find(model.Network.Id),
                Notes = model.ToXElement().ToString(),
                Type = model.Type
            };

            return result;
        }

        public Device ToRepositoryType(IScriptRepository scripts, ITaskRepository tasks)
        {
            var result = new Device
            { 
                Address = Address,
                CurrentAction = CurrentAction,
                Id = Id,
                IsConnected = IsConnected,
                LastPing = LastPing,
                Location = Location,
                Name = Name,                
                Network = Network.ToRepositoryType(),
                ScriptRepository = scripts,
                TaskRepository = tasks,
                Type = Type
            };

            if (string.IsNullOrEmpty(Notes))
            {
                return result;
            }

            var element = XElement.Parse(Notes);
            var state = element.ToDeviceState();

            result.Update(state, false);

            return result;
        }

        #endregion

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as EntityFrameworkDeviceModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(EntityFrameworkDeviceModel that)
        {
            if (that == null)
            {
                return false;
            }

            if (this.Address != that.Address)
            {
                return false;
            }

            if (!EntityFrameworkNetworkModel.Equals(this.Network, that.Network))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion        
    }
}