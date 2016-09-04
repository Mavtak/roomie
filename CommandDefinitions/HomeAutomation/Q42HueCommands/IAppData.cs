namespace Q42HueCommands
{
    public interface IAppData
    {
        string AppName { get; }
        string DeviceName { get; }
        string AppKey { get; set; }
    }
}
