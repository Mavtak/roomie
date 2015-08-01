using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    [Table("ScriptModels")]
    public class ScriptModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public bool? Mutable { get; set; }
        public DateTime? CreationTimestamp { get; set; }
        public string Text { get; set; }
        public int? RunCount { get; set; }
        public DateTime? LastRunTimestamp { get; set; }

        public ScriptModel()
        {
            CreationTimestamp = DateTime.UtcNow;
            RunCount = 0;
        }

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "script" + Id;
            }
        }

        #endregion
    }
}