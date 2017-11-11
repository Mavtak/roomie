﻿using System.Web.Http;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Device
{
    [ApiRestrictedAccess]
    public class DeviceController : BaseController
    {
        public object Post([FromBody] Request request)
        {
            var rpcRepository = new RpcDeviceRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );

            return RpcRequestRouter.Route(rpcRepository, request);
        }
    }
}