using Base.GameManagement;
using Base.SaveSystem.SaveableScriptableObject;
using Game.FightSystem;
using Game.SaveableSos;
using Game.SaveData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Weapons
{
    [CreateAssetMenu(fileName = "WeaponDataSo", menuName = "BarrelRush/Weapons/WeaponDataSo", order = 0)]
    public sealed class WeaponDataSo: SaveableSo_Equipment
    {
        [field: SerializeField]
        public int EquipPrefabIndex { get; private set; }
        
        [field: SerializeField, GUIColor(0.2f, 0.8f, 0.8f)]
        public FightData FightData { get; private set; }

        [field: SerializeField]
        public int BulletType { get; private set; }

        protected override SaveData_Equipment GetEquipmentSaveData()
        {
            return ManagersAccess.SaveManager.GetSaveData<SaveData_Equipment>(SaveDataType.WeaponsData);
        }
    }
}