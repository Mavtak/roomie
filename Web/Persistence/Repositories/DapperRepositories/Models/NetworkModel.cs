using System;
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

        public static NetworkModel FromRepositoryType(Network network)
        {
            if (network == null)
            {
                return null;
            }

            var attachedComputerModel = ComputerModel.FromRepositoryType(network.AttatchedComputer);
            var ownerModel = UserModel.FromRepositoryType(network.Owner);

            var result = new NetworkModel
            {
                Address = network.Address,
                AttatchedComputer_Id = attachedComputerModel?.Id,
                Id = network.Id,
                LastPing = network.LastPing,
                Name = network.Name,
                Owner_Id = ownerModel?.Id,
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
