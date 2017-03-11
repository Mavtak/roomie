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
            var result = new NetworkModel
            {
                Address = network.Address,
                AttatchedComputer_Id = network?.AttatchedComputer?.Id,
                Id = network.Id,
                LastPing = network.LastPing,
                Name = network.Name,
                Owner_Id = network?.Owner?.Id,
            };

            return result;
        }

        public Network ToRepositoryType(IComputerRepository computerRepository, IUserRepository userRepository)
        {
            var result = new Network(
                address: Address,
                attatchedComputer: AttachedComputerToRepositoryType(computerRepository),
                devices: null,
                id: Id,
                lastPing: LastPing,
                name: Name,
                owner: OwnerToRepositoryType(userRepository)
            );

            return result;
        }

        private Computer AttachedComputerToRepositoryType(IComputerRepository computerRepository)
        {
            var id = AttatchedComputer_Id;

            if (id == null)
            {
                return null;
            }

            if (computerRepository == null)
            {
                return new ComputerModel
                {
                    Id = id.Value
                }.ToRepositoryType(null, null);
            }

            return computerRepository.Get(id.Value);
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
