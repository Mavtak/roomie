using System;
using Roomie.Web.Persistence.Helpers.Secrets;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Secret { get; set; }
        public DateTime? RegisteredTimestamp { get; set; }

        public static UserModel FromRepositoryType(User repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var result = new UserModel
            {
                Alias = repositoryModel.Alias,
                Email = repositoryModel.Email,
                Id = repositoryModel.Id,
                RegisteredTimestamp = repositoryModel.RegisteredTimestamp,
                Secret = (repositoryModel.Secret == null) ? null : repositoryModel.Secret.Format(),
                Token = repositoryModel.Token
            };

            return result;
        }

        public User ToRepositoryType()
        {
            var result = new User(
                alias: Alias,
                email: Email,
                id: Id,
                registeredTimestamp: RegisteredTimestamp,
                secret: SecretExtensions.Parse(Secret),
                token: Token
            );

            return result;
        }

        public static User ToRepositoryType(int? id, IUserRepository userRepository)
        {
            if (id == null)
            {
                return null;
            }

            if (userRepository == null)
            {
                return new UserModel
                {
                    Id = id.Value
                }.ToRepositoryType();
            }

            return userRepository.Get(id.Value);
        }
    }
}
