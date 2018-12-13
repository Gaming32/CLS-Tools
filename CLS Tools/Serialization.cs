using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
        public static void Save<T>(T settings, string fileName)
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
        public static T Load<T>(string fileName)
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
    }
}
