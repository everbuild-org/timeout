using System.ServiceProcess;

namespace Timeout
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TimeoutService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
