using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Website.Controllers.Api.Computer.Actions
{
    public class RunScript
    {
        private IComputerRepository _computerRepository;
        private IScriptRepository _scriptRepository;
        private ITaskRepository _taskRepository;

        public RunScript(IComputerRepository computerRepository, IScriptRepository scriptRepository, ITaskRepository taskRepository)
        {
            _computerRepository = computerRepository;
            _scriptRepository = scriptRepository;
            _taskRepository = taskRepository;
        }

        public void Run(Persistence.Models.User user, Persistence.Models.Computer computer, string source, string scriptText, bool updateLastRunScript)
        {
            var script = Persistence.Models.Script.Create(false, scriptText);
            _scriptRepository.Add(script);

            var task = Persistence.Models.Task.Create(user, source, computer, script);

            _taskRepository.Add(task);

            if (updateLastRunScript)
            {
                computer.UpdateLastScript(task.Script);
                _computerRepository.Update(computer);
            }
        }
    }
}