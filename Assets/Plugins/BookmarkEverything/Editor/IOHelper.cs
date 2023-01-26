#region

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

#endregion

namespace BookmarkEverything
{
    public static class IOHelper
    {
    #region Public Methods

        public static void ClearData(string fileName)
        {
            var path = Application.persistentDataPath + "/" + fileName + ".dat";
            if (File.Exists(path))
                using (var fileStream = File.Open(path , FileMode.Open))
                {
                    fileStream.SetLength(0L);
                }
        }

        public static bool Exists(string value , ExistentialCheckStrategy strategy = ExistentialCheckStrategy.Path)
        {
            if (strategy == ExistentialCheckStrategy.GUID) value = AssetDatabase.GUIDToAssetPath(value);
            var existsDir                                        = Directory.Exists(value);
            var existsFile                                       = File.Exists(value);
            return existsDir || existsFile;
        }

        public static bool IsFolder(string value , ExistentialCheckStrategy strategy = ExistentialCheckStrategy.Path)
        {
            if (strategy == ExistentialCheckStrategy.GUID) value = AssetDatabase.GUIDToAssetPath(value);
            return Directory.Exists(value);
        }

        public static T ReadFromDisk<T>(string fileName)
        {
            var path         = Application.persistentDataPath + "/" + fileName + ".dat";
            var returnObject = default(T);
            if (File.Exists(path))
            {
            #if UNITY_5_4_OR_NEWER
                using (var streamReader = new StreamReader(path))
                {
                    string line;
                    while (!string.IsNullOrEmpty(line = streamReader.ReadLine())) returnObject = Deserialize<T>(line);
                }
            #else
				FileStream fs = new FileStream(path, FileMode.Open);
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
 new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				fs.Seek(0, SeekOrigin.Begin);
				returnObject = (T)bf.Deserialize(fs);
				fs.Close();
            #endif
            }

            return returnObject;
        }

        public static void WriteToDisk(string fileName , object serializeObject)
        {
            var path = Application.persistentDataPath + "/" + fileName + ".dat";
        #if UNITY_5_4_OR_NEWER
            var str = JsonUtility.ToJson(serializeObject);
            File.AppendAllText(path , str + Environment.NewLine);
        #else
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
 new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
			bf.Serialize(fs, serializeObject);
			fs.Close();
        #endif
        }

    #endregion

    #region Private Methods

    #if UNITY_5_4_OR_NEWER
        private static T Deserialize<T>(string text)
        {
            text = text.Trim();
            var    typeFromHandle = typeof(T);
            object obj            = null;
            try
            {
                obj = JsonUtility.FromJson<T>(text);
            }
            catch (Exception ex)
            {
                Debug.LogError("Cannot deserialize to type " + typeFromHandle + ": " + ex.Message + ", Json string: " + text);
            }

            if (obj != null && obj.GetType() == typeFromHandle) return (T)obj;
            return default(T);
        }
    #endif

    #endregion
    }

    public enum ExistentialCheckStrategy // :)
    {
        Path ,
        GUID
    }
}