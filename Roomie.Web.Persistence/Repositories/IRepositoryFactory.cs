namespace Roomie.Web.Persistence.Repositories
{
    public interface IRepositoryFactory
    {
        IComputerRepository GetComputerRepository();
        IDeviceRepository GetDeviceRepository();
        INetworkGuestRepository GetNetworkGuestRepository();
        INetworkRepository GetNetworkRepository();
        IScriptRepository GetScriptRepository();
        ITaskRepository GetTaskRepository();
        IUserRepository GetUserRepository();
        ISessionRepository GetSessionRepository();
    }
}
