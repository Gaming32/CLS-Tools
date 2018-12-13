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
    /// <summary>
    /// Contains the basic tools for CLS Tools
    /// </summary>
    class Tools
    {
        DateTime startTime = DateTime.Now;

        /// <summary>
        /// The length that the program has been running
        /// </summary>
        public TimeSpan runLength
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
        public void Log(object message, string path = "", bool includeAppName = true)
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
        /// <summary>
        /// Saves the object to the specified file
        /// </summary>
        /// <typeparam name="T">The type of object you are saving (must have the Serializable attribute)</typeparam>
        /// <param name="settings">The object you are saving</param>
        /// <param name="fileName">The name of the file you are saving to</param>
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

        /// <summary>
        /// Recalls a serialized object from a file
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="fileName">The file to recall from</param>
        /// <returns>The object you deserialized</returns>
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
