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
    }
}
