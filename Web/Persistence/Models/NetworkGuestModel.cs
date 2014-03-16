
namespace Roomie.Web.Persistence.Models
{
    public class NetworkGuestModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public NetworkModel Network { get; set; }
    }
}
