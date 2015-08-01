using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Roomie.Web.Persistence.Models
{
    [Table("UserSessionModels")]
    public class EntityFrameworkUserSessionModel
    {
        [Key]
        public int Id { get; set; }
        
        public virtual EntityFrameworkUserModel User { get; set; }
        public DateTime CreationTimeStamp { get; set; }
        public DateTime LastContactTimeStamp { get; set; }
        public string Token { get; set; }
        public string Data { get; set; }
    }
}
