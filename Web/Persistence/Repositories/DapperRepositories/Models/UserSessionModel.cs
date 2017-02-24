﻿using System;
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
            var result = new UserSessionModel
            {
                CreationTimeStamp = model.CreationTimeStamp,
                Id = model.Id,
                LastContactTimeStamp = model.LastContactTimeStamp,
                Token = model.Token,
                User_Id = model.User.Id,
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
                user: UserToRepositoryType(userRepository)
            );

            return result;
        }

        private User UserToRepositoryType(IUserRepository userRepository)
        {
            if (User_Id == null)
            {
                return null;
            }

            if (userRepository == null)
            {
                return new UserModel
                {
                    Id = User_Id.Value
                }.ToRepositoryType();
            }

            return userRepository.Get(User_Id.Value);
        }
    }
}
