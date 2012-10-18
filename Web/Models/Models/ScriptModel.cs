using System;
using System.ComponentModel.DataAnnotations;
using Roomie.Web.Models.Helpers;

namespace Roomie.Web.Models
{
    public class ScriptModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }
        
        public bool? Mutable { get; set; }
        public DateTime? CreationTimestamp { get; set; }
        public String Text { get; set; }
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