using Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api.Computer
{
    public static class RpcComputerRepositoryHelpers
    {
        public static Response CreateNotFoundError()
        {
            return Response.CreateError("Computer not found", "not-found", "invalid-request");
        }
    }
}