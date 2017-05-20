using ApiModels = Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api.WebHookSession
{
    public class WebHookSessionController : BaseController
    {
        public object Post(string action, ApiModels.WebHookSession.CreateSession.Request requestData)
        {
            object result;

            switch(action)
            {
                default:
                    result = Error($"Invalid action \"{action}\" for resource \"WebHookSession\"","programming-error", "invalid-action");

                    break;
            }

            return result;
        }

        private static ApiModels.Response<object> Error(string message, params string[] types)
        {
            return new ApiModels.Response<object>
            {
                Error = new Common.Api.Models.Error
                {
                    Message = message,
                    Types = types,
                }
            };
        }
    }
}