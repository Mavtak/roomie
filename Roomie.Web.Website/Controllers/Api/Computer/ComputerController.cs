using System;
using System.Web;
using System.Web.Http.Controllers;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Computer
{
    [ApiRestrictedAccess]
    public class ComputerController : BaseController
    {
        private RpcComputerRepository _rpcComputerRepository;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _rpcComputerRepository = new RpcComputerRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );
        }

        public Persistence.Models.Computer[] Get()
        {
            var result = _rpcComputerRepository.List();

            return result;
        }

        public Persistence.Models.Computer Get(int id)
        {
            var result = _rpcComputerRepository.Read(id);

            if (result == null)
            {
                throw new HttpException(404, "Computer not found");
            }

            return result;
        }

        public Persistence.Models.Computer Get(string accessKey)
        {
            var result = _rpcComputerRepository.Read(accessKey);

            if (result == null)
            {
                throw new HttpException(404, "Computer not found");
            }

            return result;
        }

        public void Post(AddComputerModel add)
        {
            add = add ?? new AddComputerModel();

            _rpcComputerRepository.Create(add.Name);
        }

        public void Post(int id, string action, ComputerActionOptions options)
        {
            options = options ?? new ComputerActionOptions();

            switch(action)
            {
                case "DisableWebHook":
                    _rpcComputerRepository.DisableWebHook(id);
                    break;

                case "RenewWebHookKeys":
                    _rpcComputerRepository.RenewWebHookKeys(id);
                    break;

                case "RunScript":
                    _rpcComputerRepository.RunScript(id, options.Script);
                    break;

                case "SendScript":

                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}