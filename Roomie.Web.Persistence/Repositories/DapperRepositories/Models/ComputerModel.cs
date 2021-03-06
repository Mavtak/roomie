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

        public static ComputerModel FromRepositoryType(Computer repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var lastScriptRepositoryModel = ScriptModel.FromRepositoryType(repositoryModel.LastScript);
            var ownerRepositoryModel = UserModel.FromRepositoryType(repositoryModel.Owner);

            var result = new ComputerModel
            {
                AccessKey = repositoryModel.AccessKey,
                Address = repositoryModel.Address,
                EncryptionKey = repositoryModel.EncryptionKey,
                Id = repositoryModel.Id,
                LastPing = repositoryModel.LastPing,
                LastScript_Id = lastScriptRepositoryModel?.Id,
                Name = repositoryModel.Name,
                Owner_Id = ownerRepositoryModel?.Id
            };

            return result;
        }

        public Computer ToRepositoryType(IRepositoryModelCache cache, IScriptRepository scriptRepository, IUserRepository userRepository)
        {
            var result = new Computer(
                accessKey: AccessKey,
                address: Address,
                encryptionKey: EncryptionKey,
                id: Id,
                lastPing: LastPing,
                lastScript: ScriptModel.ToRepositoryType(cache, LastScript_Id, scriptRepository),
                name: Name,
                owner: UserModel.ToRepositoryType(cache, Owner_Id, userRepository)
            );

            cache?.Set(result.Id, result);

            return result;
        }

        public static Computer ToRepositoryType(IRepositoryModelCache cache, int? id, IComputerRepository computerRepository)
        {
            if (id == null)
            {
                return null;
            }

            var cachedValue = cache?.Get<Computer>(id);

            if (cachedValue != null)
            {
                return cachedValue;
            }

            if (computerRepository == null)
            {
                return new ComputerModel
                {
                    Id = id.Value
                }.ToRepositoryType(cache, (IScriptRepository)null, (IUserRepository)null);
            }

            return computerRepository.Get(id.Value, cache);
        }
    }
}
