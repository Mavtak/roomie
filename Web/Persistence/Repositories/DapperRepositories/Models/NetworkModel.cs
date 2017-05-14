﻿using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class NetworkModel
    {
        public string Address { get; set; }
        public int? AttatchedComputer_Id { get; set; }
        public int Id { get; set; }
        public DateTime? LastPing { get; set; }
        public string Name { get; set; }
        public int? Owner_Id { get; set; }

        public static NetworkModel FromRepositoryType(Network repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var attachedComputerRepositoryModel = ComputerModel.FromRepositoryType(repositoryModel.AttatchedComputer);
            var ownerRepositoryModel = UserModel.FromRepositoryType(repositoryModel.Owner);

            var result = new NetworkModel
            {
                Address = repositoryModel.Address,
                AttatchedComputer_Id = attachedComputerRepositoryModel?.Id,
                Id = repositoryModel.Id,
                LastPing = repositoryModel.LastPing,
                Name = repositoryModel.Name,
                Owner_Id = ownerRepositoryModel?.Id,
            };

            return result;
        }

        public Network ToRepositoryType(IComputerRepository computerRepository, IUserRepository userRepository)
        {
            var result = new Network(
                address: Address,
                attatchedComputer: ComputerModel.ToRepositoryType(AttatchedComputer_Id, computerRepository),
                devices: null,
                id: Id,
                lastPing: LastPing,
                name: Name,
                owner: UserModel.ToRepositoryType(Owner_Id, userRepository)
            );

            return result;
        }
        public  static Network ToRepositoryType(int? id, INetworkRepository networkRepository)
        {
            if (id == null)
            {
                return null;
            }

            if (networkRepository == null)
            {
                new NetworkModel
                {
                    Id = id.Value
                }.ToRepositoryType((IComputerRepository) null, (IUserRepository) null);
            }

            return networkRepository.Get(id.Value);
        }
    }
}
