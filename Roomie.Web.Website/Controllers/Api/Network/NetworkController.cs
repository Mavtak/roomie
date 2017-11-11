using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Network
{
    [ApiRestrictedAccess]
    public class NetworkController : BaseController
    {
        private RpcNetworkRepository _rpcNetworkRepository;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _rpcNetworkRepository = new RpcNetworkRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );
        }

        public object Post([FromBody] Request request)
        {
            return RpcRequestRouter.Route(_rpcNetworkRepository, request);
        }

        public IEnumerable<Persistence.Models.Network> Get()
        {
            var result = _rpcNetworkRepository.List();

            return result;
        }

        public Persistence.Models.Network Get(int id)
        {
            var result = _rpcNetworkRepository.Read(id);

            if (result == null)
            {
                throw new HttpException(404, "Network not found");
            }

            return result;
        }

        public void Put(int id, [FromBody] NetworkUpdateModel update)
        {
            update = update ?? new NetworkUpdateModel();

            _rpcNetworkRepository.Update(id, update.Name);
        }

        public void Post(int id, string action, string computerName, string sentDevicesXml)
        {
            switch (action)
            {
                case "add-device":
                    _rpcNetworkRepository.AddDevice(id);
                    break;

                case "remove-device":
                    _rpcNetworkRepository.RemoveDevice(id);
                    break;

                case "sync-whole-network":
                    _rpcNetworkRepository.SyncWholeNetwork(id, computerName, sentDevicesXml);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void Delete(int id)
        {
            _rpcNetworkRepository.Delete(id);

        }
    }
}
