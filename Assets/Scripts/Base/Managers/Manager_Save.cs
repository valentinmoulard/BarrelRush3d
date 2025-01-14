using System;
using System.Linq;
using Base.Helpers;
using Base.SaveSystem.SaveableScriptableObject;
using Base.SaveSystem.SaveableScriptableObject.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base.Managers
{
    public class Manager_Save : ManagerBase
    {
        [SerializeField]
        private UnitySerializedDictionary<SaveDataType, SerializedSaveData> saveDataDictionary;

        public T GetSaveData<T>(SaveDataType saveDataType) where T : SerializedSaveData
        {
            saveDataDictionary.TryGetValue(saveDataType, out var saveData);
            return (T) saveData;
        }

        public override void SetUp()
        {
            base.SetUp();
            Load();
        }

        [Button]
        public void Save()
        {
            SaveOrLoad(true);
        }

        [Button]
        public void Load()
        {
            SaveOrLoad(false);
        }

        private void SaveOrLoad(bool isSave)
        {
            foreach (var data in saveDataDictionary.Values.Where(data => data != null))
            {
                try
                {
                    if (isSave)
                    {
                        data.Save();
                    }
                    else
                    {
                        data.Load();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error {(isSave ? "saving" : "loading")} data: {ex.ToString()}");
                }
            }
        }

        public void DeleteAll()
        {
            foreach (var data in saveDataDictionary.Values.Where(data => data != null))
            {
                data.Delete();
            }
        }

#if UNITY_EDITOR
        [Button]
        private void FindAllAndSetSaveDataToList_Editor()
        {
            saveDataDictionary = new UnitySerializedDictionary<SaveDataType, SerializedSaveData>();
            var saveDatas = Resources.LoadAll<SerializedSaveData>("");
            for (int i = 0; i < saveDatas.Length; i++)
            {
                var saveData = saveDatas[i];
                saveDataDictionary.Add(saveData.SaveDataType, saveData);
            }
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(gameObject);
        }
#endif

    }
}