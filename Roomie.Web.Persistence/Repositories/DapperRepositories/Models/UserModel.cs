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

        public User ToRepositoryType(IRepositoryModelCache cache)
        {
            var result = new User(
                alias: Alias,
                email: Email,
                id: Id,
                registeredTimestamp: RegisteredTimestamp,
                secret: SecretExtensions.Parse(Secret),
                token: Token
            );

            cache?.Set(result.Id, result);

            return result;
        }

        public static User ToRepositoryType(IRepositoryModelCache cache, int? id, IUserRepository userRepository)
        {
            if (id == null)
            {
                return null;
            }

            var cachedValue = cache?.Get<User>(id);

            if (cachedValue != null)
            {
                return cachedValue;
            }

            if (userRepository == null)
            {
                return new UserModel
                {
                    Id = id.Value
                }.ToRepositoryType((IRepositoryModelCache)null);
            }

            var result = userRepository.Get(id.Value);

            cache?.Set(result.Id, result);

            return result;
        }
    }
}
