using Roomie.Web.Persistence.Repositories;
using ApiModels = Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api.WebHookSession.Actions
{
    public class Create
    {
        private IComputerRepository _computerRepository;
        private ISessionRepository _sessionRepository;

        public Create(IComputerRepository computerRepository, ISessionRepository sessionRepository)
        {
            _computerRepository = computerRepository;
            _sessionRepository = sessionRepository;
        }

        public ApiModels.Response<ApiModels.WebHookSession.CreateSession.Response> Run(ApiModels.WebHookSession.CreateSession.Request requestData)
        {
            if (requestData == null)
            {
                return Error("no request data", "programming-error", "missing-data", "no-data");
            }

            if (requestData.AccessKey == null)
            {
                return Error($"{nameof(requestData.AccessKey)} not included", "programming-error", "missing-data");
            }

            if (requestData.AccessKey == string.Empty)
            {
                return Error($"{nameof(requestData.AccessKey)} empty", "missing-data", "invalid-data");
            }

            var computer = _computerRepository.Get(requestData.AccessKey);

            if (computer == null)
            {
                return Error("No computer found that corrosponds to the provided access key", "invalid-data", "user-friendly");
            }

            var session = Persistence.Models.WebHookSession.Create(computer);

            _sessionRepository.Add(session);

            var response = new ApiModels.Response<ApiModels.WebHookSession.CreateSession.Response>
            {
                Data = new ApiModels.WebHookSession.CreateSession.Response
                {
                    Token = session.Token,
                },
            };

            return response;
        }

        private static ApiModels.Response<ApiModels.WebHookSession.CreateSession.Response> Error(string message, params string[] types)
        {
            return new ApiModels.Response<ApiModels.WebHookSession.CreateSession.Response>
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