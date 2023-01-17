using System;
using System.IO;

namespace Timeout
{
    class Library
    {
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
}
