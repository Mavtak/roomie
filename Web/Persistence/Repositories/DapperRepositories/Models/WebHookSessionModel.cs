using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class WebHookSessionModel
    {
        public int? Computer_Id { get; set; }
        public int Id { get; set; }
        public DateTime? LastPing { get; set; }
        public string Token { get; set; }

        public static WebHookSessionModel FromRepositoryType(WebHookSession model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new WebHookSessionModel
            {
                Computer_Id = model.Computer.Id,
                Id = model.Id,
                LastPing = model.LastPing,
                Token = model.Token,
            };

            return result;
        }

        public WebHookSession ToRepositoryType(IComputerRepository computerRepository)
        {
            var result = new WebHookSession(
                computer: ComputerModel.ToRepositoryType(Computer_Id, computerRepository),
                id: Id,
                lastPing: LastPing,
                token: Token
            );

            return result;
        }
    }
}
