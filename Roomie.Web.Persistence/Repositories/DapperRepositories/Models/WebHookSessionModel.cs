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

        public static WebHookSessionModel FromRepositoryType(WebHookSession repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var result = new WebHookSessionModel
            {
                Computer_Id = repositoryModel.Computer.Id,
                Id = repositoryModel.Id,
                LastPing = repositoryModel.LastPing,
                Token = repositoryModel.Token,
            };

            return result;
        }

        public WebHookSession ToRepositoryType(IRepositoryModelCache cache, IComputerRepository computerRepository)
        {
            var result = new WebHookSession(
                computer: ComputerModel.ToRepositoryType(cache, Computer_Id, computerRepository),
                id: Id,
                lastPing: LastPing,
                token: Token
            );

            cache?.Set(result.Id, result);

            return result;
        }
    }
}
