﻿using System.Web.Http;
using Roomie.Common.Api.Models;
using Roomie.Web.Backend.Helpers;

namespace Roomie.Web.Backend.Controllers.Api.Task
{
    [ApiRestrictedAccess]
    public class TaskController : BaseController
    {
        public object Post([FromBody] Request request)
        {
            var rpcRepository = new RpcTaskRepository(
                computer: Computer,
                repositoryFactory: RepositoryFactory,
                user: User
            );

            return RpcRequestRouter.Route(rpcRepository, request);
        }
    }
}
