using System.Collections.Generic;
using Base.SaveSystem.SaveableScriptableObject.Scripts;
using Game._Inventory;
using Game.SaveableSos;
using Game.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SaveData
{
    [CreateAssetMenu(fileName = "SaveData_Inventory", menuName = "Save/SaveData_Inventory", order = 1)]
    [InlineEditor]
    public class SaveData_Inventory : SerializedSaveData
    {
        [field: SerializeField]
        public Dictionary<InventoryType, SaveableSo_Equipment> Dictionary { get; private set; } = new ();

        public void SetData(InventoryType inventoryType, SaveableSo_Equipment newValue)
        {
            Dictionary[inventoryType] = newValue;
        }

        public override void Delete()
        {
            base.Delete();
            Dictionary.Clear();
#if UNITY_EDITOR
            AddDefaultWeapon();
            AssetDatabase.SaveAssets();
#endif
            Save();
        }
#if UNITY_EDITOR
        private void AddDefaultWeapon()
        {
            var weaponDataSoGuid = AssetDatabase.FindAssets("t:WeaponDataSo");
            var weaponDataSo = AssetDatabase.LoadAssetAtPath<WeaponDataSo>(AssetDatabase.GUIDToAssetPath(weaponDataSoGuid[0]));
            Dictionary.Add(InventoryType.Weapon, weaponDataSo);
        }
#endif
        
    }

}