using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class UserSessionModel
    {
        public DateTime CreationTimeStamp { get; set; }
        public int Id { get; set; }
        public DateTime LastContactTimeStamp { get; set; }
        public string Token { get; set; }
        public int? User_Id { get; set; }

        public static UserSessionModel FromRepositoryType(UserSession model)
        {
            if (model == null)
            {
                return null;
            }

            var userModel = UserModel.FromRepositoryType(model.User);

            var result = new UserSessionModel
            {
                CreationTimeStamp = model.CreationTimeStamp,
                Id = model.Id,
                LastContactTimeStamp = model.LastContactTimeStamp,
                Token = model.Token,
                User_Id = userModel.Id,
            };

            return result;
        }

        public UserSession ToRepositoryType(IUserRepository userRepository)
        {
            var result = new UserSession(
                creationTimeStamp: CreationTimeStamp,
                id: Id,
                lastContactTimeStamp: LastContactTimeStamp,
                token: Token,
                user: UserModel.ToRepositoryType(User_Id, userRepository)
            );

            return result;
        }
    }
}
