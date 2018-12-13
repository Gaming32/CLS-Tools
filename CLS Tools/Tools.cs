using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLS_Tools
{
    class Tools
    {
        DateTime startTime = DateTime.Now;

        public TimeSpan runLength { get
            {
                return DateTime.Now - startTime;
            } }

        public void Log(string message, string path = "", bool includeAppName = false)
        {
            if (!File.Exists(path))
                path = Directory.GetCurrentDirectory();
            if (includeAppName)
                path += System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            path += startTime.ToString("MM-dd-yyyy-HH-mm-ss");
            path += ".log.csv";

            StreamWriter logger = new StreamWriter(path);
            if (logger.BaseStream.Length == 0)
                logger.WriteLine("TIMESTAMP,MESSAGE");
            logger.WriteLine(runLength.TotalSeconds.ToString() + "," + message);
        }
    }
}
