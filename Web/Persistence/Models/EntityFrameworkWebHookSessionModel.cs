﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roomie.Web.Persistence.Models
{
    [Table("WebHookSessionModels")]
    //TODO: integrate with UserSessionModel
    public class EntityFrameworkWebHookSessionModel
    {
        [Key]
        public int Id { get; set; }

        public virtual EntityFrameworkComputerModel Computer { get; set; }
        public string Token { get; set; }

        public DateTime? LastPing { get; set; }

        #region Conversions

        public static EntityFrameworkWebHookSessionModel FromRepositoryType(WebHookSession model)
        {
            var result = new EntityFrameworkWebHookSessionModel
            {
                Computer = model.Computer,
                Id = model.Id,
                LastPing = model.LastPing,
                Token = model.Token,
            };

            return result;
        }

        public WebHookSession ToRepositoryType()
        {
            var result = new WebHookSession(
                computer: Computer,
                id: Id,
                lastPing: LastPing,
                token: Token
            );

            return result;
        }

        #endregion
    }
}