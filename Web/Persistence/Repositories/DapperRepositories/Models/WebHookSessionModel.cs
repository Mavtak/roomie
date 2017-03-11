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
                computer: ComputerToRepositoryType(computerRepository),
                id: Id,
                lastPing: LastPing,
                token: Token
            );

            return result;
        }

        private Computer ComputerToRepositoryType(IComputerRepository computerRepository)
        {
            var id = Computer_Id;

            if (id == null)
            {
                return null;
            }

            if (computerRepository == null)
            {
                return new ComputerModel
                {
                    Id = id.Value
                }.ToRepositoryType(null, null);
            }

            return computerRepository.Get(id.Value);
        }
    }
}
