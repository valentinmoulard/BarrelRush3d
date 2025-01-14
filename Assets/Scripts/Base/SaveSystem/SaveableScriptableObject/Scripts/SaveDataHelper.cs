using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Base.SaveSystem.SaveableScriptableObject.Scripts
{
    public static class SaveDataHelper
    {
        private const string FirstPass = "pass";
        private const string SecondPass = "word";
        private static readonly StringBuilder SaveName = new ();
 
        public static void Save(this SerializedSaveData data)
        {
            SaveEncryptedJson(data);
        }
        
        public static void Load(this SerializedSaveData data)
        {
            LoadEncryptedJson(data);
        }

        public static void Delete(this SerializedSaveData data)
        {
            DeleteEncryptedJson(data);
        }

        public static void DeleteAllSerializedSaveDataWithFind()
        {
            SerializedSaveData[] saveDatas = Resources.LoadAll<SerializedSaveData>("");
            for (int i = 0; i < saveDatas.Length; i++)
            {
                var saveData = saveDatas[i];
                saveData.Delete();
            }
        }

        private static void SaveEncryptedJson(SerializedSaveData data)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(GetFullPath(data)));
                
                string dataStore = JsonUtility.ToJson(data, true);
                dataStore = EncryptDecrypt(dataStore);
                using (FileStream stream = new FileStream(GetFullPath(data), FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataStore);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
 

        private static void LoadEncryptedJson(SerializedSaveData data)
        {
            var fullPath = GetFullPath(data);
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    
                    JsonUtility.FromJsonOverwrite(EncryptDecrypt(dataToLoad), data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        private static string EncryptDecrypt(string data)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                char character = (char)(data[i] ^ FirstPass[i % FirstPass.Length] ^ SecondPass[i % SecondPass.Length]);
                result.Append(character);
            }
            return result.ToString();
        }
        
        private static void DeleteEncryptedJson(SerializedSaveData data)
        {
            var fullPath = GetFullPath(data);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        
        public static string GetDirectoryPath(SerializedSaveData data)
        {
            return Path.GetDirectoryName(GetFullPath(data));
        }
        
        public static string GetFullPath(SerializedSaveData data)
        {
            return Path.Combine(Application.persistentDataPath, GetFullName(data));
        }

        private static string GetFullName(SerializedSaveData data)
        {
            SaveName.Clear();
            SaveName.Append(data.name);
            SaveName.Append(GetExtension());
            return SaveName.ToString();
        }
        private static string GetExtension()=> ".encJson";
    }
}
