using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Task
{
    [ApiRestrictedAccess]
    public class TaskController : BaseController
    {
        RpcTaskRepository _rpcTaskRepository;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _rpcTaskRepository = new RpcTaskRepository(
                computer: Computer,
                repositoryFactory: RepositoryFactory,
                user: User
            );
        }

        public Persistence.Models.Task Get(int id)
        {
            var result = _rpcTaskRepository.Read(id);

            if (result == null)
            {
                throw new HttpException(404, "Task not found");
            }

            return result;
        }

        public Page<Persistence.Models.Task> Get([FromUri] ListFilter filter)
        {
            return _rpcTaskRepository.List(filter.Count, filter.SortDirection.ToString(), filter.Start);
        }

        public object Post(string action, string computerName = null, TimeSpan? pollInterval = null, TimeSpan ? timeout = null)
        {
            switch(action)
            {
                case "Clean":
                    return _rpcTaskRepository.Clean(timeout);

                case "GetForComputer":
                    return _rpcTaskRepository.GetForComputer(computerName, timeout, pollInterval);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
