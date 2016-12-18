using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    public class DeviceLocationModel : Location
    {
        [Key]
        public int Id { get; set; }
        
        public UserModel Owner { get; set; }

        public string Name
        {
            get
            {
                return this.Format();
            }
            set
            {
                Update((value ?? string.Empty).Split('/'));
            }
        }

        public virtual ICollection<DeviceModel> Devices { get; set; }
    }
}
