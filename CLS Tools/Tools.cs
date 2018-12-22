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
                logger.WriteLine("Timestamp,App Run Time,Message");
            logger.WriteLine(DateTime.Now.ToString("ddd dd MMMM yyyy HH:mm:ss") + ", " + runLength.TotalSeconds.ToString() + " sec" + "," + message);
            logger.Close();
        }

        #region FixNull
        public static int FixNull(int? input)
        {
            if (input != null)
                return (int)input;
            return 0;
        }

        public static string FixNull(string input)
        {
            if (input != null)
                return input;
            return "";
        }
        #endregion

        /// <summary>
        /// Splits a string into a string array where each substring is the specified length.
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
