using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Device
{
    [ApiRestrictedAccess]
    public class DeviceController : BaseController
    {
        RpcDeviceRepository _rpcDeviceRepository;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _rpcDeviceRepository = new RpcDeviceRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );
        }

        public object Post([FromBody] Request request)
        {
            return RpcRequestRouter.Route(_rpcDeviceRepository, request);
        }

        public IEnumerable<Persistence.Models.Device> Get(bool examples = false)
        {
            var result = _rpcDeviceRepository.List(examples);

            return result;
        }

        public Persistence.Models.Device Get(int id)
        {
            var result = _rpcDeviceRepository.Read(id);

            return result;
        }

        public void Put(int id, [FromBody] DeviceUpdateModel update)
        {
            _rpcDeviceRepository.Update(id, update?.Location, update?.Name, update?.Type);
        }
    }
}
