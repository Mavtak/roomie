using System.Data.Entity.ModelConfiguration;

namespace Roomie.Web.Persistence.Database
{
    public class DeviceTypeMapping : ComplexTypeConfiguration<Roomie.Common.HomeAutomation.DeviceType>
    {
        public DeviceTypeMapping()
        {
            this.Property(p => p.Name);
        }
    }
}