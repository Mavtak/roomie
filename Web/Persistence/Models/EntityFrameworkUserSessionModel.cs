using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Roomie.Web.Persistence.Models
{
    [Table("UserSessionModels")]
    public class EntityFrameworkUserSessionModel
    {
        [Key]
        public int Id { get; set; }
        
        public virtual EntityFrameworkUserModel User { get; set; }
        public DateTime CreationTimeStamp { get; set; }
        public DateTime LastContactTimeStamp { get; set; }
        public string Token { get; set; }
        public string Data { get; set; }

        #region conversions

        public static EntityFrameworkUserSessionModel FromRepositoryType(UserSession model, DbSet<EntityFrameworkUserModel> users)
        {
            var result = new EntityFrameworkUserSessionModel
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
