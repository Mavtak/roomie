using System;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class TaskModel
    {
        public DateTime? Expiration { get; set; }
        public int Id { get; set; }
        public string Origin { get; set; }
        public int? Owner_Id { get; set; }
        public DateTime? ReceivedTimestamp { get; set; }
        public int? Script_Id { get; set; }
        public int? Target_Id { get; set; }

        public static TaskModel FromRepositoryType(Task repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var ownerRepositoryModel = UserModel.FromRepositoryType(repositoryModel.Owner);
            var scriptRepositoryModel = ScriptModel.FromRepositoryType(repositoryModel.Script);
            var targetRepositoryModel = ComputerModel.FromRepositoryType(repositoryModel.Target);

            var result = new TaskModel
            {
                Expiration = repositoryModel.Expiration,
                Id = repositoryModel.Id,
                Origin = repositoryModel.Origin,
                Owner_Id = ownerRepositoryModel?.Id,
                ReceivedTimestamp = repositoryModel.ReceivedTimestamp,
                Script_Id = scriptRepositoryModel?.Id,
                Target_Id = targetRepositoryModel?.Id,
            };

            return result;
        }

        public Task ToRepositoryType(IRepositoryModelCache cache, IComputerRepository computerRepository, IScriptRepository scriptRepository, IUserRepository userRepository)
        {
            var cachedValue = cache?.Get<Task>(Id);

            if (cachedValue != null)
            {
                return cachedValue;
            }

            var result = new Task(
                expiration: Expiration,
                id: Id,
                origin: Origin,
                owner: UserModel.ToRepositoryType(cache, Owner_Id, userRepository),
                receivedTimestamp: ReceivedTimestamp,
                script: ScriptModel.ToRepositoryType(cache, Script_Id, scriptRepository),
                target: ComputerModel.ToRepositoryType(cache, Target_Id, computerRepository)
            );

            cache?.Set(result.Id, result);

            return result;
        }
    }
}
