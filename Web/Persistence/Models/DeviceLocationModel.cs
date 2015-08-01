﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class DeviceLocationModel : Location, IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public EntityFrameworkUserModel Owner { get; set; }

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

        public virtual ICollection<EntityFrameworkDeviceModel> Devices { get; set; }

        #region HasId implementation

        //TODO: explicitly emplement this interface (for the other models also)
        public string DivId
        {
            get
            {
                return "location" + Id;
            }
        }

        #endregion
    }
}
