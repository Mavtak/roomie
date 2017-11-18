using Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api.Device
{
    public static class RpcDeviceRepositoryHelpers
    {
        private static string NotFoundErrorMessage => "Device not found";
        private static string[] NotFoundErrorTypes => new[] { "not-found", "invalid-request" };


        public static Response CreateNotFoundError()
        {
            return Response.CreateError(NotFoundErrorMessage, NotFoundErrorTypes);
        }

        public static Response<TData> CreateNotFoundError<TData>()
            where TData : class
        {
            return Response.CreateError<TData>(NotFoundErrorMessage, NotFoundErrorTypes);
        }
    }
}