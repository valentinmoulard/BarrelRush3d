using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base.AssetsManagement
{
    public static class AssetsHelper
    {
        #region UNITY_EDITOR

#if UNITY_EDITOR
        public static List<Object> GetAssets(Type type, string parentPath = "Assets")
        {
            var objectGuids = AssetDatabase.FindAssets($"t:{type.Name}", new []{parentPath} );
            var objects = new List<Object>();
            foreach (var objectGuid in objectGuids)
            {
                var pathToAsset = AssetDatabase.GUIDToAssetPath(objectGuid);
                var scriptableObject = AssetDatabase.LoadAssetAtPath<Object>(pathToAsset);
                objects.Add(scriptableObject);
            }
            return objects;
        }
        
        public static List<T> GetAssets<T>(string parentPath = "Assets") where T : Object
        {
            var type = typeof(T);
            var objectGuids = AssetDatabase.FindAssets($"t:{type.Name}", new []{parentPath} );
            var objects = new List<T>();
            foreach (var objectGuid in objectGuids)
            {
                var pathToAsset = AssetDatabase.GUIDToAssetPath(objectGuid);
                var scriptableObject = AssetDatabase.LoadAssetAtPath<T>(pathToAsset);
                objects.Add(scriptableObject);
            }
            return objects;
        }
        
        public static List<T> GetInterfacesWithScriptableObjects<T>(string parentPath = "Assets") where T : class
        {
            var objectGuids = AssetDatabase.FindAssets($"t:ScriptableObject", new []{parentPath} );
            var objects = new List<T>();
            foreach (var objectGuid in objectGuids)
            {
                var pathToAsset = AssetDatabase.GUIDToAssetPath(objectGuid);
                var scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(pathToAsset);
                if (scriptableObject is T t)
                {
                    objects.Add(t);
                }
            }
            return objects;
        }

        public static void CreateAndSaveAsset(AssetSaveData assetSaveData)
        {
            AssetDatabase.CreateAsset(assetSaveData.ObjToSave, 
                $"{assetSaveData.Path}/{assetSaveData.ObjectName}.{assetSaveData.FileExtension}");
            AssetDatabase.SaveAssets();
        }
        
        public static void RefreshAssets()
        {
            AssetDatabase.Refresh();
        }
#endif

        #endregion


    }
}