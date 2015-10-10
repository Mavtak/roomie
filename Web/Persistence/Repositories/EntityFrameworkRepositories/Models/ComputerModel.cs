﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories.Models
{
    [Table("ComputerModels")]
    public class ComputerModel
    {
        [Key]
        public int Id { get; set; }
        
        public virtual UserModel Owner { get; set; }
        public string Name { get; set; }
        public virtual ScriptModel LastScript { get; set; }

        public string AccessKey { get; set; }
        public string EncryptionKey { get; set; }

        public string Address { get; set; }

        public DateTime? LastPing { get; set; }

        #region Conversions

        public static ComputerModel FromRepositoryType(Computer model, DbSet<ScriptModel> scripts, DbSet<UserModel> users)
        {
            var result = new ComputerModel
            {
                AccessKey = model.AccessKey,
                Address = model.Address,
                EncryptionKey = model.EncryptionKey,
                Id = model.Id,
                LastPing = model.LastPing,
                LastScript = model.LastScript == null ? null : scripts.Find(model.LastScript.Id),
                Name = model.Name,
                Owner = users.Find(model.Owner.Id)
            };

            return result;
        }

        public Computer ToRepositoryType()
        {
            var result = new Computer(
                accessKey: AccessKey,
                address: Address,
                encryptionKey: EncryptionKey,
                id: Id,
                lastPing: LastPing,
                lastScript: (LastScript == null) ? null : LastScript.ToRepositoryType(),
                name: Name,
                owner: Owner.ToRepositoryType()
            );

            return result;
        }

        #endregion

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as ComputerModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(ComputerModel that)
        {
            if (that == null)
            {
                return false;
            }

            if (this.Id != that.Id)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            builder.Append("Computer{Name='");
            builder.Append(Name ?? "(null)");
            builder.Append("', Owner='");
            builder.Append((Owner != null) ? Owner.ToString() : "(null)");
            builder.Append("'}");

            return builder.ToString();
        }

        #endregion
    }
}