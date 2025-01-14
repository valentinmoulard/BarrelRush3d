using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.SerializedResourcedScriptableObject
{
    public class SerializedResourcedScriptableObject<T> : SerializedScriptableObject where T : SerializedResourcedScriptableObject<T>
    {
        public const string Path = "ResourcedScriptableObjects/";
        public static T Instance()
        {
            if (_instance != null) return _instance;
#if UNITY_EDITOR
            string fullPath = $"{Path}{typeof(T).Name}";
            _instance = Resources.Load<T>(fullPath);
            if (_instance == null)
            {
                _instance = CreateInstance<T>();
                Directory.CreateDirectory($"{Application.dataPath}/Resources/{Path}");
                UnityEditor.AssetDatabase.CreateAsset(_instance, $"Assets/Resources/{fullPath}.asset");
                UnityEditor.AssetDatabase.SaveAssets();
            }
#endif

            _instance = Resources.Load<T>($"{Path}{typeof(T).Name}");
            return _instance;
        }

        private static T _instance;
    }
}
