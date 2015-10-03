using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Roomie.Web.Persistence.Models
{
    [Table("NetworkModels")]
    public class EntityFrameworkNetworkModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public virtual EntityFrameworkUserModel Owner { get; set; }

        public virtual EntityFrameworkComputerModel AttatchedComputer { get; set; }

        public EntityFrameworkNetworkModel()
        {
            Devices = new List<EntityFrameworkDeviceModel>();

        }

        public DateTime? LastPing { get; set; }
        
        public virtual ICollection<EntityFrameworkDeviceModel> Devices { get; set; }


        #region Conversions

        public static EntityFrameworkNetworkModel FromRepositoryType(Network network, DbSet<EntityFrameworkComputerModel> computers, DbSet<EntityFrameworkUserModel> users)
        {
            var result = new EntityFrameworkNetworkModel
            {
                Address = network.Address,
                AttatchedComputer = network.AttatchedComputer == null ? null : computers.Find(network.AttatchedComputer.Id),
                //TODO: include devices?,
                Id = network.Id,
                LastPing = network.LastPing,
                Name = network.Name,
                Owner = users.Find(network.Owner.Id)
            };

            return result;
        }

        public Network ToRepositoryType()
        {
            var result = new Network(
                address: Address,
                attatchedComputer: AttatchedComputer.ToRepositoryType(),
                devices: null, //TODO: remove this property completely
                id: Id,
                lastPing: LastPing,
                name: Name,
                owner: Owner.ToRepositoryType()
            );

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

            var that = obj as EntityFrameworkNetworkModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(EntityFrameworkNetworkModel that)
        {
            if (!base.Equals(that))
            {
                return false;
            }

            if (!EntityFrameworkUserModel.Equals(this.Owner, that.Owner))
            {
                return false;
            }

            return true;
        }

        public static bool Equals(EntityFrameworkNetworkModel network1, EntityFrameworkNetworkModel network2)
        {
            if(network1 == null && network2 == null)
            {
                return true;
            }

            if(network1 == null ^ network2 == null)
            {
                return false;
            }

            return network1.Equals(network2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("Network{Name=");
            builder.Append(Name);
            builder.Append(", Address='");
            builder.Append(Address);
            builder.Append("', AttatchedComputer=");
            builder.Append(AttatchedComputer);
            builder.Append(", Owner=");
            builder.Append(Owner);
            builder.Append("}");

            return builder.ToString();
        }

        #endregion
    }
}