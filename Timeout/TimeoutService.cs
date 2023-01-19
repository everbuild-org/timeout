using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace Timeout
{
    public partial class TimeoutService : ServiceBase
    {
        private Timer _timer = null;
        private bool _locked = false;
        private int _lockedTimeout = 10;
        private Settings _settings;

        public TimeoutService()
        {
            try
            {
                Library.InitRegistry();
            } catch(Exception e)
            {
                Library.WriteErrorLog(e);
            }

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _settings = Library.GetSettings();
            _lockedTimeout = _settings.LockedCycles;

            _timer = new Timer { Interval = _settings.TimerInterval };
            _timer.Elapsed += new ElapsedEventHandler(this.Tick);
            _timer.Enabled = true;
            Library.WriteErrorLog("Timeout started, Settings: " + _settings);
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            _settings = Library.GetSettings();

            var date = DateTime.Now;
            if (_settings.ShutdownNotBeforeHours > date.Hour || _settings.ShutdownNotBeforeMinutes > date.Minute) return;
            
            _locked = Process.GetProcessesByName("logonui").Length > 0;

            if (_locked)
            {
                _lockedTimeout--;
                Library.WriteErrorLog("Locking soon: Timeout: " + _lockedTimeout);


                if (_lockedTimeout <= 0)
                {
                    Library.WriteErrorLog("Shutdown");
                    if (_settings.DryMode == 0)
                      Library.ExecuteCommandSync("shutdown /s /t 0");
                }
            }
            else
            {
                _lockedTimeout = _settings.LockedCycles;
            }
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            Library.WriteErrorLog("Timeout stopped");
        }
    }
}
