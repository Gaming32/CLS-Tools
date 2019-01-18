using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLSTools
{
    /// <summary>
    /// Contains the basic tools for CLS Tools
    /// </summary>
    public class BasicTools
    {
        /// <summary>
        /// The time that the program started running
        /// </summary>
        public static DateTime StartTime { get; } = DateTime.Now;

        /// <summary>
        /// The time that the program has been running
        /// </summary>
        public static TimeSpan RunLength => DateTime.Now - StartTime;
        
        /// <summary>
        /// Logs messages to a file
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="path">The path to store the log (uses the working directory be default)</param>
        /// <param name="includeAppName">Whether to include the name of the executable in the name of the log file</param>
        /// <returns>The time of the logging in a tuple (time of logging, time that the application has run)</returns>
        public static Tuple<DateTime, TimeSpan> LogCsv(object message, string path = "", bool includeAppName = true)
        {
            if (!Directory.Exists(path))
                path = Directory.GetCurrentDirectory();
            if (!path.EndsWith(@"\"))
                path += @"\";
            if (includeAppName)
                path += AppDomain.CurrentDomain.FriendlyName
                    .TrimEnd(".exe".ToCharArray())
                    .Replace(' ', '-')
                    + "_";
            path += StartTime.ToString("MM-dd-yyyy-HH-mm-ss");
            path += ".log.csv";

            StreamWriter logger = new StreamWriter(path);
            if (logger.BaseStream.Length == 0)
                logger.WriteLine("Timestamp,App Run Time,Message");
            DateTime time = DateTime.Now;
            TimeSpan length = RunLength;
            logger.WriteLine(time.ToString("ddd dd MMMM yyyy HH:mm:ss") + ", " + length.TotalSeconds.ToString() + " sec" + "," + message);
            logger.Flush();

            return new Tuple<DateTime, TimeSpan>(time, length);
        }

        /// <summary>
        /// Logs the specified exception to a file
        /// </summary>
        /// <param name="ex">The exception to log</param>
        /// <param name="path">The place to directory to log to</param>
        /// <param name="includeAppName">Whether to include the name of the program in the log file name</param>
        /// <returns>The time that the exception was logged</returns>
        public static DateTime LogErr(Exception ex, string path = "", bool includeAppName = true)
        {
            if (!Directory.Exists(path))
                path = Directory.GetCurrentDirectory();
            if (!path.EndsWith(@"\"))
                path += @"\";
            if (includeAppName)
                path += AppDomain.CurrentDomain.FriendlyName
                    .TrimEnd(".exe".ToCharArray())
                    .Replace(' ', '-')
                    + "_";
            DateTime time = DateTime.Now;
            path += time.ToString("MM-dd-yyyy-HH-mm-ss");
            path += ".err.log";

            StreamWriter logger = new StreamWriter(path);
            logger.Write(ex);
            logger.Close();

            return time;
        }
        
        /// <summary>
        /// Takes in a null or non-null variable and returns a non-null variable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The null or non-null variable</param>
        /// <returns>The non-null variable</returns>
        public static T FixNull<T>(T input)
        {
            if (input == null)
                return default(T);
            return input;
        }

        /// <summary>
        /// Splits a string into a string array where each substring is the specified length
        /// </summary>
        /// <param name="str">The string to split</param>
        /// <param name="substrLength">The length of each substring</param>
        /// <returns>The split string</returns>
        public static string[] StrSplit(string str, int substrLength)
        {
            var words = new List<string>();

            for (int i = 0; i < str.Length; i += substrLength)
                if (str.Length - i >= substrLength) words.Add(str.Substring(i, substrLength));
                else words.Add(str.Substring(i, str.Length - i));

            return words.ToArray();
        }
    }
}
