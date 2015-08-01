using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roomie.Web.Persistence.Models
{
    [Table("WebHookSessionModels")]
    //TODO: integrate with UserSessionModel
    public class EntityFrameworkWebHookSessionModel
    {
        [Key]
        public int Id { get; set; }

        public virtual EntityFrameworkComputerModel Computer { get; set; }
        public string Token { get; set; }

        public DateTime? LastPing { get; set; }
    }
}
