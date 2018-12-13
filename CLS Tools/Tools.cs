using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CLS_Tools
{
    class Tools
    {
        DateTime startTime = DateTime.Now;

        public TimeSpan runLength
        {
            get
            {
                return DateTime.Now - startTime;
            }
        }

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

        #region Serialization
        static void SerializationSave<T>(T settings, string fileName)
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, settings);
            }
            catch
            {
                // do nothing, just ignore any possible errors
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }

        static T SerializationLoad<T>(string fileName)
        {
            Stream stream = null;
            T settings = default(T);
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                int version = (int)formatter.Deserialize(stream);
                settings = (T)formatter.Deserialize(stream);
            }
            catch
            {
                // do nothing, just ignore any possible errors
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
            return settings;
        }
        #endregion
    }
}
