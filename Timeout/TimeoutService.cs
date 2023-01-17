using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace Timeout
{
    public partial class TimeoutService : ServiceBase
    {
        private const int _timeout = 1000; // 1s
        private Timer timer = null;
        private bool locked = false;
        private int _lockedTimeout = 10;

        public TimeoutService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer
            {
                Interval = _timeout
            };
            timer.Elapsed += new ElapsedEventHandler(this.Tick);
            timer.Enabled = true;
            Library.WriteErrorLog("Timeout started");
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            locked = Process.GetProcessesByName("logonui").Length > 0;

            if (locked)
            {
                _lockedTimeout--;
                Library.WriteErrorLog("Locking soon: Timeout: " + _lockedTimeout);
                

                if (_lockedTimeout <= 0)
                {
                    Library.WriteErrorLog("Shutdown");
                    //Library.ExecuteCommandSync("shutdown /s /t 0");
                }
            } else
            {
                _lockedTimeout = 10;
            }
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Library.WriteErrorLog("Timeout stopped");
        }
    }
}
