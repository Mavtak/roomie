using System.ServiceProcess;

namespace Roomie.Desktop.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new RoomieService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
