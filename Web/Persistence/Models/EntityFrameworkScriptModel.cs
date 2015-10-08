using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    [Table("ScriptModels")]
    public class ScriptModel
    {
        [Key]
        public int Id { get; set; }
        
        public bool? Mutable { get; set; }
        public DateTime? CreationTimestamp { get; set; }
        public string Text { get; set; }
        public int? RunCount { get; set; }
        public DateTime? LastRunTimestamp { get; set; }

        #region Convertions

        public static ScriptModel FromRepositoryType(Script model)
        {
            var result = new ScriptModel
            {
                CreationTimestamp = model.CreationTimestamp,
                Id = model.Id,
                LastRunTimestamp = model.LastRunTimestamp,
                Mutable = model.Mutable,
                RunCount = model.RunCount,
                Text = model.Text
            };

            return result;
        }

        public Script ToRepositoryType()
        {
            var result = new Script(
                creationTimestamp: CreationTimestamp,
                id: Id,
                lastRunTimestamp: LastRunTimestamp,
                mutable: Mutable,
                runCount: RunCount,
                text: Text
            );

            return result;
        }

        #endregion
    }
}