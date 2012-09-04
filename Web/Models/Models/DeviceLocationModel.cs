using Roomie.Web.Models.Helpers;
using BaseDeviceLocation = Roomie.Common.HomeAutomation.DeviceLocation;
using System.ComponentModel.DataAnnotations;

namespace Roomie.Web.Models
{
    public class DeviceLocationModel : BaseDeviceLocation, IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public UserModel Owner { get; set; }
        //public string Name { get; set; }
        //public virtual ICollection<DeviceModel> Devices { get; set; }

        //public override string ToString()
        //{
        //    return Name;
        //}

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
