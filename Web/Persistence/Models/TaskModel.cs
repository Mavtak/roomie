﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    [Table("TaskModels")]
    public class TaskModel : IHasDivId
    {
        public TaskModel()
        {
            Expiration = DateTime.UtcNow.AddSeconds(30);
        }

        [Key]
        public int Id { get; set; }

        public virtual UserModel Owner { get; set; }
        public virtual ComputerModel Target { get; set; }
        public string Origin { get; set; }
        public virtual ScriptModel Script { get; set; }

        public DateTime? Expiration { get; set; }
        public DateTime? ReceivedTimestamp { get; set; }
        public bool Received
        {
            get
            {
                return ReceivedTimestamp.HasValue;
            }
        }
        public bool Waiting
        {
            get
            {
                return !Received
                    && ( (Expiration >= DateTime.UtcNow)
                        || !Expiration.HasValue);
            }
        }
        public bool Expired
        {
            get
            {
                return !Received && (Expiration < DateTime.UtcNow);
            }
        }

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "task" + Id;
            }
        }

        #endregion
    }
}