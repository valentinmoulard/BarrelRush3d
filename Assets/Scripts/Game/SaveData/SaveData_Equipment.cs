using System;
using System.Collections.Generic;
using Base.SaveSystem.SaveableScriptableObject.Scripts;
using Game.Clothes;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using Game.Weapons;
using UnityEditor;
#endif

namespace Game.SaveData
{
    [CreateAssetMenu(fileName = "SaveData_Equipment", menuName = "Save/SaveData_Equipment", order = 1)]
    [InlineEditor]
    public sealed class SaveData_Equipment : SerializedSaveData
    {
        [field: SerializeField]
        public Dictionary<uint, EquipmentState> Dictionary { get; private set; } = new Dictionary<uint, EquipmentState>();

        public EquipmentState GetData(uint uniqueID)
        {
            return Dictionary[uniqueID];
        }

        public void SetData(uint uniqueID, EquipmentState newValue)
        {
            Dictionary[uniqueID] = newValue;
        }

        public override void Delete()
        {
            base.Delete();
            
#if UNITY_EDITOR
            SetDictionary();
            AssetDatabase.SaveAssets();
#endif
            Save();
        }

#if UNITY_EDITOR

        [Button]
        private void SetDictionary()
        {
            Dictionary.Clear();
            if (name is "SaveData_WeaponData")
            {
                var guidOfAllWeaponDataSos = AssetDatabase.FindAssets("t:WeaponDataSo", new[] {"Assets"});
                var allWeaponDataSos = new List<WeaponDataSo>();
                foreach (var guid in guidOfAllWeaponDataSos)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var weaponDataSo = AssetDatabase.LoadAssetAtPath<WeaponDataSo>(path);
                    allWeaponDataSos.Add(weaponDataSo);
                }
                foreach (var weaponDataSo in allWeaponDataSos)
                {
                    Dictionary.Add(weaponDataSo.UniqueID, new EquipmentState());
                    if(weaponDataSo.name is "WeaponDataSo 0")
                    {
                        Dictionary[weaponDataSo.UniqueID].IsEquipped = true;
                        Dictionary[weaponDataSo.UniqueID].IsUnlocked = true;
                    }
                }
            }
            else if (name is "SaveData_ClothesData")
            {
                var guidOfAllClothesDataSos = AssetDatabase.FindAssets("t:ClothesDataSo", new[] {"Assets"});
                var allClothesDataSos = new List<ClothesDataSo>();
                foreach (var guid in guidOfAllClothesDataSos)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var clothesDataSo = AssetDatabase.LoadAssetAtPath<ClothesDataSo>(path);
                    allClothesDataSos.Add(clothesDataSo);
                }
                foreach (var clothesDataSo in allClothesDataSos)
                {
                    Dictionary.Add(clothesDataSo.UniqueID, new EquipmentState());
                }
            }
  
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

#endif
    }
}

[Serializable]
public class EquipmentState
{
    [field: SerializeField]
    public int Level { get; set; }

    [field: SerializeField]
    public bool IsUnlocked { get; set; }

    [field: SerializeField]
    public bool IsEquipped { get; set; }
}
