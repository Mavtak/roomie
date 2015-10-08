using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    [Table("DeviceModels")]
    public class DeviceModel
    {
        [Key]
        public int Id { get; set; }

        public bool? IsConnected { get; set; }
        public DeviceType Type { get; set; }

        public string CurrentAction { get; set; }

        public virtual NetworkModel Network { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        protected virtual DeviceLocationModel Location { get; set; } //TODO: remove completely

        public string Notes { get; set; }

        public DateTime? LastPing { get; set; }

        #region conversions

        public static DeviceModel FromRepositoryType(Device model, DbSet<NetworkModel> networks)
        {
            var result = new DeviceModel
            {
                Address = model.Address,
                CurrentAction = model.CurrentAction,
                Id = model.Id,
                IsConnected = model.IsConnected,
                LastPing = model.LastPing,
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

            var that = obj as DeviceModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(DeviceModel that)
        {
            if (that == null)
            {
                return false;
            }

            if (this.Address != that.Address)
            {
                return false;
            }

            if (!NetworkModel.Equals(this.Network, that.Network))
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