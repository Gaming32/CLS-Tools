using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLS_Tools
{
    /// <summary>
    /// Contains the basic tools for CLS Tools
    /// </summary>
    public class BasicTools
    {
        static DateTime startTime = DateTime.Now;

        /// <summary>
        /// The length that the program has been running
        /// </summary>
        public static TimeSpan runLength
        {
            get
            {
                return DateTime.Now - startTime;
            }
        }

        /// <summary>
        /// Logs messages to a file
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="path">The path to store the log (uses the working directory be default)</param>
        /// <param name="includeAppName">Whether to include the name of the executable in the name of the log file.</param>
        public static void Log(object message, string path = "", bool includeAppName = true)
        {
            if (!Directory.Exists(path))
                path = Directory.GetCurrentDirectory();
            if (!path.EndsWith(@"\"))
                path += @"\";
            if (includeAppName)
                path += System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_";
            path += startTime.ToString("MM-dd-yyyy-HH-mm-ss");
            path += ".log.csv";

            StreamWriter logger = new StreamWriter(path);
            if (logger.BaseStream.Length == 0)
                logger.WriteLine("Timestamp,Total App Run Length,Message");
            logger.WriteLine(DateTime.Now + "," + runLength.TotalSeconds.ToString() + "," + message);
        }
    }
}
