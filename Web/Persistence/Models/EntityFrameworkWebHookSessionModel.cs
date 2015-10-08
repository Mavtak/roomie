using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    [Table("WebHookSessionModels")]
    //TODO: integrate with UserSessionModel
    public class WebHookSessionModel
    {
        [Key]
        public int Id { get; set; }

        public virtual ComputerModel Computer { get; set; }
        public string Token { get; set; }

        public DateTime? LastPing { get; set; }

        #region Conversions

        public static WebHookSessionModel FromRepositoryType(WebHookSession model, DbSet<ComputerModel> computers)
        {
            var result = new WebHookSessionModel
            {
                Computer = computers.Find(model.Computer.Id),
                Id = model.Id,
                LastPing = model.LastPing,
                Token = model.Token,
            };

            return result;
        }

        public WebHookSession ToRepositoryType()
        {
            var result = new WebHookSession(
                computer: Computer.ToRepositoryType(),
                id: Id,
                lastPing: LastPing,
                token: Token
            );

            return result;
        }

        #endregion
    }
}
