using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    [Table("UserSessionModels")]
    public class UserSessionModel
    {
        [Key]
        public int Id { get; set; }
        
        public virtual UserModel User { get; set; }
        public DateTime CreationTimeStamp { get; set; }
        public DateTime LastContactTimeStamp { get; set; }
        public string Token { get; set; }
        public string Data { get; set; }

        #region conversions

        public static UserSessionModel FromRepositoryType(UserSession model, DbSet<UserModel> users)
        {
            var result = new UserSessionModel
            {
                CreationTimeStamp = model.CreationTimeStamp,
                Id = model.Id,
                LastContactTimeStamp = model.LastContactTimeStamp,
                Token = model.Token,
                User = users.Find(model.User.Id)
            };

            return result;
        }

        public UserSession ToRepositoryType()
        {
            var result = new UserSession(
                creationTimeStamp: CreationTimeStamp,
                id: Id,
                lastContactTimeStamp: LastContactTimeStamp,
                token: Token,
                user: User.ToRepositoryType()
            );

            return result;
        }

        #endregion
    }
}
