using System;
using System.ComponentModel.DataAnnotations;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class SavedScriptModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }

        public UserModel Owner { get; set; }
        public String Name { get; set; }
        public DateTime? ModificationTimestamp { get; set; }
        public ScriptModel Script { get; set; }



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