using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    [Table("TaskModels")]
    public class EntityFrameworkTaskModel
    {
        [Key]
        public int Id { get; set; }

        public virtual EntityFrameworkUserModel Owner { get; set; }
        public virtual EntityFrameworkComputerModel Target { get; set; }
        public string Origin { get; set; }
        public virtual EntityFrameworkScriptModel Script { get; set; }

        public DateTime? Expiration { get; set; }
        public DateTime? ReceivedTimestamp { get; set; }

        #region Conversions

        public static EntityFrameworkTaskModel FromRepositoryType(Task model, DbSet<EntityFrameworkComputerModel> computers,DbSet<EntityFrameworkScriptModel> scripts, DbSet<EntityFrameworkUserModel> users)
        {
            var result = new EntityFrameworkTaskModel
            {
                Expiration = model.Expiration,
                Id = model.Id,
                Origin = model.Origin,
                Owner = users.Find(model.Owner.Id),
                ReceivedTimestamp = model.ReceivedTimestamp,
                Script = scripts.Find(model.Script.Id),
                Target = computers.Find(model.Target.Id)
            };

            return result;
        }

        public Task ToRepositoryType()
        {
            var result = new Task(
                expiration: Expiration,
                id: Id,
                origin: Origin,
                owner: Owner.ToRepositoryType(),
                receivedTimestamp: ReceivedTimestamp,
                script: Script.ToRepositoryType(),
                target: Target.ToRepositoryType()
            );

            return result;
        }

        #endregion
    }
}