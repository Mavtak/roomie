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
            if (AttatchedComputer_Id == null)
            {
                return null;
            }

            if (computerRepository == null)
            {
                return new ComputerModel
                {
                    Id = AttatchedComputer_Id.Value
                }.ToRepositoryType(null, null);
            }

            return computerRepository.Get(AttatchedComputer_Id.Value);
        }

        private User OwnerToRepositoryType(IUserRepository userRepository)
        {
            if (Owner_Id == null)
            {
                return null;
            }

            if (userRepository == null)
            {
                return new UserModel
                {
                    Id = Owner_Id.Value
                }.ToRepositoryType();
            }

            return userRepository.Get(Owner_Id.Value);
        }
    }
}
