using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace Timeout
{
    class Library
    {
        public static RegistryKey registryKey;

        public static void InitRegistry()
        {
            RegistryKey software = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);

            if (!Registry.LocalMachine.GetSubKeyNames().Contains("SOFTWARE"))
            {
                software = Registry.LocalMachine.CreateSubKey("SOFTWARE", true);
            }

            RegistryKey stellarverse = software.OpenSubKey("Stellarverse", true);

            if (!software.GetSubKeyNames().Contains("Stellarverse"))
            {
                stellarverse = software.CreateSubKey("Stellarverse", true);
            }

            registryKey = stellarverse.OpenSubKey("Timeout", true);

            if (!stellarverse.GetSubKeyNames().Contains("Timeout"))
            {
                registryKey = stellarverse.CreateSubKey("Timeout", true);
            }


            if (registryKey.GetValue("TimerInterval") == null) registryKey.SetValue("TimerInterval", "300");
            if (registryKey.GetValue("LockedCycles") == null) registryKey.SetValue("LockedCycles", "3");
            if (registryKey.GetValue("ShutdownNotBeforeHours") == null) registryKey.SetValue("ShutdownNotBeforeHours", "0");
            if (registryKey.GetValue("ShutdownNotBeforeMinutes") == null) registryKey.SetValue("ShutdownNotBeforeMinutes", "0");
            if (registryKey.GetValue("DryMode") == null) registryKey.SetValue("DryMode", "0");

            GetSettings();
        }

        public static Settings GetSettings()
        {
            var settings = new Settings
            {
                TimerInterval = int.Parse((string)registryKey.GetValue("TimerInterval")),
                LockedCycles = int.Parse((string)registryKey.GetValue("LockedCycles")),
                ShutdownNotBeforeHours = int.Parse((string)registryKey.GetValue("ShutdownNotBeforeHours")),
                ShutdownNotBeforeMinutes = int.Parse((string)registryKey.GetValue("ShutdownNotBeforeMinutes")),
                DryMode = int.Parse((string)registryKey.GetValue("DryMode"))
            };

            return settings;
        }

        public static void WriteErrorLog(Exception ex)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Environment.GetEnvironmentVariable("temp") + "\\TimeoutErrorLog.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteErrorLog(string Message)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Environment.GetEnvironmentVariable("temp") + "\\TimeoutErrorLog.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void ExecuteCommandSync(object command)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command)
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                System.Diagnostics.Process proc = new System.Diagnostics.Process
                {
                    StartInfo = procStartInfo
                };

                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                WriteErrorLog(objException);
            }
        }
    }

    public class Settings
    {
        public int TimerInterval;
        public int LockedCycles;
        public int ShutdownNotBeforeHours;
        public int ShutdownNotBeforeMinutes;
        public int DryMode;

        public override string ToString()
        {
            return "Settings(TimerInterval="+TimerInterval+", LockedCycles="+LockedCycles+ ", ShutdownNotBefore="+ShutdownNotBeforeHours+":"+ShutdownNotBeforeMinutes+", DryMode=" + DryMode + ")";
        }
    }
}
