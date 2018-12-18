using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace CLS_Tools
{
    /// <summary>
    /// Class for saving and loading objects from files (similar to Python's shelve)
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Saves the object to the specified file
        /// </summary>
        /// <typeparam name="T">The type of object you are saving (must have the Serializable attribute)</typeparam>
        /// <param name="settings">The object you are saving</param>
        /// <param name="fileName">The name of the file you are saving to</param>
        /// <param name="keyName">The name of the key you are saving to</param>
        public static void Save<T>(T settings, string fileName, string keyName)
        {
            Stream stream = null;
            ZipArchive file = null;
            try
            {
                file = ZipFile.Open(fileName, ZipArchiveMode.Update);
                IFormatter formatter = new BinaryFormatter();
                try { file.GetEntry(keyName).Delete(); }
                catch { }
                stream = file.CreateEntry(keyName).Open();
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
                file.Dispose();
            }
        }

        /// <summary>
        /// Recalls a serialized object from a file
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="fileName">The file to recall from</param>
        /// <param name="keyName">The key to recall from</param>
        /// <returns>The object you deserialized</returns>
        public static T Load<T>(string fileName, string keyName)
        {
            Stream stream = null;
            T settings = default(T);
            ZipArchive file = null;
            try
            {
                file = ZipFile.OpenRead(fileName);
                IFormatter formatter = new BinaryFormatter();
                stream = file.GetEntry(keyName).Open();
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
                file.Dispose();
            }
            return settings;
        }
    }
}
