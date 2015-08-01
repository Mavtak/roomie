using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    [Table("WebHookSessionModels")]
    //TODO: integrate with UserSessionModel
    public class EntityFrameworkWebHookSessionModel : IHasDivId
    {
        [Key]
        public int Id { get; set; }

        public virtual EntityFrameworkComputerModel Computer { get; set; }
        public string Token { get; set; }
        
        #region LastPing

        public DateTime? LastPing { get; set; }

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                    return null;
                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        public bool IsConnected
        {
            get
            {
                TimeSpan? temp = TimeSinceLastPing;
                if (TimeSinceLastPing == null)
                    return false;

                return temp.Value.TotalSeconds < 10;
            }
        }

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        #endregion

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "webhooksession" + Id;
            }
        }

        #endregion
    }
}
