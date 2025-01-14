using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.SaveSystem.SaveableScriptableObject.Scripts
{
    [CreateAssetMenu(fileName = "NewSaveData", menuName = "Save/New Save Data", order = 0)]
    [Serializable]
    public class SerializedSaveData : SerializedScriptableObject
    {
        [field: SerializeField]
        public SaveDataType SaveDataType { get; private set; }
        
        [Button]
        public virtual void Save()
        {
            SaveDataHelper.Save(this);
        }

        [Button]
        public virtual void Load()
        {
            SaveDataHelper.Load(this);
        }

        [Button]
        public virtual void Delete()
        {
            SaveDataHelper.Delete(this);
        }
    }
}