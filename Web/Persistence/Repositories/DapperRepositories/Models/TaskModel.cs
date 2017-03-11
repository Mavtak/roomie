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
        
        public static TaskModel FromRepositoryType(Task model)
        {
            var ownerModel = UserModel.FromRepositoryType(model.Owner);
            var scriptModel = ScriptModel.FromRepositoryType(model.Script);
            var targetModel = ComputerModel.FromRepositoryType(model.Target);

            var result = new TaskModel
            {
                Expiration = model.Expiration,
                Id = model.Id,
                Origin = model.Origin,
                Owner_Id = ownerModel.Id,
                ReceivedTimestamp = model.ReceivedTimestamp,
                Script_Id = scriptModel.Id,
                Target_Id = targetModel.Id,
            };

            return result;
        }

        public Task ToRepositoryType(IComputerRepository computerRepository, IScriptRepository scriptRepository, IUserRepository userRepository)
        {
            var result = new Task(
                expiration: Expiration,
                id: Id,
                origin: Origin,
                owner: UserModel.ToRepositoryType(Owner_Id, userRepository),
                receivedTimestamp: ReceivedTimestamp,
                script: ScriptModel.ToRepositoryType(Script_Id, scriptRepository),
                target: ComputerModel.ToRepositoryType(Target_Id, computerRepository)
            );

            return result;
        }
    }
}
