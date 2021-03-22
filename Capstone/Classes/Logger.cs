using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    class Logger
    {
        static string logFile = "Log.txt";
        static string dir = Environment.CurrentDirectory;
        static string fullPath = Path.Combine(dir, logFile);

        public static void Log(string action, decimal startBalance, decimal endBalance)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    string message = $"{DateTime.Now.ToString()} {action} {startBalance:C2} {endBalance:C2}";
                    sw.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Log file not accessible");
            }

        }
    }
}
