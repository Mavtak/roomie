using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    [Table("SavedScriptModels")]
    public class EntityFrameworkSavedScriptModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }

        public virtual EntityFrameworkUserModel Owner { get; set; }
        public virtual String Name { get; set; }
        public virtual DateTime? ModificationTimestamp { get; set; }
        public virtual EntityFrameworkScriptModel Script { get; set; }

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "savedscript" + Id;
            }
        }

        #endregion
    }
}