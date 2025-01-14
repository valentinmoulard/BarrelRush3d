using System;
using System.Collections.Generic;
using Base.SaveSystem;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Game.SaveableSos
{
    public abstract class SaveableSo: ScriptableObject
    {
        [field: SerializeField, ReadOnly, BoxGroup("Base Info"), GUIColor(0.8f, 0.8f, 0.2f)]
        public uint UniqueID { get; private set; }
        
        [field: SerializeField, PreviewField, BoxGroup("Base Info"), GUIColor(0.8f, 0.8f, 0.2f)]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField, BoxGroup("Base Info"), GUIColor(0.8f, 0.8f, 0.2f)]
        public string Title { get; private set; }

        [field: SerializeField, BoxGroup("Upgrade Costs"), ListDrawerSettings(ShowIndexLabels = true), ReadOnly]
        public List<float> UpgradeCosts { get; private set; } = new ();
        
        public float GetCost(int level)
        {
            var clampedLevel = Mathf.Clamp(level, 0, UpgradeCosts.Count - 1);
            return UpgradeCosts[clampedLevel];
        }
        
        protected virtual SaveData_UintDictionary GetSaveData()
        {
            throw new NotImplementedException();
        }
        
        public virtual int UpgradeIndex
        {
            get => GetSaveData().GetData(UniqueID); 
            set => GetSaveData().SetData(UniqueID, value);
        }
        
        public virtual bool IsOnMaxLevel => UpgradeIndex >= UpgradeCosts.Count - 1;
        
        #region UNITY_EDITOR

#if UNITY_EDITOR
        
        [Button, BoxGroup("Upgrade Costs")]
        private void SetCost(int howManyValues, float minCost, float maxCost)
        {
            UpgradeCosts = new List<float>();
            for (int i = 0; i < howManyValues; i++)
            {
                int value = (int) Mathf.Lerp(minCost, maxCost, (float)i / (howManyValues - 1));
                UpgradeCosts.Add(value);
            }
        }
        
        [Button]
        public void SetInstanceId()
        {
            int hashCode = name.GetHashCode();
            uint positiveHashCode = unchecked((uint)hashCode);
            UniqueID = positiveHashCode;
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
        
        public virtual void DeleteSaves()
        {
            GetSaveData().Delete();
        }
#endif

        #endregion
    }
}