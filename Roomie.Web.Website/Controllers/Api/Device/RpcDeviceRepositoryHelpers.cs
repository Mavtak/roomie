using Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api.Device
{
    public static class RpcDeviceRepositoryHelpers
    {
        public static Response CreateNotFoundError()
        {
            return Response.CreateError("Device not found", "not-found", "invalid-request");
        }
    }
}