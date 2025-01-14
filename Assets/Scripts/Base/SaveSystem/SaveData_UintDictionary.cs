using System.Collections.Generic;
using Base.SaveSystem.SaveableScriptableObject.Scripts;
using Game.SaveableSos;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base.SaveSystem
{
    [CreateAssetMenu(fileName = "SaveData_UintDictionary", menuName = "Save/SaveData_UintDictionary", order = 0)]
    [InlineEditor]
    public sealed class SaveData_UintDictionary: SerializedSaveData
    {
        [field: SerializeField]
        public Dictionary<uint, int> Dictionary { get; private set; } = new Dictionary<uint, int>();
        
        public int GetData(uint uniqueID)
        {
            return Dictionary[uniqueID];
        }
        
        public void SetData(uint uniqueID, int newValue)
        {
            Dictionary[uniqueID] = newValue;
        }

        public override void Delete()
        {
            base.Delete();
            var keys = new List<uint>(Dictionary.Keys);
            foreach (var key in keys)
            {
                Dictionary[key] = 0;
            }
            Save();
        }


#if UNITY_EDITOR
        
        [Button]
        private void SetDictionary(List<SaveableSo> saveableSo)
        {
            Dictionary.Clear();
            foreach (var so in saveableSo)
            {
                Dictionary.Add(so.UniqueID, 0);
            }
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
        
#endif
  
    }
}