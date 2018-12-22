using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System;
using System.Collections.Generic;

namespace CLSTools
{
    /// <summary>
    /// Class for saving and loading objects from files (similar to Python's shelve)
    /// </summary>
    public class Serialization
    {
        public string LogPath { get; set; } = "";
        public bool LogIncludeAppName { get; set; } = true;

        #region Save
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
            catch(Exception ex)
            {
                BasicTools.Log($"Serialization Save Error: [Source={ex.Source};Message={ex.Message}]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                    file.Dispose();
            }
        }

        public static void Save<T>(T settings, Stream file, string keyName)
        {
            Stream stream = null;
            ZipArchive fileToSave = null;
            try
            {
                fileToSave = new ZipArchive(file);
                IFormatter formatter = new BinaryFormatter();
                try { fileToSave.GetEntry(keyName).Delete(); }
                catch { }
                stream = fileToSave.CreateEntry(keyName).Open();
                formatter.Serialize(stream, settings);
            }
            catch(Exception ex)
            {
                BasicTools.Log($"Serialization Save Error: [Source=\"{ex.Source}\";Message=\"{ex.Message}\"]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                    file.Dispose();
            }
        }
        #endregion

        #region Load
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
            catch(Exception ex)
            {
                BasicTools.Log($"Serialization Load Error: [Source={ex.Source};Message={ex.Message}]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                file.Dispose();
            }
            return settings;
        }

        public static T Load<T>(Stream file, string keyName)
        {
            Stream stream = null;
            T settings = default(T);
            ZipArchive fileToLoad = null;
            try
            {
                fileToLoad = new ZipArchive(file);
                IFormatter formatter = new BinaryFormatter();
                stream = fileToLoad.GetEntry(keyName).Open();
                settings = (T)formatter.Deserialize(stream);
            }
            catch(Exception ex)
            {
                BasicTools.Log($"Serialization Load Error: [Source={ex.Source};Message={ex.Message}]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                    file.Dispose();
            }
            return settings;
        }

        #region LoadAll
        public static Dictionary<string, T> Load<T>(string fileName)
        {
            Stream stream = null;
            Dictionary<string, T> settingsArr = new Dictionary<string, T>();
            ZipArchive file = null;
            try
            {
                file = ZipFile.OpenRead(fileName);
                IFormatter formatter = new BinaryFormatter();
                foreach(ZipArchiveEntry key in file.Entries)
                {
                    string keyName = key.FullName;
                    stream = key.Open();
                    object settings = formatter.Deserialize(stream);
                    if (settings.GetType() == typeof(T))
                        settingsArr.Add(keyName, (T)settings);
                }
            }
            catch (Exception ex)
            {
                BasicTools.Log($"Serialization Load Error: [Source={ex.Source};Message={ex.Message}]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                    file.Dispose();
            }
            return settingsArr;
        }

        public static Dictionary<string, T> Load<T>(Stream file)
        {
            Stream stream = null;
            Dictionary<string, T> settingsArr = new Dictionary<string, T>();
            ZipArchive fileToLoad = null;
            try
            {
                fileToLoad = new ZipArchive(file);
                IFormatter formatter = new BinaryFormatter();
                foreach (ZipArchiveEntry key in fileToLoad.Entries)
                {
                    string keyName = key.FullName;
                    stream = key.Open();
                    object settings = formatter.Deserialize(stream);
                    if (settings.GetType() == typeof(T))
                        settingsArr.Add(keyName, (T)settings);
                }
            }
            catch (Exception ex)
            {
                BasicTools.Log($"Serialization Load Error: [Source={ex.Source};Message={ex.Message}]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                    file.Dispose();
            }
            return settingsArr;
        }
        #region All types
        public static Dictionary<string, object> Load(string fileName)
        {
            Stream stream = null;
            Dictionary<string, object> settingsArr = new Dictionary<string, object>();
            ZipArchive file = null;
            try
            {
                file = ZipFile.OpenRead(fileName);
                IFormatter formatter = new BinaryFormatter();
                foreach (ZipArchiveEntry key in file.Entries)
                {
                    string keyName = key.FullName;
                    object settings = formatter.Deserialize(stream);
                    settingsArr.Add(keyName, settings);
                }
            }
            catch (Exception ex)
            {
                BasicTools.Log($"Serialization Load Error: [Source={ex.Source};Message={ex.Message}]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                    file.Dispose();
            }
            return settingsArr;
        }

        public static Dictionary<string, object> Load(Stream file)
        {
            Stream stream = null;
            Dictionary<string, object> settingsArr = new Dictionary<string, object>();
            ZipArchive fileToLoad = null;
            try
            {
                fileToLoad = new ZipArchive(file);
                IFormatter formatter = new BinaryFormatter();
                foreach (ZipArchiveEntry key in fileToLoad.Entries)
                {
                    string keyName = key.FullName;
                    stream = key.Open();
                    object settings = formatter.Deserialize(stream);
                    settingsArr.Add(keyName, settings);
                }
            }
            catch (Exception ex)
            {
                BasicTools.Log($"Serialization Load Error: [Source={ex.Source};Message={ex.Message}]");
            }
            finally
            {
                if (null != stream)
                    stream.Close();
                if (null != file)
                    file.Dispose();
            }
            return settingsArr;
        }
        #endregion
        #endregion
        #endregion
    }
}
