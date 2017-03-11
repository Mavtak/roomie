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

        public static UserModel FromRepositoryType(User model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new UserModel
            {
                Alias = model.Alias,
                Email = model.Email,
                Id = model.Id,
                RegisteredTimestamp = model.RegisteredTimestamp,
                Secret = (model.Secret == null) ? null : model.Secret.Format(),
                Token = model.Token
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
