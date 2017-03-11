﻿using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class ComputerModel
    {
        public string AccessKey { get; set; }
        public string Address { get; set; }
        public string EncryptionKey { get; set; }
        public int Id { get; set; }
        public DateTime? LastPing { get; set; }
        public int? LastScript_Id { get; set; }
        public string Name { get; set; }
        public int? Owner_Id { get; set; }

        public static ComputerModel FromRepositoryType(Computer model)
        {
            var result = new ComputerModel
            {
                AccessKey = model.AccessKey,
                Address = model.Address,
                EncryptionKey = model.EncryptionKey,
                Id = model.Id,
                LastPing = model.LastPing,
                LastScript_Id = model.LastScript?.Id,
                Name = model.Name,
                Owner_Id = model.Owner?.Id
            };

            return result;
        }

        public Computer ToRepositoryType(IScriptRepository scriptRepository, IUserRepository userRepository)
        {
            var result = new Computer(
                accessKey: AccessKey,
                address: Address,
                encryptionKey: EncryptionKey,
                id: Id,
                lastPing: LastPing,
                lastScript: LastScriptToRepositoryType(scriptRepository),
                name: Name,
                owner: OwnerToRepositoryType(userRepository)
            );

            return result;
        }

        private Script LastScriptToRepositoryType(IScriptRepository scriptRepository)
        {
            var id = LastScript_Id;

            if (id == null)
            {
                return null;
            }

            if (scriptRepository == null)
            {
                return new ScriptModel
                {
                    Id = id.Value
                }.ToRepositoryType();
            }

            return scriptRepository.Get(id.Value);
        }

        private User OwnerToRepositoryType(IUserRepository userRepository)
        {
            var id = Owner_Id;

            if (id == null)
            {
                return null;
            }

            if (userRepository == null)
            {
                return new UserModel
                {
                    Id = id.Value
                }.ToRepositoryType();
            }

            return userRepository.Get(id.Value);
        }
    }
}
