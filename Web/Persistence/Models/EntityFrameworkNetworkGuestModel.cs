using System.ComponentModel.DataAnnotations.Schema;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    [Table("NetworkGuestModels")]
    public class EntityFrameworkNetworkGuestModel
    {
        public int Id { get; set; }
        public EntityFrameworkUserModel User { get; set; }
        public EntityFrameworkNetworkModel Network { get; set; }
    }
}
