using System;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Website.Controllers.Api.Task.Actions
{
    public class GetForComputer
    {
        private IComputerRepository _computerRepository;
        private ITaskRepository _taskRepository;

        public GetForComputer(IComputerRepository computerRepository, ITaskRepository taskRepository)
        {
            _computerRepository = computerRepository;
            _taskRepository = taskRepository;
        }

        public Persistence.Models.Task[] Run(Persistence.Models.Computer computer, TimeSpan? timeout, TimeSpan? pollInterval)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(90);
            pollInterval = pollInterval ?? TimeSpan.FromSeconds(1 / 4);

            Persistence.Models.Task[] tasks = null;

            var endTime = DateTime.Now.Add(timeout.Value);

            while (true)
            {
                computer.UpdatePing();
                _computerRepository.Update(computer);

                var now = DateTime.UtcNow;
                var cache = new InMemoryRepositoryModelCache();
                tasks = _taskRepository.ForComputer(computer, now, cache);

                if (tasks.Length > 0 || DateTime.Now >= endTime)
                {
                    foreach (var task in tasks)
                    {
                        task.MarkAsReceived();
                        _taskRepository.Update(task);
                    }

                    return tasks;
                }

                System.Threading.Thread.Sleep(pollInterval.Value);
            }
        }
    }
}