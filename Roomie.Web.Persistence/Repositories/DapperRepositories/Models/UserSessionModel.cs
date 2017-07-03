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

        public static UserSessionModel FromRepositoryType(UserSession repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var userRepositoryModel = UserModel.FromRepositoryType(repositoryModel.User);

            var result = new UserSessionModel
            {
                CreationTimeStamp = repositoryModel.CreationTimeStamp,
                Id = repositoryModel.Id,
                LastContactTimeStamp = repositoryModel.LastContactTimeStamp,
                Token = repositoryModel.Token,
                User_Id = userRepositoryModel?.Id,
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
